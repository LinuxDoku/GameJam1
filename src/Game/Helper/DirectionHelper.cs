using LinuxDoku.GameJam1.Game.Logic;

namespace LinuxDoku.GameJam1.Game.Helper {
    public static class DirectionHelper {
        public static Direction Inverse(Direction direction) {
            switch (direction) {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
            }

            return Direction.Up;
        }
    }
}