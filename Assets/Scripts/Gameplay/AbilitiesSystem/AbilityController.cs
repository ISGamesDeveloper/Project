using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Gameplay.AbilitiesSystem
{
    public abstract class AbilityController : MonoBehaviour, ICanUseAbility
    {
        protected CharacterEntity characterEntity;
        private void Awake()
        {
            characterEntity = GetComponent<CharacterEntity>();
        }
        public virtual void ActivateAbility(Ability _ability)
        {
            _ability.Activate(characterEntity);
            Invoke(nameof(DeActivateAbility), _ability.DeactivateTime);
            Global_EventManager.CallOnAbilityCooldown(_ability.CooldownTime);
        }
        public virtual void DeActivateAbility()
        {

        }
    }

}
