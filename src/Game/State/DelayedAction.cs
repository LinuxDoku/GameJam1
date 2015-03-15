using System;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.State {
    public class DelayedAction {
        public DelayedAction(int gameTimeMilliseconds, Action<GameTime> action) {
            Milliseconds = gameTimeMilliseconds;
            Action = action;
        }

        public int Milliseconds { get; protected set; }
        public Action<GameTime> Action { get; protected set; } 
    }
}