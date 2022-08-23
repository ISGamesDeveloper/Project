using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TanksEngine.Data.PlayerTankData;

namespace TanksEngine.AngarsSytem
{
    public static class AngarsSystem
    {
        public struct AngarSlot
        {
            public List<Tank_Detail> Details_Main;
            public List<Tank_Detail> Details_Turret;
            public List<Tank_Detail> Details_Engine;
            public List<Tank_Detail> Details_Suspension;
            public List<Commander> Commanders;
        }

        public static void SelectPrevAngarSlot()
        {
            if (GameData.CurSlot > 0)
            {
                SetCurrentAngarSlot(GameData.CurSlot - 1);
            }
            else
            {
                while (GameData.CurSlot < 0)
                {
                    SetCurrentAngarSlot(GameData.CurSlot + 1);
                }
            }
        }
        public static void SelectNextAngarSlot()
        {
            if (GameData.CurSlot + 1 < GameData.AngarSlotsCount)
            {
                SetCurrentAngarSlot(GameData.CurSlot + 1);
            }
            else
            {
                while (GameData.CurSlot >= GameData.AngarSlotsCount)
                {
                    SetCurrentAngarSlot(GameData.CurSlot - 1);
                }
            }
        }
        public static void SetCurrentAngarSlot(int idx)
        {
            GameData.CurSlot = idx;
        }
        public static AngarSlot GetCurrentAngarSlot()
        {
            AngarSlot CurrentAngarSlot = new();
            if (GameData.AngarSlots[GameData.CurSlot].commanderList != null)
            {
                CurrentAngarSlot.Details_Main = GameData.AngarSlots[GameData.CurSlot].detailMainList;
                CurrentAngarSlot.Details_Turret = GameData.AngarSlots[GameData.CurSlot].detailTurretList;
                CurrentAngarSlot.Details_Engine = GameData.AngarSlots[GameData.CurSlot].detailEngineList;
                CurrentAngarSlot.Details_Suspension = GameData.AngarSlots[GameData.CurSlot].detailSuspensionList;
            }
            return CurrentAngarSlot;
        } 
    }
}
