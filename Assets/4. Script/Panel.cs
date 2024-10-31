using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    public int level;

    public void GoGame()
    {
        if(level == 1)
        {
            SceneManager.LoadScene("Ball");
        }
        else if(level == 2)
        {
            SceneManager.LoadScene("Fall");
        }
        else if (level == 3)
        {

        }
    }
}
