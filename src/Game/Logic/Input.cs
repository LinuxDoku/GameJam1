using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace LinuxDoku.GameJam1.Game.Logic {
    public static class Input {
        public static IEnumerable<Direction> KeyboardToDirections(KeyboardState state) {
            var directions = new List<Direction>();

            if (state.IsKeyDown(Keys.Up)) {
                directions.Add(Direction.Up);
            }
            if (state.IsKeyDown(Keys.Down)) {
                directions.Add(Direction.Down);
            }
            if (state.IsKeyDown(Keys.Left)) {
                directions.Add(Direction.Left);
            }
            if (state.IsKeyDown(Keys.Right)) {
                directions.Add(Direction.Right);
            }

            return directions;
        } 
    }
}