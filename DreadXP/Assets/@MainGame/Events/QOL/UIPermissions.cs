using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIPermissions : MonoBehaviour{
    public Image sub;
    public Image sleep;
    public Image overall;

    const float g = 173/255;
    private Color disabled = new Color(g,g,g,73/255);
    private Color active = new Color(0,1,0,1);
    private Color inactivity = new Color(1,0,0,1);

    public void Update() {
        
    }
}
