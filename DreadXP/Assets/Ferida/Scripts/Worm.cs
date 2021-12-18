using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class At : MonoBehaviour
{
    public GameObject target;
    public bool chased = false; 
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "submarine"){
            chased = true;
        }
    }
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "submarine"){
    //         chased = false;
    //     }
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (chased){
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, 0.5f * Time.deltaTime );
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f*Time.deltaTime);
        }

    }
}
