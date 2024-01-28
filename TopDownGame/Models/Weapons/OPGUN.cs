using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class OPGUN : Weapon
    {
        private Texture2D texture = Globals.Content.Load<Texture2D>("bullet");
        public OPGUN() 
        {
            cooldown = 0.1f;
            maxAmmo = 999;
            Ammo = maxAmmo;
            reloadTime = 1f;
            Damage = 0;
        }
        protected override void CreateProjectile(Player player)
        {
            ProjectileData pd = new()
            {
                Position = player.Position,
                Speed = 650,
                LifeSpan = 2f,
                Rotation = player.Rotation,
                Explosive = true,
            };
            ProjectileManager.AddProjectiles(pd, texture);
        }
    }
}
