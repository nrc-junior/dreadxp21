using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        print("1 aqui");
        if(other.CompareTag("Player") && other.TryGetComponent(out Moving player))
        {
            print("2 aqui");
            //player.IInteractable_3D = ;
        }
    }
}
