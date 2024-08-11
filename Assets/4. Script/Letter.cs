using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string[] texts;

    public void SetLetterText()
    {
        BallGameMgr.Instance.letterText.gameObject.SetActive(true);
        BallGameMgr.Instance.letterText.text = texts[Random.Range(0, texts.Length)];
        gameObject.SetActive(false);
    }
}

