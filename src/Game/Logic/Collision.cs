using System.Xml;
using LinuxDoku.GameJam1.Game.Entities;

namespace LinuxDoku.GameJam1.Game.Logic {
    public static class Collision {
        public static bool AreColliding(PixelBase a, PixelBase b) {
            // position check - fast
            if (a.X.Value + a.Width <= b.X.Value ||
                a.Y.Value + a.Height <= b.Y.Value ||
                a.X.Value >= b.X.Value + b.Width ||
                a.Y.Value >= b.Y.Value + b.Height) {
                return false;
            }

            // collision map

            return true;
        }

        public static Direction GetDirection(PixelBase pixelBase, PixelBase pixelBase1) {
            // TODO: finish, works for now
            return Direction.Up;
        }
    }
}