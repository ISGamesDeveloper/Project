using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksEngine.Combat
{
    public interface ICombat
    {
        public void TakeHit(float damage, DamageType damageType = default);
    }
}
