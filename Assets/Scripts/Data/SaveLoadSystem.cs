using System.Collections;
using System.Collections.Generic;
using System.IO;
using TanksEngine.Combat;
using TanksEngine.Data;
using UnityEngine;

namespace TanksEngine.SaveLoad
{
    /// <summary>
    /// Реализует сохранение и загрузку игровых данных.
    /// </summary>
    public static class SaveLoadSystem
    {
        public static void Load_GameData(string saveFilePath, Main_Data MainData)
        {
            if (File.Exists(saveFilePath))  //Если существует сохранение, то загрузить его
            {
                Debug.Log("Load game");
                try
                {
                    string json = File.ReadAllText(saveFilePath);
                    SaveData data = JsonUtility.FromJson<SaveData>(json);

                    GameData.AngarSlots = data.AngarSlots;
                    GameData.AngarSlotsCount = data.AngarSlotsCount;
                    GameData.CurSlot = data.CurSlot;
                }
                catch
                {
                    Debug.LogError("Can't load save_data file");
                }
            }
            else
            {
                Debug.Log("Save file not finded");
                int idx = 0;
                GameData.InitAngarSlots();
                while (idx < MainData.AngarSlots)
                {
                    GameData.Init();
                    GameData.AngarSlotsCount = MainData.AngarSlots;

                    //Заполняем данные(списки деталей для выбора) для игрока из бд (json файл)
                    foreach (Detail_Main_Element item in MainData.Details_Main)
                    {
                        GameData.AngarSlots[idx].detailMainList.Add(item.detail_main);
                    }
                    foreach (Detail_Engine_Element item in MainData.Details_Engine)
                    {
                        GameData.AngarSlots[idx].detailEngineList.Add(item.detail_engine);
                    }
                    foreach (Detail_Suspension_Element item in MainData.Details_Suspension)
                    {
                        GameData.AngarSlots[idx].detailSuspensionList.Add(item.detail_suspension);
                    }
                    foreach (Detail_Turret_Element item in MainData.Details_Turret)
                    {
                        GameData.AngarSlots[idx].detailTurretList.Add(item.detail_turret);
                    }
                    foreach (Commander_Element item in MainData.Commanders)
                    {
                        GameData.AngarSlots[idx].commanderList.Add(item.commander);
                    }
                    idx++;
                    
                }
                SetupTestTanks();
                
            }
            Global_EventManager.CallOnLoadGame();
        }
        private static void SetupTankData(PlayerTankData newData, int idx)
        {
            PlayerTankData oldData = GameData.AngarSlots[idx];

            oldData.slotType = newData.slotType;
            oldData.tankDataStruct.detailMain = newData.tankDataStruct.detailMain;
            oldData.tankDataStruct.detailEngine = newData.tankDataStruct.detailEngine;
            oldData.tankDataStruct.detailSuspension = newData.tankDataStruct.detailSuspension;
            oldData.tankDataStruct.detailTurret = newData.tankDataStruct.detailTurret;

            var damage = Resources.Load<DamagesDataSO>("DamagesData");
            oldData.damageData = damage.Damages[idx].Damage;

            GameData.AngarSlots[idx] = oldData;
        }
        private static void SetupTestTanks()
        {
            int tempindx = 0;

            PlayerTankData newData = GameData.AngarSlots[tempindx];
            newData.slotType = AngarSlotType.LightTank;
            newData.tankDataStruct.detailMain = GameData.AngarSlots[tempindx].detailMainList[0];
            newData.tankDataStruct.detailEngine = GameData.AngarSlots[tempindx].detailEngineList[0];
            newData.tankDataStruct.detailSuspension = GameData.AngarSlots[tempindx].detailSuspensionList[0];
            newData.tankDataStruct.detailTurret = GameData.AngarSlots[tempindx].detailTurretList[0];
            SetupTankData(newData, 0);

            newData.slotType = AngarSlotType.MediumTank;
            newData.tankDataStruct.detailMain = GameData.AngarSlots[tempindx].detailMainList[1];
            newData.tankDataStruct.detailEngine = GameData.AngarSlots[tempindx].detailEngineList[1];
            newData.tankDataStruct.detailSuspension = GameData.AngarSlots[tempindx].detailSuspensionList[1];
            newData.tankDataStruct.detailTurret = GameData.AngarSlots[tempindx].detailTurretList[1];
            SetupTankData(newData, 1);

            newData.slotType = AngarSlotType.HeavyTank;
            newData.tankDataStruct.detailMain = GameData.AngarSlots[tempindx].detailMainList[2];
            newData.tankDataStruct.detailEngine = GameData.AngarSlots[tempindx].detailEngineList[1];
            newData.tankDataStruct.detailSuspension = GameData.AngarSlots[tempindx].detailSuspensionList[2];
            newData.tankDataStruct.detailTurret = GameData.AngarSlots[tempindx].detailTurretList[1];
            SetupTankData(newData, 2);
        }
        public static void Save_GameData(string saveFilePath , bool test = false)
        {
            //Debug.Log("Save game");
            //SaveData savdata = new SaveData();
            //savdata.Details_Main = GameData.AngarSlots[GameData.CurSlot].detailMainList;
            //savdata.Commanders = GameData.AngarSlots[GameData.CurSlot].commanderList;
            //savdata.AngarSlots = GameData.AngarSlots;
            //savdata.CurSlot = GameData.CurSlot;
            //savdata.AngarSlotsCount = GameData.AngarSlotsCount;
            //try
            //{
            //    string json = JsonUtility.ToJson(savdata, true);
            //    if (test)
            //    {
            //        File.WriteAllText(Application.dataPath + @"\TestSave.txt", json);
            //    }
            //    else
            //    {
            //        File.WriteAllText(saveFilePath, json);
            //    }

            //}
            //catch
            //{
            //    Debug.LogError("Can't save data file");
            //}
        }
    }

}
