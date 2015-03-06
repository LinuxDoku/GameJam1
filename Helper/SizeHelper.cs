using LinuxDoku.GameJam1.Game.Contracts;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Helper {
    public static class SizeHelper {
        public static Vector2 Center(this IHaveASize sizeable) {
            return new Vector2(sizeable.Width / 2, sizeable.Height / 2);
        }

        public static Vector2 UpperThird(this IHaveASize sizeable) {
            return new Vector2(sizeable.Width / 2, (sizeable.Height / 3) * 0.5f);
        }

        public static Vector2 LowerThird(this IHaveASize sizeable) {
            return new Vector2(sizeable.Width / 2, (sizeable.Height / 3) * 2.5f);
        }
    }
}