using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private string currentBgm;

    [SerializeField]
    private AudioSource city;

    private List<GameObject> bgms;

    [SerializeField]
    private GameObject bgmGameObject;

    private List<GameObject> sfxs;

    [SerializeField]
    private GameObject sfxGameObject;

    private AudioSource currentBgmAudioSource;

    private float sfxVolume = 1;

    public string CurrentBgm { get => currentBgm; set => currentBgm = value; }
    public AudioSource CurrentBgmAudioSource { get => currentBgmAudioSource; set => currentBgmAudioSource = value; }
    public float SfxVolume { get => sfxVolume; set => sfxVolume = value; }



    // Start is called before the first frame update
    void Start()
    {
        bgms = new List<GameObject>();
        sfxs = new List<GameObject>();
        for(int i = 0; i < bgmGameObject.transform.childCount; i++) {
            bgms.Add(bgmGameObject.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < sfxGameObject.transform.childCount; i++)
        {
            sfxs.Add(sfxGameObject.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBgmMusic(string name)
    {
        //switch (name)
        //{
        //    case "city":
        //        city.Play();
        //        break;
        //}
        foreach(GameObject bgm in bgms)
        {
            if(bgm.name == name)
            {
                currentBgmAudioSource = bgm.GetComponent<AudioSource>();
                currentBgmAudioSource.Play();
               
            }
        }
    }

    public void StopBgmMusic(string name)
    {
        //switch (name)
        //{
        //    case "city":
        //        city.Stop();
        //        break;
        //}

        foreach (GameObject bgm in bgms)
        {
            if (bgm.name == name)
            {
                bgm.GetComponent<AudioSource>().Pause();
            }
        }
    }



    public void PlaySfxMusic(string name)
    {
        foreach (GameObject sfx in sfxs)
        {
            if (sfx.name == name)
            {
                Debug.Log("music");
                AudioSource sfxSource = sfx.GetComponent<AudioSource>();
                sfxSource.volume = sfxVolume;
                Debug.Log("music" + sfxVolume);
                sfxSource.Play();
            }
        }
    }

    public void StopSfxMusic(string name)
    {
        foreach (GameObject sfx in sfxs)
        {
            if (sfx.name == name)
            {
                sfx.GetComponent<AudioSource>().Pause();
            }
        }
    }

    public void changeBgmVolume(float volume)
    {
        currentBgmAudioSource.volume = volume;
    }

    public void changeSfxVolume(float volume)
    {
        sfxVolume = volume;
    }
}
