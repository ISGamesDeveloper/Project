using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Gameplay.AbilitiesSystem 
{
    public class SpeedAbility : Ability
    {
        public float SpeedMod;
        protected override void ExecuteAbility(CharacterEntity activator)
        {

            activator.ChangeSpeed(SpeedMod);
            Debug.Log("Speed mod Enable");
        }
        public override void DeActivate(CharacterEntity activator)
        {
            activator.ChangeSpeed(activator.SpeedStandart);
            Debug.Log("Speed mod Disable");
        }
    }
}

