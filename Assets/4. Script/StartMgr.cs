using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMgr : MonoBehaviour
{
    public GameObject black;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            black.SetActive(true);
        }
    }
}
