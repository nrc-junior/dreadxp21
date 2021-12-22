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

    private GameObject col;
    private Actor actor;
    void OnTriggerEnter(Collider col) {
        if (!col.TryGetComponent(out Actor actor)) {
            actor = col.GetComponentInChildren<Actor>();
            if (actor == null) return;
        }
        
        if (col.CompareTag("Player")) { // se for player espera apertar E
            this.col = col.gameObject;
            this.actor = actor;
            InteractionHandler.interaction += TeleportPlayer;
            return;
        } else if (actor.goingTo == room) { // se for npc e estiver indo pra "essa" sala:
            DataManager.whoEnter = actor.actor.nome;
            DataManager.whatRoom = room;
            RoomHandler.Fire();
            col.transform.position = reach.position;
        } 
    }
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Player")) { 
            InteractionHandler.interaction -= TeleportPlayer;
        }
    }

    public void TeleportPlayer() {
        RoomHandler.enable = enable;
        RoomHandler.disable = disable;
        RoomHandler.player = col.gameObject;
        RoomHandler.pos = reach.position;
        RoomHandler.fade = true;
        RoomHandler.seconds = 1 / fade_duration;
        DataManager.whoEnter = actor.actor.nome;
        DataManager.whatRoom = room;
        RoomHandler.Fire();
        InteractionHandler.interaction -= TeleportPlayer;

    }

}
