using UnityEngine;

public class FallHeartPlayer : MonoBehaviour
{
    public GameObject body;
    Rigidbody2D rb;
    public float moveSpeed;

    bool isMovingRight = false;
    bool isMovingLeft = false;

    Animator ani;

    public int hp = 3;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMovingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            body.transform.localScale = new Vector2(1, 1);
            ani.Play("BearRun");
        }
        else if (isMovingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            body.transform.localScale = new Vector2(-1, 1);
            ani.Play("BearRun");
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ani.Play("BearIdle");
        }
    }
    public void Heal()
    {
        if (0 < hp && hp < 3)
        {
            hp++;
            for(int i =0; i < FallHeartMgr.Instance.hpImages.Count; i++)
            {
                if (!FallHeartMgr.Instance.hpImages[i].gameObject.activeSelf)
                {
                    FallHeartMgr.Instance.hpImages[i].gameObject.SetActive(true);
                    break;
                }
            }
            
        }
    }

    public void TakeDamage()
    {
        if (0 < hp && hp <= 3)
        {
            hp--;
            for (int i = 0; i < FallHeartMgr.Instance.hpImages.Count; i++)
            {
                if (FallHeartMgr.Instance.hpImages[i].gameObject.activeSelf)
                {
                    FallHeartMgr.Instance.hpImages[i].gameObject.SetActive(false);
                    break;
                }
            }
        }
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
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
