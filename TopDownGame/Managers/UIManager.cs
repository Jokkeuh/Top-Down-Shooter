using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TopDownGame.Models;

namespace TopDownGame.Managers
{
    public static class UIManager
    {
        private static Texture2D bulletTexture;
        private static Texture2D healthTexture;
        private static SpriteFont ScoreFont;
        private static string TextNumber = "0";

        public static void Init(Texture2D texture) 
        {
            healthTexture = Globals.Content.Load<Texture2D>("beer");
            bulletTexture = texture;
            ScoreFont = Globals.Content.Load<SpriteFont>("fonts/DamageFont");
            
        }

        
        public static void Draw(Player player, Map map, Matrix cameraTranslation, List<Enemy> enemies)
        {
            TextNumber = player.Score.ToString();
            Color c = player.Weapon.Reloading ? Color.Red : Color.White;
            Color HP = Color.Red;


            string elementCount = player.Weapon.Ammo.ToString() + $"/{player.Weapon.maxAmmo}";
            int elementSpacing = bulletTexture.Height * 2;

            var offsetX = 150;
            var offsetY = 150;
            // Clamping offsets to the edge of the screen
            int maxOffsetX = (Globals.Bounds.X) - (player.Weapon.Ammo * elementSpacing);
            int maxOffsetY = Globals.Bounds.Y - elementSpacing;
            offsetX = MathHelper.Clamp(offsetX, 0, maxOffsetX);
            offsetY = MathHelper.Clamp(offsetY, 0, maxOffsetY);


           
            for (int i = 0; i < player.PlayerHealth; i++)
            {
                Vector2 pos = new Vector2((player.Position.X + i * elementSpacing) -250, player.Position.Y);
                Globals.SpriteBatch.Draw(healthTexture, pos, null, HP, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            }
            if (!player.Weapon.Reloading)
            {
                Globals.SpriteBatch.DrawString(ScoreFont, elementCount, new(player.Position.X - 150, player.Position.Y), c * 0.75f);
            }
            foreach (var enemy in enemies)
            {
                var offset = new Vector2(-enemy.HP.ToString().Length * 12, -80);
                var enemyPosOnScreen = enemy.Position;
                var healthNumberPosition = enemyPosOnScreen + offset;
                Globals.SpriteBatch.DrawString(ScoreFont, enemy.HP.ToString(), healthNumberPosition, Color.Red);

            }




            Vector2 textSize = ScoreFont.MeasureString(TextNumber);
            Vector2 textPosition = new Vector2((Globals.Bounds.X - textSize.X) / 2, offsetY);
            textPosition -= new Vector2(cameraTranslation.Translation.X, cameraTranslation.Translation.Y);
            Globals.SpriteBatch.DrawString(ScoreFont, TextNumber, textPosition, Color.White);
        }
    }
}
