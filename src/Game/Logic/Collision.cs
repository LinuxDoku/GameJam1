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
    }
}