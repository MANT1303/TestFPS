using Assets._project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.Scripts.Game
{
    public class WinState : IState
    {
        private PlayerControl _player;

        public StateMachine StateMachine { get; private set; }

        public WinState(StateMachine stateMachine, PlayerControl player) 
        {
            StateMachine = stateMachine;
            _player = player;
        }

        public void Enter()
        {
            _player.Win();
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}