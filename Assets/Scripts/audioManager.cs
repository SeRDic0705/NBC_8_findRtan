using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgmusic;
    
    private void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeBGM(int num)
    {
        if (num == 1) 
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("StageBGM/StageEasy");
            audioSource.Play();
        }
        else if (num == 2)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("StageBGM/StageNormal");
            audioSource.Play();
        }
        else if (num == 3)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("StageBGM/StageHard");
            audioSource.Play();
        }
        else if (num == 4)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("StageBGM/Emergency");
            audioSource.Play();
        }
        else if (num == 5)
        {
            gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("StageBGM/Menu");
            audioSource.Play();
        }
    }
}
