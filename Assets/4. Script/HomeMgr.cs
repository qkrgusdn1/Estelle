using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeMgr : MonoBehaviour
{
    public TMP_Text dayText;

    private void Start()
    {
        dayText.text = "Day " + DonDestory.Instance.day;
    }
}
