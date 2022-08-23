using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Combat 
{
    public class PlayerAttackZone : MonoBehaviour
    {
        public TankTurret _Combat;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _Combat.EnemyToAttack.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                try
                {
                    _Combat.EnemyToAttack.Remove(other.gameObject);

                }
                catch
                {
                    Debug.LogError("Cant remove enemey in player");
                }
            }
        }
    }
}

