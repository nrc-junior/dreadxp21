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
    public Theme(GameObject actor, AudioSource src, AudioClip clip, AudioClip other = default) {
        this.actor = actor;
        this.src = src;
        this.src.clip = clip;
        this.other = other;
        
        src.volume = natural_intensity;
        src.Play();
        src.Pause();
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
                themes[actorNome] = new Theme(actor, new GameObject(actor.name + " soundsrc").AddComponent<AudioSource>(), music.clip);
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
            if ((dst = actor.distanceTo(amelia.position)) < 15) {
                float intensity = Mathf.InverseLerp(15, 7, dst);
                actor.SetIntensity(intensity);
                
                
                //float decreaser = Mathf.Lerp(1f, 0.6f,hearing.Count * 0.125f);
                
                if(hearing.Contains(actor)) continue;
                actor.time = amelia.time;
                actor.Play();
                hearing.Add(actor);
                
                //foreach (var person2 in themes.Keys) themes[person2].SetIntensity();
                
            } else if (hearing.Contains(actor)) {
                actor.SetIntensity(0);
                actor.Pause();
                hearing.Remove(actor);
            }
        }


    }
}
