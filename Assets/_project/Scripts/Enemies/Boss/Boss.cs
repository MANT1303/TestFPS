using Assets._project.Scripts.Common;
using Assets._project.Scripts.Player;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._project.Scripts.Enemies.Boss
{
    public class Boss : AbstractEnemy
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private BossStateMachine _stateMachine;
        private List<PlayerControl> _players = new List<PlayerControl>();


        public override event Action Dead;
        public void SetPlayers()
        {
            _stateMachine.ControlPlayers = _players;
        }
        private void Start()
        {
            MaxHealth = _maxHealth;
            _slider.maxValue = MaxHealth;
            Health = MaxHealth;
            _slider.value = Health;
        }
        public override void Takedamage(float damage)
        {
            Health -= damage;
            photonView.RPC(nameof(HealthChanged), RpcTarget.All, Health);
            if (Health < 0)
            {
                _stateMachine.BossDead();
                Dead?.Invoke();
            }
        }
        [PunRPC]
        private void HealthChanged(float health)
        {
            Health = health;
            _slider.value = health;
        }
        private void OnValidate()
        {
            if (_stateMachine == null)
            {
                _stateMachine = GetComponentInChildren<BossStateMachine>();
            }
            if (_slider == null)
                _slider = GetComponentInChildren<Slider>();
        }

        [PunRPC]
        public void AddPlayer(int playerViewID)
        {
            // Находим игрока по ViewID и добавляем его в список
            PhotonView playerView = PhotonView.Find(playerViewID);
            if (playerView != null)
            {
                PlayerControl playerComponent = playerView.GetComponent<PlayerControl>();
                if (playerComponent != null)
                {
                    _players.Add(playerComponent);
                }
            }
        }
    }
}