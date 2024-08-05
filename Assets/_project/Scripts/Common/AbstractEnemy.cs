using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Common
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }

    }
}