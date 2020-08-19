using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public string[] texts = new string[] {"aaa", "bbb", "ccc"};
    public Text contant;

    int currentTextIdx = -1;

    private void Start()
    {
        this.UpdateText();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.UpdateText();
        }
    }

    void UpdateText()
    {
        this.currentTextIdx++;

        if (this.currentTextIdx >= this.texts.Length)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.contant.text = this.texts[this.currentTextIdx];
        }
    }
}
