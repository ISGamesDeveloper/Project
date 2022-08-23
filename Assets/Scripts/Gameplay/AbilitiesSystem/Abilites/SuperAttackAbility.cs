using System.Collections;
using System.Collections.Generic;
using TanksEngine.Combat;
using UnityEngine;

namespace TanksEngine.Gameplay.AbilitiesSystem
{
    public class SuperAttackAbility : Ability
    {
        public DamageDataSO damageData;
        protected override void ExecuteAbility(CharacterEntity activator)
        {

            CombatProfile combatProfile = new();
            combatProfile.damageData = damageData;
            AttackSystem.CreateProjectiles(activator.GetComponent<TankTurret>().PointsForBullet, combatProfile);
        }
        public override void DeActivate(CharacterEntity activator)
        {

        }
    }
}

