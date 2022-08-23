using TanksEngine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TanksEngine.Combat;

[RequireComponent(typeof(CharacterEntity))]
/// <summary>
/// Управляет деталями танка.
/// </summary>
public class TankDetailsManager : MonoBehaviour
{
    [Header("Tank models")]
    public GameObject ModelDetailMain;
    public GameObject ModelDetailEngine;
    public GameObject ModelDetailSuspension;
    public GameObject ModelDetailTurret;
    [HideInInspector]
    public TankDataStruct tankDataStruct;
    private CharacterEntity entity;
    //public int coins;

    private void Start()
    {
        entity = GetComponent<CharacterEntity>();
        if (entity.IsLocalPlayer)
        {
            Global_EventManager.CallOnPlayerLoaded(gameObject);
            GetTankData();
            SetModulesToTank();
        }
    }
    /// <summary>
    /// Получаем структуру с деталями
    /// </summary>
    private void GetTankData()
    {
        if (GameData.AngarSlots.Count > 0)
        {
            tankDataStruct = GameData.AngarSlots[GameData.CurSlot].tankDataStruct;
        }

    }
    /// <summary>
    /// Меняем модельки и материалы деталей на танке
    /// </summary>
    private void SetModulesToTank()
    {
        CombatProfile newCombatProfile = new();
        newCombatProfile.damageData = new();
        newCombatProfile.damageData.Damage = GameData.AngarSlots[GameData.CurSlot].damageData;
        GetComponent<CharacterEntity>().combatProfile = newCombatProfile;

        ModelDetailMain.GetComponent<MeshFilter>().mesh = tankDataStruct.detailMain.Detail_mesh;
        ModelDetailMain.GetComponent<MeshRenderer>().material = tankDataStruct.detailMain.Detail_material;

        ModelDetailEngine.GetComponent<MeshFilter>().mesh = tankDataStruct.detailEngine.Detail_mesh;
        ModelDetailEngine.GetComponent<MeshRenderer>().material = tankDataStruct.detailEngine.Detail_material;

        ModelDetailSuspension.GetComponent<MeshFilter>().mesh = tankDataStruct.detailSuspension.Detail_mesh;
        ModelDetailSuspension.GetComponent<MeshRenderer>().material = tankDataStruct.detailSuspension.Detail_material;

        ModelDetailTurret.GetComponent<MeshFilter>().mesh = tankDataStruct.detailTurret.Detail_mesh;
        ModelDetailTurret.GetComponent<MeshRenderer>().material = tankDataStruct.detailTurret.Detail_material;
    }
}
