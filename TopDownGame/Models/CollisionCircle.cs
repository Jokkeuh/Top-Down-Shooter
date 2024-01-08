using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models
{
    public class CollisionCircle
    {
        private readonly Texture2D texture;
        public Vector2 origin { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public int Speed { get; set; }
        public Color color { get; set; }




    }
}
