using System;
using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Common
{
    public class ShootAttack : AbstractAttack
    {
        [SerializeField] private BulletMove _bulletPrefab;
        [SerializeField] private float _shootPerSecond;
        [SerializeField] private float _shootLifeTime;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Transform _bulletPool;
        private BulletMove[] _bullets;
        private int _currentBullet;

        public override event Action SuccessedAttack;
        public override void Attack(Vector3 direction)
        {
            BulletMove currentBullet = _bullets[_currentBullet];
            currentBullet.transform.position = _pointFromAttack.position;
            currentBullet.SetDirection(direction);
            currentBullet.gameObject.SetActive(true);

            _currentBullet++;
            if (_currentBullet == _bullets.Length)
            {
                _currentBullet = 0;
            }
        }
        private void Start()
        {
            _bullets = new BulletMove[Mathf.FloorToInt(_shootPerSecond * _shootLifeTime) + 1];
            for (int i = 0; i < _bullets.Length; i++)
            {

                _bullets[i] = Instantiate(_bulletPrefab, _bulletPool);
                _bullets[i].gameObject.SetActive(false);
                _bullets[i].SetSettings(_shootLifeTime, _bulletSpeed);
                _bullets[i].CollisionEntered += AttackSuccess;

            }
        }
        private void AttackSuccess()
        {
            SuccessedAttack?.Invoke();
        }

        private void OnDisable()
        {
            foreach (var bullet in _bullets)
            {
                bullet.CollisionEntered -= AttackSuccess;
            }

        }
    }
}