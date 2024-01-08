using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models
{
    public class Animation
    {
        private readonly Texture2D texture;
        private readonly List<Rectangle> sourceRectangle = new();

        public readonly int frames;
        private int frame;
        private readonly float frameTime;
        private float frameTimeLeft;
        private bool active = true;
        private int framesX;
        private int framesY;
        
        

        public Animation(Texture2D texture, int framesXaxis, int framesYaxis, float frameTime,int row = 1)
        {
            
                this.texture = texture;
                this.frameTime = frameTime;
                frames = framesYaxis > framesXaxis ? framesYaxis : framesXaxis;


                var framewidth = texture.Width / framesXaxis;
                var frameHeight = texture.Height / framesYaxis;
                framesX = framesXaxis;
                framesY = framesYaxis;

                for (int i = 0; i < frames; i++)
                {
                    sourceRectangle.Add(new(i * framewidth, (row - 1) * frameHeight, framewidth, frameHeight));
                }
            
            
        }
        public bool IsFinished()
        {
            return frame == frames - 1;
        }

        public void Stop()
        {
            active = false;
        }
        public void Start()
        {
            active = true;
        }
        public void ResetAnimation()
        {
            frame = 0;
            frameTimeLeft = frameTime;
        }

        public void Update()
        {
            if (!active) return;
            
            frameTimeLeft -= Globals.TotalSeconds;
            
            if (frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                frame = (frame + 1) % frames;
                
            }
            
        }
        public void Draw(Vector2 pos, float rotation = 0, bool flipped = false)
        {
            if (flipped)
            {
                Globals.SpriteBatch.Draw(texture, pos, sourceRectangle[frame], Color.White, rotation, new(texture.Width / (2 * framesX), texture.Height / (2 * framesY)), Vector2.One, SpriteEffects.FlipVertically, 1);
                                                                                                                                                                                                  //.FlipHor
            }
            else
            {
                Globals.SpriteBatch.Draw(texture, pos, sourceRectangle[frame], Color.White, rotation, new(texture.Width / (2 * framesX), texture.Height / (2 * framesY)), Vector2.One, SpriteEffects.None, 1);

            }

        }
    }
}
