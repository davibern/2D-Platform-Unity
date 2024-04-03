using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Living Time")]
    [SerializeField] private float livingTime;

    [Header("Score")]
    [SerializeField] private int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SetScore(score);
            AudioManager.Instance.PlayCoin();
            gameObject.SetActive(false);
            Destroy(gameObject, livingTime);
        }       
    }
}
