using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TopDownGame.Models;

namespace TopDownGame.Managers
{
    public class GameManager
    {
        private readonly Player player;
        public Map map = new();
        
        
        public Matrix cameraTranslation;
         
        public GameManager()
        {
            EnemyManager.Init();
            var bulletTexture = Globals.Content.Load<Texture2D>("bullet");
            ProjectileManager.Init(bulletTexture);
            UIManager.Init(bulletTexture);
            
            player = new(Globals.Content.Load<Texture2D>("pistol"), new(300,300), map);
        }    

        private void CalculateTranslation()
        {
            var dx = player.Position.X - (Globals.Bounds.X / 2);
            var dy = player.Position.Y - (Globals.Bounds.Y / 2);

            var minX = 0;
            var minY = 0;
            var maxX = map.MAP_SIZE.X * map.TILE_SIZE.X - Globals.Bounds.X;
            var maxY = map.MAP_SIZE.Y * map.TILE_SIZE.Y - Globals.Bounds.Y;


            dx = MathHelper.Clamp(dx, minX, maxX);
            dy = MathHelper.Clamp(dy, minY, maxY);

            cameraTranslation = Matrix.CreateTranslation(-dx, -dy, 0f);
        }
        public void Update()
        {
            InputManager.Update(cameraTranslation);
            player.Update();
            CalculateTranslation();
            
           
            EnemyManager.Update(player);
            DamageNumbersManager.Update();
           
            ProjectileManager.Update(EnemyManager.enemies, player);

        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(transformMatrix: cameraTranslation);
            map.Draw();
            
            player.Draw();
          
            
           

            EnemyManager.Draw();
            ProjectileManager.Draw(player);
            UIManager.Draw(player, map, cameraTranslation, EnemyManager.enemies);
            DamageNumbersManager.Draw();

            Globals.SpriteBatch.End();  
        }
    }
}
