using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._project.Scripts.Common
{
    public class InputManager : MonoBehaviourPunCallbacks
    {
        private Input _input;

        public event Action Attacked;
        public event Action<Vector2> Moved;
        public event Action<Vector2> Rotated;
        private void Awake()
        {
            _input = new Input();
        }
        private void Start()
        {
            if (photonView.IsMine)
            {
                _input.Player.Attack.performed += (InputAction.CallbackContext context) => Attacked?.Invoke();
                AddEventInput(_input.Player.Move, (InputAction.CallbackContext context) => Moved?.Invoke(context.ReadValue<Vector2>()));
                AddEventInput(_input.Player.Rotate, (InputAction.CallbackContext context) => Rotated?.Invoke(context.ReadValue<Vector2>()));
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _input.Enable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _input?.Disable();
        }

        private void AddEventInput( InputAction input, Action<InputAction.CallbackContext> action)
        {
            input.performed += action;
            input.canceled += action;
        }

    }
}