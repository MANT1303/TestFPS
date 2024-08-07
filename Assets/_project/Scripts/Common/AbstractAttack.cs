using System;
using System.Collections;
using UnityEngine;

namespace Assets._project.Scripts.Common
{
    public abstract class AbstractAttack : MonoBehaviour
    {
        [SerializeField] protected Transform _pointFromAttack;
        public virtual event Action SuccessedAttack;
        public abstract void Attack(Vector3 direction);

    }
}