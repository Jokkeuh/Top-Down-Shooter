using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TopDownGame.Models
{
    public abstract class Enemy : Sprites
    {
        protected Enemy(Texture2D texture,Vector2 pos) : base(texture, pos)
        {
         
        }

        public Texture2D Texture { get; set; }
        public virtual int HP { get; protected set; }
        //public virtual Vector2 Position { get; protected set; }
        public abstract void Update(Player player);
        public abstract override void Draw();
        public abstract void TakeDamage(int damage, bool Crit, Player player);

    }
}
