using System.Collections;
using System.Collections.Generic;
using TanksEngine.Data;
using UnityEngine;
namespace TanksEngine.Combat
{
    [CreateAssetMenu(fileName = "New DamagesData", menuName = "GameData/DamagesData")]
    [System.Serializable]
    public class DamagesDataSO : ScriptableObject
    {
        public DamageDataSO[] Damages;
    }
    [System.Serializable]
    public struct DamageData
    {
        [Tooltip("Информация о типе атаки")]
        public DamageType DamageType;
        [Tooltip("Объект снаряда")]
        public GameObject[] Projectiles;
        [Tooltip("Время существования снаряда")]
        public float ProjectileLife;
        [Tooltip("Эффект, который будет применен на цели")]
        public EffectData HitEffect;
        [Tooltip("Базовый урон")]
        public float BaseDamage;
        [Tooltip("Базовая скорость")]
        public float BaseSpeed;
    }

}
