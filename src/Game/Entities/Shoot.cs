using System.Collections.Generic;
using LinuxDoku.GameJam1.Game.Contracts;
using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.State;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Entities {
    public class Shoot : PixelBase {
        public Shoot(GameState gameState) : base(gameState) {
            Color = Color.Red;
        }

        public Direction Direction { get; set; }
        public ICanShoot Shooter { get; set; }

        public override int Width {
            get { return 5; }
        }

        public override int Height {
            get { return 5; }
        }

        public override void Update(GameTime gameTime, List<PixelBase> objects) {
            MoveShoot();

            base.Update(gameTime, objects);
        }

        protected override void OnCollide(Direction direction, PixelBase gameObject) {
            if (gameObject is Enemy) {
                Destroy();
                Shooter.OnShotDestroyed(gameObject);
            }
        }

        protected override void OnBoundaryCollide(Direction direction) {
            Destroy();
        }

        public void MoveShoot() {
            MoveByDirections(new [] { Direction });
        }
    }
}