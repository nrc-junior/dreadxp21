using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum musics {
eletricista,
eletricista_cthullu,   
capitão,               
capitão_cthullu,       
engenheiro,
engenheiro_cthullu,
oficial,
oficial_cthullu,
contramestre,          
contramestre_cthullu,  
pesquisadora,
pesquisadora_cthullu,
cozinheiro,            
cozinheiro_cthullu,    
auxiliar, 
auxiliar_cthullu,
cthullu,               
oceanico,
combate_intro,         
combate_loop,          
combate_climax_intro,  
combate_climax_loop,   
combate_fim
}

public class Theme {
    public float natural_intensity = 1;
    public GameObject actor;
    public Vector3 position  => actor.transform.position;

    public float distanceTo(Vector3 target) => Vector3.Distance(position, target);

    private AudioSource src;
    private AudioClip other;
    private float intensity;
    private float multiplier;

    public float factor {
        get => multiplier;
        set => multiplier = value;
    }

    public Theme(GameObject actor, AudioSource src, AudioClip clip, float factor, AudioClip other = default) {
        this.actor = actor;
        this.src = src;
        this.src.clip = clip;
        this.other = other;
        this.src.loop = true;
        multiplier = factor;
        
        this.src.volume = natural_intensity;
        this.src.Play();
        this.src.Pause();
    }

    public void Pause() => src.Pause();
    public void Play() => src.UnPause();

    public float time {
        get => src.time;
        set => src.time = value;
    }

    public void SetIntensity(float intensity = -1) {
        if (intensity == -1) intensity = natural_intensity;
        this.intensity = intensity; 
        src.volume = intensity;
    }

}


public class MusicManager : MonoBehaviour {
    
    [System.Serializable]
    public class Music {
        public Person theme = Person.undefined;
        public AudioClip clip;
        public AudioClip alternative;
        public musics tag;
        [Range(0.05f, 1)] public float factor = 1; 
    }

    public List<Music> musics; 
    
    public GameObject[] actors;
    private float time;
    private Theme amelia;
    
    private Dictionary<Person, Theme> themes;
    List<Theme> hearing;
    
    private void Start() {
        themes = new Dictionary<Person, Theme>();

        foreach (var music in musics) {
            if (music.theme == Person.undefined) continue;
            foreach (var actor in actors) {
                var actorNome = actor.GetComponent<Actor>().actor.nome;
                if (actorNome != music.theme) continue;
                themes[actorNome] = new Theme(actor, new GameObject(actor.name + " soundsrc").AddComponent<AudioSource>(), music.clip, music.factor);
                break;
            }
        }

        amelia = themes[Person.Amelia];
        amelia.Play();
        hearing = new List<Theme>();
    }
   
    private void Update() {

        float dst = 10;
        foreach (var person in themes.Keys) {
            var actor = themes[person];
            if ((actor.distanceTo(amelia.position)) < 15) {


                if (!hearing.Contains(actor)) {
                    actor.time = amelia.time;
                    actor.Play();
                    hearing.Add(actor);
                } 
                
                var pessoas = hearing.Count;
                float spreading = 1;
                
                foreach (var t in hearing) {
                    var intensity = Mathf.InverseLerp(15, 7, t.distanceTo(amelia.position));
                    spreading += (t.factor*intensity) / pessoas;
                }
                
                foreach (var t in hearing) {
                    var intensity = Mathf.InverseLerp(15, 7, t.distanceTo(amelia.position));
                    t.SetIntensity(Mathf.InverseLerp(0, spreading,t.factor * intensity/pessoas));
                }
                
            } else if (hearing.Contains(actor)) {
                actor.SetIntensity(0);
                actor.Pause();
                hearing.Remove(actor);
            }
        }


    }
}
