using Assets._project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.Scripts.Game
{
    public class WinState : IState
    {
        private List<PlayerControl> _players;

        public StateMachine StateMachine { get; private set; }
        public WinState(StateMachine stateMachine)
        {
        }

        public WinState(StateMachine stateMachine, List<PlayerControl> players) 
        {
            _players = players;
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            foreach (var player in _players)
            {
                player.Win();
            }
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}