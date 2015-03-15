using System.Collections.Generic;
using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.State;
using LinuxDoku.GameJam1.Game.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LinuxDoku.GameJam1.Game.Entities {
    public class Enemy : PixelBaseBitmapBase {
        public Enemy(GameState gameState) : base(gameState) {
            Bitmap = new Bitmap(6, 11, 6);

            Bitmap.Map = new [] {
                new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new short[] { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 }
            };

            Bitmap.Colors.Add(0, Color.Transparent);
            Bitmap.Colors.Add(1, Color.Black);

            // speed
            X.Speed[Direction.Left] = 1.0f;
            X.Speed[Direction.Right] = 1.0f;

            X.Speed[Direction.Down] = 1.5f;
        }

        protected override Bitmap Bitmap { get; set; }

        public override void Update(GameTime gameTime, List<PixelBase> objects) {
            MoveByDirections(new [] {
                Direction.Right
            });

            base.Update(gameTime, objects);
        }

        protected override void OnCollide(Direction direction, PixelBase gameObject) {
            if (gameObject is Shoot) {
                var shoot = gameObject as Shoot;
                Destroy();
            }
        }
    }
}