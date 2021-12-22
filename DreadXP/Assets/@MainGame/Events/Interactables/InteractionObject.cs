using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {
    public enum pickup {
        Day1_Briefing,
        Day1_Keys,
        Day4_MonsterMeat,
        Day5_Book,
    }

    public pickup item;
    static private GameObject gameobject;
    public void Start() {
        if (item == pickup.Day1_Briefing) {
            if (DataManager.briefing_collected) {
                InventoryControl.i.AddItem(0);
                Destroy(this);
            }
        }
        gameobject = gameObject;
    }

    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent(out InteractionHandler ih);
        if(ih == null) return;
        GetPickup(true);
    }    
    
    private void OnTriggerExit(Collider other) {
        other.TryGetComponent<InteractionHandler>(out InteractionHandler ih);
        if(ih == null) return;
        GetPickup(false);
    }

    static void pickBriefing() {
        DataManager.briefing_collected = true;
        InventoryControl.i.AddItem(0);
        Destroy(gameobject);
    }   
    
    static void pickKeys() {
        DataManager.keys_collected = true;
        Destroy(gameobject);
    }
    
    static void pickMonterMeat() {
        DataManager.meat_collected = true;
        Destroy(gameobject);
    }
        
    static void pickNotebook() {
        InventoryControl.i.AddItem(4);
        Destroy(gameobject);
    }
    
    
    
    private void GetPickup(bool add) {
        if (add) {
            switch (item) {
                case pickup.Day1_Briefing:
                    InteractionHandler.interaction += pickBriefing;
                    break;

                case pickup.Day1_Keys:
                    InteractionHandler.interaction += pickKeys;
                    break;

                case pickup.Day4_MonsterMeat:
                    InteractionHandler.interaction += pickMonterMeat;
                    break;
                
                case pickup.Day5_Book:
                    InteractionHandler.interaction += pickNotebook;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }else {
            switch (item) {
                case pickup.Day1_Briefing:
                    InteractionHandler.interaction -= pickBriefing;
                    break;

                case pickup.Day1_Keys:
                    InteractionHandler.interaction -= pickKeys;
                    break;

                case pickup.Day4_MonsterMeat:
                    InteractionHandler.interaction -= pickMonterMeat;
                    break;
                
                case pickup.Day5_Book:
                    InteractionHandler.interaction -= pickNotebook;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}
