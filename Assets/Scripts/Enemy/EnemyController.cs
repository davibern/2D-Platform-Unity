using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController _instance;

    [Header("Components")]
    private Animator _anim;

    [Header("Live")]
    private float _livingTime = 0.5f;
    private bool _isAlive; 

    public static EnemyController Instance {  get { return _instance; } }

    private void Start()
    {
        _isAlive = true;
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DefeatPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isAlive = false;
            _anim.SetTrigger("Die");
            PlayerController.Instance.HitEnemy = true;
            AudioManager.Instance.PlayDefeatSlime();
            Destroy(gameObject, _livingTime);
        }
    }

    private void DefeatPlayer()
    {
        PlayerController.Instance.GetDamage();
    }

    public bool IsAlive() { return _isAlive; }
}
