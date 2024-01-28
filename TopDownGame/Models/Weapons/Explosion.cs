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
        public AnimationManager animationManager { get; set; }
        private readonly Texture2D texture;
        private Guid AnimationId;
        public Vector2 Positition { get; set; }
        public Vector2 HitPositition { get; set; } = Vector2.Zero;

        public Explosion(AnimationManager animationManager)
        {
            this.animationManager = animationManager;
            texture ??= Globals.Content.Load<Texture2D>("exp2");
            AnimationId = Guid.NewGuid();
            animationManager.AddAnimation($"explode{AnimationId}", new(texture, 4, 4, 0.08f,grid:true, scale: 2));

        }

        public void Update()
        {
            animationManager.Update($"explode{AnimationId}");
        }

        public void Explode(Vector2 hitpos)
        {
            animationManager.StartAnimation($"explode{AnimationId}");
            HitPositition = hitpos;
            

        }

        public void Draw()
        {
            animationManager.Draw(HitPositition, 0);
        }

    }
}
