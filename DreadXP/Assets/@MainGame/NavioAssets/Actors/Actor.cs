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
