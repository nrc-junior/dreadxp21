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
    
    void Start() => cam = Camera.main;
    void Update(){
        if(temFita){ // o player ja selecionou uma fita, então tem que exibir 
            if(!renderFita){ // ainda n está renderizando 
                renderFita = true;
      
                fita_selecionada = azul ? fita_azul : fita_vermelha;
                fita_selecionada.SetActive(true);
            }// com a fita ativa na tela, da pra começar a mexer ela.

            var mira  = cam.ScreenToWorldPoint(Input.mousePosition);
            var pos = fita_selecionada.transform.position;
            if (chegada < 1) {
                chegada += Time.deltaTime * (1 / tempo_para_chegar);
            }
            fita_selecionada.transform.position = Vector3.Lerp(pos, mira, chegada);
        }

        if(Input.GetMouseButton(0)){ //clico com botão esquerdo
        
            print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

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
