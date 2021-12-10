using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anime;
    public float speed;
    Vector3 move;
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        move = new Vector3(dir.x,dir.y,0);
        transform.localRotation =  dir.x < 0 ? transform.localRotation = Quaternion.Euler(new Vector3(0,180,0)) : Quaternion.Euler(new Vector3(0,0,0));
        anime.SetFloat ("Speed", Mathf.Abs(dir.x));
    }
    void FixedUpdate(){
        rb.MovePosition(rb.position+move * speed * Time.fixedDeltaTime);
    }
}
