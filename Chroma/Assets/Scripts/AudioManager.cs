using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // current instance of audio manager, to stop duplicates adding in new scenes
    // need to add audiomanager prefab to each scene
    public static AudioManager instance;
    public AudioMixer mixer;
    //public AudioMixerGroup fire;
    //public AudioMixerGroup mixerGroup;

    private AudioMixerSnapshot startSnapshot;
    private AudioMixerSnapshot musicSnapshot;
    private AudioMixerSnapshot masterSnapshot;

    private enum AudioGroups { Music, SFX };

    [HideInInspector]
    public bool drawing_playing = false;
    [HideInInspector]
    public bool flame1 = false;
    [HideInInspector]
    public bool flame2 = false;
    [HideInInspector]
    public bool flame3 = false;

    private bool outsideMusic = false;
    private bool insideMusic = false;
    private bool fire_crackle = false;
    private bool firefly = false;

    private bool draw = false;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Audio Manager opened");

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // make persist between scenes so music does not restart when scene changes

        foreach (Sound s in sounds)
        {
            //GameObject g = s.gameObject;

            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            //s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spread = s.spread;
            s.source.spatialBlend = s.spatialBlend;
            s.source.outputAudioMixerGroup = s.mixerGroup; // mixerGroup
        }
        // Play("inside_music");
        Debug.Log("HERE MUSIC");
        Sound sx = Array.Find(sounds, sound => sound.name == "checkpoint");
        sx.source.volume = 6f;
        //sx.source.spread = 360;
        //sx.source.pitch = 1.5f;
        //fire.audioMixer.SetFloat("pitchBend", 1f / 1.5f);
        //sx.source.Play();
        Music();
        //SFX();
    }

    private void Start()
    {
        //startSnapshot = mixer.FindSnapshot("Start");
        //musicSnapshot = mixer.FindSnapshot("Music");
        masterSnapshot = mixer.FindSnapshot("Master");

        //Play("draw_sound");
        // Play("death_sound");
        // Play("ball_in_well");
        // Play("ball_shoot");
        // Play("pool_crash");
        // Play("delete_drawing");
        // Play("flame_lit");
        // Play("pool_spawned");
        // Play("checkpoint");

    }

    private void Update()
    {
        Vector3 posDiff;
        Sound drawSound = Array.Find(sounds, sound => sound.name == "draw_sound");
        if (draw)
        {
            posDiff = DrawingPitch.curr - DrawingPitch.prev;
            if (posDiff.y > 0)
            {
                drawSound.source.pitch += 0.1f;
            }
            else if (posDiff.y < 0)
            {
                drawSound.source.pitch -= 0.1f;
            }

        }
    }

    public void Music()
    {
        Debug.Log("Music function");
        string current = SceneManager.GetActiveScene().name;

        if (current == "outside")
        {
            if (insideMusic)
            {
                Stop("inside_music");
                insideMusic = false;
            }
            if (!outsideMusic)
            {
                Play("outside_music");
                outsideMusic = true;
            }
        }
        else
        {
            Debug.Log("In else");
            if (outsideMusic)
            {
                Stop("outside_music");
                outsideMusic = false;
            }
            if (!insideMusic)
            {
                Debug.Log("In IF");
                Play("inside_music");
                insideMusic = true;
            }
        }
    }

    public void SFX()
    {
        string current = SceneManager.GetActiveScene().name;
        if (current == "outisde")
        {
            if (fire_crackle)
            {
                Stop("fire_crackle");
                fire_crackle = false;
            }
            if (!firefly)
            {
                Play("firefly");
                firefly = true;
            }
        }
        else
        {
            if (firefly)
            {
                Stop("firefly");
                firefly = false;
            }
            if (!fire_crackle)
            {
                Play("firefly");
                Play("fire_crackle");
                fire_crackle = true;
            }

        }
    }

    public void Play(string name)
    {
        Debug.Log("PLAY  " + name);
        // to play sound from another script
        // FindObjectOfType<AudioManager>().Play("name");
        if (name == "draw_sound")
        {
            draw = true;
        }

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        if (name == "arrow_collision" || name == "death_sound")
        {
            s.source.PlayOneShot(s.source.clip);
        }
        else
        {
            s.source.Play();
        }

    }

    public void ChangeVolume(string name, int value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.volume += value;
    }

    public void ChangePitch(string name, int value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.pitch += value;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (name == "draw_sound")
        {
            draw = false;
            s.source.pitch = 1f;
        }

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.UnPause();
    }

    public void SetExposedParam(string name, float value)
    {
        mixer.SetFloat(name, value);
    }

    public void ResetExposedParam(string name)
    {
        mixer.ClearFloat(name);
    }

    public void SnapshotStart()
    {
        startSnapshot.TransitionTo(.5f);
    }

    public void SnapshotMusic()
    {
        musicSnapshot.TransitionTo(.5f);
    }

    public void SnapshotTransition(string name, float value)
    {
        AudioMixerSnapshot s = mixer.FindSnapshot(name);
        if (s == null)
        {
            Debug.LogWarning("Snapshot: " + name + " not found");
            return;
        }
        s.TransitionTo(value);
    }
}

