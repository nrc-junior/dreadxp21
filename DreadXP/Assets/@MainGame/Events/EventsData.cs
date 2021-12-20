using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsData : MonoBehaviour {
    static Dictionary<Person, Transform> puppet;
    static Dictionary<Person, Actor> _actor;
    
    public static void Attribuate() {
        puppet = new Dictionary<Person, Transform>();
        _actor = new Dictionary<Person, Actor>();
        foreach (var a in FindObjectsOfType<Actor>()) {
            puppet[a.actor.nome] = a.puppeter;
            _actor[a.actor.nome] = a;
        }
    }
    public static void Teleport(Person actor, Vector3 pos) {
        puppet[actor].position = pos;
        print("teleported "+ actor);
    }

    /// <summary>
    /// seta um dialogo, para um NPC, usando a classe DialogueActivator.
    /// </summary>
    /// <param name="actor"> enum Person = nome do npc</param>
    /// <param name="dialogue">atributo do dialogo</param>
    public static void SetDialog(Person actor, DialogueObject dialogue) => puppet[actor].GetComponent<DialogueActivator>().UpdateDialogObject(dialogue);

    /// <summary>
    /// Seta animação de um NPC / Ator
    /// </summary>
    /// <param name="name">enum Person = nome do npc</param>
    /// <param name="anim">enum Animations = nome da animação, se não tiver = Idle</param>
    public static void SetAnimation(Person name, Animations anim) => _actor[name].SetAnimation(anim);

    
}
