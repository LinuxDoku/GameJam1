using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.State {
    public class GameState {
        public GameState() {
            FrameStack = new List<FrameStackItem>();
            DelayedStack = new List<DelayedAction>();
            GameOver = false;

            // initial game state
            ShootsAvailable = 20f;
            ShootsRefillPerSecond = 1;
            FiresPerSecond = 5;
        }

        public void Update(GameTime gameTime) {
            GameTime = gameTime;

            var elapsedSeconds = (float) gameTime.ElapsedGameTime.Milliseconds / 1000;

            // scene
            Scene.Update(gameTime);

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

            // delayed stack
            var delayedItems = DelayedStack.Where(x => x.Milliseconds <= gameTime.TotalGameTime.TotalMilliseconds).ToList();
            for (int i = 0; i < delayedItems.Count; i++) {
                var delayedStackItem = delayedItems[i];
                delayedStackItem.Action(gameTime);
                DelayedStack.Remove(delayedStackItem);
            }
        }

        protected List<FrameStackItem> FrameStack { get; set; }
        protected List<DelayedAction> DelayedStack { get; set; } 
        public SceneManager Scene { get; set; }

        public GameTime GameTime { get; protected set; }
        public bool GameOver { get; protected set; }
        public TimeSpan GameOverTime { get; set; }

        public float ShootsRefillPerSecond { get; protected set; }
        public float ShootsAvailable { get; set; }
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

        public void RunInSeconds(float seconds, Action<GameTime> action) {
            int delayTime = 0;

            if (GameTime != null) {
                delayTime = (int) (GameTime.TotalGameTime.TotalMilliseconds + (seconds * 1000));
            }

            DelayedStack.Add(new DelayedAction(delayTime, action));
        }

        public void RunEvery(float seconds, Action<GameTime> action) {
            Action<GameTime> repeatingAction = null;

            repeatingAction = time => {
                action(time);
                RunInSeconds(seconds, repeatingAction);
            };

            RunInSeconds(0, repeatingAction);
        }

        public void ItsGameOver() {
            GameOver = true;
            GameOverTime = GameTime.TotalGameTime;
        }
    }
}