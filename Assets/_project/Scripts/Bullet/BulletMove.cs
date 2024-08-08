using Assets._project.Scripts.Common;
using Assets._project.Scripts.Enemies.Boss;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float _lifeTime;
    private float _velocity;
    private Vector3 _direction;
    private float _currentTime;

    public event Action CollisionEntered;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _currentTime += Time.deltaTime / _lifeTime;
        if (_currentTime > 1)
        {
            gameObject.SetActive(false);
        }
        transform.position += _direction * _currentTime * _velocity;
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        _currentTime = 0;
    }
    public void SetSettings(float lifetime, float velocity)
    {
        _lifeTime = lifetime;
        _velocity = velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        if(collision.gameObject.TryGetComponent(out AbstractEnemy enemy)) 
        {
            CollisionEntered?.Invoke();
            enemy.Takedamage(1);
        }

    }
}
