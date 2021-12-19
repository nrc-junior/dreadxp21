using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormActivator : MonoBehaviour
{
    // Start is called before the first frame update

    public bool chased = false; 
    // Update is called once per frame
   
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Submarine"){
            chased = true;
        }
    }
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "submarine"){
    //         chased = false;
    //     }
        
    // }
}
