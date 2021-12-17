using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssTrack : MonoBehaviour {
    private SoundManager.Track theme;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Submarine") || other.CompareTag("Player")) {
            theme = SoundManager.MakeTrack(SoundManager.Sound.AbyssMusic,.3f);
        };
    }
}
