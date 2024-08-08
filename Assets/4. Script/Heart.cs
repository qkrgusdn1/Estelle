using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image hpBar;
    public float hp;
    public float maxHp;

    private void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / maxHp;
    }
}
