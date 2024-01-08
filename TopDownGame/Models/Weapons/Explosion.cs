using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Managers;

namespace TopDownGame.Models.Weapons
{
    public class Explosion
    {
        private readonly AnimationManager animationManager;
        private readonly Texture2D texture;

        public Vector2 Positition { get; set; }
        public Explosion(Vector2 Pos)
        {
            texture ??= Globals.Content.Load<Texture2D>("ExplosionRadiusAnimationV2");
            
            animationManager = new AnimationManager();
            animationManager.AddAnimation("explode", new(texture, 1, 8, 0.02f));
            Positition = Pos;
        }

        public void Update()
        {
            animationManager.Update("explode");
        }

        public void Explode()
        {
            animationManager.StartAnimation("explode");
        }

        public void Draw()
        {
            animationManager.Draw(Positition, 0);
        }

    }
}
