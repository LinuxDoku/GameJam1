using LinuxDoku.GameJam1.Game.State;
using LinuxDoku.GameJam1.Game.Texture;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Entities {
    public abstract class PixelBaseBitmapBase : PixelBase {
        protected PixelBaseBitmapBase(GameState gameState) : base(gameState) { }

        protected abstract Bitmap Bitmap { get; set; }

        public override int Width {
            get { return Bitmap.GetWidth(); }
        }

        public override int Height {
            get { return Bitmap.GetHeight(); }
        }

        protected override Color[] GetColorForTexture() {
            return Bitmap.ToTextureColors();
        }
    }
}