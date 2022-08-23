using System.Collections;
using System.Collections.Generic;
using TanksEngine.Data;
using UnityEngine;
namespace TanksEngine.Gameplay.AbilitiesSystem
{

    public abstract class Ability:MonoBehaviour
    {
        public float DeactivateTime;
        public float CooldownTime;
        [SerializeField]
        protected EffectData effectData;
        public void Activate(CharacterEntity activator)
        {
            PlayEffects();
            ExecuteAbility(activator);
        }
        public virtual void DeActivate(CharacterEntity activator)
        {

        }
        private void PlayEffects()
        {
            Debug.Log("Ability effects");
            
        }
        protected virtual void ExecuteAbility(CharacterEntity activator)
        {

        }

    }



}
