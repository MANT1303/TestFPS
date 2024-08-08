using System;
using UnityEngine;

namespace Assets._project.Scripts.Common
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }

        public abstract event Action Dead;

        public abstract void Takedamage(float damage);

    }
}