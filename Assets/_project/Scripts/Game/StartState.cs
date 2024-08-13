using Assets._project.Scripts.Enemies.Boss;
using Assets._project.Scripts.Player;
using System.Collections.Generic;

namespace Assets._project.Scripts.Game
{
    public class StartState : IState
    {
        private Boss _boss;
        private PlayerControl _player;

        public StateMachine StateMachine { get; private set; }

        public StartState(StateMachine stateMachine, Boss boss, PlayerControl player)
        {
            StateMachine = stateMachine;
            _boss = boss;
            _player = player;
        }

        public void Enter()
        {
            _boss.SetPlayers(  );
            _boss.gameObject.SetActive(true);
            _boss.photonView.RPC(nameof(_boss.AddPlayer), Photon.Pun.RpcTarget.AllBuffered, _player.photonView.ViewID);
        }

        public void Exit()
        {

        }

        public void Update()
        {
            StateMachine.SetState<BattleState>();
        }
    }
}