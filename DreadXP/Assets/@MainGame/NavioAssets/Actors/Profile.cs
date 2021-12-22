using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Person {
        undefined,
        Aaron,
        Antoine,
        Demeter,
        Patrick,
        Jerome,
        Ivana,
        Gisele,
        Amelia,
        Monster
    }

    public enum Animations {
        idle,
        walk,
        run,
        watch,
        fix,
        note,
        coffe,
        insanity,
        mop,
        spawn,
        attack
    }


[System.Serializable]
public class Waiter {
    public float x;
    public float y;

    public float seconds = 1;
    public bool skip;
    public Animations animation;
}

[System.Serializable]
public class Profile{
    public Person nome;
    public Vector2 offset;
    [System.Serializable] public class Handler {
       public Animations name;
       public float frame_duration = 0.2f;
       public Vector2 start;
       public Vector2 end;

       public List<Waiter> cycles;
   }

    public Dictionary<Animations,Handler> animations;
}
