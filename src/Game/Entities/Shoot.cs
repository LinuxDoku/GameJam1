﻿using System.Collections.Generic;
using LinuxDoku.GameJam1.Game.Helper;
using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.State;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Entities {
    public class Shoot : PixelBase {
        public Shoot(GameState gameState) : base(gameState) {
            Color = Color.Red;
            Width = 5;
            Height = 5;
        }

        public Direction Direction { get; set; }

        public override void Update(GameTime gameTime, List<PixelBase> objects) {
            MoveShoot();

            base.Update(gameTime, objects);
        }

        protected override void OnBoundaryCollide(Direction direction) {
            Direction = DirectionHelper.Inverse(direction);
        }

        public void MoveShoot() {
            MoveByDirections(new [] { Direction });
        }
    }
}