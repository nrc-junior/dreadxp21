using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver{
    
    public Material skin;
    public MeshRenderer render;
    public GameObject gameobject;



    
    public Diver(Material skin, GameObject gameobject, MeshRenderer render) {
        this.skin = skin;
        this.gameobject = gameobject;
        this.render = render;
    }

}
