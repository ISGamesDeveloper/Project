using System.Collections;
using System.Collections.Generic;
using TanksEngine.Character;
using UnityEngine;
/// <summary>
/// Реализует систему скрытности
/// </summary>
public class PlayerStealthController : MonoBehaviour, ICanStealth
{
    public bool IsStealthy { get; set; }
    private CharacterEntity characterEntity; 
    private void Awake()
    {
        characterEntity = GetComponent<CharacterEntity>();
    }
    public void DisableStealt()
    {
        IsStealthy = false;
        if (characterEntity.IsLocalPlayer)
        {
            Global_EventManager.CallOnAttributeUpdate(AttributeTypes.Stealth);
        } 
    }
    public void EnableStealt()
    {
        IsStealthy = true; 
        if (characterEntity.IsLocalPlayer)
        {
            Global_EventManager.CallOnAttributeUpdate(AttributeTypes.Stealth);
        }
          
    }

}
