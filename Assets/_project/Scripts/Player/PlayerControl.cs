using Assets._project.Scripts.Common;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [Header("Attack")]
        [SerializeField] private AbstractAttack _attack;
        [SerializeField] private float _secondBetweenAttack;
        [Header("Move")]
        [SerializeField] private float _moveSpeed;
        [Header("Rotate")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _rotateSpeed;
        private float _rotationX;
        private float _rotationY;
        private float _currentTime;
        private Vector3 _currentRotatiion;
        private Vector3 _moveDirection;
        private int _score;
        private void OnEnable()
        {
            _inputManager.Attacked += OnAttack;
            _inputManager.Moved += OnMove;
            _inputManager.Rotated += OnRotate;
            _attack.SuccessedAttack += OnSuccessAttak;
        }


        private void OnDisable()
        {
            _inputManager.Attacked -= OnAttack;
            _inputManager.Moved -= OnMove;
            _inputManager.Rotated -= OnRotate;
        }
        private void OnValidate()
        {
            if (_attack == null)
                _attack = GetComponentInChildren<AbstractAttack>();
            if (_camera == null)
                _camera = GetComponentInChildren<Camera>();
        }
        private void OnRotate(Vector2 obj)
        {
            _rotationX = obj.x * _rotateSpeed;
            _rotationY = -obj.y * _rotateSpeed;
        }

        private void OnMove(Vector2 obj)
        {
            _moveDirection = new Vector3(obj.x, 0, obj.y) * _moveSpeed;

        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            transform.Translate(_moveDirection);
            Rotate();
        }
        private void Rotate()
        {
            transform.eulerAngles += new Vector3(0, _rotationX, 0);
            if (Vector3.Angle(transform.forward, _camera.transform.forward) < 60)
            {
                _currentRotatiion = _camera.transform.localEulerAngles;
                _camera.transform.Rotate(Vector3.right, _rotationY, Space.Self);//Надо ограничить поворот камеры
            }
            else
            {
                _camera.transform.localEulerAngles = _currentRotatiion;
            }

        }
        private void OnAttack()
        {
            
            _attack.Attack(_camera.transform.forward);
        }

        private void OnSuccessAttak()
        {
            _score += 10;
            print(_score);
        }

        public void Win()
        {
            print("Включился экран выигрыша");
        }

    }
}