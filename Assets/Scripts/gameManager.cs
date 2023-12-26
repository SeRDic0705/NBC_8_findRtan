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
    public Text matchCnt;
    public Text scoreTxt;
    public Text remainTimeTxt;
    public GameObject endCanvas;
    public GameObject endText;
    public GameObject card;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip wrong;
    public AudioClip shuffle;
    public Text nameText;
    public GameObject NTxt;
    public GameObject FailText;
    public Sprite[] sprites;
    public GameObject NowDifficulty;
    public audioManager audiomanager;

    int mCnt = 0;
    private bool isPlay;

    void Awake()
    {
        I = this;
    }
    void Textfalse()
    {
        FailText.SetActive(false);
        NTxt.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlay = false;
        audiomanager = GameObject.Find("audioManager").GetComponent<audioManager>();
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(shuffle);

        // ���̵��� ���� �̹��� ���� ����
        int[] images;
        NowDifficulty = GameObject.Find("NowDifficulty");
        string difficulty = NowDifficulty.GetComponent<nowDifficulty>().difficulty;

        if (difficulty == "Easy")
        {
            images = new int[10]; // Easy ���̵��� 10�� �̹���
            for (int i = 0; i < images.Length / 2; i++)
            {
                images[2 * i] = i;
                images[2 * i + 1] = i;
            }
        }
        else if (difficulty == "Normal")
        {
            images = new int[20]; // Normal ���̵��� 20�� �̹���
            for (int i = 0; i < images.Length / 2; i++)
            {
                images[2 * i] = i;
                images[2 * i + 1] = i;
            }
        }
        else // Hard �Ǵ� ��Ÿ
        {
            images = new int[30]; // Hard ���̵��� 30�� �̹���
            for (int i = 0; i < images.Length / 2; i++)
            {
                images[2 * i] = i;
                images[2 * i + 1] = i;
            }
        }

        // ī�带 ���� ���� �ڵ�
        //images = images.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        // ī�带 ���� ���ο� �ڵ�
        for (int i = 0; i < images.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, images.Length);
            int temp = images[i];
            images[i] = images[randomIndex];
            images[randomIndex] = temp;
        }


        // ���̵����� �ٸ� ī�� ��ġ
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

                var spriteName = sprites[images[i]].name;
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);
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

                var spriteName = sprites[images[i]].name;
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);
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

                var spriteName = sprites[images[i]].name;
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString("N2");

        if (!isPlay && time < 20.0f)
        {
            isPlay= true;
            audiomanager.ChangeBGM(4);
            timeText.color = Color.red;
        }

        if (time < 0.0f)
        {
            remainTimeTxt.text = time.ToString("N2");
            matchCnt.text = mCnt.ToString();
            scoreTxt.text = (50 - mCnt).ToString();
            endCanvas.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void isMatched()
    {
        mCnt += 1;
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            NTxt.SetActive(true);
            if (int.Parse(firstCardImage.Substring(5)) >= 13)
            {
                nameText.text = "이승배";
            }
            else if (int.Parse(firstCardImage.Substring(5)) >= 10)
            {
                nameText.text = "이도현";
            }
            else if (int.Parse(firstCardImage.Substring(5)) >= 7)
            {
                nameText.text = "박소이";
            }
            else if (int.Parse(firstCardImage.Substring(5)) >= 4)
            {
                nameText.text = "김준하";
            }
            else if (int.Parse(firstCardImage.Substring(5)) >= 1)
            {
                nameText.text = "김성우";
            }
            Invoke("Textfalse", 0.8f);
            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                remainTimeTxt.text = time.ToString("N2");
                matchCnt.text = mCnt.ToString();
                scoreTxt.text = (50 - mCnt + Math.Round(time)).ToString();
                endCanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(wrong);
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
            FailText.SetActive(true);
            Invoke("Textfalse", 0.8f);
        }

        firstCard = null;
        secondCard = null;
    }

}
