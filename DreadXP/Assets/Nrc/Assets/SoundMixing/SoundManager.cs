using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{
    public enum  Sound {
        inventory_change,
        inventory_hide,
        hit,
        softhit,
        welding,
        propeller_fast,
        leakpressure,
        underwater,
        mask,
        hiss,
        AbyssMusic
        
    }

    private static float default_volume = 1;
    private static Dictionary<Sound, float> soundClock;
    //private static Dictionary<Sound, Track> playingMix;

    public static void Initialize() {
        soundClock = new Dictionary<Sound, float>();
        //playingMix = new Dictionary<Sound, Track>();
        soundClock[Sound.hiss] = 0;

    }

      
    public class Track {
        public  AudioSource src;
        public bool adjusted = false;
        public Track(AudioSource src) {
            this.src = src;
        }

        public IEnumerator fade(float target = 0,float timefactor = 1) {
            adjusted = true;
            
            float t = 0;
            float start = src.volume;
            
            if (start == target) yield return false;
            
            while (t < 1) {
                t +=  Time.deltaTime * timefactor;
                src.volume = Mathf.Lerp(start, target, t);
            }

            yield return true;
        }

        public void Remove(float time = 0) {
            GameObject.Destroy(src.gameObject, time);
        }



    }

    public static Track MakeTrack(Sound sound, Transform fat = default,  bool looping = false, float dst = 1){
        return MakeTrack(sound, default_volume,fat, looping, dst);
    }

    public static Track MakeTrack(Sound sound, float vol, Transform fat = default, bool looping = false, float dst = 1){
        GameObject soundGo = new GameObject("Sound");
        AudioSource audioSource = soundGo.AddComponent<AudioSource>();
        audioSource.volume = vol;
        audioSource.loop = looping;
        audioSource.clip = GetAudioClip(sound);

        if (fat != default) {
            soundGo.transform.position = fat.position;
            soundGo.transform.parent = fat;
            
            audioSource.spatialBlend = 1;
            audioSource.minDistance = dst;
        }

        audioSource.Play();
        return new Track(audioSource); 
        
    }

    

    public static void PlaySound(Sound sound,  float vol = -1, Vector3 pos = default, float dst = 1) {
        if (!canPlay(sound)) return;
        if (vol == -1) vol = default_volume;

        GameObject soundGo = new GameObject("Sound");
        AudioSource audioSource = soundGo.AddComponent<AudioSource>();
        audioSource.volume = vol;
        
        AudioClip clip = GetAudioClip(sound);
        if (pos != default) {
            soundGo.transform.position = pos;
            audioSource.spatialBlend = 1;
            audioSource.minDistance = dst;
            audioSource.clip = clip;
            audioSource.Play();
        }else {
            audioSource.PlayOneShot(clip);
        }
        
        GameObject.Destroy(soundGo, clip.length);
    }

    static bool canPlay(Sound sound) {
        switch (sound){
            default: return true;
            case Sound.hiss:
                if (soundClock.ContainsKey(sound)) {
                    float lastPlayed = soundClock[sound];
                    float delay = 1;
                    
                    if (lastPlayed + delay < Time.time) {
                        soundClock[sound] = Time.time;
                        return true;
                    } else {
                        return false;
                    } 
                } else {
                    return false;
                }
        }
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
