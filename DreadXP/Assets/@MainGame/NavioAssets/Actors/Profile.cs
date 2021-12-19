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
        Amelia
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
        mop
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
    }

    public Dictionary<Animations,Handler> animations;
}
