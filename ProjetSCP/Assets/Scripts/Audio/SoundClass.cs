using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0,1)]
    public float volume;

    public bool loop;

    public enum soundType { UI, Game, Music, Master };

    public soundType type;

    public AudioSource source;

    public void AssignSound()
    {
        source.loop = loop;

        source.clip = clip;

        source.volume = volume * SoundManager.instance.soundParams.globalVolume;

        switch (type)
        {
            case soundType.UI:
                source.outputAudioMixerGroup = SoundManager.instance.soundParams.uiMixer;
                break;
            case soundType.Game:
                source.outputAudioMixerGroup = SoundManager.instance.soundParams.gameMixer;
                break;
            case soundType.Music:
                source.outputAudioMixerGroup = SoundManager.instance.soundParams.musicMixer;
                break;
            default:
                break;
        }

        source.playOnAwake = false;
    }
}




