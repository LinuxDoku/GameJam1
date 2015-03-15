using LinuxDoku.GameJam1.Game.Entities;
using LinuxDoku.GameJam1.Game.State;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Level {
    public class Level0 : LevelBase {
        private readonly GameState _gameState;
        public Level0(GameState gameState) {
            _gameState = gameState;
        }

        public override void Setup() {
            // spawn some enemies
            _gameState.RunEvery(5, x => {
                var enemy = new Enemy(_gameState);
                _gameState.Scene.Add(enemy);
            });
        }

        public override void Update(GameTime gameTime) {
            
        } 
    }
}