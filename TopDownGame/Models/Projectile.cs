using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;
using TopDownGame.Models.Weapons;

namespace TopDownGame.Models
{
    public class Projectile : Sprites
    {
        public Vector2 Velocity { get; set; }
        public AnimationManager AnimationManagerProjectiles = new();
        public ProjectileData data;
        public bool explodingState {get; set; } = false;
        public bool explodingAmmo { get; set; } = false;
        public bool IsEnemyProjectile { get; set; } = false;
        
       
        public float LifeSpan { get; private set; }
        public Projectile(Texture2D texture, ProjectileData data) : base(texture, data.Position)
        { //ANIMATIONS FOR ALL AMMOS
            var animationRevolver = Globals.Content.Load<Texture2D>("fancyRevolverBulletAnimation");
            var animationShotgun = Globals.Content.Load<Texture2D>("fancyShotgunBulletAnimation");
            var animationSniper = Globals.Content.Load<Texture2D>("fancySniperBulletAnimation");
            var animationMachineGun = Globals.Content.Load<Texture2D>("fancyMachineGunBulletAnimation");
            var animationRocket = Globals.Content.Load<Texture2D>("RocketAnimation");


            var ExplosionAnimation = Globals.Content.Load<Texture2D>("ExplosionRadiusAnimationV2");
            

            









            AnimationManagerProjectiles.AddAnimation("RevolverBullet", new(animationRevolver, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("ShotgunBullet", new(animationShotgun, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("SniperBullet", new(animationSniper, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("MachineGunBullet", new(animationMachineGun, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("rocket", new(animationRocket, 3, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("explode", new(ExplosionAnimation, 1, 8, 0.031f));




            this.data = data;
            Rotation = data.Rotation;
            Speed = data.Speed;
            Velocity = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)); 
            LifeSpan = data.LifeSpan;
            explodingAmmo = data.Explosive;
            IsEnemyProjectile = data.IsEnemyProjectile;
        }
        public void Destroy()
        {
               
                LifeSpan = 0;
            
        }

        

        public void Update(Player player)
        {
            

            Position += Velocity * Speed * Globals.TotalSeconds;
            LifeSpan -= Globals.TotalSeconds;
            if (explodingState)
            {
                AnimationManagerProjectiles.Update("explode");
                
                if (LifeSpan <= 0) // timing for animation on explosion
                {
                   explodingState = false;
                    
                }
            }
            
            else
            {
                

                if (LifeSpan > 0)
                {
                    //CHANGES WHEN BULLETS STILL TRAVELLING
                    if (player.Weapon is Shotgun)
                    {
                        AnimationManagerProjectiles.Update("ShotgunBullet");

                    }
                    if (player.Weapon is SniperRifle)
                    {
                        AnimationManagerProjectiles.Update("SniperBullet");

                    }
                    if (player.Weapon is SixShooter)
                    {
                        AnimationManagerProjectiles.Update("RevolverBullet");

                    }
                    if (player.Weapon is MachineGun)
                    {
                        AnimationManagerProjectiles.Update("MachineGunBullet");

                    }
                    
                    if (data.Explosive && !explodingState)
                    {
                            AnimationManagerProjectiles.Update("rocket");
                    }

                }
                if (LifeSpan <= 0.025 && data.Explosive)
                {

                    Explode();
                    AnimationManagerProjectiles.StartAnimation("explode");
                    

                }


            }
            
            
            

        }

        public void Explode()
        {
            if (explodingState) return;
            
            Velocity = Vector2.Zero;
            this.explodingState = true;
        }

        public override void Draw()
        {
            AnimationManagerProjectiles.Draw(Position, Rotation);
            
            
            
            //Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new(Texture.Width / 2, Texture.Height / 2), 1f, SpriteEffects.None, 1f);
        }


    }
}
