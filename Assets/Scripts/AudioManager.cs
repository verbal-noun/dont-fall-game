    using System;
using UnityEngine;

//Followed tutorial from https://www.youtube.com/watch?v=6OT43pvUyfY
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update

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
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start(){
        Play("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound " + name + "not found");
            return;
        }
        s.source.Play();
    }

    public void PlayJump(float power){
        String name;
        
        Sound jc = Array.Find(sounds, sound => sound.name.Equals("JumpCharge"));

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
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
            if (s == null) return;
        Sound s1 = Array.Find(sounds, sound => sound.name.Equals("Jump"));
            if (s1 == null) return;
        s.source.PlayOneShot(s.clip, s.volume);
        s1.source.PlayOneShot(s1.clip, s1.volume);
        
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        if (s == null || s.source == null) {
            Debug.Log("Sound " + name + "not found");
            return;
        }
        s.source.PlayOneShot(s.clip, s.volume);
    }
}
