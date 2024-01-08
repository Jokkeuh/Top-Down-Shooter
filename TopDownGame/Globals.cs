using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame
{
    public static class Globals
    {
        public static float TotalSeconds { get; set; }
        public static Point Bounds { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static SpriteFont font { get; set; }
        public static void Update(GameTime time)
        {
            TotalSeconds = (float)time.ElapsedGameTime.TotalSeconds;
        }

        public enum WeaponType
        {
            Shotgun,
            MachineGun,
            SniperRifle,
            SixShooter,
            Blastwave
        }
    }
}
