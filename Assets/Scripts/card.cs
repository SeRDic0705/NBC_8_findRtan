using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;
    float flipTime;

    private Vector3 pos;

    private bool clickSecondCard; // check for close4sec

    // Start is called before the first frame update
    void Start()
    {
        //anim.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        // check for close4sec
        if (gameManager.I.firstCard != gameObject)
        {
            clickSecondCard = true;
        }

    }


    public void shuffle()
    {
        if (this.pos == null)
        {
            this.pos = this.transform.position;
            this.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            //this.transform.Translate(pos*2);
        }
    }

    public void openCard()
    {
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if (gameManager.I.firstCard == null)
        {
            gameManager.I.firstCard = gameObject;

            //처음 카드 선택한 시간
            flipTime = Time.time;
            Invoke("closeCard4sec", 4.0f);
        }
        else
        {
            gameManager.I.secondCard = gameObject;

            gameManager.I.isMatched();
        }
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255f / 69f, 170f / 255f, 169f / 255f);
        anim.SetBool("isOpen", false);
        transform.Find("front").gameObject.SetActive(false);
    }
    void closeCard4sec()
    {
        if (clickSecondCard == true)
        {
            clickSecondCard = false;
        }
        else
        {
            transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(255f / 69f, 170f / 255f, 169f / 255f);
            anim.SetBool("isOpen", false);
            transform.Find("front").gameObject.SetActive(false);
            gameManager.I.firstCard = null;
        }
    }
}
