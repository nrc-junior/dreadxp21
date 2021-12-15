using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmarineMove : MonoBehaviour {
    Vector2 thrust = Vector2.zero; 
    private Rigidbody rb;

    public float hor_stability_factor = 2;
    private bool desacelerating;
    private float desacelation = 0;
    float hor_desacelation_rate = 0f;

    public float ver_stabilty_factor = 2;
    private bool stabilizing;
    private float ver_stability = 0;
    float ver_stabilization_rate = 0f;

    private int max_hor_speed = 14;
    private int max_ver_speed = 7;

    private bool last_operator = true;
    private float turn_point = 0;
    private float turn_factor = 0;
    private bool facing_right = true;
    private float facctor_update = 0.1f;
    
    private Transform obj;
    private float angle = 0;
    private bool turning = false;
    private float rotation = 0;


    [System.Serializable]
    
    public class Particle {
        public int max_hits = 3;
        public GameObject src;
        public Particle[] effects;
    }

    private int collisions = 1;
    public Particle[] vfx;
    private bool sinking;
    
    void Start() {
        obj = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        hor_desacelation_rate = 1 / hor_stability_factor;
        ver_stabilization_rate = 1 / ver_stabilty_factor;
    }

    void Update() {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!sinking) {
            Thrusting(inputs);
            DirectionTurns(inputs);
        }else {
            Thrusting(new Vector2(inputs.x, -1));
        }
    }

    void OnCollisionEnter (Collision other) {
        if (other.relativeVelocity.magnitude > (13/(collisions*0.98f))) {
            collisions++;
            
            if (collisions-1 >= 3) sinking = true;
            max_hor_speed /= collisions;
            foreach (var particle in vfx) {
                if (collisions-1 >= particle.max_hits) {
                    particle.src.SetActive(true);
                }
            }
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
