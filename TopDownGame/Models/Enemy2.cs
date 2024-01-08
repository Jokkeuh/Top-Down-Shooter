using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

using TopDownGame.Managers;

namespace TopDownGame.Models
{
    public class Enemy2 : Enemy //Sprites
    {
        public override int HP { get; protected set; }
        private Texture2D texture { get; set; }
        private bool hasBeenHit = false;
        private float animationTimer;
        private float AttackCoolDown;

        private AnimationManager animationManager { get; set; } = new();
        public Enemy2(Texture2D texture, Vector2 pos, int HP) : base(texture, pos)
        {
            this.HP = HP;
            Speed = 100;
            this.texture = Globals.Content.Load<Texture2D>("DemonSlimeSheet");

            animationManager.AddAnimation("Walk", new(this.texture, 8, 5, 0.10f, 2));
            animationManager.AddAnimation("Melee", new(this.texture, 8, 5, 0.10f, 3));
            animationManager.AddAnimation("GotHit", new(this.texture, 8, 5, 0.16f, 4));
            animationTimer = 0f;
            AttackCoolDown = 1f;


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

                //stun logic here this.stunned;
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
            player.Immune = true;
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
                if (pursuitPlayer.Length() > 60)
                {
                    var vel = Vector2.Normalize(pursuitPlayer);
                    Position += vel * Speed * Globals.TotalSeconds;
                    animationManager.Update("Walk");
                }
                else
                {
                    if (AttackCoolDown > 0)
                    {
                        AttackCoolDown -= Globals.TotalSeconds;
                        animationManager.Update("Melee");
                    }
                    else
                    {
                        DamageToPlayer(player);
                        AttackCoolDown = 1f;
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
