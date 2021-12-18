using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAssets : MonoBehaviour {
    private Actor[] actors;
    private void Awake() {
        actors = FindObjectsOfType<Actor>();
    }
    public Actor GetActor(Person n) {
        foreach (Actor a in actors) {
            if (n == a.actor.nome) return a;
        }
        return null;
    }
}
