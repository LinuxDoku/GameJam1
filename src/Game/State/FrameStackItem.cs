using System;

namespace LinuxDoku.GameJam1.Game.State {
    public class FrameStackItem {
        public FrameStackItem(int frames, Action<int, int> action) {
            Frames = frames;
            Action = action;
            Elapsed = 0;
        }

        public int Frames { get; protected set; }
        public int Elapsed { get; protected set; }
        public Action<int, int> Action { get; protected set; }

        public void Execute() {
            if (!DeleteMe()) {
                Action(Frames, ++Elapsed);
            }
        }

        public bool DeleteMe() {
            return Frames == Elapsed;
        }
    }
}