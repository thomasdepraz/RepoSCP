using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public SoundList soundList;

    public static SoundManager instance;

    public SoundParams soundParams;

    public List<Sound> playingSounds;

    private AudioSource[] audioSourcesTab;

    public GameObject UISounds;

    public GameObject GameSounds;

    public GameObject Musics;

    public GameObject audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playingSounds = new List<Sound>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        /*audioSourcesTab = gameObject.GetComponents<AudioSource>();

        foreach (var source in audioSourcesTab)
        {
            if (source.isPlaying == false)
            {
                Destroy(source);
            }
        }*/
    }

    public void PlaySound(string name)
    {
        Sound tempSound;

        tempSound = soundList.FindSoundByName(name);

        switch (tempSound.type)
        {
            case Sound.soundType.UI:
                tempSound.source = Instantiate(audioSource, UISounds.transform).AddComponent<AudioSource>();
                break;
            case Sound.soundType.Game:
                tempSound.source = Instantiate(audioSource, GameSounds.transform).AddComponent<AudioSource>();
                break;
            case Sound.soundType.Music:
                tempSound.source = Instantiate(audioSource, Musics.transform).AddComponent<AudioSource>();
                break;
            case Sound.soundType.Master:
                break;
            default:
                break;
        }

        tempSound.AssignSound();

        StartCoroutine(Play(tempSound));
    }

    public IEnumerator Play(Sound sound)
    {
        playingSounds.Add(sound);

        sound.source.Play();

        while (sound.source.isPlaying)
        {
            yield return null;
        }

        Destroy(sound.source.gameObject);

        playingSounds.Remove(sound);
    }

    public void SetVolume(Sound.soundType type, float number)
    {
        switch (type)
        {
            case Sound.soundType.UI:
                soundParams.uiMixer.audioMixer.SetFloat("UIVolume", number);
                break;
            case Sound.soundType.Game:
                soundParams.uiMixer.audioMixer.SetFloat("GameVolume", number);
                break;
            case Sound.soundType.Music:
                soundParams.uiMixer.audioMixer.SetFloat("MusicVolume", number);
                break;
            case Sound.soundType.Master:
                soundParams.uiMixer.audioMixer.SetFloat("MasterVolume", number);
                break;
            default:
                break;
        }
    }



    public void TestSound()
    {
        PlaySound("UIMenuButton");
    }

    public void TestMusic()
    {
        PlaySound("GameMusic");
    }
}



