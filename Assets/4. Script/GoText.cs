using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoText : MonoBehaviour
{
    TMP_Text text;

    public string[] texts;

    public float nextTextTime;

    public int lv;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(CoText());
    }

    IEnumerator CoText()
    {
        foreach (string sentence in texts)
        {
            text.text = "";

            for (int i = 0; i < sentence.Length; i++)
            {
                text.text += sentence[i];
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(nextTextTime);
        }
        Car.Instance.ResetGo();
        yield return new WaitForSeconds(nextTextTime + 1);
        if (DonDestory.Instance.gameClear)
        {
            DonDestory.Instance.gameClear = false;
            DonDestory.Instance.day++;
            SceneManager.LoadScene("Home");
        }
        else
        {
            SceneManager.LoadScene("Talk");
        }

    }
}
