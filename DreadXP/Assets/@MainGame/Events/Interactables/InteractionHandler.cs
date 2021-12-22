using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour {
    public delegate void Interactable();
    public static event Interactable interaction;
    public KeyCode interact_key;


    public void Update() {
        if (Input.GetKeyDown(interact_key))
            if (interaction != null) interaction();
    }
}
