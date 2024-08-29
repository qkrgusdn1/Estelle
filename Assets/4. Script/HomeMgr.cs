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
    public Animator doorAni;

    bool openDoor;

    public void OpenDoor(Transform playerTransform)
    {
        Vector3 doorPosition = doorAni.gameObject.transform.position;
        Vector3 playerPosition = playerTransform.position;

        if (playerPosition.x < doorPosition.x)
        {
            doorAni.gameObject.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            doorAni.gameObject.transform.localScale = new Vector2(-1, 1);
        }

        doorAni.Play("OpenDoor");
    }

    private void Start()
    {
        dayText.text = "Day " + DonDestory.Instance.day;
    }


}
