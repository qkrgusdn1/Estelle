using Cinemachine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PuzzleMgr : MonoBehaviour
{
    public ThreeStageMove threeStageMove;
    public GameObject[] maps;
    public GameObject currentMap;
    public float cameraMoveSpeed;
    private void Start()
    {
        currentMap = maps[0];
    }

    public void OnClickedRightBtn()
    {
        for(int i = 0; i < maps.Length; i++)
        {
            if (maps[i] == currentMap)
            {
                if(i == maps.Length - 1)
                {
                    currentMap = maps[0];
                    break;
                }

                currentMap = maps[i + 1];
                break;
            }
        }
        StartCoroutine(MoveCameraToTarget(currentMap.transform.position));
        threeStageMove.gameObject.SetActive(true);

    }
    public void OnClickedLefttBtn()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            if (maps[i] == currentMap)
            {
                if(i == 0)
                {
                    currentMap = maps[maps.Length - 1];
                    break;
                }
                currentMap = maps[i - 1];
                break;
            }
        }
        StartCoroutine(MoveCameraToTarget(currentMap.transform.position));
        threeStageMove.gameObject.SetActive(true);
    }

    private IEnumerator MoveCameraToTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = Camera.main.transform.position;
        Vector3 finalPosition = new Vector3(targetPosition.x, targetPosition.y, -10);

        float t = 0;
        while (t < 1.0f)
        {
            t += Time.deltaTime * cameraMoveSpeed;
            Camera.main.transform.position = Vector3.Lerp(startPosition, finalPosition, t);
            yield return null;
        }

        Camera.main.transform.position = finalPosition;
    }
}
