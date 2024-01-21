using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    // only one instance of this class
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // Instance should not be defined before this runs
        if (Instance != null)
        {
            Debug.LogError("More than one audio manager in scene");
        }

        Instance = this;
    }

    // play the provided sound once
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        // RuntimeManager is part of FMOD
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

}
