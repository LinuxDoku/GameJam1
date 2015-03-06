using System.Collections.Generic;
using System.Linq;

namespace LinuxDoku.GameJam1.Game.Logic {
    public class Axis {
        public Axis() {
            Speed = new Dictionary<Direction, float>();
        }

        public float Value { get; set; }
        public IDictionary<Direction, float> Speed { get; set; }

        public float GetSpeed(Direction direction) {
            if (Speed.ContainsKey(direction)) {
                return Speed[direction];
            }

            return 0;
        }
    }
}