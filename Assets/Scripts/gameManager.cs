using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Text timeText;
    float time = 60.0f;

    public GameObject endText;
    public GameObject card;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip wrong;
    public AudioClip shuffle;
    public GameObject NameText;
    public GameObject FailText;


    void Awake()
    {
        I = this;
    }
    void ShowFail()
    {
        FailText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(shuffle);

        int[] images = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14 };

        images = images.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for ( int i = 0; i < 30; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;      

            float x = (i / 6) * 1.3f - 2.6f;
            float y = (i % 6) * 1.3f - 3.6f;
            newCard.transform.position = new Vector3(x, y, 0);

            string imageName = "image" + images[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imageName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString("N2");

        if (time < 0.0f)
        {
            endText.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            NameText.SetActive(true);

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                endText.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(wrong);
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
            FailText.SetActive(true);
            Invoke("ShowFail", 0.6f);
        }

        firstCard = null;
        secondCard = null;
    }

}
