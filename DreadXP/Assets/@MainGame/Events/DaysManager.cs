using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DaysManager : MonoBehaviour {
    private int day; 
    private void Start() {
        day = DataManager.todayIsDay;
        EventsData.artefact_activate(day > 2 || DataManager.artefact_collected );
        if(day >= 4 ) EventsData.SetRabiscos(true);
        
        switch (day) {
            case 1:
                DataManager.submarine = false;
                DataManager.canSleep = false;
                if (!DataManager.keys_collected) {
                    
                    EventsData.Teleport(Person.Antoine, new Vector3(-149.52f,2.609f,176.83f));
                    //exemplo setar dialogo... EventsData.SetDialog(Person.Antoine, PegarDialogoAntoine.dialogo_dia1);

                    EventsData.Teleport(Person.Aaron, new Vector3(-81.19f, 2.6f, 325.79f));
                    EventsData.SetAnimation(Person.Aaron, Animations.watch);
                    //setar dialogo .... do aron ...
                    
                    EventsData.Teleport(Person.Ivana, new Vector3(-81.12f, 2.60f, 328.95f));
                    EventsData.SetAnimation(Person.Ivana, Animations.idle);
                    
                    // a Ivana chega depois do dialogo do Demeter que fala sobre o submarino, chamar TODO: day1_IvanaArrives() 
                    EventsData.Teleport(Person.Demeter, new Vector3(-99.05f,2.60f,-6.84f));
                    EventsData.SetAnimation(Person.Demeter, Animations.fix);

                    EventsData.Teleport(Person.Gisele, new Vector3(100.08f,2.609f,-32.75f));
                    EventsData.SetAnimation(Person.Gisele, Animations.idle);
                    
                    
                    EventsData.Teleport(Person.Patrick, new Vector3(-164.96f,2.60f,149.91f));

                    //deixar jerome no refeitório ?
                    
                } else {
                    // apos voltar com o submarino:
                    DataManager.canSleep = true;
                }
                break;
           
            case 2: 
                DataManager.submarine = false;
                DataManager.canSleep = false;
                if (!DataManager.artefact_collected) {
                 
                 //demeter no quarto, chamar em qualquer dialogo: TODO: day2_TalksToDemeter(); 
                 EventsData.Teleport(Person.Demeter, new Vector3(53.930f,2.6097f,-31.1399f));
                 EventsData.SetAnimation(Person.Demeter, Animations.idle);
                 
                 //gisele e antoine batendo um papo:
                 EventsData.Teleport(Person.Gisele, new Vector3(-145.3994f, 2.6097f, 174.779f));
                 EventsData.SetAnimation(Person.Gisele,Animations.idle);

                 EventsData.Teleport(Person.Antoine, new Vector3(-149.52f,2.609f,176.83f));
                 
                 //confraternização da galera no refeitório 
                 EventsData.Teleport(Person.Ivana, new Vector3(-129.4f,2.609f,151.35f));
                 EventsData.SetAnimation(Person.Ivana, Animations.idle);

                 EventsData.Teleport(Person.Patrick, new Vector3(-128.050f,2.60976f,162.970f));
                 EventsData.SetAnimation(Person.Patrick, Animations.idle);
                 
                 // Jerome   (sem dialogo)
                 EventsData.Teleport(Person.Jerome, new Vector3(-114.1999f,2.6097f,157.880f));
                 EventsData.SetAnimation(Person.Jerome,Animations.idle);

                } else {
                    //após a missão de recuperar pedra misteriosa com submarino:
                    EventsData.artefact_activate(true);
                    
                    //jerome conta pra doutora sobre artefato doc (analisys)
                    EventsData.Teleport(Person.Jerome, new Vector3(-48.630f,2.609f,-23.069f));
                    EventsData.SetAnimation(Person.Jerome,Animations.note);
                    
                    DataManager.canSleep = true;
                }
                break;
            
            case 3:
                DataManager.submarine = false;
                DataManager.canSleep = false;
                if (!DataManager.dream1complete) {
                    //todo: carregar cena do pesadelo 
                    
                    // eventos na cena do pesadelo
                    
                } else {
                    __d3videoEvent__();
                    
                    // dialogos refeitório maluco
                    EventsData.Teleport(Person.Antoine, new Vector3(-132.11f,2.609f,157.940f));
                    EventsData.SetAnimation(Person.Antoine, Animations.idle);
                    
                    EventsData.Teleport(Person.Patrick, new Vector3(-134.110f,2.609f,156.399f));
                    EventsData.SetAnimation(Person.Patrick, Animations.idle);
                    
                    EventsData.Teleport(Person.Gisele, new Vector3(-132.160f,2.609f,154.449f));
                    EventsData.SetAnimation(Person.Gisele, Animations.idle);
                    
                    //jerome no Research Lab, conversa da pesquisa
                    EventsData.Teleport(Person.Jerome, new Vector3(-48.2900009f,2.60975766f,-34.74017f));
                    EventsData.SetAnimation(Person.Jerome, Animations.idle);
                    
                    //dialogos capitão, se o player aceitar o prato chamar TODO: day3_CaptainGivesItem();
                    EventsData.Teleport(Person.Aaron, new Vector3(-81.19f, 2.6f, 325.79f));
                    EventsData.SetAnimation(Person.Aaron, Animations.insanity);

                    //demeter falando com chave no corredor, se o player aceitar o objetivo, chamar TODO: day3_DemeterGivesLocations(); 
                    EventsData.Teleport(Person.Demeter, new Vector3(16.700f,2.6097f,-10.369f));
                    EventsData.SetAnimation(Person.Demeter, Animations.fix);

                }
                break;

            
            case 4:
                trafficlock = false;
                DataManager.submarine = false;
                DataManager.canSleep = false;
                EventsData.Raining(true);
                 
                if (!DataManager.meat_collected) {
                    //Ivana esta no quarto do player pra falar que chegaram, chamar em qualquer dialogo: todo: day4_IvanaOnMorning()
                    EventsData.Teleport(Person.Ivana, new Vector3(53.930f,2.6097f,-31.1399f));
                }else {
                    // sem dialogo
                    EventsData.Teleport(Person.Ivana, Vector3.one*500); 
                    
                    
                    //dialogo do jerome (pesquisa 2)
                    EventsData.SetMeat(true);
                    EventsData.Teleport(Person.Jerome, new Vector3(-54.080f,2.6097f,-21.360f));
                    EventsData.SetAnimation(Person.Jerome,Animations.idle);
                    DataManager.canSleep = true;
                }
                
                //Patrick enxugando a chuva
                EventsData.Teleport(Person.Patrick,new Vector3(-163.649f,2.609f,142.029f) );
                EventsData.SetAnimation(Person.Patrick, Animations.mop);
                
                //Demeter pregos cozinha
                EventsData.Teleport(Person.Demeter, new Vector3(-152.979996f,2.60975766f,169.419998f));
                EventsData.Teleport(Person.Antoine, new Vector3(-156.619995f,2.60975766f,175f));
                EventsData.SetPregos(true);
                
                
                //Gisele desenhando na elétrica
                EventsData.Teleport(Person.Gisele, new Vector3(96.9899979f,2.60975766f,-31.3999996f));;
                EventsData.SetAnimation(Person.Gisele, Animations.idle);
                EventsData.SetRabiscos(true);
                break;
            
            case 5: 
                
                
                
                break;
        }
    }
    // ----------------- eventos dia 1

    public static void day1_IvanaArrives() {
        EventsData.Teleport(Person.Ivana, new Vector3(-105.98f,2.60f,-11.09f));
        // Todo criar target para navmesh de Ivana aqui 
    }

    // ----------------- eventos dia 2
    private static bool trafficlock;
    
    //dia 2 quando o fala com o demeter q aparece no começo no quarto.
    public static void day2_TalksToDemeter() {
        if (trafficlock) return;
        DataManager.submarine = true;
        RoomHandler.LoadRoom += __demetertp__;
        trafficlock = true;
    }
    
    // ----------------- eventos dia 3 todo: fazer sistema da quest da demeter, e testar inventario, alem de criar os itens
    public static void day3_CaptainGivesItem() {
        InventoryControl.i.AddItem(0); //plate
    }

    public static void day3_DemeterGivesLocations() {
        DataManager.canSleep = true;
        InventoryControl.i.AddItem(1); //screwdriver
        InventoryControl.i.AddItem(2); //locations
    }
    
    // ----------------- eventos dia 4
    public static void day4_IvanaOnMorning() {
        if(trafficlock) return;
        DataManager.submarine = true;
        trafficlock = true;
        RoomHandler.LoadRoom += __ivanadesapear__;
    }

    
    
    
    // ----------------- eventos gerais
    public static void PermitSubmarine(bool permission = true) {
        DataManager.submarine = permission;
    }

    private void __d3videoEvent__() {
        RoomHandler.LoadRoom += __videokitchen__;
    }

    public static void __videokitchen__() {
        if(DataManager.whoEnter == Person.Amelia && DataManager.whatRoom == Room.cozinha) {
            EventsData.d3_playvideo();
            RoomHandler.LoadRoom -= __videokitchen__;
        }
    }

    public static void __demetertp__() {
        EventsData.Teleport(Person.Demeter, new Vector3(-102.959f,2.609f,-8.35f));
        RoomHandler.LoadRoom -= __demetertp__;

    }

    /// <summary>
    /// dia 4, ivana sai do quarto apos player sair
    /// </summary>
    public static void __ivanadesapear__() {
        
        EventsData.Teleport(Person.Ivana, Vector3.one*500);
        RoomHandler.LoadRoom -= __ivanadesapear__;

    }
    
}
