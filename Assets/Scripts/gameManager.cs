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

        // 난이도에 따른 이미지 갯수 조정
        int[] images;
        NowDifficulty = GameObject.Find("NowDifficulty");
        string difficulty = NowDifficulty.GetComponent<nowDifficulty>().difficulty;

        if (difficulty == "Easy")
        {
            images = new int[10]; // Easy 난이도는 10개 이미지
            for (int i = 0; i < images.Length / 2; i++)
            {
                // 인당 준비된 세 종류 카드 중 랜덤하게 하나씩 선택
                int randomValue = UnityEngine.Random.Range(0, 3);
                images[2 * i] = i + randomValue;
                images[2 * i + 1] = i + randomValue;
            }
        }
        else if (difficulty == "Normal")
        {
            images = new int[20]; // Normal 난이도는 20개 이미지
            
            // 인당 준비된 세 종류 카드 중 앞 두개 카드 선택.(랜덤하게 선택하기엔 아이디어부족+코드 길어짐)
            int value = 0; // 실제 images 배열에 넣을 값

            for (int i = 0; i < images.Length / 2; i++)
            {
                // 배열에 값을 할당
                images[2 * i] = value;
                images[2 * i + 1] = value;

                // 다음 값으로 증가시킴. 2, 5, 8, 11, 14는 건너뛰기
                if (value == 1 || value == 4 || value == 7 || value == 10 || value == 13)
                {
                    value += 2; // 2, 5, 8, 11, 14를 건너뛰기 위해 2 증가
                }
                else
                {
                    value += 1; // 그 외에는 1 증가
                }
            }
        }
        else // Hard 또는 기타
        {
            images = new int[30]; // Hard 난이도는 30개 이미지
            for (int i = 0; i < images.Length / 2; i++)
            {
                images[2 * i] = i;
                images[2 * i + 1] = i;
            }
        }

        // 카드를 섞는 원래 코드
        //images = images.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        // 카드를 섞는 새로운 코드
        for (int i = 0; i < images.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, images.Length);
            int temp = images[i];
            images[i] = images[randomIndex];
            images[randomIndex] = temp;
        }


        // 난이도별로 다른 카드 배치
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
