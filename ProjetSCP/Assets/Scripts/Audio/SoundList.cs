using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "Audio/SoundList")]
public class SoundList : ScriptableObject
{

    public Sound[] uiSounds;

    public Sound[] gameSounds;

    public Sound[] gameMusics;


    public Sound FindSoundByName(string name)
    {
        Sound tempSound = new Sound();

        foreach (var sound in uiSounds)
        {
            if (sound.name == name)
            {
                tempSound = sound;

                tempSound.type = Sound.soundType.UI;

                return tempSound;
            }
        }

        foreach (var sound in gameSounds)
        {
            if (sound.name == name)
            {
                tempSound = sound;

                tempSound.type = Sound.soundType.Game;

                return tempSound;
            }
        }

        foreach (var sound in gameMusics)
        {
            if (sound.name == name)
            {
                tempSound = sound;

                tempSound.type = Sound.soundType.Music;

                return tempSound;
            }
        }

        return null;
    }



}
