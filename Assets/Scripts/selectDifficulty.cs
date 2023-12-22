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
        // �� ���̵��� ���� �ҷ�����
        easyScore = PlayerPrefs.GetFloat("EasyScore", 0f);
        normalScore = PlayerPrefs.GetFloat("NormalScore", 0f);
        hardScore = PlayerPrefs.GetFloat("HardScore", 0f);

        // �� ���̵��� ���� �ؽ�Ʈ�� �ݿ�
        EasyScoreText.text = easyScore.ToString("F2") + " pt";
        NormalScoreText.text = normalScore.ToString("F2") + " pt";
        HardScoreText.text = hardScore.ToString("F2") + " pt";

        // "NowDifficulty" GameObject�� ã�� �����Ѵ�
        NowDifficulty = GameObject.Find("NowDifficulty");

        // ��ư �� �ݿ�
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
            NormalScoreText.text = "Easy "+ canNormal.ToString("F0") + " pt ��";
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
            HardScoreText.text = "Normal " + canHard.ToString("F0") + " pt ��";
        }


    }

    public void EasyClick()
    {
        // Easy �������� ����
        NowDifficulty.GetComponent<nowDifficulty>().difficulty = "Easy";
        SceneManager.LoadScene("MainScene");
    }
    public void NormalClick()
    {
        // Easy�� {CanNormal}�� �̻��� ��� Normal ����
        if(easyScore > canNormal)
        {
            NowDifficulty.GetComponent<nowDifficulty>().difficulty = "Normal";
            SceneManager.LoadScene("MainScene");
        }

    }
    public void HardClick()
    {
        // Normal�� {CanHard}�� �̻��� ��� Hard ����
        if(normalScore > canHard)
        {
            NowDifficulty.GetComponent<nowDifficulty>().difficulty = "Hard";
            SceneManager.LoadScene("MainScene");
        }

    }
}
