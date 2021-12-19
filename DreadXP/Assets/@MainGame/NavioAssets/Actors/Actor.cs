using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    public Profile actor;
    public bool onScene;

    private float nextupdate;
    private Material skin;
    private Vector2 uv;
    private float ms;
    public Animations start;

    [Space(10)] 
    [Header("Sound Effects")]
    public AudioSource theme;
    
    [Header("Sound Effects")]
    public float delay = .16f;
    private float clock =0;
    
    [Space(10)]
    public List<Profile.Handler> animations;
    private Profile.Handler current;

    
    
    private void Awake() {
        skin = GetComponent<Renderer>().material;

        // Animações do inspetor sendo atribuidas ao dicionario: 
        actor.animations = new Dictionary<Animations, Profile.Handler>();
        foreach (var anm in animations) actor.animations[anm.name] = anm;
        current = actor.animations[start];
        ms = current.frame_duration;
        uv = current.start;
    }

    public void Update() {
        nextupdate += Time.deltaTime * (1/ms);
        if (nextupdate < 1) return;
        
        nextupdate = 0;
        uv.x = uv.x < current.end.x ? uv.x + actor.offset.x : current.start.x;
        if ((uv.x == current.start.x) && (current.start.y != current.end.y)) {
            uv.y = uv.y < current.end.y ? uv.y + actor.offset.y : current.start.y;
        }
        skin.mainTextureOffset = new Vector2(uv.x, uv.y);

        switch (current.name) {
            default: break;
            case Animations.walk:
                Play(SoundManager.Sound.footstep);                
                break;
            
            case Animations.mop:
                Play(SoundManager.Sound.mopping, true);
                break;
            
            case Animations.fix:
                Play(SoundManager.Sound.fixing);
                break;
        }
    }

    void Play(SoundManager.Sound sound, bool use_clip_time = false) {
        if (Time.time < clock) return;
        float clipLength = SoundManager.PlaySound(sound, -1, transform.position);
        clock = Time.time + (use_clip_time ? clipLength + delay : delay);
        
    }
    
    /// <summary>
    /// Anime é uma animação do Enum Animations, se veja o script Profile para ver o enum.
    /// Current recebe o valor do dicionario contido em actor, atribuido na inspetor do menu.
    /// </summary>
    /// <param name="anime"></param>
    public void SetAnimation(Animations anime) {
        current = actor.animations[anime];
        uv = current.start;
        ms = current.frame_duration;
    }

    public void Flip() {
        var t = transform;
        Vector3 scale = t.localScale;
        scale.x = -scale.x;
        t.localScale = scale;
        
    }
    
}
