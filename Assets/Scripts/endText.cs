using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endText : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gameover;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(gameover);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
