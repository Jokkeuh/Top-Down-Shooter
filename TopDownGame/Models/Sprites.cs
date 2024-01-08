using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models
{
    public class Sprites
    {
        public readonly Texture2D Texture;
        public readonly Vector2 origin;
        public Vector2 Position;
        public int Speed;
        public float Rotation;

        public Sprites(Texture2D texture, Vector2 pos)
        {
            Texture = texture;
            Position = pos;
            Speed = 300;
            origin =  new Vector2(pos.X, pos.Y);
            //origin =  new Vector2((texture.Width /2),( texture.Height / 2));

        }

        public virtual void Draw()
        {
            
            //Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, origin, 0.5f, SpriteEffects.None, 1f);
        }

    }
}
