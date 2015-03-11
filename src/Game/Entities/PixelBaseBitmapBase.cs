using LinuxDoku.GameJam1.Game.State;
using LinuxDoku.GameJam1.Game.Texture;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Entities {
    public abstract class PixelBaseBitmapBase : PixelBase {
        protected PixelBaseBitmapBase(GameState gameState) : base(gameState) { }

        protected abstract Bitmap Bitmap { get; set; }
        
        protected override Color[] GetColorForTexture() {
            return Bitmap.ToTextureColors();
        }
    }
}