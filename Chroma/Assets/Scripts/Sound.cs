using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 1f)]
    public float volumeVariance = 0f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = 0f;
    [Range(0, 360)]
    public int spread = 0;

    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float spatialBlend;

    public GameObject gameObject;

    [HideInInspector]
    public AudioSource source;

    //[HideInInspector]
    //public AudioReverbFilter reverb; // pitch shift, delay etc

    public bool loop;
}
