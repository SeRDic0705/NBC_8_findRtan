using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] Bgmlist;
    
    private void Start()
    {
        audioSource.clip = Bgmlist[0];
        audioSource.Play();
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeBGM(int num)
    {
        audioSource.clip = Bgmlist[num];
        audioSource.Play();
    }
}
