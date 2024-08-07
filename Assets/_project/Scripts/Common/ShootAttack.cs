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
        private BulletMove[] _bullets;
        private int _currentBullet;
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
            //перетаскиваем пулю в начальную точку, посылаем пулю в заданном направлении
        }
        private void Start()
        {
            _bullets = new BulletMove[Mathf.FloorToInt(_shootPerSecond*_shootLifeTime) +1];
            for (int i = 0;  i < _bullets.Length; i++)
            {
                _bullets[i] = Instantiate(_bulletPrefab, transform);
                _bullets[i].gameObject.SetActive(false);
                _bullets[i].SetSettings(_shootLifeTime,_bulletSpeed);
            }
        }
    }
}