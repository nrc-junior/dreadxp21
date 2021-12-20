using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaysManager : MonoBehaviour {
    private int day; 
    private void Start() {
        day = DataManager.todayIsDay;

        switch (day) {
            case 1: 
                
                EventsData.Teleport(Person.Antoine, new Vector3(-149.52f,2.609f,176.83f));
                //exemplo setar dialogo... EventsData.SetDialog(Person.Antoine, PegarDialogoAntoine.dialogo_dia1);

                EventsData.Teleport(Person.Aaron, new Vector3(-81.19f, 2.6f, 325.79f));
                EventsData.SetAnimation(Person.Aaron, Animations.watch);
                //setar dialogo
                
                EventsData.Teleport(Person.Ivana, new Vector3(-81.12f, 2.60f, 328.95f));
                EventsData.SetAnimation(Person.Ivana, Animations.idle);
                //setar dialogo
                

                EventsData.Teleport(Person.Demeter, new Vector3(-99.05f,2.60f,-6.84f));
                EventsData.SetAnimation(Person.Demeter, Animations.fix);

                EventsData.Teleport(Person.Gisele, new Vector3(100.08f,2.609f,-32.75f));
                EventsData.SetAnimation(Person.Gisele, Animations.idle);

                EventsData.Teleport(Person.Patrick, new Vector3(-164.96f,2.60f,149.91f));

                // Todo Jerome acompanha, a pesquisadora, durante todo o primeiro dia
                break;
        }
    }

    public static void day1_IvanaArrives() {
        EventsData.Teleport(Person.Ivana, new Vector3(-105.98f,2.60f,-11.09f));
        // Todo criar target para navmesh de Ivana aqui 
    }
}
