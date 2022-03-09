using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundParams", menuName = "Audio/SoundParams")]
public class SoundParams : ScriptableObject
{

    [Range(0, 1)]
    public float globalVolume;

    public AudioMixerGroup masterMixer;

    public AudioMixerGroup gameMixer;

    public AudioMixerGroup uiMixer;

    public AudioMixerGroup musicMixer;

}
