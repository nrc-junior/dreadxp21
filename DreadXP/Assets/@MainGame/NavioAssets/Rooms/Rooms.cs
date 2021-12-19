using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
public class Rooms: MonoBehaviour {
    public GameObject disable;
    public GameObject enable;
    
    public Room room = Room.undefined;
    public Transform reach;
    public float fade_duration = .8f;

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) return;

        Actor actor;
        if (!col.TryGetComponent(out actor)) {
            actor = col.GetComponentInChildren<Actor>();
            if (actor == null) return;
        }
        
        DataManager.whoEnter = actor.actor.nome;
        DataManager.whatRoom = room;
        RoomHandler.Fire();
        col.transform.position = reach.position;
        
    }
    void OnTriggerStay(Collider col) {
        if (Input.GetKeyDown(KeyCode.E)) {
            Actor actor;
            if (!col.TryGetComponent(out actor)) {
                actor = col.GetComponentInChildren<Actor>();
                if (actor == null) return;
            }

            DataManager.whoEnter = actor.actor.nome;
            DataManager.whatRoom = room;
            RoomHandler.Fire();

            if (col.CompareTag("Player")) {
                RoomHandler.enable = enable;
                RoomHandler.disable = disable;
                RoomHandler.player = col.gameObject;
                RoomHandler.pos = reach.position;
                RoomHandler.fade = true;
                RoomHandler.seconds = 1 / fade_duration;
            } else {
                col.transform.position = reach.position;
            }
        }
    }

}
