using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("Movmenet")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    [Header("TypeMovement")]
    [SerializeField] private bool _isRandomMovement;
    [SerializeField] private Transform[] _transform;
    private int _nextStep = 0;
    private int _randNumber;
    private SpriteRenderer _sp;
    private Animator _anim;

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

    private void FixedUpdate()
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
        transform.position = Vector2.MoveTowards(transform.position, _transform[_nextStep].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _transform[_nextStep].position) < _distance )
        {
            _nextStep += 1;

            if (HasParameter("Walk"))
                _anim.SetBool("Walk", true);

            if (_nextStep >= _transform.Length )
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
        } else
        {
            _sp.flipX = true;
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
