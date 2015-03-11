using System.Collections.Generic;
using System.Linq;
using LinuxDoku.GameJam1.Game.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LinuxDoku.GameJam1.Game.State {
    public class SceneManager {
        public SceneManager() {
            Objects = new List<PixelBase>();
            GameState = GameState.Instance;
        }

        protected GameState GameState { get; set; }
        protected List<PixelBase> Objects { get; set; }

        public void Update(GameTime gameTime) {
            foreach (var obj in Objects) {
                obj.Update(gameTime, Objects.Where(x => x != obj));
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
            foreach (var obj in Objects) {
                spriteBatch.Draw(obj.GetTexture(graphicsDevice), obj.GetPosition());
            }
        }

        public void Add(PixelBase obj) {
            Objects.Add(obj);
        }

        public void Clear() {
            Objects.Clear();
        }
    }
}