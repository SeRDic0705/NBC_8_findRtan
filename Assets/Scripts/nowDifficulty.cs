using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nowDifficulty : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // 현재 게임 오브젝트를 씬 전환시에도 유지
    }
    public string difficulty;
}
