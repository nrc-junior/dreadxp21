using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DaysManager : MonoBehaviour {
    private static SceneManager sm;
    private int day; 
    private void Start() {
        day = DataManager.todayIsDay;
        EventsData.artefact_activate(day > 2 || DataManager.artefact_collected );
        if(day >= 4 ) EventsData.SetRabiscos(true);
        if(day > 5 || DataManager.dream2complete) EventsData.EnableBloodSymbols();
        if(day > 5) EventsData.JeromeDead(true);
        
        // RoomHandler.LoadRoom += __videokitchen__; // testes
        
        switch (day) {
            case 1:
                DataManager.submarine = false;
                DataManager.canSleep = false;
                if (!DataManager.keys_collected) {
                    
                    EventsData.Teleport(Person.Antoine, new Vector3(-149.52f,2.609f,176.83f));
                    //EventsData.SetDialog(Person.Antoine, Person.Antoine.dialogues[0]);
                    //exemplo setar dialogo... EventsData.SetDialog(Person.Antoine, PegarDialogoAntoine.dialogo_dia1);

                    //EventsData.SetDialog(Person.Aaron,0);
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

                    //deixar jerome no refeitório ? no roteiro era pra ele seguir o player mas vamos ver se dá.
                    EventsData.SetDialog(Person.Jerome,0);
                    EventsData.Teleport(Person.Jerome, new Vector3(-114.1999f,2.6097f,157.880f));
                    EventsData.SetAnimation(Person.Jerome,Animations.coffe);
                    
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
                 EventsData.SetAnimation(Person.Jerome,Animations.coffe);

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

                DataManager.dream1complete = true;          //todo: remover isso depois <<<<<<<<<<<<<<<<<<<<<<<<
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
                trafficlock = false;
                DataManager.submarine = false;
                DataManager.canSleep = false;
                EventsData.Raining(true);
                
                if (!DataManager.dream2complete) {
                    //evento do pesadelo
                    EventsData.Inundation(true);
                    RoomHandler.LoadRoom += __entergarage__;

                } else EventsData.EnableBloodSymbols();
                
                //evento patrick limpando corpo do jerome
                EventsData.JeromeDead(true);
                EventsData.Teleport(Person.Patrick, new Vector3(-60.0800018f,2.60975766f,-35.069999f));
                EventsData.SetAnimation(Person.Patrick, Animations.mop);
                

                //dialogo ivana e demeter oficina
                EventsData.Teleport(Person.Ivana, new Vector3(38.930f,2.609f,1.37f));
                EventsData.Teleport(Person.Demeter, new Vector3(40.40f,2.609f,3.970f));
                EventsData.SetAnimation(Person.Demeter,Animations.idle);
                break;
            
            
            case 6:
                trafficlock = false;
                DataManager.submarine = false;
                DataManager.canSleep = false;
                EventsData.Raining(true);
                EventsData.LeaveTrigger(false);

                
                EventsData.Teleport(Person.Demeter, new Vector3(-89,2.6097f,-2f));
                EventsData.SetAnimation(Person.Demeter,Animations.idle);
                    
                
                // 2 coisas, chamar em qualquer momento do dialogo -> TODO: day6_TalksToIvana()
                // e no final do dialogo caso o player escolha fugir com o navio -> TODO: day6_PlayerAccepts
                // e no final do dialogo caso o player escolha fugir com o submarino -> TODO: day6_PlayerRefuse
                EventsData.Teleport(Person.Ivana, new Vector3(52.7000008f,2.60975766f,-32.1300011f));
                
                // fazer todas as luzes apagar
                // mecanica de combate
                // mecanica da lanterna
                
                
                break;
        }
    }
    // ----------------- eventos dia 1
    private static bool trafficlock;

    public static void day1_IvanaArrives() {
        EventsData.Teleport(Person.Ivana, new Vector3(-105.98f,2.60f,-11.09f));
        DataManager.submarine = true;
        // Todo criar target para navmesh de Ivana aqui 
    }

    // ----------------- eventos dia 2
    
    //dia 2 quando o fala com o demeter q aparece no começo no quarto.
    public static void day2_TalksToDemeter() {
        if (trafficlock) return;
        DataManager.submarine = true;
        RoomHandler.LoadRoom += __demetertp__;
        trafficlock = true;
    }
    
    // ----------------- eventos dia 3 todo: fazer sistema da quest da demeter, e testar inventario, alem de criar os itens
    public static void day3_CaptainGivesItem() {
        EventsData.GiveItem(1); //plate
    }

    public static void day3_DemeterGivesLocations() {
        DataManager.canSleep = true;
        EventsData.GiveItem(2); //screwdriver
        EventsData.GiveItem(3); //locations
    }
    
    // ----------------- eventos dia 4
    public static void day4_IvanaOnMorning() {
        if(trafficlock) return;
        DataManager.submarine = true;
        trafficlock = true;
        RoomHandler.LoadRoom += __ivanadesapear__;
    }
    // ----------------- s/ eventos para chamar no dia 5
    
    // ----------------- eventos dia 6
    public static void day6_TalksToIvana() {
        if (trafficlock) return;
        RoomHandler.LoadRoom += __ivanadesapear__;
        EventsData.LeaveTrigger(true);
        trafficlock = true;
    }


    public static void day6_PlayerAccepts() {
        //todo: colocar dialogo "Saindo do navio" da Demeter aqui
        
        InventoryControl.i.AddItem(5);
    
    }    
    
    public static void day6_PlayerRefuse() {
        //todo: colocar dialogo "Final Submarino" da Demeter aqui

        DataManager.submarine = true;

    }

    
    
    
    // ----------------- eventos gerais

    public static void __entergarage__()
    {
        if (DataManager.whatRoom == Room.garagem && DataManager.whoEnter == Person.Amelia)
        {
            RoomHandler.LoadRoom -= __entergarage__;
            DataManager.dream2complete = true;

            //todo: carregar cena do sonho.
            print("vc terminou o sonho");
            SceneManager.LoadScene(0);
        }
    }

    public static void PermitSubmarine(bool permission = true) {
        DataManager.submarine = permission;
    }

    private void __d3videoEvent__() {
        RoomHandler.LoadRoom += __videokitchen__;
    }

    public static void __videokitchen__() {
        if(DataManager.whoEnter == Person.Amelia && DataManager.whatRoom == Room.refeitorio) {
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
        
        EventsData.Teleport(Person.Ivana, new Vector3(-81.12f, 2.60f, 328.95f));
        RoomHandler.LoadRoom -= __ivanadesapear__;

    }
    
    
}
