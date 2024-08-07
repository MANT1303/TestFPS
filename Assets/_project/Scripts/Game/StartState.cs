using Assets._project.Scripts.Enemies.Boss;
using Assets._project.Scripts.Player;
using System.Collections.Generic;

namespace Assets._project.Scripts.Game
{
    public class StartState : IState
    {
        private Boss _boss;
        private List<PlayerControl> _players;

        public StateMachine StateMachine { get; private set; }

        public StartState(StateMachine stateMachine, Boss boss, List<PlayerControl> players)
        {
            StateMachine = stateMachine;
            _boss = boss;
            _players = players;
        }

        public void Enter()
        {
            _boss.SetPlayers( _players );
            _boss.gameObject.SetActive(true);
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}