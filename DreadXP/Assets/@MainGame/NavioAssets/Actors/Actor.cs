using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    public Profile actor;
    public bool onScene;
    public DialogueObject[] dialogs;
    public static DialogueObject[] _dialogs;
    private float nextupdate;
    private Material skin;
    private Vector2 uv;
    private float ms;
    public Animations start;

    private List<Waiter> cycles;
    private float cycle_locked;
    
    [Space(10)] 
    [Header("Sound Effects")]
    public AudioSource theme;
    
    [Header("Sound Effects")]
    public float delay = .16f;
    private float clock =0;
    
    [Space(10)]
    public List<Profile.Handler> animations;
    private Profile.Handler current;

    [HideInInspector] public Transform puppeter;

    public Room goingTo = Room.undefined;
    private void Awake() {
        skin = GetComponent<Renderer>().material;
        _dialogs = dialogs;
        Transform parent = transform;
        while (parent.parent != null) {
            parent = parent.transform.parent;
        }
        
        puppeter = parent;

        // Animações do inspetor sendo atribuidas ao dicionario: 
        actor.animations = new Dictionary<Animations, Profile.Handler>();
        foreach (var anm in animations) actor.animations[anm.name] = anm;
        current = actor.animations[start];
        ms = current.frame_duration;
        uv = current.start;
        cycles = current.cycles;
        
        EventsData.Attribuate(); // atribui puppeter ao gerenciador de eventos.
    }
    
    

    public void Update() {
        nextupdate += Time.deltaTime * (1/ms);
        
        if (nextupdate < 1) return;
        
        nextupdate = 0;

        if (Time.time > cycle_locked) {
            uv.x = uv.x < current.end.x ? uv.x + actor.offset.x : current.start.x;
            if ((uv.x == current.start.x) && (current.start.y != current.end.y)) {
                uv.y = uv.y < current.end.y ? uv.y + actor.offset.y : current.start.y;
            }

            if (cycles != null) {
                foreach (var hold in cycles) {
                    if (hold.x == uv.x && hold.y == uv.y) {
                        cycle_locked = Time.time + hold.seconds;
                        if (hold.skip) {
                            SetAnimation(hold.animation);
                        }
                        return;
                        
                    } 
                }
            }
            skin.mainTextureOffset = new Vector2(uv.x, uv.y);
        }

        switch (current.name) {
            default: break;
            case Animations.walk:
                Play(SoundManager.Sound.footstep,vol: .6f, max_dst: 10, mode: AudioRolloffMode.Custom);                
                break;
            
            case Animations.mop:
                Play(SoundManager.Sound.mopping,0.3f , true, max_dst: 10, mode: AudioRolloffMode.Custom);
                break;
            
            case Animations.fix:
                Play(SoundManager.Sound.fixing, vol: .6f ,max_dst: 10, dst:1, mode: AudioRolloffMode.Custom );
                break;
        }
    }

    void Play(SoundManager.Sound sound, float vol = -1, bool use_clip_time = false, float dst = 1, float max_dst = 50, AudioRolloffMode mode = AudioRolloffMode.Logarithmic) {
        if (Time.time < clock) return;
        float clipLength = SoundManager.PlaySound(sound, vol, transform.position, dst: dst,  max_dst: max_dst, mode: mode);
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
