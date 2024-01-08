using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace TopDownGame.Models
{
    public class Map
    {
        public readonly Point MAP_SIZE = new Point(128, 128);
        public readonly Point TILE_SIZE;
        public readonly Tile[,] TILES;
        public readonly Tile[,] FenceTiles;
        //private Vector2 OffTiltMap = new(128,128);
        public Map()
        {
            TILES = new Tile[MAP_SIZE.X, MAP_SIZE.Y];
            FenceTiles = new Tile[MAP_SIZE.X, MAP_SIZE.Y];
            
            Texture2D[] texture =
            {
                Globals.Content.Load<Texture2D>("SandTile"),
                Globals.Content.Load<Texture2D>("SandTile"),
                Globals.Content.Load<Texture2D>("SandTile"),
                Globals.Content.Load<Texture2D>("SandTile"),
                Globals.Content.Load<Texture2D>("SandTile"),
            };
            Texture2D[] textureFence =
            {
                Globals.Content.Load<Texture2D>("HorFence"),
                Globals.Content.Load<Texture2D>("VertFence"),
            };
            TILE_SIZE.X = texture[0].Width / 2;
            TILE_SIZE.Y = texture[0].Height/ 2;

            Random rnd = new Random();
            for (int i = 0; i < MAP_SIZE.X; i++)
            {
                for (int j = 0; j < MAP_SIZE.Y; j++)
                {
                    int ran = rnd.Next(0, texture.Length);
                    TILES[i, j] = new(texture[ran], ToScreen(i, j));
                    FenceTiles[i, j] = new(textureFence[0], ToScreen(i, j, 200));
                }
            }
            
            

        }

        public bool IsWalkable(int x, int y)
        {
            if (x < 0 || x>= MAP_SIZE.X || y < 0 || y >= MAP_SIZE.Y)
            {
                return false;
            }
            return true;
        }

        


        private Vector2 ToScreen(int x, int y, float offset = 0) 
        {
            var DisplayX = x * TILE_SIZE.X; /*(x-y) * TILE_SIZE.X / 2 + (OffTiltMap.X * TILE_SIZE.X);*/
            var DisplayY = y * TILE_SIZE.Y;/*(y + x) * TILE_SIZE.Y /2 + (OffTiltMap.X * TILE_SIZE.Y);*/

            return new Vector2(DisplayX + offset, DisplayY + offset);
        }

        public void Draw()
        {
            for (int i = 0; i < MAP_SIZE.X; i++)
            {
                for (int j = 0; j < MAP_SIZE.Y; j++)
                {
                    

                    
                    if(IsWalkable(i ,j))
                    {
                        TILES[i, j].Draw();
                    }
                }
            }
        }
    }
}
