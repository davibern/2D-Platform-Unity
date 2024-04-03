using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("Movemenet")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private bool _isFacingRight;

    [Header("TypeMovement")]
    [SerializeField] private bool _isRandomMovement;
    [SerializeField] private Transform[] _transform;
    private int _nextStep = 0;
    private int _randNumber;
    private SpriteRenderer _sp;
    private Animator _anim;

    [Header("Player")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistance;

    private void Awake()
    {
        _randNumber = UnityEngine.Random.Range(0, _transform.Length);
        _sp = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        Flip();
    }

    private void Start()
    {
        _anim.SetBool("Walk", true);
    }

    private void Update()
    {
        if (_isRandomMovement)
        {
            MovementBetweenRandmonPoints();
        } else
        {
            MovementBetweenTwoPoints();
        }
    }

    private void MovementBetweenTwoPoints()
    {
        if (EnemyController.Instance.IsAlive())
        {
            transform.position = Vector2.MoveTowards(transform.position, _transform[_nextStep].position, _speed * Time.deltaTime);

            if (HasParameter("Attack"))
            {
                Attack();
            }
            else
            {
                _nextStep += 1;

                if (_nextStep >= _transform.Length)
                    _nextStep = 0;

                Flip();
            }
        }
    }

    private void Attack()
    {

        float distance = Vector2.Distance(transform.position, _target.position);

        if (distance < _minDistance && !_isFacingRight)
        {
            _anim.SetBool("Attack", true);
            _anim.SetBool("Walk", false);

            BoxCollider2D childCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
            if (!_isFacingRight)
            {
                childCollider.offset = new Vector2(childCollider.offset.x * -1, childCollider.offset.y);
            }
            else
            {
                childCollider.offset = new Vector2(1.0f, childCollider.offset.y);
            }
        }
        else if (Vector2.Distance(transform.position, _transform[_nextStep].position) < _distance)
        {
            _anim.SetBool("Attack", false);
            _anim.SetBool("Walk", true);

            _nextStep += 1;

            if (_nextStep >= _transform.Length)
                _nextStep = 0;

            Flip();
        }
    }

    private void MovementBetweenRandmonPoints()
    {
        transform.position = Vector2.MoveTowards(transform.position, _transform[_randNumber].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _transform[_randNumber].position) < _distance )
        {
            _randNumber = UnityEngine.Random.Range(0, _transform.Length);
            Flip();
        }
    }

    private void Flip()
    {
        int next = _isRandomMovement ? _randNumber : _nextStep;

        if (transform.position.x < _transform[next].position.x)
        {
            _sp.flipX = false;
            _isFacingRight = true;
        } else
        {
            _sp.flipX = true;
            _isFacingRight = false;
        }
    }

    private bool HasParameter(string parameter)
    {
        bool result = false;

        for (int i = 0; i < _anim.parameterCount; i++)
        {
            if (_anim.parameters[i].name == parameter)
            {
                result = true;
            }
        }

        return result;
    }
}
