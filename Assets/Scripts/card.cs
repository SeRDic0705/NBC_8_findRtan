using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;
    float flipTime;

    private Button btn;

    private Vector3 pos;

    private bool isSelf = false;

    // Start is called before the first frame update
    void Start()
    {
        btn = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSelf){
            if(gameManager.instance.isLock) {
            btn.interactable = false;
        }else btn.interactable = true;
        }
    }


    public void shuffle() {
        if(this.pos == null) {
            this.pos = this.transform.position;
            this.transform.position = new Vector3(0, 0, 0);
        }
        else{
            this.transform.Translate(pos*2);
        }
    }

    public void openCard()
    {
        isSelf = true;
        btn.interactable = false;
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        
        if (gameManager.instance.firstCard == null)
        {
            gameManager.instance.firstCard = gameObject;
            //처음 카드 선택한 시간
            flipTime = Time.time;
            Invoke("closeCardInvoke", 5.0f);
        }
        else
        {
            gameManager.instance.isLock = true;
            gameManager.instance.secondCard = gameObject;
            gameManager.instance.isMatched();
        }
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
        gameManager.instance.isLock = false;
        gameManager.instance.firstCard = null;
        gameManager.instance.secondCard = null;
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
        isSelf = false;
        gameManager.instance.isLock = false;
        gameManager.instance.firstCard = null;
        gameManager.instance.secondCard = null;
    }
}
