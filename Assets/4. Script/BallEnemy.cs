using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float maxMoveSpeed;
    public float atkRange;
    public int damage;
    public int maxDamage;
    public float attackSpeed;
    bool canAttack;

    public GameObject body;

    public Letter letterPrefab;
    public float hp;
    public float maxHp;

    public bool back;

    public BallEnemyType ballEnemyType;

    public bool touch;

    Rigidbody2D rb;

    public Collider2D col;
    public bool dontOut;

    bool inRange;


    InEnemyEffect inEnemyEffect;

    private void OnEnable()
    {
        moveSpeed = maxMoveSpeed;
        canAttack = false;
        StartCoroutine(CoAttack());
        StopCoroutine(CoBack());
        hp = maxHp;
        touch = false;
        col.isTrigger = true;
        dontOut = false;
        SpriteRenderer bodySpriteRenderer = body.GetComponent<SpriteRenderer>();
        if (bodySpriteRenderer != null)
        {
            Color bodyColor = bodySpriteRenderer.color;
            bodyColor.a = 0.7f;
            bodySpriteRenderer.color = bodyColor;
        }
        inRange = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        inEnemyEffect = GetComponentInChildren<InEnemyEffect>(true);
        
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, BallGameMgr.Instance.heart.transform.position);
        if (distance <= atkRange)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            if (back)
            {
                StartCoroutine(CoBack());
                back = false;
            }
            else
            {

                transform.position = Vector2.MoveTowards(transform.position, BallGameMgr.Instance.heart.transform.position, moveSpeed * Time.deltaTime);
            }

        }

        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hole") && touch)
        {
            Die();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BallGameWall") && !inRange)
        {
            inRange = true;
            inEnemyEffect.gameObject.SetActive(true);
            dontOut = true;
            SpriteRenderer bodySpriteRenderer = body.GetComponent<SpriteRenderer>();
            if (bodySpriteRenderer != null)
            {
                Color bodyColor = bodySpriteRenderer.color;
                bodyColor.a = 1;
                bodySpriteRenderer.color = bodyColor;
            }
        }
    }

    public void Die()
    {
        if (Random.Range(0, 2) == 0)
        {
            if (BallGameMgr.Instance.letterPoolings.Count > 0)
            {
                bool letterActivated = false;
                foreach (Letter letter in BallGameMgr.Instance.letterPoolings)
                {
                    if (!letter.gameObject.activeSelf)
                    {
                        letter.gameObject.SetActive(true);
                        letter.transform.position = transform.position;
                        letterActivated = true;
                        break;
                    }
                }
                if (!letterActivated)
                {
                    Letter letter = Instantiate(letterPrefab);
                    letter.transform.position = transform.position;
                    BallGameMgr.Instance.letterPoolings.Add(letter);
                }
            }
            else
            {
                Letter letter = Instantiate(letterPrefab);
                letter.transform.position = transform.position;
                BallGameMgr.Instance.letterPoolings.Add(letter);
            }
        }
        gameObject.SetActive(false);

    }
    IEnumerator CoAttack()
    {
        while (true)
        {
            if (canAttack)
            {
                Attack();
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    void Attack()
    {
        if (BallGameMgr.Instance.heart != null && BallGameMgr.Instance.heart.gameObject.activeSelf)
        {
            float dis = Vector2.Distance(transform.position, BallGameMgr.Instance.heart.transform.position);

            if (dis <= atkRange)
            {
                Damage(BallGameMgr.Instance.heart);
            }
        }
    }
    IEnumerator CoBack()
    {
        float backEndTime = Time.time + 1f;
        Vector2 direction = (transform.position - BallGameMgr.Instance.heart.transform.position).normalized;

        while (Time.time < backEndTime)
        {
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    void Damage(Heart target)
    {
        int randomDamage = Random.Range(damage, maxDamage);
        target.GetComponent<Heart>().TakeDamage(randomDamage);
        StopCoroutine(CoAttack());
        gameObject.SetActive(false);
    }



    private void Flip()
    {
 
        if (transform.position.x > 0)
        {
            body.transform.localScale = new Vector2(-1f, body.transform.localScale.y);
        }
        else
        {
            body.transform.localScale = new Vector2(1f, body.transform.localScale.y);
        }
    }
    void FixedUpdate()
    {
        float slow = 0.99f;
        rb.velocity *= slow;
    }

}
