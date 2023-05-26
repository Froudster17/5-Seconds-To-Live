using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private string currentMusicName; // Keep track of the currently playing music

    private static AudioManager instance;

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance != null)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }

        instance = this; // Set the instance
        DontDestroyOnLoad(gameObject); // Keep the AudioManager alive when loading new scenes

        // Create AudioSources for all sounds in the sounds array
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        Play("Music1"); // Play the initial music in the first level
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene requires a specific music track
        string sceneName = scene.name;

        if (sceneName != "Level1")
        {
            // Only play the music if it's different from the currently playing music
            if (currentMusicName != null)
            {
                Play(currentMusicName);
            }
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound != null)
        {
            sound.source.Play();
            currentMusicName = name;
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }
}
