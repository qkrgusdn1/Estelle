using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Car : MonoBehaviour
{
    private static Car instance;
    public static Car Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool go;
    public bool activeText;
    public GoText text;

    public float moveDuration;
    public float totalDistance;
    public float elapsedTime;

    private void Update()
    {
        if (go)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime <= moveDuration)
            {
                float distanceToMove = (totalDistance / moveDuration) * Time.deltaTime;
                transform.position += new Vector3(distanceToMove, 0, 0);
            }
            else
            {
                go = false;
                if (!activeText)
                {
                    text.gameObject.SetActive(true);
                }
            }
        }
    }
    public void ResetGo()
    {
        elapsedTime = 0;
        go = true;
    }
}