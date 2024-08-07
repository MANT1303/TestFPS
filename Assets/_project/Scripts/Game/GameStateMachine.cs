using Assets._project.Scripts.Enemies;
using Assets._project.Scripts.Enemies.Boss;
using Assets._project.Scripts.Game;
using Assets._project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: нужно здесь делать проверку на количество игроков и если кто-то отвалится, то обновлять список целей у босса
//      также этот класс должен решать проблемы с созданием игроков (возможно через другие классы
public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private List<PlayerControl> _players;
    [SerializeField] private Boss _boss;



    private StateMachine _fsm;

    private void Start()
    {
        _fsm = new StateMachine();

        _fsm.AddState(new StartState(_fsm, _boss, _players));
        _fsm.AddState(new BattleState(_fsm));
        _fsm.AddState(new WinState(_fsm, _players));

        _fsm.SetState<StartState>();
    }
}
