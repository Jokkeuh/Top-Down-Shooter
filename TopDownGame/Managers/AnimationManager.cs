using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using TopDownGame.Models;

namespace TopDownGame.Managers
{
    public class AnimationManager
    {
        private readonly Dictionary<object, Animation> animations = new();
        private readonly List<object> activeAnimations = new();

        private object lastKey;

        public void AddAnimation(object key, Animation animation)
        {
            this.animations.Add(key, animation);
            lastKey ??= key;
        }

        public void StartAnimation(object key)
        {
            if (animations.ContainsKey(key) && !activeAnimations.Contains(key))
                activeAnimations.Add(key);
        }
        public void StopAnimation(object key)
        {
            if (activeAnimations.Contains(key))
            {
                foreach (var animation in activeAnimations)
                {
                    animations[animation].Stop();
                }
                activeAnimations.Remove(key);
            }
                
        }
        public bool IsFinished(object key)
        {
            if (animations.TryGetValue(key, out Animation value))
            {
                return value.IsFinished();
            }
            return true;
        }

        public void Update(object key)
        {

            foreach (var animation in activeAnimations)
            {
                animations[animation].Start();
                animations[animation].Update();
            }
            if (key != null)
            {
                if (animations.TryGetValue(key, out Animation value))
                {
                    value.Start();
                    animations[key].Update();
                    lastKey = key;
                }
                else
                {
                    animations[lastKey].Stop();
                    animations[lastKey].ResetAnimation();
                }
            }
            
        }


        public void Draw(Vector2 position, float rotation = 0f, bool flipped = false)
        {
            foreach (var key in activeAnimations)
            {
                animations[key].Draw(position, rotation, flipped);
            }
            animations[lastKey].Draw(position, rotation, flipped);
            
            
        }
    }
}
