using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeMgr : MonoBehaviour
{
    private static HomeMgr instance;
    public static HomeMgr Instance
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
    public TMP_Text dayText;
    bool left = true;
    public Animator doorAni;

    public void OpenDoor()
    {
        if (left)
        {
            doorAni.gameObject.transform.localScale = new Vector2(-1, 1);
            left = false;
        }
        else
        {
            doorAni.gameObject.transform.localScale = new Vector2(1, 1);
            left = true;
        }
        doorAni.Play("OpenDoor");
        
    }

    private void Start()
    {
        dayText.text = "Day " + DonDestory.Instance.day;
    }


}
