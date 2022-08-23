using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsUISpawner : MonoBehaviour
{
    public GameObject StatsUI;

    private void Start()
    {
        var entity = GetComponent<CharacterEntity>();
        var obj = GameObject.Instantiate(StatsUI);
        var comp = GetComponent<CharacterStatsUI>();
        comp.DisplayHpDamage = true;
        comp.DisplayRage = true;
        comp.DisplayHpSlider = false;
        GetComponent<CharacterStatsUI>().DamageCountText = obj.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        GetComponent<CharacterStatsUI>().SetupDamageCountAnimator();


    }
}
