using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Black : MonoBehaviour
{
    public bool start;
    public void MoveScene()
    {
        if (start)
        {
            SceneManager.LoadScene("Home");
        }
        else
        {
            SceneManager.LoadScene("Go");
        }

    }

    public void RemoveBlack()
    {
        gameObject.SetActive(false);
    }
}
