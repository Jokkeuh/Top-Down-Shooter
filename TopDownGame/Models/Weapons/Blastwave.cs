using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class Blastwave : Weapon
    {
        public Texture2D texture = Globals.Content.Load<Texture2D>("BlastWaveStatic");
        public Blastwave() 
        {
            cooldown = 1;
            maxAmmo = 1;
            Ammo = maxAmmo;
            reloadTime = 5f;
            Damage = 50;
            Penetration = 1000;        
        }

        protected override void CreateProjectile(Player player)
        {
            ProjectileData pd = new()
            {
                Position = player.Position,
                Speed = 250,
                LifeSpan = 10f,
                Rotation = player.Rotation,
            };
            ProjectileManager.AddProjectiles(pd, texture);
        }
    }
}

