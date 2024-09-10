using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTr;
    public Vector3 offset;
    float fixedY = 0.6699833f;

    void LateUpdate()
    {
        Vector3 newPosition = playerTr.position + offset;
        newPosition.y = fixedY; 
        transform.position = newPosition;
    }
}
