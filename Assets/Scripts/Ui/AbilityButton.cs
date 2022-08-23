using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TanksEngine.UI {
    public class AbilityButton : MonoBehaviour
    {
        private Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
        }
        public void OnEnable()
        {
            Global_EventManager.eventAbilityCooldown += OnAbilityCooldown;
        }
        private void OnDisable()
        {
            Global_EventManager.eventAbilityCooldown -= OnAbilityCooldown;
        }
        private void OnAbilityCooldown(float value)
        {
            Invoke(nameof(ResetButton), value);
        }
        private void ResetButton()
        {
            button.interactable = true;
        }
        public void ClickAbilityButton()
        {
            //button.interactable = false;
            Global_EventManager.CallOnUseAbility();
        }
    }
}


