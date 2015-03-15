using System.Collections.Generic;
using LinuxDoku.GameJam1.Game.Contracts;
using LinuxDoku.GameJam1.Game.Helper;
using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.State;
using LinuxDoku.GameJam1.Game.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LinuxDoku.GameJam1.Game.Entities {
    public class Player : PixelBaseBitmapBase, ICanShoot {
        public Player(GameState gameState, int speed, Vector2 startPosition, bool centerStartPosition = true) : base(gameState){
            // bitmap
            Bitmap = new Bitmap(6, 11, 8);

            Bitmap.Map[0] = new short[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
            Bitmap.Map[1] = new short[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
            Bitmap.Map[2] = new short[] { 0, 2, 0, 0, 1, 1, 1, 0, 0, 2, 0 };
            Bitmap.Map[3] = new short[] { 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1 };
            Bitmap.Map[4] = new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            Bitmap.Map[5] = new short[] { 1, 0, 3, 0, 0, 0, 0, 0, 3, 0, 1 };

            Bitmap.Colors.Add(0, Color.Transparent);
            Bitmap.Colors.Add(1, Color.Black);
            Bitmap.Colors.Add(2, Color.Red);
            Bitmap.Colors.Add(3, Color.Orange);
            Bitmap.Colors.Add(4, Color.DarkGray);

            // speed
            Y.Speed.Add(Direction.Up, speed);
            Y.Speed.Add(Direction.Down, speed / 1.7f);
            X.Speed.Add(Direction.Left, speed / 1.3f);
            X.Speed.Add(Direction.Right, speed / 1.3f);

            // position
            X.Value = startPosition.X;
            Y.Value = startPosition.Y;

            if (centerStartPosition) {
                X.Value -= Width / 2;
                Y.Value -= Height / 2;
            }
        }

        protected override Bitmap Bitmap { get; set; }

        public override void Update(GameTime gameTime, List<PixelBase> objects) {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && GameState.RequestShoot()) {
                var pos = this.TopCenter();
                var shoot = new Shoot(GameState) {
                    X = {
                        Value = X.Value + pos.X
                    },
                    Y = {
                        Value = Y.Value - 5
                    },
                    Shooter = this
                };
                shoot.Y.Speed[Direction.Up] = Y.GetSpeed(Direction.Up) * 2.0f;
                shoot.Y.Speed[Direction.Down] = shoot.Y.Speed[Direction.Up];

                GameState.Scene.Add(shoot);
            }

            MoveByDirections(Input.KeyboardToDirections(Keyboard.GetState()));

            base.Update(gameTime, objects);
        }

        protected override void OnCollide(Direction direction, PixelBase gameObject) {
            if (gameObject is Shoot || gameObject is Enemy) {
                GameState.ItsGameOver();
            }
        }

        public void OnShotDestroyed(PixelBase pixel) {
            GameState.ShootsAvailable += 5;
        }
    }
}