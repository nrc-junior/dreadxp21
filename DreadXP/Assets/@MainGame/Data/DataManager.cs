using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
   public static Person whoEnter = Person.undefined;
   public static Room whatRoom = Room.undefined;
   
   public static int todayIsDay = 1;
   
   public static bool canSleep = false;
   public static bool submarine = false;

   public static Room playerIsIn = Room.undefined;

   public static bool dream1complete = false;

   public static bool meat_collected;
   public static bool keys_collected;
   public static bool artefact_collected;

}
