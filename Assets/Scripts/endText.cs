using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endText : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gameover;
    public GameObject audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.Find("audioManager");
        audioSource.PlayOneShot(gameover);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void retryGame()
    {
        audiomanager.GetComponent<audioManager>().ChangeBGM(0);
        Destroy(GameObject.Find("NowDifficulty"));
        SceneManager.LoadScene("SelectDifficultyScene");
    }
}
