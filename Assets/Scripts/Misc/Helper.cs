using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksEngine.Combat;
using UnityEngine;

namespace TanksEngine.Misc
{
    public static class Helper
    {
        public static ParticleSystem FindParticle(DamageType damageType)
        {
            ParticleSystem _particle = null;

            foreach (var item in GameData.damagesDataSO.Damages)
            {
                if (item.Damage.DamageType == damageType)
                {
                    //Партиклы искать в effecData
                     //_particle = item.HitEffect;
                }
            }
            return _particle;
        }
        //public static ParticleSystem[] FindParticles(DamageData damage)
        //{
        //    ParticleSystem _particle[] = null;

        //    foreach (var item in damage.HitEffect.effectsData.effectParticles.particles)
        //    {
                
        //    }
        //    return _particle;
        //}
    }
}
