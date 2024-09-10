using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private static Ball instance;
    public static Ball Instance
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
    public int damage;

    public Camera mainCamera;
    Rigidbody2D rb;

    public Image powerBar;
    public bool onDrag;
    Vector2 startDrag;

    public float margin;
    public float maxPower;
    public float power
    {
        get;
        private set;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Letter"))
        {
            collision.GetComponent<Letter>().SetLetterText();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<BallEnemy>().touch = true;
            if(collision.GetComponent<BallEnemy>().dontOut == true)
            {
                collision.GetComponent<BallEnemy>().col.isTrigger = false;
            }
            
        }
    }

    void Update()
    {
        if (!onDrag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 point = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Collider2D col = Physics2D.OverlapPoint(point, LayerMask.GetMask("Ball"));

                if (col != null)
                {
                    onDrag = true;
                    startDrag = point;
                }
            }
        }
        else
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vector = startDrag - point;
            power = vector.magnitude;
            if (power >= maxPower)
            {
                power = maxPower;
            }

            Vector2 dir = vector.normalized;

            if (Input.GetMouseButtonUp(0))
            {
                onDrag = false;

                rb.AddForce(dir * power * 50);
            }
        }
    }

    void FixedUpdate()
    {
        float slow = 0.99f;
        rb.velocity *= slow;

        if (rb.velocity.magnitude < 0.1f)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
