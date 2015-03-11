using LinuxDoku.GameJam1.Game.Entities;
using LinuxDoku.GameJam1.Game.Helper;
using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LinuxDoku.GameJam1.Game {
    public class MainGame : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;

        private GameState _gameState;

        public MainGame()
            : base() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font/Lucida Console");

            var viewport = new Boundary {
                Width = GraphicsDevice.Viewport.Width,
                Height = GraphicsDevice.Viewport.Height
            };
            
            _gameState = new GameState {
                Scene = new SceneManager {
                    Boundary = viewport,
                    Viewport = viewport
                }
            };

            _gameState.Scene.Add(new Player(_gameState, 8, _gameState.Scene.Viewport.LowerThird()));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            if (!_gameState.GameOver) {
                // update game state
                _gameState.Update(gameTime);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            // background color
            GraphicsDevice.Clear(Color.LightSkyBlue);

            _spriteBatch.Begin();

            if (_gameState.GameOver) {
                _spriteBatch.DrawString(_font, "Game Over", new Vector2(200, 100), Color.Red);
            }
            
            // scene
            _gameState.Scene.Draw(_spriteBatch, GraphicsDevice);

            // hud
            _spriteBatch.DrawString(
                _font,
                string.Format("Shots: {0}", _gameState.ShootsAvailable.ToString("000")),
                new Vector2(10, 10),
                Color.Black
            );
            _spriteBatch.DrawString(
                _font, 
                string.Format("{0}:{1}", gameTime.TotalGameTime.Minutes.ToString("00"), gameTime.TotalGameTime.Seconds.ToString("00")),
                new Vector2(_gameState.Scene.Viewport.Width - 70, 10),
                Color.Black
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
