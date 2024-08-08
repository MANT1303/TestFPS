using Assets._project.Scripts.Enemies.Boss;
using Assets._project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.Scripts.Enemies
{
    public class BossStateMachine : MonoBehaviour
    {
        [SerializeField] private float _timeCreatingAndDeadBoss;
        [SerializeField] private BossAttack _bossAttack;
        public List<PlayerControl> ControlPlayers { private get; set; }//?
        private StateMachine _fsm;

        private void OnValidate()
        {
            if(_bossAttack == null)
            {
                _bossAttack = GetComponentInChildren<BossAttack>();
            }
        }

        private void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState(new CreateStateBoss(_fsm, transform,_timeCreatingAndDeadBoss));
            _fsm.AddState(new ActiveStateBoss(_fsm, _bossAttack, ControlPlayers));
            _fsm.AddState(new DeadStateBoss(_fsm,transform,_timeCreatingAndDeadBoss));

            _fsm.SetState<CreateStateBoss>();
        }
        private void Update()
        {
            _fsm.Update();
        }

        public void BossDead()
        {
            _fsm.SetState<DeadStateBoss>();
        }
    }

}