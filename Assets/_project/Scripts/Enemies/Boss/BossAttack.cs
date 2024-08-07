using Assets._project.Scripts.Common;
using Assets._project.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.Scripts.Enemies.Boss
{
    public class BossAttack : MonoBehaviour
    {
        [SerializeField] private AbstractAttack _attack;
        [SerializeField] private float _pauseBetweenAttack;
        private float _currentTime;
        private List<PlayerControl> _players;
        private PlayerControl _currentPlayer;

        private void OnValidate()
        {
            if(_attack == null) 
                _attack = GetComponentInChildren<AbstractAttack>();
        }
        public void Attack()
        {
            _currentTime += Time.deltaTime;
            transform.LookAt(Vector3.Lerp(transform.position, _currentPlayer.transform.position,_currentTime),Vector3.up);

            if (_currentTime > _pauseBetweenAttack)
            {
                _currentTime = 0;
                _attack.Attack(transform.forward);
                _currentPlayer = _players[Random.Range(0, _players.Count)];
            }
        }
        public void Wait()
        {

        }
        public void SetTargetAttack(List<PlayerControl> players)
        {
            _players = players;
            _currentPlayer = _players[Random.Range(0, _players.Count)];
        }
    }
}