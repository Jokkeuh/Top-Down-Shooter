using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class Shotgun :Weapon
    {
        private Texture2D texture = Globals.Content.Load<Texture2D>("bullet");
        public Shotgun() 
        {
            cooldown = 0.5f;
            maxAmmo = 2;
            Ammo = maxAmmo;
            reloadTime = 2.5f;
            Penetration = 50;
            Damage = 15;

        }

        protected override void CreateProjectile(Player player)
        {
            const float angleBullets = (float)(Math.PI / 16);

            ProjectileData pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation + 45,
                LifeSpan = 0.25f,
                Speed = 900,
                Explosive = false,

            };

            for(int i = 0; i < 10; i++)
            {
                pd.Rotation -= angleBullets;
                ProjectileManager.AddProjectiles(pd, texture);
            }
        }
    }
}
