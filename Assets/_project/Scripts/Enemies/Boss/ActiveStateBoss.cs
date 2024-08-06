using Assets._project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.Scripts.Enemies.Boss
{
    public class ActiveStateBoss : IState
    {
        public StateMachine StateMachine { get; private set; }
        private BossAttack _attack;
        private List<PlayerControl> _players;

        public ActiveStateBoss(StateMachine stateMachine,BossAttack attack, List<PlayerControl> players)
        {
            StateMachine = stateMachine;
            _attack = attack;
            _players = players;
        }

        public void Enter()
        {
            _attack.SetTargetAttack(_players);
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_players.Count > 0)
            {
                _attack.Attack();
            }
            else
            {
                _attack.Wait();
            }
        }
    }
}