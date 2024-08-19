using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBg : MonoBehaviour
{
    public float scrollSpeed;

    private Vector3 startPosition;

    public bool start;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!start)
        {
            if (!DonDestory.Instance.gameClear)
            {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 19.2f);
                transform.position = startPosition + Vector3.left * newPosition;
            }
            else
            {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 19.2f);
                transform.position = startPosition + Vector3.right * newPosition;
            }
        }
        else
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 19.2f);
            transform.position = startPosition + Vector3.left * newPosition;
        }


        
        
    }
}
