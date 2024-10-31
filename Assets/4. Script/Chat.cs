using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public TMP_Text text;
    public Animator animator;
    public bool mine;
    public bool remove;
    public bool isNew = false;
    public RectTransform rectTransform;
    RectTransform baseRectTransform;

    public float baseWidth;
    public float additionalWidthPerChar;
    public Transform baseTransform;
    private void Awake()
    {
        baseTransform = transform;
        baseRectTransform = rectTransform;
        baseWidth = baseRectTransform.sizeDelta.x;
        
    }

    private void OnEnable()
    {
        animator.Play("Chat");
        
    }

    private void Update()
    {
        var textColor = text.color;
        textColor.a = 1;
        text.color = textColor;
    }
    public void AdjustWidthBasedOnText()
    {
        int characterCount = text.text.Length;

        if (characterCount > 11)
        {
            int extraCharCount = characterCount - 11;

            float extraWidth = extraCharCount * additionalWidthPerChar;

            if (mine)
            {
                transform.position = new Vector3(baseTransform.position.x + -(20 * extraCharCount), baseTransform.transform.position.y, 0);
            }
            else
            {
                transform.position = new Vector3(baseTransform.position.x + (20 * extraCharCount), baseTransform.transform.position.y, 0);
            }
            

            float newWidth = baseWidth + extraWidth;
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(baseWidth, rectTransform.sizeDelta.y);
        }
    }
}
