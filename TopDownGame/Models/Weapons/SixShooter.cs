using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class SixShooter : Weapon
    {
        private Texture2D texture = Globals.Content.Load<Texture2D>("bullet");

        public SixShooter() 
        {
            cooldown = 0.1f;
            maxAmmo = 6;
            Ammo = 5;
            reloadTime = 1.3f;
            Penetration = 2;
            Damage = 25;
        }

        protected override void CreateProjectile(Player player)
        {
            ProjectileData pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation,
                LifeSpan = 2f,
                Speed = 1000,
                Explosive = false,
            };

            ProjectileManager.AddProjectiles(pd, texture);
        }
    }
}
