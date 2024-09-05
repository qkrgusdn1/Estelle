using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FallHeartPlayer : MonoBehaviour
{
    public GameObject body;
    Rigidbody2D rb;
    public float moveSpeed;

    bool isMovingRight = false;
    bool isMovingLeft = false;

    public Image hpBar;
    public float hp;
    public float maxHp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    private void Update()
    {
        if (isMovingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            body.transform.localScale = new Vector2(1, 1);  
        }
        else if (isMovingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            body.transform.localScale = new Vector2(-1, 1); 
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); 
        }
    }
    public void Heal(float healAmount)
    {
        if(hp < maxHp)
        {
            hp += healAmount;
            hpBar.fillAmount = hp / maxHp;
        }

    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / maxHp;
    }

    public void OnClickedRightBtn()
    {
        Debug.Log("OnClickedRightBtn");
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void OnClickedLeftBtn()
    {
        Debug.Log("OnClickedLeftBtn");
        isMovingRight = false;
        isMovingLeft = true;
    }

    public void NoClickedRightBtn()
    {
        isMovingRight = false;
    }

    public void NoClickedLeftBtn()
    {
        isMovingLeft = false;
    }
}
