using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CausticsAnimation : MonoBehaviour {
    public float duration;
   
    [SerializeField] private Texture[] sprites;
       
    public Material material;
    private int index = 0;
    private float timer = 0;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    [ExecuteAlways]
    private void Update() {
        if((timer+=Time.deltaTime) >= (duration / sprites.Length)) {
            timer = 0;
            material.SetTexture(MainTex, sprites[index]); 
            index = (index + 1) % sprites.Length;
        }
    }
}
