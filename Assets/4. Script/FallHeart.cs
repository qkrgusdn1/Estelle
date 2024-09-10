using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHeart : MonoBehaviour
{
    string groundLayerName = "Ground";
    public float damage;
    public float healAmount;
    public bool heal;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            gameObject.SetActive(false);
        }else if (collision.gameObject.CompareTag("Player"))
        {
            FallHeartPlayer player = collision.gameObject.GetComponent<FallHeartPlayer>();
            if (!heal)
            {
                player.TakeDamage();
                gameObject.SetActive(false);
            }
            else
            {
                player.Heal();
                gameObject.SetActive(false);
            }

        }
    }
}
