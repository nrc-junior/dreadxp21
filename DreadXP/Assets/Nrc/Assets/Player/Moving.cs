using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    //controls
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 mov;
    [HideInInspector] public bool sit;

    
    bool walking;
    bool interacting;
    Material player_skin;


    void Start() {
        player_skin = transform.GetChild(0).GetComponent<Renderer>().material;
        
    }

    float offset;
    float time;

    void Update()
    {
        if (!sit)
        {
            #region moviment

            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dir != Vector2.zero)
            {
                if (!(new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y)) == Vector2.one))
                {
                    dir =
                        dir.x > 0 && dir.y == 0 ? new Vector2(dir.x, dir.x) : // vai pra direita? 
                        dir.x < 0 && dir.y == 0 ? new Vector2(dir.x, dir.x) : // vai pra esquerda?  
                        new Vector2(-dir.y, dir.y); // cima & baixo
                }
                else
                {
                    dir =
                        dir.x > 0 && dir.y < 0 ? new Vector2(dir.x * 1.5f, -dir.y / 4) : // direita  baixo?
                        dir.x > 0 && dir.y > 0 ? new Vector2(-dir.x / 4, dir.y * 1.5f) : // direita cima ?  
                        dir.x < 0 && dir.y < 0 ? new Vector2(-dir.x / 4, dir.y * 1.5f) : // esquerda  baixo?  
                        new Vector2(dir.x * 1.5f, -dir.y / 5); // esquerda cima?
                }

            }

            mov = new Vector3(dir.x, 0, dir.y);

            #endregion

            #region animation

            walking = dir != Vector2.zero;
            if (walking)
            {

                //walks
                if (Time.time > time)
                {
                    time = Time.time + .25f;
                    offset = offset >= 0.75f ? 0 : offset + 0.25f;
                    player_skin.mainTextureOffset = new Vector2(offset, 0.333f);
                }
            }
            else if (interacting)
            {
                //interact
                if (Time.time > time)
                {
                    time = Time.time + .25f;
                    offset = offset >= 0.75f ? 0 : offset + 0.25f;
                    player_skin.mainTextureOffset = new Vector2(offset, 0);
                }
            }
            else if (Time.time > time)
            {
                // idle
                time = Time.time + .25f;
                offset = offset >= 0.75f ? 0 : offset + 0.25f;
                player_skin.mainTextureOffset =  new Vector2(offset, 0.666f);
            }

            #endregion
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }
}
