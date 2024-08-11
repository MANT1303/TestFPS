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


        public override event Action Dead;
        public void SetPlayers(List<PlayerControl> controlsPlayers)
        {
            _stateMachine.ControlPlayers = controlsPlayers;
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
    }
}