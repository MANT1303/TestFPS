using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Enemies.Boss
{
    public class DeadStateBoss : IState
    {
        public StateMachine StateMachine { get; private set; }
        private Transform _bossTransform;
        private float _timeDead;
        private float _currentTime;

        public DeadStateBoss (StateMachine stateMachine, Transform bossTransform, float timeDead)
        {
            StateMachine = stateMachine;
            _bossTransform = bossTransform;
            _timeDead = timeDead;
        }


        public void Enter()
        {
            _bossTransform.localScale = Vector3.one;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            _currentTime += Time.deltaTime / _timeDead;
            _bossTransform.localScale = Vector3.Lerp(Vector3.one,Vector3.zero, _currentTime);
            if (_currentTime > 1)
            {
                GameObject.Destroy(_bossTransform.gameObject);
            }
        }
    }
}