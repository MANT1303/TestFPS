using Assets._project.Scripts.Common;
using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._project.Scripts.Player
{
    public class PlayerControl : MonoBehaviourPunCallbacks
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
        [Header("UI")]
        [SerializeField] private GameObject _gameplayUI;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _winUI;
        [SerializeField] private TextMeshProUGUI _winScoreText;
        [SerializeField] private Button _leaveButton;

        private float _rotationX;
        private float _rotationY;
        private float _currentTime;
        private Vector3 _currentRotatiion;
        private Vector3 _moveDirection;
        private int _score;

        private void Awake()
        {
            _winUI.SetActive(false);
            if (!photonView.IsMine)
            {
                _camera.gameObject.SetActive(false);
            }
        }
        public override void OnEnable()
        {
            base.OnEnable();
            _inputManager.Attacked += OnAttack;
            _inputManager.Moved += OnMove;
            _inputManager.Rotated += OnRotate;
            _attack.SuccessedAttack += OnSuccessAttak;
            _leaveButton.onClick.AddListener(LeaveRoom);
            
        }


        public override void OnDisable()
        {
            base.OnDisable();
            _inputManager.Attacked -= OnAttack;
            _inputManager.Moved -= OnMove;
            _inputManager.Rotated -= OnRotate;
            _attack.SuccessedAttack -= OnSuccessAttak;
            _leaveButton.onClick.RemoveAllListeners();
        }
        private void OnValidate()
        {
            if (_attack == null)
                _attack = GetComponentInChildren<AbstractAttack>();
            if (_camera == null)
                _camera = GetComponentInChildren<Camera>();
            if(_inputManager == null)
                _inputManager = GetComponentInChildren<InputManager>();
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
            _scoreText.text = $"Score: {_score}";
        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            SceneManager.LoadScene(0);
            PhotonNetwork.Disconnect();
        }
        public void Win()
        {
            _winScoreText.text = $"Your score: {_score}";
            _winUI.SetActive(true);
            _gameplayUI.SetActive(false);
        }

        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}