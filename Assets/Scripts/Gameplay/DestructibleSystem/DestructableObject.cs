using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Gameplay.DestructableSystem
{
    public class DestructableObject : MonoBehaviour
    {
        #region[Fields]
        [Header("Settings")]
        public bool ActivateOnEnterTank;
        public bool ActivateOnEnterBullet;
        public float Health;
        public float DamageFromTank;
        public float DamageFromBullet;

        [Header("Switching")]
        public bool SwitchObject;
        public GameObject OldObject;
        public GameObject NewObject;

        [Header("Destroying")]
        public bool DestroySelf;
        public float DeathDelay;

        [Header("Particles")]
        public bool PlayParticle;
        public bool PlayParticleOnDeath;
        public ParticleSystem particle;
        public Vector3 ParticleOffset;
        public ParticleSystem particleOnDeath;
        public Vector3 ParticleOffsetOnDeath;

        [Header("Animations")]
        public bool PlayAnimation;
        public bool PlayAnimationOnDeath;
        public string AnimTriggerName;
        public string AnimTriggerNameOnDeath;

        private bool IsDestroyed = false;
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (!IsDestroyed && CanDestruct(other.transform))
            {
                PlayEffects();
                CalculateDamage(other.transform);
            }
        }
        private void CalculateDamage(Transform _attacker)
        {
            if (_attacker.transform.tag == "Bullet")
            {
                Health -= DamageFromBullet;
            }
            else
            {
                Health -= DamageFromTank;
            }
            if (Health <= 0)
            {
                IsDestroyed = true;
                Death();
            }
        }
        private void PlayEffects()
        {
            if (PlayParticle)
            {
                ParitcleEffect(particle, ParticleOffset);
            }
            if (PlayAnimation)
            {
                AnimationEffect(AnimTriggerName);
            }
        }
        private bool CanDestruct(Transform _attacker)
        {
            var objTag = _attacker.tag;
            if (objTag == "Player" || objTag == "Enemy" || objTag == "Bullet")
            {
                if (objTag == "Bullet" & !ActivateOnEnterBullet)
                {
                    return false;
                }
                if (objTag != "Bullet" & !ActivateOnEnterTank)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #region[Effects methods]
        private void ParitcleEffect(ParticleSystem _particle, Vector3 _pos)
        {
            var obj = GameObject.Instantiate(_particle, transform);
            obj.GetComponent<ParticleSystem>().Play();
            obj.transform.localPosition = _pos;
        }
        private void AnimationEffect(string _trigName)
        {
            GetComponent<Animator>().SetTrigger(_trigName);
        }

        #endregion
        public void Death()
        {
            if (PlayParticleOnDeath)
            {
                ParitcleEffect(particleOnDeath, ParticleOffsetOnDeath);
            }
            if (PlayAnimationOnDeath)
            {
                AnimationEffect(AnimTriggerNameOnDeath);
            }
            if (SwitchObject)
            {
                OldObject.SetActive(false);
                NewObject.SetActive(true);
            }
            if (DestroySelf)
            {
                GameObject.Destroy(gameObject, DeathDelay);
            }
        }
    }
}
