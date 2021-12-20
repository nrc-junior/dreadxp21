using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public AudioSource src;
    public bool paused;
    
    bool temFita = true; // supostamente ele clicou em uma fita 
    bool azul = false; // ele clicou na fita vermelha 
    bool renderFita = false; // fita ainda não está sendo renderizada 

    public GameObject fita_azul;
    public GameObject fita_vermelha;

    public GameObject fita_selecionada;

    private Camera cam;
    private float chegada;
    public float tempo_para_chegar = 2; //segundos

    void Start() {
        //0,233333
        //0,266666
        //0,1
        print(Mathf.InverseLerp(0, 0.599999f,0.233333f));
        print(Mathf.InverseLerp(0, 0.599999f,0.266666f));
        print(Mathf.InverseLerp(0, 0.599999f,0.1f));
    }
    
  
    void ASDA() {
        if (Input.GetKeyDown(KeyCode.E)) {
            print(src.time);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            src.time += 10;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            src.time -= 10;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (paused) {
                src.UnPause();
                paused = false;
            }else {
                src.Pause();
                paused = true;
            }
        }
    }
}
