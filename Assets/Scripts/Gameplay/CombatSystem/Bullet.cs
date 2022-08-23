using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace TanksEngine.Combat
{
    public class Bullet : MonoBehaviour
    {
        public DamageData damageData;
        public GameObject target;
        public Vector3 target_pos;
        public float speed;
        public bool PlayerBullet;//≈сли true то пул¤ не будет атаковать игрока
      
        private void OnTriggerEnter(Collider other)
        {
            if (AttackSystem.CanAttackTarget(PlayerBullet, other.gameObject))
            {
                AttackSystem.AttackTarget(other.transform.gameObject, damageData);
                Debug.Log($"Bullet attack: {other.name} with damage: {damageData.BaseDamage}");
                Destroy(gameObject);
            }
        }
    }
}
