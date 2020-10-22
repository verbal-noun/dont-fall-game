using UnityEngine;

[System.Serializable]
//Followed tutorial from https://www.youtube.com/watch?v=6OT43pvUyfY
public class Sound 
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
