using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float _lifeTime;
    private float _velocity;
    private Vector3 _direction;
    private float _currentTime;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _currentTime += Time.deltaTime / _lifeTime;
        if (_currentTime > 1)
        {
            _currentTime = 0;
            gameObject.SetActive(false);
        }
        transform.position += _direction * _currentTime * _velocity;
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    public void SetSettings(float lifetime, float velocity)
    {
        _lifeTime = lifetime;
        _velocity = velocity;
    }
}
