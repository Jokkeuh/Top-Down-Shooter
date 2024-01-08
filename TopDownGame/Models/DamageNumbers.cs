using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;
using static System.Formats.Asn1.AsnWriter;

namespace TopDownGame.Models
{
    public class DamageNumbers
    {
        public SpriteFont font;
        private Vector2 Position;
        private Vector2 Velocity;
        private float Duration;
        private float Timer;
        private Color color;
        public string TextNumber { get; }
        private float bounceHeight;
        private float bounceSpeed;

        public DamageNumbers(SpriteFont font, Vector2 Pos, int damage, float duration, Color color)
        { 
            this.font = font;
            this.Position = Pos;
            this.Duration = duration;
            this.Timer = 0;
            TextNumber = damage.ToString();
            this.color = color;
            this.Velocity = new Vector2(1, -bounceSpeed);
            this.bounceHeight = 2f;
            this.bounceSpeed = -15f;
        }
        public void Update()
        {

            float bounce = (float)Math.Sin(Timer * bounceSpeed) * this.bounceHeight;
            Position += Velocity * Globals.TotalSeconds;
            Position.Y += bounce;
            Timer += (float)Globals.TotalSeconds;
            if (Timer > Duration)
            {
                DamageNumbersManager.DamageNumbers.Remove(this);
            }
        }

        public void Draw()
        {
            //var origin = font.MeasureString(TextNumber) / 2;
           
            Globals.SpriteBatch.DrawString(font, TextNumber, Position ,color);

        }
    }
}
