using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.State {
    public class GameState {
        private static GameState _instance;

        public static GameState Instance {
            get {
                if (_instance == null) {
                    _instance = new GameState();
                }

                return _instance;
            }
        }

        public GameState() {
            FrameStack = new List<FrameStackItem>();
            GameOver = false;

            // initial game state
            ShootsAvailable = 20f;
            ShootsRefillPerSecond = 3 ;
            FiresPerSecond = 5;
        }

        public void Update(GameTime gameTime) {
            var elapsedSeconds = (float) gameTime.ElapsedGameTime.Milliseconds / 1000;

            // player
            ShootsAvailable += elapsedSeconds * ShootsRefillPerSecond;
            FiresLastSecond -= elapsedSeconds * FiresPerSecond;

            // frame stack
            for (int i = 0; i < FrameStack.Count; i++) {
                var frameStackItem = FrameStack[i];
                frameStackItem.Execute();

                if (frameStackItem.DeleteMe()) {
                    FrameStack.Remove(frameStackItem);
                }
            }
        }

        protected List<FrameStackItem> FrameStack { get; set; }

        public SceneManager Scene { get; set; }
        public bool GameOver { get; protected set; }

        public float ShootsRefillPerSecond { get; protected set; }
        public float ShootsAvailable { get; protected set; }
        public float FiresPerSecond { get; protected set; }
        public float FiresLastSecond { get; protected set; }

        public bool RequestShoot() {
            if (CanFire()) {
                FiresLastSecond += 1;
                ShootsAvailable -= 1;
                return true;
            }

            return false;
        }

        public bool CanFire() {
            return ShootsAvailable >= 1 && FiresLastSecond < FiresPerSecond;
        }

        public void RunTimes(int frames, Action<int, int> action) {
            FrameStack.Add(new FrameStackItem(frames, action));
        }

        public void ItsGameOver() {
            GameOver = true;
        }
    }
}