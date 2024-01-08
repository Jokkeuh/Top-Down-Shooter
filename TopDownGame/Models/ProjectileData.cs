using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Models
{
    public class ProjectileData
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float LifeSpan { get; set; }
        public int Speed { get; set; }
        public bool Explosive { get; set; } = false;
        public bool IsEnemyProjectile { get; set; } = false;

        //public void Update()
        //{

        //    LifeSpan -= Globals.TotalSeconds;
        //}

    }
}
