using TanksEngine.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Реализует отображение интерфейса танка игрока
/// </summary>
public class Player_UI : MonoBehaviour
{
    #region[Fields]
    [SerializeField] private Slider hp_Bar;
    [SerializeField] private Slider ammo_Bar;
    [SerializeField] private Toggle stealth_Toggle;
    [SerializeField] private Slider rage_Bar;
    private CharacterEntity player;
    #endregion
    #region[EventsSetup]
    private void OnEnable()
    {
        Global_EventManager.eventOnAttributeUpdate += HandlerOnAttributeUpdate;
        //Global_EventManager.eventOnAttributeUpdate += UpdateBars;
        Global_EventManager.eventOnPlayerLoaded += Init;
    }
    private void OnDisable()
    {
        Global_EventManager.eventOnAttributeUpdate -= HandlerOnAttributeUpdate;
       // Global_EventManager.eventOnAttributeUpdate -= UpdateBars;
        Global_EventManager.eventOnPlayerLoaded -= Init;
    }
    #endregion
    /// <summary>
    /// Получает ссылку на игрока
    /// </summary>
    /// <param name="_player"></param>
    private void Init(GameObject _player)
    {
        player = _player.GetComponent<CharacterEntity>();
        Debug.Log("init");
        UpdateRage();
        UpdateHP(); 
    }
    /// <summary>
    /// Обновляет бары аттрибутов
    /// </summary>
    private void UpdateBars(AttributeTypes type)
    {
        if (player != null)
        {
            UpdateRage();
            UpdateHP();
            UpdateStealth();
        }
    }
    /// <summary>
    /// Обрабатывает событие обновления атрибутов
    /// </summary>
    /// <param name="attr_ID">Тип атрибута</param>
    private void HandlerOnAttributeUpdate(AttributeTypes attr_ID)
    {
        switch (attr_ID)
        {
            case AttributeTypes.Health:
                UpdateHP();
                break;
            case AttributeTypes.Stealth:
                UpdateStealth();
                break;
            case AttributeTypes.Rage:
                UpdateRage();
                break;
        }
    }
    /// <summary>
    /// Обновляет переключатель скрытности
    /// </summary>
    private void UpdateStealth()
    {
        if (player != null)
        {
            stealth_Toggle.isOn = player.gameObject.GetComponent<ICanStealth>().IsStealthy;
        }
    }
    /// <summary>
    /// Обновляет бар хп
    /// </summary>
    public void UpdateHP() 
    {
        if (player != null)
        {
            hp_Bar.minValue = 0;
            hp_Bar.maxValue = player.Hp_Max;
            hp_Bar.value = player.Cur_HP;
        }
    }
    private void UpdateRage()
    {
        if (player != null)
        {
            hp_Bar.minValue = 0;
            rage_Bar.maxValue = player.Rage_Max;
            rage_Bar.value = player.Rage_Cur;
        }
    }
}
