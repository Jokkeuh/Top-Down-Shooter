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
        public Vector2 BulletPosition { get; set; }
        public AnimationManager AnimationManagerProjectiles = new();
        private Guid animationID {  get; set; }
        public ProjectileData data;
        public bool explodingState {get; set; } = false;
        public bool explodingAmmo { get; set; } = false;
        public bool IsEnemyProjectile { get; set; } = false;
        public int Dmg { get; set; } = 0;
        public Explosion Explosion { get; set; }


        public float LifeSpan { get; set; }
        public Projectile(Texture2D texture, ProjectileData data) : base(texture, data.Position)
        {
            animationID = Guid.NewGuid();
            //ANIMATIONS FOR ALL AMMOS
            var animationRevolver = Globals.Content.Load<Texture2D>("fancyRevolverBulletAnimation");
            var animationShotgun = Globals.Content.Load<Texture2D>("fancyShotgunBulletAnimation");
            var animationSniper = Globals.Content.Load<Texture2D>("fancySniperBulletAnimation");
            var animationMachineGun = Globals.Content.Load<Texture2D>("fancyMachineGunBulletAnimation");
            var animationRocket = Globals.Content.Load<Texture2D>("RocketAnimation");

            




            

            AnimationManagerProjectiles.AddAnimation("RevolverBullet", new(animationRevolver, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("ShotgunBullet", new(animationShotgun, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("SniperBullet", new(animationSniper, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("MachineGunBullet", new(animationMachineGun, 4, 1, 0.08f));
            AnimationManagerProjectiles.AddAnimation("rocket", new(animationRocket, 3, 1, 0.08f));



            this.data = data;
            Rotation = data.Rotation;
            Speed = data.Speed;
            Velocity = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)); 
            LifeSpan = data.LifeSpan;
            explodingAmmo = data.Explosive;
            IsEnemyProjectile = data.IsEnemyProjectile;
            Dmg = data.Dmg;
            Explosion = new Explosion(AnimationManagerProjectiles);

            

        }

        private float explosionTimer = 0.36f;

        public void Destroy()
        {   
                LifeSpan = 0;   
        }

        public Vector2 GetCurrentPos()
        {
            return Position;
        }

        public void Update(Player player)
        {

            //AnimationManagerProjectiles.Update("explode");
            Position += Velocity * Speed * Globals.TotalSeconds;
            LifeSpan -= Globals.TotalSeconds;
            if (explodingState)
            {
                AnimationManagerProjectiles.Update($"explode{animationID}");

                explosionTimer -= Globals.TotalSeconds;
                if (explosionTimer <= 0)
                {
                    explodingState = false;
                    explosionTimer = 0.21f; // Reset the timer
                    
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
                    
                    if (data.Explosive)
                    {
                            AnimationManagerProjectiles.Update("rocket");
                    }

                }
                
                if (LifeSpan <= 0.16 && data.Explosive && !explodingState)
                {
                    
                    ExplodeAtCurrentPosition(Position);
                    Velocity = Vector2.Zero;
                    
                }


            }
            
            
            

        }
        
        public void ExplodeAtCurrentPosition(Vector2 pos)
        {

            Explosion.Explode(pos);
            LifeSpan = 0.10f;
            this.explodingState = true;
            
        }

        public override void Draw()
        {
            AnimationManagerProjectiles.Draw(Position, Rotation);
            Explosion.Draw();
            //expl.Draw();
            //Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new(Texture.Width / 2, Texture.Height / 2), 1f, SpriteEffects.None, 1f);
        }


    }
}
