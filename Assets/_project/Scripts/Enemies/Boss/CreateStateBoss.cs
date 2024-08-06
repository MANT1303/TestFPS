using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Enemies.Boss
{
    public class CreateStateBoss : IState
    {
        public StateMachine StateMachine { get; private set; }
        private Transform _bossTransform;
        private float _timeCreating;
        private float _currentTime;

        public CreateStateBoss(StateMachine stateMachine, Transform bossTransform, float timeCreating)
        {
            StateMachine = stateMachine;
            _bossTransform = bossTransform;
            _timeCreating = timeCreating;
        }

        public void Enter()
        {
            _bossTransform.localScale = Vector3.zero;
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _currentTime += Time.deltaTime / _timeCreating;
            _bossTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _currentTime);
            if (_currentTime > 1)
            {
                StateMachine.SetState<ActiveStateBoss>();
            }
        }
    }
}