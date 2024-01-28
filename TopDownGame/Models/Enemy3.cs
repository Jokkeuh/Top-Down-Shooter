using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

using TopDownGame.Managers;

namespace TopDownGame.Models
{
    public class Enemy3 : Enemy //Sprites
    {
        public override int HP { get; protected set; }
        private Texture2D texture { get; set; }
        private Texture2D bulletTexture { get; set; }
        private bool hasBeenHit = false;
        private float animationTimer;
        private float AttackCoolDown;

        
        private float timeSinceLastShot = 0.0f;
        private float shootingCooldown = 2.0f;

        private AnimationManager animationManager { get; set; } = new();
        
        public Enemy3(Texture2D texture, Vector2 pos, int HP) : base(texture, pos)
        {
            this.HP = HP;
            Speed = 100;
            this.texture = Globals.Content.Load<Texture2D>("mechaStoneBossV2");
            this.bulletTexture = Globals.Content.Load<Texture2D>("bullet");

            animationManager.AddAnimation("Walk", new(this.texture, 10, 10, 0.10f, 2));
            animationManager.AddAnimation("Ranged", new(this.texture, 10, 10, 0.10f, 3));
            animationManager.AddAnimation("GotHit", new(this.texture, 10, 10, 0.16f, 4));
            animationTimer = 0f;
            AttackCoolDown = 1f;
        }
        private void Shoot(Player player)
        {
            Vector2 Direction = Vector2.Normalize(player.Position - this.Position);
            ProjectileData pd = new()
            {
                Position = this.Position,
                Speed = 250,
                LifeSpan = 10f,
                Rotation = (float)Math.Atan2(Direction.Y, Direction.X),
                Explosive = false,
                IsEnemyProjectile = true,
                Dmg = 1,
            };
            ProjectileManager.AddProjectiles(pd, texture);



        }
        public override void TakeDamage(int damage, bool Crit, Player player)
        {
            

            HP -= damage;
            if (this.HP <= 0)
            {
                player.Score++;
            }
            if (Crit)
            {
                var DamageNumber = new DamageNumbers(Globals.font, Position, damage, 1f, Color.Gold);
                DamageNumbersManager.DamageNumbers.Add(DamageNumber);

                //stun logic here;
                hasBeenHit = true;
                animationTimer = 1f;

            }
            else
            {
                var DamageNumber = new DamageNumbers(Globals.font, Position, damage, 1f, Color.White);
                DamageNumbersManager.DamageNumbers.Add(DamageNumber);
                
            }

        }
        public void DamageToPlayer(Player player)
        {
            if (player.Immune)
            {
                return;
            }
            player.CopyHP = player.PlayerHealth;
            player.PlayerHealth -= 1;
            
        }
        public override void Update(Player player)
        {
            if (hasBeenHit && animationTimer > 0)
            {
                animationTimer -= Globals.TotalSeconds;
                animationManager.Update("GotHit");

            }
            else
            {
                var pursuitPlayer = new Vector2(player.Position.X, player.Position.Y) - Position;
                Rotation = (float)Math.Atan2(pursuitPlayer.X, pursuitPlayer.Y);
                if (pursuitPlayer.Length() > 450)
                {
                    var vel = Vector2.Normalize(pursuitPlayer);
                    Position += vel * Speed * Globals.TotalSeconds;
                    animationManager.Update("Walk");
                }
                else
                {
                    {
                        
                        timeSinceLastShot += Globals.TotalSeconds;
                        if (timeSinceLastShot >= shootingCooldown)
                        {
                            Shoot(player);
                            timeSinceLastShot = 0;
                        }
                        animationManager.Update("ranged");
                    }
                    


                }
            }
            
            
        }
        public override void Draw()
        {
            animationManager.Draw(new Vector2(Position.X, Position.Y));
        }




    }
}
