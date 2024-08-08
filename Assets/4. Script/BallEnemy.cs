using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float atkRange;
    public int damage;
    public int maxDamage;
    public float attackSpeed;
    bool canAttack;

    public Image hpBar;
    public float hp;
    public float maxHp;

    public bool back;

    public BallEnemyType ballEnemyType;

    private void OnEnable()
    {
        StartCoroutine(CoAttack());
        hp = maxHp;
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
    }

    

    public void TakeDamage(float damage)
    {
        back = true;
        hp -= damage;
        hpBar.fillAmount = hp / maxHp;
        if(hp <= 0)
        {
            
            StopCoroutine(CoAttack());
            StopCoroutine(CoBack());
            gameObject.SetActive(false);
        }
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
        Vector2 direction = (transform.position - BallGameMgr.Instance.heart.transform.position).normalized; // Heart 반대 방향

        while (Time.time < backEndTime)
        {
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime); // Heart 반대 방향으로 이동
            yield return null;
        }
    }
    void Damage(Heart target)
    {
        int randomDamage = Random.Range(damage, maxDamage);
        target.GetComponent<Heart>().TakeDamage(randomDamage);
        gameObject.SetActive(false);
    }
}
