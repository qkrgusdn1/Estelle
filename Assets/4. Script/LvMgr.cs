using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LvMgr : MonoBehaviour
{
    public List<GameObject> goLvList = new List<GameObject>();
    public List<GameObject> backLvList = new List<GameObject>();
    public GameObject rain;
    
    private void Start()
    {


        if (!DonDestory.Instance.gameClear)
        {
            goLvList[DonDestory.Instance.day - 1].gameObject.SetActive(true);
            Car.Instance.text = goLvList[DonDestory.Instance.day - 1].GetComponentInChildren<GoText>(true);
        }
        else
        {
            backLvList[DonDestory.Instance.day - 1].gameObject.SetActive(true);
            Car.Instance.text = backLvList[DonDestory.Instance.day - 1].GetComponentInChildren<GoText>(true);
            if (DonDestory.Instance.day == 2)
            {
                rain.SetActive(true);
            }
        }

    }
}
