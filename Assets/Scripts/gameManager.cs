using System;
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

    public GameObject NowDifficulty;

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(shuffle);

        // Adjusting the Number of Cards According to Difficulty
        int[] images;
        NowDifficulty = GameObject.Find("NowDifficulty");
        string difficulty = NowDifficulty.GetComponent<nowDifficulty>().difficulty;

        // Easy : 10 Cards(5 Types)
        if (difficulty == "Easy")
        {
            images = new int[10];
            for (int i = 0; i < images.Length / 2; i++)
            {
                int randomValue = UnityEngine.Random.Range(0, 3);
                images[2 * i] = i + randomValue;
                images[2 * i + 1] = i + randomValue;
            }
        }
        // Normal : 20 Cards(10 Types)
        else if (difficulty == "Normal")
        {
            images = new int[20]; 
            
            int value = 0; 

            for (int i = 0; i < images.Length / 2; i++)
            {
                images[2 * i] = value;
                images[2 * i + 1] = value;

                if (value == 1 || value == 4 || value == 7 || value == 10 || value == 13)
                {
                    value += 2;
                }
                else
                {
                    value += 1;
                }
            }
        }
        // Hard : 30 Cards(15 Types)
        else
        {
            images = new int[30];
            for (int i = 0; i < images.Length / 2; i++)
            {
                images[2 * i] = i;
                images[2 * i + 1] = i;
            }
        }

        // shuffling cards (Before)
        //images = images.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        // shuffling cards (After)
        for (int i = 0; i < images.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, images.Length);
            int temp = images[i];
            images[i] = images[randomIndex];
            images[randomIndex] = temp;
        }

        // Set Card Position & Get sprite : EASY
        if (difficulty == "Easy")
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("cards").transform;

                float x;
                float y;

                x = (i % 3) * 1.3f - 1.3f;
                y = (i / 3) * 1.3f - 2.3f;
                if (i >= 3 && i <= 6)
                {
                    x = ((i + 1) % 4) * 1.3f - 1.3f - 0.65f;
                    y = ((i + 1) / 4) * 1.3f - 2.3f;
                }
                else if (i > 6)
                {
                    x = ((i - 1) % 3) * 1.3f - 1.3f;
                    y = ((i - 1) / 3) * 1.3f - 2.3f;
                }
                newCard.transform.position = new Vector3(x, y, 0);

                string imageName = "image" + images[i].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imageName);
            }
        }
        // Set Card Position & Get sprite : Normal
        else if (difficulty == "Normal")
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("cards").transform;

                float x = (i / 5) * 1.3f - 2.6f + 0.65f;
                float y = (i % 5) * 1.3f - 3.6f;
                newCard.transform.position = new Vector3(x, y, 0);

                string imageName = "image" + images[i].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imageName);
            }

        }
        // Set Card Position & Get sprite : Hard
        else
        {
            for (int i = 0; i < 30; i++)
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
        }

        firstCard = null;
        secondCard = null;
    }

}
