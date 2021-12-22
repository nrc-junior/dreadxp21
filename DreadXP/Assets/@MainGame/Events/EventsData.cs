using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EventsData : MonoBehaviour {
    static Dictionary<Person, Transform> puppet;
    static Dictionary<Person, Actor> _actor;

    private void Awake() {
        _artefact = artefact;
        _vGO = medo_do_melhor;
        _vPlayer = video;
        _rabiscos = rabiscos_eletrica;
        _chuva = chuva_objs;
        _pregos = pregos;
        _meat = meat;
        _DoorTriggers = doorTriggers;
        _WaterAndSymbols = water_and_symbols;
        _jerome = jerome;
        _jeromedead = jeromedead;
        _leaveTrigger = leave_triggers;
        _table = table;

    }

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

    public static void GiveItem(int id) => InventoryControl.i.AddItem(id);
    
    
    // DAY 2 EVENTS ATRIBUTES
    public GameObject artefact;
    private static GameObject _artefact;
    public static void artefact_activate(bool active) => _artefact.SetActive(active);
    
    // DAY 3 EVENTS ATRIBUTES
    public GameObject medo_do_melhor;
    public VideoPlayer video;
    
    private static GameObject _vGO;
    private static VideoPlayer _vPlayer;
    public static void d3_playvideo() {

     _vGO.SetActive(true);
     _vPlayer.Play();
     _vGO.GetComponent<Animator>().Play("anim");
     GameObject.Destroy(_vGO,11);
     GameObject.Destroy(_vPlayer,11);
    }
    
    // DAY 4 EVENTS ATRIBUTES
    public GameObject rabiscos_eletrica;
    private static GameObject _rabiscos;
    public static void SetRabiscos(bool s) => _rabiscos.SetActive(s);

    public GameObject[] chuva_objs;
    private static GameObject[] _chuva;

    public static void Raining(bool c) {
        foreach (var o in _chuva) o.SetActive(c);
    }

    public GameObject pregos;
    private static GameObject _pregos;
    public static void SetPregos(bool c) => _pregos.SetActive(c);
    
    public GameObject meat;
    private static GameObject _meat;
    public static void SetMeat(bool c) => _meat.SetActive(c);
    
    // DAY 5 EVENTS ATRIBUTES
    public GameObject[] doorTriggers;
    private static GameObject[] _DoorTriggers;
    public GameObject[] water_and_symbols;
    private static GameObject[] _WaterAndSymbols;
    public static void Inundation(bool c) {
        foreach (var o in _WaterAndSymbols) o.SetActive(c);
        if(!c) return;
        foreach (var o in _DoorTriggers) o.SetActive(false);
    }
    public GameObject bloodsymbols;
    static GameObject _bloodsymbols;

    public GameObject jerome;
    public GameObject jeromedead;
    static GameObject _jerome;
    static GameObject _jeromedead;
    public static void EnableBloodSymbols() => _bloodsymbols.SetActive(true);

    public static void JeromeDead(bool c) {
        _jerome.SetActive(!c);
        _jeromedead.SetActive(c);
    }
    
    // DAY 6 ATRIBUTES

    private static GameObject _leaveTrigger; 
    public GameObject leave_triggers;
    private static GameObject _table; 
    public GameObject table;

    
    
    private static GameObject[] _EndingTriggers; 
    public GameObject[] ending_triggers;

    //true = cabine  | false = submarino
    public static void LeaveTrigger(bool c) {
        _leaveTrigger.SetActive(c);
        _table.SetActive(true);
    }

    
    
}
