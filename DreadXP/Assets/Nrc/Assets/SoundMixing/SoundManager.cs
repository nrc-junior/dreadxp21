using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{
    public enum  Sound {
        inventory_change,
        inventory_hide,
        hit,
        softhit,
    }
    public static void PlaySound(Sound sound) {
        GameObject soundGo = new GameObject("Sound");
        AudioSource audioSource = soundGo.AddComponent<AudioSource>();
        
        AudioClip clip = GetAudioClip(sound);
        
        audioSource.PlayOneShot(clip);
        GameObject.Destroy(soundGo, clip.length);
    }
    
    static AudioClip GetAudioClip(Sound sound){
        foreach (SceneAssets.SoundAudioClip clip in SceneAssets.i.soundClips) {
            if (clip.sound == sound) {
                return clip.audioClip.Length > 1 ? clip.audioClip[Random.Range(0,clip.audioClip.Length)] : clip.audioClip[0];
            }
        }
        
        Debug.Log("Not Founded Enum sound");
        return null;
    }
    
}
