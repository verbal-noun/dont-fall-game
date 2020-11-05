    using System;
using UnityEngine;

//Followed tutorial from https://www.youtube.com/watch?v=6OT43pvUyfY
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.maxVolume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
           // Debug.Log(s.source.volume);
        }
    }

    public Sound[] getSounds(){
        return AudioManager.instance.sounds;
    }

    public void SetVolume(float vol){
        foreach(Sound s in getSounds()){
            s.source.volume = s.maxVolume * vol;
        }
    }
    void Start(){
        Play("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(getSounds(), sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound " + name + "not found");
            return;
        }
        s.source.Play();
    }

    public void SetThemeMusic(bool b){
        Sound s = Array.Find(getSounds(), sound => sound.name == "Theme");
        if (s == null) return;

        s.source.mute = !b;
    }

    public void SetSFX(bool b){
        foreach (Sound s in getSounds()){
            if (!s.name.Equals("Theme")){
                if (s.source == null){
                    Debug.Log("NULL");
                }
                s.source.mute = !b;
            }
        }
    }

    public void Pause(string name){
        Sound s = Array.Find(getSounds(), sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound " + name + "not found");
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name){
        Sound s = Array.Find(getSounds(), sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound " + name + "not found");
            return;
        }
        s.source.Stop();
    }

    public void PlayJump(float power){
        String name;
        
        Sound jc = Array.Find(getSounds(), sound => sound.name.Equals("JumpCharge"));

        if (jc == null){
            Debug.Log("Cannot find jumpcharge");
            return;
        }
        if (jc.source.isPlaying){
            jc.source.Stop();
        }
        
        if (power <= 0.75){
            name = "JumpVoice1";
        }
        else{
            name = "JumpVoice2";
        }
        Sound s = Array.Find(getSounds(), sound => sound.name.Equals(name));
            if (s == null) return;
        Sound s1 = Array.Find(getSounds(), sound => sound.name.Equals("Jump"));
            if (s1 == null) return;
        s.source.PlayOneShot(s.clip, s.volume);
        s1.source.PlayOneShot(s1.clip, s1.volume);
        
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(getSounds(), sound => sound.name.Equals(name));
        if (s == null || s.source == null) {
            Debug.Log("Sound " + name + "not found");
            return;
        }
        s.source.PlayOneShot(s.clip, s.volume);
    }
}
