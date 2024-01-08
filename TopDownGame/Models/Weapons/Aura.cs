using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models.Weapons
{
    public class Aura
    {
        private Texture2D texture;
        private float cooldownRemaining;
        public float radius;
        public Color color;
        public Vector2 origin;
        public float AttackRate;
        public bool Active;

        public Aura(Texture2D texture, float radius, Color color, float attackRate)
        {
            this.texture = texture;
            this.radius = radius;
            this.color = color;
            this.origin = new(radius, radius);
            this.AttackRate = attackRate;
            this.cooldownRemaining = 0;
            this.Active = true;
        }

        public void Update(List<Enemy> enemies, Player player)
        {
            cooldownRemaining -= Globals.TotalSeconds;
            if (Active)
            {
                if (cooldownRemaining > 0) return;
                foreach (Enemy enemy in enemies)

                {


                    cooldownRemaining = AttackRate;
                    {
                        Vector2 drawPos = new(player.Position.X, player.Position.Y);
                        float distance = Vector2.Distance(enemy.Position, drawPos);
                        if (distance <= radius)
                        {
                            enemy.TakeDamage(1, true, player);

                        }
                    }

                }
            }
            
            


           
        }
        public void Start()
        {
            this.Active = true;
        }
        public void Stop() 
        {
            this.Active = false;  
        }

        public void Draw(Vector2 pos)
        {
            if (Active)
            {
                //Vector2 drawPos = new(pos.X, pos.Y);
                Globals.SpriteBatch.Draw(texture, pos, null, color, 0f, new(texture.Width/2, texture.Height/2), 1f, SpriteEffects.None, 0f);
            }
            
        }

    }
}
