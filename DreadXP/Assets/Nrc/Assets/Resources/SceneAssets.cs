using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAssets : MonoBehaviour {

    private static SceneAssets _i;
    public static SceneAssets i {
        get {
            if (_i == null)
                _i = Instantiate(Resources.Load<SceneAssets>("SceneAssets"));
            return _i;
        }
    }
   
    
    
    //audios
    [System.Serializable]
    public class SoundAudioClip {
        public AudioClip[] audioClip = new AudioClip[1];
        public SoundManager.Sound sound;

    }
    
    public SoundAudioClip[] soundClips;
    //------
}
