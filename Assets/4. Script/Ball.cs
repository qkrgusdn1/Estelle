using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage;

    public Camera mainCamera;
    Rigidbody2D rb;

    bool onDrag;
    Vector2 startDrag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BallEnemy>().TakeDamage(damage);
        }
    }

    void Update()
    {
        if (!onDrag)
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Collider2D col = Physics2D.OverlapPoint(point, LayerMask.GetMask("Ball"));

            if (col != null)
            {
                onDrag = true;
                startDrag = point;
            }
        }
        else
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vector = startDrag - point;
            float power = vector.magnitude;
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
