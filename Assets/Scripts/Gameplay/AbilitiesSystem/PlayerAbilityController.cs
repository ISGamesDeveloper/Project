using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Gameplay.AbilitiesSystem
{
    
    public class PlayerAbilityController : AbilityController
    {
        private Ability ability;
        public void OnEnable()
        {
            ability = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.ability;
            Global_EventManager.eventUseAbility += UseAbility;
        }
        private void OnDisable()
        {
            Global_EventManager.eventUseAbility -= UseAbility;
        }

        private void UseAbility()
        {
            if (characterEntity.Rage_Cur >= 10)
            {
                ActivateAbility(ability);
                characterEntity.ResetRage();
            }
            
        }
        public override void ActivateAbility(Ability _ability)
        {
            if (characterEntity.IsLocalPlayer)
            {
                _ability.Activate(characterEntity);
                Invoke(nameof(DeActivateAbility), _ability.DeactivateTime);
                Global_EventManager.CallOnAbilityCooldown(_ability.CooldownTime);
            }
           
        }
        public override void DeActivateAbility()
        {
            if (characterEntity.IsLocalPlayer)
            {
                ability.DeActivate(characterEntity);
            }
        }
    }

}
