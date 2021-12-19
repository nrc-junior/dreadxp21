using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAssets : MonoBehaviour {
    public static GameObject _player;
    //TODO sistema de musica sendo feito.    
    
    public GameObject player;
    private Actor[] actors;
    private void Awake() {
        actors = FindObjectsOfType<Actor>();
        _player = player;
    }
    public Actor GetActor(Person n) {
        foreach (Actor a in actors) {
            if (n == a.actor.nome) return a;
        }
        return null;
    }
}
