using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Models;

namespace TopDownGame.Managers
{
    public static class EnemyManager
    {
        public static List<Enemy> enemies;
        
        public static Texture2D texture;
        private static float interval = 1f;
        private static float cooldown;
        private static int padding;
        private static Random Ran;
    
        public static void Init()
        {
            cooldown = 1f;
            Ran = new Random();
            padding = 800;
            enemies = new List<Enemy>();
        }

        public static void AddEnemy(Player player, Texture2D texture, int EnemyNumber, int currentWave)
        {


            if (EnemyNumber == 1)
            {
                enemies.Add(new Enemy1(texture, RandomPos(player), 10 * currentWave));
            }
            if (EnemyNumber == 2)
            {
                enemies.Add(new Enemy2(texture, RandomPos(player),15 * currentWave));
            }
            if (EnemyNumber == 3)
            {
                enemies.Add(new Enemy3(texture, RandomPos(player), 25 * currentWave));
            }





            //enemies.Add(new(texture, RandomPos(player)));
        }
        public static void Update(Player player)
        {

            interval -= Globals.TotalSeconds;
            if (interval <= 0)
            {
                interval += cooldown;
                if (player.Score < 100)  AddEnemy(player, Globals.Content.Load<Texture2D>("DemonSlimeSheet"), 1, 1);
                if (player.Score > 10)  AddEnemy(player, Globals.Content.Load<Texture2D>("mechaStoneBossV2"), 2, 2);
                if (player.Score > 10)  AddEnemy(player, Globals.Content.Load<Texture2D>("mechaStoneBossV2"), 3, 5);
            }
            

            foreach (var enemy in enemies)
            {
                enemy.Update(player);
            }
            enemies.RemoveAll((e) => e.HP <= 0);
        }
        public static void Draw()
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw();
            }
        }

        public static Vector2 RandomPos(Player player)
        {
            float x = Globals.Bounds.X;
            float y = Globals.Bounds.Y;
            Vector2 pos = new Vector2();

            
            int edge = Ran.Next(4); // 0: top, 1: right, 2: bottom, 3: left

            switch (edge)
            {
                case 0: // Top edge
                    pos.X = player.Position.X +  (float)(Ran.NextDouble() * x);
                    pos.Y = player.Position.Y - padding;
                    break;
                case 1: // Right edge
                    pos.X = player.Position.X + x + padding;
                    pos.Y = player.Position.Y + (float)(Ran.NextDouble() * y);
                    break;
                case 2: // Bottom edge
                    pos.X = player.Position.X + (float)(Ran.NextDouble() * x);
                    pos.Y = player.Position.Y + y + padding;
                    break;
                case 3: // Left edge
                    pos.X = player.Position.X - padding;
                    pos.Y = player.Position.Y +  (float)(Ran.NextDouble() * y);
                    break;
            }

            return pos;
        }
    }
}
