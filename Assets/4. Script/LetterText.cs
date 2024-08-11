using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class LetterText : MonoBehaviour
{
    TMP_Text text;
    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(FadeInAndOutText());
        for (int i = 0; i < BallGameMgr.Instance.ballEnemiesPoolings.Count; i++)
        {
            BallGameMgr.Instance.ballEnemiesPoolings[i].moveSpeed = BallGameMgr.Instance.ballEnemiesPoolings[i].moveSpeed / 3;
        }
    }

    IEnumerator FadeInAndOutText()
    {
        Color color = text.color;
        color.a = 0f;
        text.color = color;

        float fadeDuration = 1f;
        float displayDuration = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            text.color = color;
            yield return null;
        }

        color.a = 1f;
        text.color = color;

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            text.color = color;
            yield return null;
        }

        color.a = 0f;
        text.color = color;
        for (int i = 0; i < BallGameMgr.Instance.ballEnemiesPoolings.Count; i++)
        {
            BallGameMgr.Instance.ballEnemiesPoolings[i].moveSpeed = BallGameMgr.Instance.ballEnemiesPoolings[i].maxMoveSpeed;
        }
        gameObject.SetActive(false);
    }

}
