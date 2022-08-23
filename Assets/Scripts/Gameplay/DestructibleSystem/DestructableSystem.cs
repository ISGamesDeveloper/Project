using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Gameplay.DestructableSystem
{
    public static class DestructableSystem 
    {
        public static bool CanDestruct(Transform attacker,bool ActivateOnEnterBullet, bool ActivateOnEnterTank)
        {
            var objTag = attacker.tag;
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
    }

}
