using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InEnemyEffect : MonoBehaviour
{
    public Animator ani;

    private void OnEnable()
    {
        ani.Play("InEnemyEffect");
    }

    public void DontActive()
    {
        gameObject.SetActive(false);
    }
}
