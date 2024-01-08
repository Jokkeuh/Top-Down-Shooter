using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models
{
    public class Tile
    {
        private readonly Texture2D texture;
        private readonly Vector2 position;

        public Tile(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(texture, position, Color.White);
        }
    }
}
