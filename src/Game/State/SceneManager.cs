using System.Collections.Generic;
using System.Linq;
using LinuxDoku.GameJam1.Game.Entities;
using LinuxDoku.GameJam1.Game.Level;
using LinuxDoku.GameJam1.Game.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LinuxDoku.GameJam1.Game.State {
    public class SceneManager {
        public SceneManager(LevelBase level) {
            Objects = new List<PixelBase>();
            Level = level;

            Setup();
        }

        protected List<PixelBase> Objects { get; set; }
        public LevelBase Level { get; protected set; }
        public Boundary Boundary { get; set; }
        public Boundary Viewport { get; set; } 

        public void Setup() {
            Level.Setup();
        }

        public void Update(GameTime gameTime) {
            Level.Update(gameTime);

            for (int i = 0; i < Objects.Count; i++) {
                var obj = Objects[i];
                obj.Update(gameTime, Objects.Where(x => x != obj).ToList());
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
            for (int i = 0; i < Objects.Count; i++) {
                var obj = Objects[i];
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