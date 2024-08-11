using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOnTheWayToWork : MonoBehaviour
{
    public GameObject black;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            black.gameObject.SetActive(true);
        }
    }

}
