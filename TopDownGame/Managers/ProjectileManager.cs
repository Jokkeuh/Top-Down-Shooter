﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Models;
using TopDownGame.Models.Weapons;

namespace TopDownGame.Managers
{
    public static class ProjectileManager
    {
        private static Texture2D texture;
        public static List<Projectile> Projectiles { get; } = new();
        public static List<Explosion> explosions { get; } = new();
        public static int critDamage;
        
        static Random random = new Random();
        public static void Init(Texture2D _texture)
        {
            texture = _texture;
          
        }

        public static void AddProjectiles(ProjectileData data, Texture2D texture)
        {
            Projectiles.Add(new(texture, data));
        }

        public static void Update(List<Enemy> enemies, Player player)
        {
            //10% crit chance


            foreach (var bullet in Projectiles)
            {
                if (!bullet.IsEnemyProjectile)
                {


                    var crit = random.Next(1, 11);
                    int targetsHit = 0;
                    bullet.Update(player);
                    foreach (var b2 in enemies)
                    {
                        critDamage = player.Weapon.Damage * 3;
                        if (b2.HP <= 0) continue;
                        if ((bullet.Position - b2.Position).Length() < 32)
                        {
                            if (bullet.explodingAmmo)
                            {
                                bullet.Explode();
                                bullet.Destroy();
                            }
                            else
                            {
                                if (crit > 9)
                                {
                                    b2.TakeDamage(player.Weapon.Damage + critDamage, true, player);
                                }
                                else
                                {
                                    b2.TakeDamage(player.Weapon.Damage, false, player);
                                }

                                targetsHit++;
                                if (targetsHit > player.Weapon.Penetration)
                                {
                                    if (bullet.explodingAmmo)
                                    {
                                        bullet.Explode();
                                        bullet.Destroy();

                                    }
                                    else
                                    {
                                        bullet.Destroy();
                                    }




                                    break;
                                }
                            }
                        }



                        float distanceToEnemy = Vector2.Distance(bullet.Position, b2.Position);
                        if (bullet.explodingState)
                        {

                            float radius = 150f;

                            // needed as prop
                            if (distanceToEnemy < radius)
                            {

                                float distanceFactor = 1.0f - (distanceToEnemy / radius);
                                float proximityDamage = player.Weapon.Damage * distanceFactor;
                                b2.TakeDamage((int)proximityDamage, false, player);
                            }

                        }

                    }
                }else bullet.Update(player);

            }
            
            Projectiles.RemoveAll((bullet) => bullet.LifeSpan <= 0);
        }


        public static void Draw(Player player)
        {
            foreach(var bullet in Projectiles)
            {
                bullet.Draw();
            }
        }
    }
}
