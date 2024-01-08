using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;
using TopDownGame.Models.Weapons;

namespace TopDownGame.Models
{
    public class Enemy1 : Enemy
    {
        public override int HP { get; protected set; }
        private Texture2D texture { get; set; }
        private float animationTimer;
        private bool hasBeenHit = false;
        private float AttackCoolDown = 1f;

        //MechaStone mob
        private AnimationManager animationManager { get; set; } = new();
        public Enemy1(Texture2D texture, Vector2 pos, int HP) : base(texture, pos)
        {
            this.HP = HP;
            Speed = 120;
            this.texture = Globals.Content.Load<Texture2D>("mechaStoneBossV2");
            
            animationManager.AddAnimation("Walk", new(this.texture, 10, 10, 0.19f, 1));
            animationManager.AddAnimation("Shoot", new(this.texture, 10, 10, 0.19f, 3));
            animationManager.AddAnimation("GotHit", new(this.texture, 10, 10, 0.16f, 4));
            animationTimer = 0f;

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
                hasBeenHit = true;
                animationTimer = 0.5f;
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
            player.PlayerHealth -= 1;
            player.CopyHP = player.PlayerHealth;
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
                if (pursuitPlayer.Length() > 6)
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
                        animationManager.Update("Shoot");
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
