using TanksEngine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TanksEngine.Combat;
/// <summary>
/// Внутриигровая база данных
/// </summary>
public static class GameData
{
    public static int AngarSlotsCount { get; set; }
    public static int CurSlot { get; set; }
    public static DamagesDataSO damagesDataSO { get; private set; }

    public static List<PlayerTankData> AngarSlots { get; set; }
    public static void InitAngarSlots()
    {
        AngarSlots = new List<PlayerTankData>(5);
    }
    public static void Init()
    {
        
        InitTanksData();
        InitGlobalData();

    }
    private static void InitTanksData()
    {
        PlayerTankData a = new();

        a.commanderList = new();
        a.detailEngineList = new();
        a.detailMainList = new();
        a.detailSuspensionList = new();
        a.detailTurretList = new();
        a.tankDataStruct = new();

        AngarSlots.Add(a);
    }
    private static void InitGlobalData()
    {
        damagesDataSO = Resources.Load<DamagesDataSO>("DamagesData");
    }
}
