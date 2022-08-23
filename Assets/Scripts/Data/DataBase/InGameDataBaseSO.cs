using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New GlobalDataBase", menuName = "GameData/GlobalDataBase")]
    public class InGameDataBaseSO : ScriptableObject
    {
        public SpritesList DBSprites;
        public Sprite EmptySprite;
    }
    [System.Serializable]
    public struct SpritesList
    {
        public List<SpriteElement> CommandersSprites;
        public List<SpriteElement> MainDetailsSprites;
        public List<SpriteElement> EngineDetailsSprites;
        public List<SpriteElement> SuspensionDetailsSprites;
        public List<SpriteElement> TurretDetailsSprites;
        public List<SpriteElement> TanksSprites;
    }
    [System.Serializable]
    public struct SpriteElement
    {
        public string name;
        public Sprite sprite;
    }
    

}

