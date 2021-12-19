using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Room {
    undefined,
    main,
    superior,
    bridge,
    deck_traseiro,
    descontaminacao,
    lab,
    garagem,
    oficina_submarino,
    oficina_engenheiro,
    oficina_eletrica,
    lavanderia,
    conferencia,
    vent,
    banheiros,
    chuveiro,
    quarto_pesquisadores,
    hospital,
    deposito,
    deck_social,
    refeitorio,
    cozinha,
    lazer,
    deck_superior,
    corredor_main,
    escadaria,
    corredor_superior,
    leme
}

public class RoomHandler : MonoBehaviour {
    public static GameObject disable;
    public static GameObject enable;
    
    public static GameObject player;
    public static Vector3 pos;
    public static float seconds;
    public static bool fade;
    
    public Image screen;
    private float alpha = 0;  
    private bool dark;

    private void Update() {
        if(!fade) return;

        if (!dark) {
            if (alpha < 1) {
                alpha += Time.deltaTime * seconds/2; 
                var c = screen.color;
                c.a = alpha;
                screen.color = c;
                return;
            } else {
                alpha = 0;
                dark = true;
                enable.SetActive(true);
                disable.SetActive(false);
                player.transform.position = pos;
                SoundManager.PlaySound(SoundManager.Sound.door, -1, pos,5);
                return;
            }
        } else {
            if (alpha < 1) {
                alpha += Time.deltaTime * seconds/2; 
                var c = screen.color;
                c.a = Mathf.Lerp(1,0,alpha);
                screen.color = c;
                return;
            } 
        }

        alpha = 0;
        dark = false;
        fade = false;
    }

    public delegate void RoomChange();
  public static event RoomChange LoadRoom;

    public static void Fire() {
        if (LoadRoom != null) {
            LoadRoom();
        }
    }

}
