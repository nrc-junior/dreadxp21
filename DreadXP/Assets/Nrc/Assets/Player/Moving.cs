using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {
    public Room isIn = Room.undefined;

    void Awake() => DataManager.playerIsIn = isIn;
    
    
    public ShipAssets assets;
    private Actor player;
    private bool right;
    bool walking;
    private bool _lock1;

    [HideInInspector] public bool freeze;
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 mov;


    private void Start() {
       player = assets.GetActor(Person.Amelia);
    }

    void Update() {
        
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dir.x < 0 && right) {
            right = false;
            player.Flip();
        } else if (dir.x > 0 && !right) {
            right = true;
            player.Flip();
        }
        
        
        if (dir != Vector2.zero) {
        if (freeze) return;
            if (!(new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y)) == Vector2.one)) {
                dir =
                    dir.x > 0 && dir.y == 0 ? new Vector2(dir.x, dir.x) : // vai pra direita? 
                    dir.x < 0 && dir.y == 0 ? new Vector2(dir.x, dir.x) : // vai pra esquerda?  
                    new Vector2(-dir.y, dir.y); // cima & baixo
            } else {
                dir =
                    dir.x > 0 && dir.y < 0 ? new Vector2(dir.x * 1.5f, -dir.y / 4) : // direita  baixo?
                    dir.x > 0 && dir.y > 0 ? new Vector2(-dir.x / 4, dir.y * 1.5f) : // direita cima ?  
                    dir.x < 0 && dir.y < 0 ? new Vector2(-dir.x / 4, dir.y * 1.5f) : // esquerda  baixo?  
                    new Vector2(dir.x * 1.5f, -dir.y / 5); // esquerda cima?
            }

        }
        mov = new Vector3(dir.x, 0, dir.y);


        walking = dir != Vector2.zero;
        if (walking && !_lock1) {
            _lock1 = true;
            player.SetAnimation(Animations.walk);
            SoundManager.PlaySound(SoundManager.Sound.footstep, default, transform.position);
        }else if(!walking && _lock1){
            _lock1 = walking;
            player.SetAnimation(Animations.idle);
        }

    }
    

    void FixedUpdate() {
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }
}
