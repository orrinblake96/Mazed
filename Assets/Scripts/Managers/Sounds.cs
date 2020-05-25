using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    [System.Serializable]
    public class Sounds
    {
        // Class is used by Audiomanager to give greater control over audio 
        
        public String name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;

        [HideInInspector] 
        public AudioSource source;
    }
}