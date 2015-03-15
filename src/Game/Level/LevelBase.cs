using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Level {
    public abstract class LevelBase {
        public abstract void Setup();
        public abstract void Update(GameTime gameTime);
    }
}