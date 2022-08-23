using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterEntity))]
public class CharacterStatsUI : MonoBehaviour
{
    public bool DisplayHpSlider;
    public bool DisplayHpDamage;
    public bool DisplayAmmo;
    public bool DisplayRage;

    public Slider SliderHP;
    public Slider SliderAmmo;
    public Slider SliderRage;
    public Text DamageCountText;
    private Animator DamageCountAnimator;
    private CharacterEntity characterEntity;


    private void Awake()
    {
        characterEntity = GetComponent<CharacterEntity>();
        if (DisplayHpDamage)
        {
            SetupDamageCountAnimator();
        }
        
    }
    public void SetupDamageCountAnimator()
    {
        if (DamageCountText != null)
        {
            DamageCountAnimator = DamageCountText.GetComponent<Animator>();
        }
    }
    public void UpdateUI()
    {
        if (DisplayHpSlider)
        {
            SliderHP.maxValue = characterEntity.Hp_Max;
            SliderHP.value = characterEntity.Cur_HP;
        }
        if (DisplayHpDamage)
        {
            DamageCountText.text = Mathf.Round(characterEntity.HP_LastDelta).ToString();
            DamageCountAnimator.SetTrigger("Start");
        }
        if (DisplayRage)
        {
            SliderRage.maxValue = characterEntity.Rage_Max;
            SliderRage.value = characterEntity.Rage_Cur;
        }

    }
}
