using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Entities {
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
            // initial game state
            ShootsAvailable = 20f;
            ShootsRefillPerSecond = 1;
            FiresPerSecond = 2;
        }

        public void Update(GameTime gameTime) {
            var elapsedSeconds = (float) gameTime.ElapsedGameTime.Milliseconds / 1000;

            ShootsAvailable += elapsedSeconds * ShootsRefillPerSecond;
            FiresLastSecond -= elapsedSeconds * FiresPerSecond;
        }

        public float ShootsRefillPerSecond { get; protected set; }
        public float ShootsAvailable { get; protected set; }
        public float FiresPerSecond { get; protected set; }
        public float FiresLastSecond { get; protected set; }

        public bool RequestFire() {
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
    }
}