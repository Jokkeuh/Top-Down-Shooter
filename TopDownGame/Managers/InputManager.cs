using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownGame.Managers
{
    public class InputManager
    {
        public static MouseState lastMouseState;
        public static KeyboardState lastKeyboardState;
        public static Vector2 velocity;
        public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
        public static Vector2 MouseWorldPosition { get; set; }
        public static bool Moving => velocity != Vector2.Zero;
        public static bool MouseClicked { get; set; }
        public static bool MouseRightClicked { get; set; } 
        public static bool MouseLeftDown { get; private set; }
        public static bool SpacePressed { get; private set; }
        public static bool numpad1 { get; private set; }
        public static bool numpad2 { get; private set; }
        public static bool numpad3 { get; private set; }
        public static bool numpad4 { get; private set; }
        public static bool numpad5 { get; private set; }
        public static bool numpad9 { get; private set; }
        public static bool Fkey { get; private set; }   
        public static bool ShiftKey { get; private set; }
        public static bool Space { get; internal set; }

        public static void Update(Matrix CameraTranslation)
        {
            var keyState = Keyboard.GetState();
            var MouseState = Mouse.GetState();
            velocity = Vector2.Zero;
            if (keyState.IsKeyDown(Keys.Z)) 
            {
                velocity.Y -= 1f; 
            }
            if (keyState.IsKeyDown(Keys.Q))
            {
                velocity.X += -1f;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity.Y -= -1f;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X += 1f;
            }
            MouseWorldPosition = Vector2.Transform(MousePosition, Matrix.Invert(CameraTranslation));
            MouseLeftDown = MouseState.LeftButton == ButtonState.Pressed;
            MouseClicked = (MouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released);
            MouseRightClicked = MouseState.RightButton == ButtonState.Pressed && (lastMouseState.RightButton == ButtonState.Released);
            SpacePressed = lastKeyboardState.IsKeyUp(Keys.Space) && keyState.IsKeyDown(Keys.Space);
            numpad1 = lastKeyboardState.IsKeyUp(Keys.NumPad1) && keyState.IsKeyDown(Keys.NumPad1);
            numpad2 = lastKeyboardState.IsKeyUp(Keys.NumPad2) && keyState.IsKeyDown(Keys.NumPad2);
            numpad3 = lastKeyboardState.IsKeyUp(Keys.NumPad3) && keyState.IsKeyDown(Keys.NumPad3);
            numpad4 = lastKeyboardState.IsKeyUp(Keys.NumPad4) && keyState.IsKeyDown(Keys.NumPad4);
            numpad5 = lastKeyboardState.IsKeyUp(Keys.NumPad5) && keyState.IsKeyDown(Keys.NumPad5);
            numpad9 = lastKeyboardState.IsKeyUp(Keys.NumPad9) && keyState.IsKeyDown(Keys.NumPad9);
            Fkey = lastKeyboardState.IsKeyUp(Keys.F) && keyState.IsKeyDown(Keys.F);
            ShiftKey = lastKeyboardState.IsKeyUp(Keys.LeftShift) && keyState.IsKeyDown(Keys.LeftShift);
            Space = lastKeyboardState.IsKeyUp(Keys.Space) && keyState.IsKeyDown(Keys.Space);

            lastMouseState = Mouse.GetState();
            lastKeyboardState = Keyboard.GetState();



        }
    }
}
