using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class SniperRifle : Weapon
    {
        private Texture2D texture = Globals.Content.Load<Texture2D>("SniperBullet");
        public SniperRifle()
        {
            cooldown = 1f;
            maxAmmo = 1;
            Ammo = maxAmmo;
            reloadTime = 1f;
            Penetration = 5;
            Damage = 150;

        }

        protected override void CreateProjectile(Player player)
        {
            ProjectileData pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation,
                LifeSpan = 3f,
                Speed = 2500,
                Explosive = false,   
            };

            ProjectileManager.AddProjectiles(pd, texture);
        }

        
    }
}
