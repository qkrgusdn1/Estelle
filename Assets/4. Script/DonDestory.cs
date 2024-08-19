using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestory : MonoBehaviour
{
    private static DonDestory instance;
    public static DonDestory Instance
    {
        get
        {
            return instance;
        }
    }
    public GoOrBack goOrBack;
    public int day = 1;
    public bool gameClear;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }



}


public enum GoOrBack
{
    go,
    back
}
