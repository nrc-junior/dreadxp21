using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmarineMove : MonoBehaviour {
    
    //Physics
    Vector2 thrust = Vector2.zero; 
    private Rigidbody rb;
    
    //Horizontal Controllers
    public float hor_stability_factor;
    float max_hor_speed = 14;
    float hor_desacelation_rate = 0f;
    private bool desacelerating;
    private float desacelation = 0;
    
    //Vertical Controllers
    public float ver_stability_factor;
    float max_ver_speed = 7;
    float ver_stabilization_rate = 0f;    
    private bool stabilizing;
    private float ver_stability = 0;

    
    // Turning
    private bool last_operator = true;
    private float turn_point = 0;
    private float turn_factor = 0;
    private bool facing_right = true;
    private float facctor_update = 0.1f;
    
    private Transform obj;
    private float angle = 0;
    private bool turning = false;
    private float rotation = 0;

    [System.Serializable] public class Particle {
        public int max_hits = 3;
        public GameObject src;
        public Particle[] effects;
    }

    private Animator shaker;
    private int collisions = 1;
    public Particle[] vfx;
    private bool sinking;
    
    public class SubmarineTrigger : MonoBehaviour {
        public bool player_on_trigger;
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                player_on_trigger = true;
            }
        }       
        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                player_on_trigger = false;
            }
        }
    }
    
    public KeyCode leave;
    private SubmarineTrigger sub_trigger;
    private bool player_controlling = true;
    
    //repairing
    private bool repairing;
    public KeyCode repair_key;
    private float repair_clock = 0;

    //skin
    private Diver diver;
    private float time = 0;
    private float offset;

    //Camera
    private Camera cam;
    private float focused = 93;
    private float panorama = 43;
    private float cam_clock = 0;
    private const float reach_focus = 0.75f;
    private bool focus_reached = false;
    private Vector3 cam_origin;
    public Transform player;
    private Transform cam_holder;
    
    //diver move
    Rigidbody rb_diver;
    
    void Start() {
        obj = submarine = transform.GetChild(0);
        diver = new Diver(transform.GetChild(1).GetComponent<Renderer>().material,
                          transform.GetChild(1).gameObject);
        
        sub_trigger = obj.gameObject.AddComponent<SubmarineTrigger>();
        rb = GetComponent<Rigidbody>();
        rb_diver = diver.gameobject.GetComponent<Rigidbody>();
        
        cam_origin = (cam_holder = (cam = Camera.main).transform.parent.parent).localPosition;
        shaker = cam_holder.GetComponent<Animator>();
        hor_desacelation_rate = 1 / hor_stability_factor;
        ver_stabilization_rate = 1 / ver_stability_factor;
        
    }
    
    
    private Transform submarine;
    void Update() {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (player_controlling) {
            if (focus_reached) {
                if (cam_clock > 1) {
                    focus_reached = false;
                    cam_clock = 0;
                    return;
                }

                cam_clock += Time.deltaTime * reach_focus;
                cam.focalLength = Mathf.Lerp(focused, panorama, cam_clock);
                Vector3 pos = cam_holder.localPosition;

                cam_holder.localPosition = new Vector3(Mathf.Lerp(pos.x, cam_origin.x, cam_clock),
                    Mathf.Lerp(pos.y, cam_origin.y, cam_clock), cam_origin.z);
            }

            if (Input.GetKeyDown(leave) && !focus_reached) {

                diver.gameobject.SetActive(true);
                diver.gameobject.transform.position = obj.transform.position + new Vector3(0.84f, 6.47f, -2.373f);
                player_controlling = false;

                rb.isKinematic = true;
                return;
            }
            
            if (!sinking) {
                Thrusting(inputs);
                DirectionTurns(inputs);
            }else {
                Thrusting(new Vector2(inputs.x, -1));
            }
        
        }else {
            
            if (Input.GetKeyDown(leave) && sub_trigger.player_on_trigger) {
                diver.gameobject.SetActive(false);
                player_controlling = true;
                rb.isKinematic = false;
                if(collisions == 0) collisions = 1;
                if (max_hor_speed > 14)
                {
                    max_hor_speed = 14;
                    max_ver_speed = max_hor_speed / 2;
                }
                return;
            }
            #region  skin
            repairing = collisions > 1 && sub_trigger.player_on_trigger && Input.GetKey(repair_key);

            if (!repairing) { // swim/idle
                if (Time.time > time) {
                    time = Time.time + .25f;
                    offset = offset >= 0.75f ? 0 : offset + 0.25f;
                    diver.skin.mainTextureOffset = collisions <= 1 ? new Vector2(offset, 0.675f) : new Vector2(offset, 1.345f);
                } 

            } else { // repair
                if(Input.GetKeyDown(repair_key)) repair_clock = Time.time + 5;
                
                if (Time.time > time) {
                    time = Time.time + .25f;
                    offset = offset >= 0.75f ? 0 : offset + 0.25f;
                    diver.skin.mainTextureOffset = new Vector2(offset, 2);
                }
            }
            #endregion
            
            //mov
            #region mov
            var localRotation = diver.gameobject.transform.localRotation;
            localRotation =  inputs.x < 0 ? localRotation = Quaternion.Euler(new Vector3(0,180,0)) : Quaternion.Euler(new Vector3(0,0,0));
            diver.gameobject.transform.localRotation = localRotation;
            rb_diver.velocity = 280 * new Vector2(inputs.x, inputs.y) * Time.deltaTime;            
            #endregion

            //cam
            #region cam
            if (!focus_reached) {
                if (cam_clock > 1) {
                    focus_reached = true;
                    cam_clock = 0;
                    return;
                }
                cam_clock += Time.deltaTime * reach_focus;
                cam.focalLength = Mathf.Lerp(panorama, focused, cam_clock);
                Vector3 pos = player.localPosition;
                
                cam_holder.localPosition = new Vector3(Mathf.Lerp(cam_origin.x, pos.x, cam_clock),
                    Mathf.Lerp(cam_origin.y, pos.y, cam_clock), cam_origin.z);
            }else {
                Vector3 pos = player.localPosition;
                Vector3 campos = cam_holder.localPosition;
                cam_holder.localPosition = new Vector3(Mathf.Lerp(campos.x, pos.x, Time.deltaTime),
                    Mathf.Lerp(campos.y, pos.y, Time.deltaTime), campos.z);
            }
            #endregion

            //repair
            #region repair

            if (repairing && Time.time > repair_clock && collisions > 0) {
                max_hor_speed *= collisions;
                max_ver_speed = max_hor_speed/2;
                if(sinking) {
                    sinking = !(collisions < 4);
                };
                
                repair_clock = Time.time + 5;
                collisions--;
                
                foreach (var particle in vfx) {
                    if (particle.max_hits >= collisions) {
                        particle.src.SetActive(false);
                    }
                }

            }
            

            #endregion
        }
    }

    void OnCollisionEnter (Collision other) {
        if(!player_controlling) return;
        
        if (other.relativeVelocity.magnitude > 4) {
            thrust = Vector2.zero;
            shaker.Play("CamShake");
            if(other.relativeVelocity.magnitude < 13/(collisions*0.98f)) {
                SoundManager.PlaySound(SoundManager.Sound.softhit);
            } 
        }
        
        if (other.relativeVelocity.magnitude > (13/(collisions*0.98f))) {
            collisions++;
            
            if (collisions-1 >= 3) sinking = true;
            max_hor_speed /= collisions;
            max_ver_speed = max_hor_speed / 2;
            foreach (var particle in vfx) {
                if (collisions-1 >= particle.max_hits) {
                    particle.src.SetActive(true);
                }
            }
            SoundManager.PlaySound(SoundManager.Sound.hit);

        };
    }
    private void DirectionTurns(Vector2 inputs) {
        if (!turning) {
            if (inputs.x == 0) {
                turn_factor = 0;
                return;
            }
            
            if (inputs.x < 0) {
                if (last_operator) {
                    turn_factor = 0;
                    last_operator = false;
                }
                
                if(!facing_right) return;
                
                turn_factor += Time.deltaTime ;
                if (turn_factor > 1) {
                    facing_right = false;
                    angle = -180;
                    turning = true;
                    turn_factor = 0;
                }
                
            }else if (inputs.x > 0) {
                if (!last_operator) {
                    turn_factor = 0;
                    last_operator = true;
                }
            
                if(facing_right) return;
                
                turn_factor += Time.deltaTime ;
                
                if (turn_factor > 1) {
                    facing_right = true;
                    turn_factor = angle = 0;
                    turning = true;
                    Turn();
                }
            }
        }else {
            Turn();
        }
    }
    private void Turn() {
        if (rotation >= 1) {
            rotation = 0;
            turn_factor = 0;
            turning = false;
            return;
        }

        rotation += Time.deltaTime; 
        
        var euler = obj.rotation.eulerAngles;
        obj.localRotation = Quaternion.Lerp(obj.localRotation, Quaternion.Euler(euler.x, angle, euler.z), rotation );
    }
    private void Thrusting(Vector2 inputs) {
        thrust += new Vector2(Mathf.Abs(thrust.x + inputs.x) < max_hor_speed ? inputs.x : 0, 
            Mathf.Abs(thrust.y + inputs.y) < max_ver_speed ? inputs.y : 0);
        
        if(Mathf.Abs(inputs.x) > 0) {
            desacelerating = false;
        }
        
        if (thrust.x != 0   ) {
            if (!desacelerating) {
                desacelation = 0;
                desacelerating = true;
            }
            
            if (desacelation < 1) {
                desacelation += Time.deltaTime * hor_desacelation_rate;
                thrust.x = Mathf.Lerp(thrust.x, 0, desacelation);
            }
        }       
        
        if(Mathf.Abs(inputs.y) > 0) {
            stabilizing = false;
        }
        if (thrust.y != 0  ) {
            if (!stabilizing) {
                ver_stability = 0;
                stabilizing = true;
            }
            
            if (ver_stability < 1) {
                ver_stability += Time.deltaTime * ver_stabilization_rate;
                thrust.y = Mathf.Lerp(thrust.y, 0, ver_stability);
            }
        }
        rb.velocity = thrust;
    }

}
