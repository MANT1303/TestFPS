using Assets._project.Scripts.Common;
using Assets._project.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.Scripts.Enemies.Boss
{
    public class Boss : AbstractEnemy
    {
        [SerializeField] private BossStateMachine _stateMachine;

        public override event Action Dead;
        public void SetPlayers(List<PlayerControl> controlsPlayers)
        {
            _stateMachine.ControlPlayers = controlsPlayers;
        }


        private void OnValidate()
        {
            if (_stateMachine == null)
            {
                _stateMachine = GetComponentInChildren<BossStateMachine>();
            }
        }
    }
}