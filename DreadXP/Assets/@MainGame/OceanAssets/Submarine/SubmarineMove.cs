using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmarineMove : MonoBehaviour {
    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update() {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 20, Input.GetAxis("Vertical") * 20, 0);

    }

}
