using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selectDifficulty : MonoBehaviour
{
    public Button EasyBtn;
    public Text EasyScoreText;
    public Button NormalBtn;
    public Text NormalScoreText;
    public Button HardBtn;
    public Text HardScoreText;
    public Image EasyScoreImg;
    public Image NormalScoreImg;
    public Image HardScoreImg;

    public float canNormal;
    public float canHard;

    float easyScore;
    float normalScore;
    float hardScore;

    public GameObject NowDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        // 각 난이도별 점수 불러오기
        easyScore = PlayerPrefs.GetFloat("EasyScore", 0f);
        normalScore = PlayerPrefs.GetFloat("NormalScore", 0f);
        hardScore = PlayerPrefs.GetFloat("HardScore", 0f);

        // 각 난이도별 점수 텍스트에 반영
        EasyScoreText.text = easyScore.ToString("F2") + " pt";
        NormalScoreText.text = normalScore.ToString("F2") + " pt";
        HardScoreText.text = hardScore.ToString("F2") + " pt";

        // "NowDifficulty" GameObject를 찾아 저장한다
        NowDifficulty = GameObject.Find("NowDifficulty");

        // 버튼 색 반영
        if (easyScore < canNormal)
        {
            //Button buttonComponent = NormalBtn.GetComponent<Button>();
            ColorBlock NormalColors = NormalBtn.colors;
            NormalColors.normalColor = Color.gray;
            NormalColors.highlightedColor = Color.gray;
            NormalColors.pressedColor = Color.gray;
            NormalColors.selectedColor = Color.gray;
            NormalBtn.colors = NormalColors;
            NormalScoreImg.color = Color.gray;
            NormalScoreText.text = "Easy "+ canNormal.ToString("F0") + " pt ↑";
        }
        if (normalScore < canHard)
        {
            ColorBlock HardColors = HardBtn.colors;
            HardColors.normalColor = Color.gray;
            HardColors.highlightedColor = Color.gray;
            HardColors.pressedColor = Color.gray;
            HardColors.selectedColor = Color.gray;
            HardBtn.colors = HardColors;
            HardScoreImg.color = Color.gray;
            HardScoreText.text = "Normal " + canHard.ToString("F0") + " pt ↑";
        }


    }

    public void EasyClick()
    {
        // Easy 스테이지 진입
        NowDifficulty.GetComponent<nowDifficulty>().difficulty = "Easy";
        SceneManager.LoadScene("MainScene");
    }
    public void NormalClick()
    {
        // Easy가 {CanNormal}점 이상일 경우 Normal 진입
        if(easyScore > canNormal)
        {
            NowDifficulty.GetComponent<nowDifficulty>().difficulty = "Normal";
            SceneManager.LoadScene("MainScene");
        }

    }
    public void HardClick()
    {
        // Normal이 {CanHard}점 이상일 경우 Hard 진입
        if(normalScore > canHard)
        {
            NowDifficulty.GetComponent<nowDifficulty>().difficulty = "Hard";
            SceneManager.LoadScene("MainScene");
        }

    }
}
