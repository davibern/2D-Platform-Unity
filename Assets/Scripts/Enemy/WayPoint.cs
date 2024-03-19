using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private Transform[] _transform;
    private int _nextStep = 0;
    private SpriteRenderer _sp;
    private Animator _anim;

    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
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

    private void Flip()
    {
        if (transform.position.x < _transform[_nextStep].position.x)
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
