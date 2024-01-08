using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models.Weapons
{
    public abstract class Weapon
    {
        protected float cooldown;
        public float cooldownRemaining;
        public int maxAmmo;
        public int Ammo { get; protected set; }
        protected float reloadTime;
        public bool Reloading { get; protected set; }
        public int Penetration { get; protected set; }
        public int Damage {get; protected set; }


        protected Weapon()
        {
            cooldownRemaining = 0f;
            Reloading = false;
            Penetration = 0;
            Damage = 1;
        }
        public virtual void Reload()
        {
            if (Reloading || Ammo == maxAmmo) return;
            cooldownRemaining = reloadTime;
            Reloading = true;
            Ammo = maxAmmo;

        }
        protected abstract void CreateProjectile(Player player);
        public virtual void Fire(Player player)
        {
            if (cooldownRemaining > 0 || Reloading) return;

            Ammo--;
            if (Ammo > 0)
            {
                cooldownRemaining = cooldown;
            }
            else
            {
                Reload();
            }
            CreateProjectile(player);

        }
        public virtual void Update()
        {
            if (cooldownRemaining > 0)
            {
                cooldownRemaining -= Globals.TotalSeconds;
            }
            else if (Reloading)
            {
                Reloading = false;
            }

        }
    }


}
