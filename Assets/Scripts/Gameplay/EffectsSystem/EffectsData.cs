using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace TanksEngine.Data 
{
    [CreateAssetMenu(fileName = "New EffectsData", menuName = "GameData/EffectsData")]
    public class EffectsData : ScriptableObject
    {
        public EffectData[] effectsData;
    }

    [System.Serializable]
    public struct EffectsStruct
    {
        public bool EnableParticles;
        public EffectParticle effectParticles;
        public bool EnableAudios;
        public EffectAudio effectAudios;
        public bool EnableAnimations;
        public EffectAnim effectAnimations;
    }
    [System.Serializable]
    public struct EffectParticle
    {
        public ParticleSystem[] particles;
    }
    [System.Serializable]
    public struct EffectAudio
    {
        public AudioClip[] audioClips;
    }
    [System.Serializable]
    public struct EffectAnim
    {
        public string[] animTriggers;
    }
    [System.Serializable]
    public enum EffectType
    {
        SmallHit,
        Heal,
    }
}


