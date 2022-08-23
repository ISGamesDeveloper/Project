using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Combat 
{
    [CreateAssetMenu(fileName = "New Damage", menuName = "GameData/Damage")]
    public class DamageDataSO : ScriptableObject
    {
        public DamageData Damage;
    }
}

