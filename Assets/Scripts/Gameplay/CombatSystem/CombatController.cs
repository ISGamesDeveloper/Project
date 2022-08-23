using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Combat 
{
    [RequireComponent(typeof(CharacterEntity))]
    public abstract class CombatController : MonoBehaviour, ICombat
    {
        protected CharacterEntity characterEntity { get; set; }

        public virtual void TakeHit(float damage, DamageType damageType = DamageType.LightProjectile)
        {
            characterEntity.ChangeCurHP(damage);
        }

        protected void Awake()
        {
            characterEntity = GetComponent<CharacterEntity>();
        }
    }
}

