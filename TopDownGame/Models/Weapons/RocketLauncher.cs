using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class RocketLauncer : Weapon
    {
        private Texture2D texture = Globals.Content.Load<Texture2D>("bullet");
        public RocketLauncer()
        {
            cooldown = 0.60f;
            maxAmmo = 3;
            Ammo = maxAmmo;
            reloadTime = 1f;
            Penetration = 0;
            Damage = 5000;
        }
        protected override void CreateProjectile(Player player)
        {
            ProjectileData pd = new()
            {
                Position = player.Position,
                Speed = 350,
                LifeSpan = 1f,
                Rotation = player.Rotation,
                Explosive = true,
            };
            ProjectileManager.AddProjectiles(pd, texture);
            

        }
    }
}
