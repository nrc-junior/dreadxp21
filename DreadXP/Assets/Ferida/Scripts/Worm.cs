using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public GameObject target;
    
    
    public GameObject wormRange;


    // Start is called before the first frame update

    // Update is called once per frame
    void Start(){
       
    }
    void Update()
    {
        if (true){
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, 2f * Time.deltaTime );
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f*Time.deltaTime);
        transform.position += transform.forward * 10f * Time.deltaTime;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //chasing = !chasing;
    }
}
