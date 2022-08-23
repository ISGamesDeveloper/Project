using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TanksEngine.Character;
using UnityEngine;
using UnityEngine.Events;
namespace TanksEngine.Combat
{
    /// <summary>
    /// Реализует управление башней танка:
    /// прицеливание и стрельба
    /// </summary>
    public class TankTurret : MonoBehaviour
    {
        #region[Fields]
        [Header("Turret objects")]
        [Tooltip("Точка, в которой спавнится снаряд")]
        public GameObject PointForBullet;
        public Transform[] PointsForBullet;
        [Tooltip("Точка, в которую будет смотреть пушка в Idle состоянии")]
        public GameObject TurretIdlePoint;
        [Tooltip("Пивот по которому будет вращаться пушка")]
        public GameObject TurretRotationPoint;
        public LayerMask layerMaskForBullet;
        [Header("Events")]
        public UnityEvent OnShoot;

        //[HideInInspector]
        public List<GameObject> EnemyToAttack = new(5);

        private CharacterEntityController Controller;
        private GameObject cur_target;

        private float _timer = 0f;
        private float _shotFrequency = 0.3f;//Задержка между выстрелами

        public float range = 30;//LEGACY  брать будем из DamageData
        public float TurretSpeed = 60f;//LEGACY  брать будем из DamageData
        public float ShotFrequency = 0.2f;//LEGACY   брать будем из DamageData
        private bool AimingDone;

        private RaycastHit hit;
        private Vector3 direction;
        private Vector3 idledirection;
        private float _timerShoot;
        private float singleStep;
        private Vector3 targetDirection;
        private Vector3 newDirection;
        private float minDistance;
        private CharacterEntity Entity;

        #endregion
        private void Awake()
        {
            Controller = GetComponent<CharacterEntityController>();
            Entity = GetComponent<CharacterEntity>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _shotFrequency)
            {
                _timer -= _shotFrequency;

                if (EnemyToAttack.Count != 0) //&& !_Controller.isMoving
                {
                    cur_target = FindNearestTarget();
                }
                else
                {
                    cur_target = null;
                }
            }
            Aiming();
        }
        private GameObject FindNearestTarget()
        {
            GameObject newTarget = null;
            if (EnemyToAttack[0] != null)
            {
                float minDist = Vector3.Distance(transform.position, EnemyToAttack[0].transform.position);
                
                if (EnemyToAttack[0] != null) { newTarget = EnemyToAttack[0]; }
                foreach (var item in EnemyToAttack.ToList())
                {
                    if (item != null)
                    {
                        if (Vector3.Distance(transform.position, item.transform.position) < minDistance)
                        {
                            newTarget = item;
                        }
                    }
                }
            }
            else
            {
                EnemyToAttack.Clear();
            }
            return newTarget;
        }
        #region[Aiming and Shoot]
        /// <summary>
        /// Осуществляет прицеливание
        /// </summary>
        public void Aiming()
        {
            idledirection = TurretIdlePoint.transform.position;
            direction = TurretRotationPoint.transform.forward;

            //Если луч задел цель -> делаем выстрел
            if (Physics.Raycast(TurretRotationPoint.transform.position, TurretRotationPoint.transform.forward * range, out hit, range, layerMaskForBullet))
            {
                CharacterEntity targetComponent = hit.transform.GetComponent<CharacterEntity>();
                if (targetComponent != null)
                {
                    if (targetComponent.IsAlive)
                    {
                        Shoot();
                    }
                    AimingDone = true;
                }
            }
            else
            {
                AimingDone = false;
            }
            //Если есть цель -> поворачиваем башню в её сторону / !player._Controller.isMoving
            if (cur_target != null)
            {
                singleStep = TurretSpeed * Time.deltaTime;
                targetDirection = cur_target.transform.position - TurretRotationPoint.transform.position;
                newDirection = Vector3.RotateTowards(TurretRotationPoint.transform.forward, targetDirection, singleStep, 0.0f);
                TurretRotationPoint.transform.rotation = new Quaternion(0, Quaternion.LookRotation(newDirection).y, 0, Quaternion.LookRotation(newDirection).w);
            }
            else
            {
                singleStep = TurretSpeed * Time.deltaTime;
                targetDirection = TurretIdlePoint.transform.position - TurretRotationPoint.transform.position;
                newDirection = Vector3.RotateTowards(TurretRotationPoint.transform.forward, targetDirection, singleStep, 0.0f);
                TurretRotationPoint.transform.rotation = new Quaternion(0, Quaternion.LookRotation(newDirection).y, 0, Quaternion.LookRotation(newDirection).w);
            }
        }
        /// <summary>
        /// Обрабатывает выстрел
        /// </summary>
        private void Shoot()
        {
            _timerShoot += Time.deltaTime;

            if (_timerShoot > ShotFrequency)
            {
                _timerShoot -= ShotFrequency;
                if (!Controller.isMoving)
                {
                    AttackSystem.CreateProjectiles(PointsForBullet, Entity.combatProfile);
                    OnShoot?.Invoke();
                    Entity.ChangeRage(5);

                    Debug.Log(cur_target, Controller);

                }
            }
        }
        #endregion
    }
}
