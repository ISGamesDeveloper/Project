using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Combat
{
    public class EnemyCombatController : CombatController
    {
        void Awake()
        {
            base.characterEntity = GetComponent<CharacterEntity>();
        }
    }
}
