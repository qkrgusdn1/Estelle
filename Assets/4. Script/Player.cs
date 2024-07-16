using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject root;
    public float moveSpeed;
    public float jumpPower;
    Rigidbody2D rb;
    public Animator ani;
    public LayerMask groundLayer;
    public bool isGrounded;
    public Camera mainCamera;
    bool hang;
    float realGravity;
    bool noJump;
    public int jumpCount;

    Coroutine resetNoJumpCoroutine;
    Coroutine resetIsMove;

    bool isBack;
    bool isMove;
    bool isDash;
    bool isDashing;
    public float deshTime;
    public float maxDeshTime;



    public Joystick joystick;
    private void Start()
    {
        isMove = true;
        rb = GetComponent<Rigidbody2D>();
        realGravity = rb.gravityScale;
    }

    private void Update()
    {
        if(!hang)
            rb.gravityScale = realGravity;
        isGrounded = Physics2D.OverlapCircle(root.transform.position, 0.1f, groundLayer);
        Move();
        Jump();
        DashTime();
        if (Input.GetKeyDown(KeyCode.LeftControl) && isDash)
        {
            if (!hang)
                StartCoroutine(CoDesh());
        }

        CheckCameraPosition();
    }

    void Move()
    {
        
        if (!isDashing)
        {
            float horizontalInput = joystick.Horizontal;
            if(Mathf.Abs(horizontalInput) > 0.1f && !hang && isMove)
            {
                isBack = false;
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

                if (horizontalInput > 0)
                {
                    transform.localScale = new Vector2(1, 1);
                    ani.SetBool("IsRun", true);
                }
                else if (horizontalInput < 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                    ani.SetBool("IsRun", true);
                }
            }
            else
            {
                ani.SetBool("IsRun", false);
                isBack = true;

                if (Mathf.Abs(rb.velocity.x) > 0.1f && isBack)
                {
                    Vector2 dir = rb.velocity.normalized;
                    rb.velocity -= new Vector2(dir.x * 0.05f, 0);
                }

            }
            
            

           
        }
       
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || !noJump || hang)
            {
                noJump = true;

                rb.gravityScale = realGravity;
                rb.velocity = new Vector2(rb.velocity.x, 0);

                if (hang)
                {
                    
                    Debug.Log("hangJump");
                    float moveDirection = transform.localScale.x > 0 ? -1 : 1;
                    Debug.Log("moveDirection : " + moveDirection);
                    rb.AddForce(new Vector2(moveDirection * jumpPower * 1.5f, jumpPower), ForceMode2D.Impulse);
                    Debug.Log("rb.velocity" + rb.velocity);
                    hang = false;
                    if (resetIsMove != null)
                        StopCoroutine(resetIsMove);
                    resetIsMove = StartCoroutine(CoSetMove());
                    ani.SetBool("IsClimb", false);
                    ani.SetTrigger("Jump");
                }
                else
                {
                    rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                    ani.SetBool("IsClimb", false);
                    ani.SetTrigger("Jump");
                }

                if (!isGrounded)
                {
                    jumpCount--;
                    if (jumpCount <= 0)
                        noJump = true;
                }
                else
                {
                    if(resetNoJumpCoroutine != null)
                        StopCoroutine(resetNoJumpCoroutine);    

                    resetNoJumpCoroutine = StartCoroutine(ResetNoJump());
                    jumpCount = 2;
                }
            }
        }
    }

    void DashTime()
    {
        if (deshTime >= maxDeshTime)
        {
            isDash = true;
            return;
        }

        deshTime += Time.deltaTime;

    }
    IEnumerator CoSetMove()
    {
        
        yield return new WaitForSeconds(0.3f);
        isMove = true;
        isBack = false;


    }
    IEnumerator CoDesh()
    {
        if (deshTime >= maxDeshTime)
        {
            isDashing = true;
            rb.velocity = new Vector2(transform.localScale.x * moveSpeed * 3, 0);
            ani.Play("Dash");
            yield return new WaitForSeconds(0.2f);
            isDash = false;
            isDashing = false;
            deshTime = 0;
        }
    }



    IEnumerator ResetNoJump()
    {
        yield return new WaitForSeconds(0.2f);
        noJump = false;
        
    }
    public void ClickedDashButton()
    {
        if(!hang)
            StartCoroutine(CoDesh());
    }

    public void ClickedJumpButton()
    {
        if (isGrounded || !noJump)
        {
            noJump = true;

            rb.gravityScale = realGravity;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            ani.SetTrigger("Climb");


            if (!isGrounded)
            {
                jumpCount--;
                if (jumpCount <= 0)
                    noJump = true;
            }
            else
            {
                StopCoroutine(ResetNoJump());
                StartCoroutine(ResetNoJump());
                jumpCount = 2;
            }
        }
    }

    void CheckCameraPosition()
    {
        Vector2 playerViewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (playerViewportPos.x >= 1)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.x += mainCamera.orthographicSize * 2.0f * mainCamera.aspect;
            mainCamera.transform.position = cameraPosition;
        }
        else if (playerViewportPos.x <= 0)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.x -= mainCamera.orthographicSize * 2.0f * mainCamera.aspect;
            mainCamera.transform.position = cameraPosition;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ClimbWall"))
        {
            Debug.Log("OnCollisionEnter2D Wall");
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            ani.SetBool("IsClimb", true);
            isMove = false;
            hang = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ClimbWall"))
        {
            Debug.Log("OnCollisionExit2D Wall");
            ani.SetBool("IsClimb", false);
            hang = false;
        }
    }

}

