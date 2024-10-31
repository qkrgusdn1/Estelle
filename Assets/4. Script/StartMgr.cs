using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMgr : MonoBehaviour
{
    public GameObject black;
    //private void Start()
    //{
    //    Screen.SetResolution(1920, 1080, true);
    //}
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            black.SetActive(true);
        }
    }
}
