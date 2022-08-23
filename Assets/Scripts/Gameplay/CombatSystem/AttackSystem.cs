using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Combat
{
    /// <summary>
    /// Реализует боевую систему.
    /// </summary>
    public static class AttackSystem
    {
        #region[Fields]
        private static float damageModifer = 1.1f;
        #endregion
        #region[Attack/Projectiles]
        /// <summary>
        /// Создает выстрелы
        /// </summary>
        /// <param name="cur_target">Цель</param>
        /// <param name="PointForBullet">Точка спавна снаряда</param>
        /// <param name="enemyProfile">Профиль бота</param>
        public static void CreateProjectiles( Transform[] PointForBullet, CombatProfile combatProfile)
        {
            for (int i = 0; i < (combatProfile.damageData.Damage.Projectiles.Length); i++)
            {
                Quaternion direction = PointForBullet[i].rotation;
                DamageData damageData = combatProfile.damageData.Damage;
                Transform bullet = GameObject.Instantiate(damageData.Projectiles[i], PointForBullet[i].transform.position, Quaternion.identity).transform;
                Bullet bulletComp = bullet.GetComponent<Bullet>();
                bulletComp.PlayerBullet = false;
                bulletComp.damageData = combatProfile.damageData.Damage;

                bullet.rotation = (direction);
                SetupProjectile(bulletComp, bullet, PointForBullet[i], damageData.ProjectileLife, damageData.BaseSpeed);
            }
        }
        /// <summary>
        /// Задает направление снаряда
        /// </summary>
        /// <param name="comp">Скрипт снаряда</param>
        /// <param name="bullet">Трансформ снаряда</param>
        /// <param name="cur_target">Цель для направления снаряда</param>
        /// <param name="PointForBullet">Точка из которой летит снаряд</param>
        private static void SetupProjectile(Bullet bulletComp, Transform bullet, Transform PointForBullet,float projectileLife, float speed)
        {
            //bulletComp.target_pos = cur_target.transform.position;
            bullet.position = new Vector3(bullet.position.x, 1, bullet.transform.position.z);
            
            Vector3 dir = PointForBullet.forward;
            bullet.transform.GetComponent<Rigidbody>().AddForce(PointForBullet.forward * speed, ForceMode.Impulse);

            GameObject.Destroy(bullet.gameObject, projectileLife);
        }
        #endregion
        #region[Damage]
        /// <summary>
        /// Вычисляет и наносит урон выбранной цели
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        public static void AttackTarget(GameObject target, DamageData damage)
        {
            #region[PlayEffects]
            var _particles = damage.HitEffect.effectsData.effectParticles.particles;

            foreach (var item in _particles)
            {
                target.GetComponent<ICanTakeEffect>().PlayEffect(item);
            }
            #endregion

            #region[DamageCalculate]
            float damageAmount = GetDamageAmount(damage.BaseDamage);
            #endregion

            #region[ChangeTargetHp]
            target.GetComponent<ICombat>().TakeHit(damageAmount * -1, damage.DamageType);
            #endregion
        }
        private static float GetDamageAmount(float BaseDamage)
        {
            float damageAmount = BaseDamage * damageModifer;
            return damageAmount;
        }
        #endregion

        /// <summary>
        /// Выдает разрешение на атаку цели.
        /// </summary>
        /// <param name="PlayerBullet"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool CanAttackTarget(bool PlayerBullet,GameObject target)
        {
            if (target.GetComponent<ICombat>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

