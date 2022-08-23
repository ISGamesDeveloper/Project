using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksEngine.Gameplay.AbilitiesSystem;
using UnityEngine;

namespace TanksEngine.Combat
{
    [CreateAssetMenu(fileName = "New EnemyProfile", menuName = "GameData/EnemyProfile")]
    public class CombatProfile : ScriptableObject
    {
        public DamageDataSO damageData;
       
    }
}
