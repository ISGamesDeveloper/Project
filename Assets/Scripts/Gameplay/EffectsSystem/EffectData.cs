
using UnityEngine;

namespace TanksEngine.Data
{
    [CreateAssetMenu(fileName = "New Effect", menuName = "GameData/Effect")]
    public class EffectData : ScriptableObject
    {
        public EffectType effectType;
        public EffectsStruct effectsData;
    }
}
