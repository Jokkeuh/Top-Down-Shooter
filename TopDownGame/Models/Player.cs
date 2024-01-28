using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using TopDownGame.Managers;
using TopDownGame.Models.Weapons;

namespace TopDownGame.Models
{
    public class Player : Sprites
    {
        public Texture2D BaseTexture { get; set; }
        public Weapon Weapon { get; set; }
        public Weapon WeaponSG { get; set; } = new Shotgun();
        public Weapon WeaponMG { get; set; } = new MachineGun();
        public Weapon WeaponSR { get; set; } = new SniperRifle();
        public Weapon WeaponSS { get; set; } = new SixShooter();
        public Weapon Blastwave { get; set; } = new Blastwave();
        public Weapon RocketLauncher { get; set; } = new RocketLauncer();
        public Weapon OPGUN { get; set; } = new OPGUN();
        public Aura aura { get; set; }







        public Vector2 shootingPos { get; set; }
        private readonly AnimationManager _animationManager = new();
        private readonly AnimationManager _animationManagerWeapon = new();
        private readonly AnimationManager _animationManagerReloading = new();
        private readonly AnimationManager _animationManagerMovement = new();


        public bool Shooting = false;
        public float animTime = 1f;
        public bool IsFlipped { get; set; }
        public int PlayerHealth { get; set; }
        public string State { get; set; } = "idle";
        public float DodgeCooldown = 0f;
        public bool Dodging = false;
        public int Score { get; set; }
        public bool Immune { get; set; } = false;
        public float CoolDownImmune { get; set; } = 0.25f;
        private Map Map { get; set; }

        public int CopyHP { get; set; }


        public Player(Texture2D texture, Vector2 pos, Map map) : base(texture, pos)
        {
            

            var auraText = Globals.Content.Load<Texture2D>("defaultAura");
            float auraRadius = 100f;
            Color auraColor = new Color(255, 255, 255, 25);
            aura = new(auraText, auraRadius, auraColor, 0.25f);

            Weapon = WeaponSG;
            PlayerHealth = 10;
            this.Map = map;

            var heroTexture = Globals.Content.Load<Texture2D>("hero");
            var dodgeTexture = Globals.Content.Load<Texture2D>("DodgeAnimation");
            var idleTexture = Globals.Content.Load<Texture2D>("idle");



            var ReloadingSniper = Globals.Content.Load<Texture2D>("ReloadingAnimationSniperV2");
            var ReloadingMachineGun = Globals.Content.Load<Texture2D>("ReloadingAnimationMachineGun");
            var ReloadingShotgun = Globals.Content.Load<Texture2D>("ReloadingAnimationShotgun");
            var ReloadingRevolver = Globals.Content.Load<Texture2D>("ReloadingAnimationRevolver");


            var IdleSniper = Globals.Content.Load<Texture2D>("IdleAnimationSniper");
            var IdleMachineGun = Globals.Content.Load<Texture2D>("IdleAnimationMachineGun");
            var IdleShotgun = Globals.Content.Load<Texture2D>("IdleAnimationShotgun");
            var IdleRevolver = Globals.Content.Load<Texture2D>("IdleAnimationRevolver");



            var shotgunBlast = Globals.Content.Load<Texture2D>("ShotgunBlast");
            var SniperShotBlast = Globals.Content.Load<Texture2D>("SniperShotAnimation");
            var MachineGunBlast = Globals.Content.Load<Texture2D>("MachineGunBlast");
            var RevolverBlast = Globals.Content.Load<Texture2D>("RevolverBlast");


            //
            //Animations
            _animationManager.AddAnimation(new Vector2(0, 1), new(heroTexture, 8, 8, 0.1f, 1));
            _animationManager.AddAnimation(new Vector2(-1, 0), new(heroTexture, 8, 8, 0.1f, 2));
            _animationManager.AddAnimation(new Vector2(1, 0), new(heroTexture, 8, 8, 0.1f, 3));
            _animationManager.AddAnimation(new Vector2(0, -1), new(heroTexture, 8, 8, 0.1f, 4));
            _animationManager.AddAnimation(new Vector2(-1, 1), new(heroTexture, 8, 8, 0.1f, 5));
            _animationManager.AddAnimation(new Vector2(-1, -1), new(heroTexture, 8, 8, 0.1f, 6));
            _animationManager.AddAnimation(new Vector2(1, 1), new(heroTexture, 8, 8, 0.1f, 7));
            _animationManager.AddAnimation(new Vector2(1, -1), new(heroTexture, 8, 8, 0.1f, 8));
            //
            //
            _animationManagerMovement.AddAnimation(("dodge"), new(dodgeTexture, 6, 1, 0.1f, 1));
            //
            //
            _animationManagerReloading.AddAnimation(("IdleSniper"), new(IdleSniper, 1, 1, 0.02f, 1));
            _animationManagerReloading.AddAnimation(("IdleMachineGun"), new(IdleMachineGun, 1, 1, 0.02f, 1));
            _animationManagerReloading.AddAnimation(("IdleShotgun"), new(IdleShotgun, 1, 1, 0.02f, 1));
            _animationManagerReloading.AddAnimation(("IdleRevolver"), new(IdleRevolver, 1, 1, 0.02f, 1));
            //
            //
            _animationManagerReloading.AddAnimation(("ReloadingSniper"), new(ReloadingSniper, 1, 26, 0.02f, 1));
            _animationManagerReloading.AddAnimation(("ReloadingMachineGun"), new(ReloadingMachineGun, 16, 1, 0.02f, 1));
            _animationManagerReloading.AddAnimation(("ReloadingShotgun"), new(ReloadingShotgun, 14, 1, 0.02f, 1));
            _animationManagerReloading.AddAnimation(("ReloadingRevolver"), new(ReloadingRevolver, 12, 1, 0.02f, 1));
            //
            //
            _animationManagerWeapon.AddAnimation(("ShotgunBlast"), new(shotgunBlast, 3, 1, 0.04f));
            _animationManagerWeapon.AddAnimation(("SniperBlast"), new(SniperShotBlast, 3,1, 0.04f));
            _animationManagerWeapon.AddAnimation(("MachineGunBlast"), new(MachineGunBlast, 3, 1, 0.04f));
            _animationManagerWeapon.AddAnimation(("RevolverBlast"), new(RevolverBlast, 5, 1, 0.04f));
            //
            //
            _animationManager.AddAnimation(("idle"), new(idleTexture, 8, 1, 0.16f));




            

        }



        public void SwapWeapons()
        {
            if (InputManager.numpad1)
            {
                Weapon = WeaponSG;
            }
            if (InputManager.numpad2)
            {
                Weapon = WeaponMG;
            }
            if (InputManager.numpad3)
            {
                Weapon = WeaponSR;
            }
            if (InputManager.numpad4)
            {
                Weapon = WeaponSS;
            }
            if (InputManager.numpad5)
            {
                Weapon = RocketLauncher;
            }
            if (InputManager.Fkey)
            {
                Weapon = Blastwave;
            }
            if (InputManager.numpad9)
            {
                Weapon = OPGUN;
            }
        }
        //public void Dodge()
        //{
        //    Dodging = true;
        //    DodgeCooldown = 0.1f;
        //    Position += InputManager.velocity * Speed * Globals.TotalSeconds * 50f;
        //}
        public void Dodge()
        {
            Dodging = true;
            DodgeCooldown = 0.1f;

            Vector2 nextPosition = Position + InputManager.velocity * Speed * Globals.TotalSeconds * 50f;

            if (nextPosition.X >= 0 && nextPosition.X <= Map.MAP_SIZE.X * Map.TILE_SIZE.X &&
                nextPosition.Y >= 0 && nextPosition.Y <= Map.MAP_SIZE.Y * Map.TILE_SIZE.Y)
            {
                Position = nextPosition;
            }
            else
            {
                return;
            }
        }


        public void Update()
        {

            if (CoolDownImmune <= 0)
            {
                this.Immune = false;
                CoolDownImmune = 0.2f;

            }
            if (Immune)
            {
                
                CoolDownImmune -= Globals.TotalSeconds;
                
            }
            



            if (PlayerHealth < 0)
            {
                //ResetGame();
                PlayerHealth = 10;
                Score = 0;
                Position = new((Map.MAP_SIZE.X * Map.TILE_SIZE.X)/2, (Map.MAP_SIZE.Y* Map.TILE_SIZE.Y) / 2);
            }

            ;
            
            Vector2 nextPosition = Position + InputManager.velocity * Speed * Globals.TotalSeconds;
            int nextTileX = (int)(nextPosition.X / Map.TILE_SIZE.X);
            int nextTileY = (int)(nextPosition.Y / Map.TILE_SIZE.Y);
            bool isNextTileWalkable = Map.IsWalkable(nextTileX, nextTileY);
            if (isNextTileWalkable)
            {
                //Position = new Vector2(
                //MathHelper.Clamp(Position.X, 0, Map.MAP_SIZE.X * Map.TILE_SIZE.X),
                //MathHelper.Clamp(Position.Y, 0, Map.MAP_SIZE.Y * Map.TILE_SIZE.Y));
                if (nextPosition.X < 0) 
                {
                    Position.X = Map.MAP_SIZE.X * Map.TILE_SIZE.X;
                }
                else if (nextPosition.X > Map.MAP_SIZE.X * Map.TILE_SIZE.X - 10)
                {
                    Position.X = 0;
                }
                if (nextPosition.Y < 0)
                {
                    Position.Y = Map.MAP_SIZE.Y * Map.TILE_SIZE.Y;
                }
                else if (nextPosition.Y > Map.MAP_SIZE.Y * Map.TILE_SIZE.Y - 10)
                {
                    Position.Y = 0;
                }

                //Position = nextPosition;
                if (InputManager.Moving)
                {

                    _animationManager.Update(InputManager.velocity);
                    Shooting = false;

                }
            } else return;
            
            Weapon.Update();
            aura.Update(EnemyManager.enemies, player: this);
            shootingPos = InputManager.MouseWorldPosition;
            

            if (Weapon.GetType() == typeof(Shotgun))
            {
                _animationManagerReloading.Update("IdleShotgun");
            }
            if (Weapon.GetType() == typeof(SniperRifle))
            {
                _animationManagerReloading.Update("IdleSniper");
            }
            if (Weapon.GetType() == typeof(SixShooter))
            {
                _animationManagerReloading.Update("IdleRevolver");
            }
            if (Weapon.GetType() == typeof(MachineGun))
            {
                _animationManagerReloading.Update("IdleMachineGun");
            }
                





            
            if (!Dodging)
            {
                _animationManagerMovement.StopAnimation("dodge");
            }
            else
            {
                _animationManagerMovement.Update("dodge");
            }

            
            if (Shooting)
            {
                animTime -= Globals.TotalSeconds;
                if (animTime <= 0) Shooting = false;

                if (!Shooting)
                {
                    _animationManagerWeapon.StopAnimation("ShotgunBlast");
                    _animationManagerWeapon.StopAnimation("MachineGunBlast");
                    _animationManagerWeapon.StopAnimation("SniperBlast");
                    _animationManagerWeapon.StopAnimation("RevolverBlast");
                }
                //else
                //{
                //    _animationManagerWeapon.Update(null);
                //}

            }
            else if (this.Weapon.Reloading)
            {

                if (Weapon.GetType() == typeof(Shotgun))
                {
                    _animationManagerReloading.Update("ReloadingShotgun");
                }
                if (Weapon.GetType() == typeof(SniperRifle))
                {
                    _animationManagerReloading.Update("ReloadingSniper");
                }
                if (Weapon.GetType() == typeof(SixShooter))
                {
                    _animationManagerReloading.Update("ReloadingRevolver");
                }
                if (Weapon.GetType() == typeof(MachineGun))
                {
                    _animationManagerReloading.Update("ReloadingMachineGun");

                }
            }
            else
            {

                _animationManager.Update("idle");
                


            }

            if (DodgeCooldown > 0)
            {
                DodgeCooldown -= Globals.TotalSeconds;
                Dodging = true;
            }
            else
            {
                Dodging = false;
                _animationManagerMovement.StopAnimation("dodge");
            }

            if (InputManager.ShiftKey && InputManager.Moving)
            {

                if (DodgeCooldown <= 0)
                {
                    this.Dodge();
                    Dodging = true;
                    _animationManagerMovement.StartAnimation("dodge");

                }

            }





            if (InputManager.velocity != Vector2.Zero)
            {
                // += vel * Speed * Globals.TotalSeconds;
                var vel = Vector2.Normalize(InputManager.velocity);
                Position = new(
                    Position.X + (vel.X * Speed * Globals.TotalSeconds),
                    Position.Y + (vel.Y * Speed * Globals.TotalSeconds)
            );
            }


            var toMouse = InputManager.MouseWorldPosition - Position;
            Rotation = (float)Math.Atan2(-toMouse.X, toMouse.Y) + (float)Math.PI / 2;

            if (InputManager.MouseWorldPosition.X < Position.X)
            {
                IsFlipped = true;
            }
            else
            {
                IsFlipped = false;
            }

            if (InputManager.MouseLeftDown && !Weapon.Reloading && !InputManager.Moving)
            {
                if (Weapon is Shotgun)
                {
                    _animationManagerWeapon.StartAnimation("ShotgunBlast");
                    _animationManagerWeapon.Update("ShotgunBlast");

                    animTime = 0.12f;
                    Shooting = true;
                    Weapon.Fire(this);
                }
                if (Weapon is SniperRifle)
                {
                    _animationManagerWeapon.StartAnimation("SniperBlast");
                    _animationManagerWeapon.Update("SniperBlast");
                    animTime = 0.12f;
                    Shooting = true;
                    Weapon.Fire(this);
                }
                if (Weapon is MachineGun)
                {
                    _animationManagerWeapon.StartAnimation("MachineGunBlast");
                    _animationManagerWeapon.Update("MachineGunBlast");

                    animTime = 0.12f;
                    Shooting = true;
                    Weapon.Fire(this);
                }
                if (Weapon is OPGUN)
                {
                    _animationManagerWeapon.StartAnimation("MachineGunBlast");
                    _animationManagerWeapon.Update("MachineGunBlast");

                    animTime = 0.12f;
                    Shooting = true;
                    Weapon.Fire(this);
                }

                if (Weapon is SixShooter)
                {
                    _animationManagerWeapon.StartAnimation("RevolverBlast");
                    _animationManagerWeapon.Update("RevolverBlast");

                    animTime = 0.12f;
                    Shooting = true;
                    Weapon.Fire(this);
                }
                else
                {

                    Weapon.Fire(this);
                }

            }
            if (InputManager.Space)
            {
                if (aura.Active == true)
                {
                    aura.Stop();
                }
                else
                {
                    aura.Start();
                }


            }


            if (InputManager.MouseRightClicked)
            {

                Weapon.Reload();
            }




            if (InputManager.numpad1
                || InputManager.numpad2
                || InputManager.numpad3
                || InputManager.numpad4
                || InputManager.numpad5
                || InputManager.numpad9
                || InputManager.Fkey
                )
            {
                this.SwapWeapons();
            }
            _animationManager.Update(InputManager.velocity);

        }

        public override void Draw()
        {
            var offsetx = 0;
            var offsety = 0;
            aura.Draw(new(Position.X - offsetx, Position.Y - offsety));
            if (Dodging)
            {
                _animationManagerMovement.Draw(Position, Rotation);
            }
            else
            {
                _animationManager.Draw(new(Position.X - offsetx, Position.Y - offsety));
            }



            //float extraRotation = MathHelper.ToRadians(93);
            //var RealRotation = Rotation + extraRotation;
            _animationManagerReloading.Draw(new(Position.X, Position.Y), Rotation, IsFlipped);
            
            


            if (Shooting)
            {

                _animationManagerWeapon.Draw(Position, Rotation);


            }










        }

    }
}
