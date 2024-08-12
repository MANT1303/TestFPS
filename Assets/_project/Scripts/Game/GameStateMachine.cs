using Assets._project.Scripts.Enemies.Boss;
using Assets._project.Scripts.Game;
using Assets._project.Scripts.Player;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

//TODO: нужно здесь делать проверку на количество игроков и если кто-то отвалится, то обновлять список целей у босса
//      также этот класс должен решать проблемы с созданием игроков (возможно через другие классы
public class GameStateMachine : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Boss _boss;
    private List<PlayerControl> _players = new List<PlayerControl>();


    private StateMachine _fsm;

    private void Start()
    {
        _players.Add(PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity).GetComponentInChildren<PlayerControl>());

        _fsm = new StateMachine();

        _fsm.AddState(new StartState(_fsm, _boss, _players));
        _fsm.AddState(new BattleState(_fsm));
        _fsm.AddState(new WinState(_fsm, _players));

        _fsm.SetState<StartState>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        _boss.Dead += BossDead;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _boss.Dead -= BossDead;
    }
    private void BossDead()
    {
        _fsm.SetState<WinState>();
    }
    private void Update()
    {
        _fsm.Update();
    }

    [PunRPC]
    private void AddPLayer()
    {
    }
}
