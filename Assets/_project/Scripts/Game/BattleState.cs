using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Game
{
    public class BattleState : IState
    {
        public StateMachine StateMachine { get; private set; }
        public BattleState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}