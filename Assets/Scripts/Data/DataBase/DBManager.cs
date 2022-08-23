using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TanksEngine.Data
{
    public static class DBManager
    {
        public static InGameDataBaseSO dataBase;
        private static Sprite FindElement(string name, List<SpriteElement> list)
        {
            foreach (var item in list)
            {
                if (item.name == name)
                {
                    return item.sprite;
                }
            }
            return dataBase.EmptySprite;
        }
        public static Sprite FindSprite(string name, DBElementType elementType)
        {

            if (dataBase == null)
            {
                SetupDB();
            }
            Sprite sprite = dataBase.EmptySprite;
            switch (elementType)
            {
                case DBElementType.Commander:
                    sprite = FindElement(name, dataBase.DBSprites.CommandersSprites);
                    break;
                case DBElementType.Main:
                    sprite = FindElement(name, dataBase.DBSprites.MainDetailsSprites);
                    break;
                case DBElementType.Suspension:
                    sprite = FindElement(name, dataBase.DBSprites.SuspensionDetailsSprites);
                    break;
                case DBElementType.Turret:
                    sprite = FindElement(name, dataBase.DBSprites.TurretDetailsSprites);
                    break;
                case DBElementType.Engine:
                    sprite = FindElement(name, dataBase.DBSprites.EngineDetailsSprites);
                    break;
                case DBElementType.Tank:
                    sprite = FindElement(name, dataBase.DBSprites.TanksSprites);
                    break;
                default:
                    break;
            }
            return sprite;
        }
        private static void SetupDB()
        {
            dataBase = Resources.Load<InGameDataBaseSO>("GlobalDB/DataBase");
        }
    }

}
