using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nowDifficulty : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // ���� ���� ������Ʈ�� �� ��ȯ�ÿ��� ����
    }
    public string difficulty;
}
