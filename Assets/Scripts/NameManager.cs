using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public GameObject NameText;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowName", 1.0f);
    }
    void ShowName()
    {
        NameText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
