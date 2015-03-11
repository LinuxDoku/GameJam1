using LinuxDoku.GameJam1.Game.Entities;
using LinuxDoku.GameJam1.Game.Helper;
using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LinuxDoku.GameJam1.Game {
    public class MainGame : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SpriteFont _font;

        private SceneManager _scene;
        private Player _player;
        private Boundary _viewport;


        public MainGame()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font/Lucida Console");

            _scene = new SceneManager();
            _viewport = new Boundary() {
                Width = GraphicsDevice.Viewport.Width,
                Height = GraphicsDevice.Viewport.Height
            };

            _player = new Player(8, _viewport.LowerThird()) {
                Boundary = _viewport
            };
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

            if (!GameState.Instance.GameOver) {

                // update game state
                GameState.Instance.Update(gameTime);

                _scene.Update(gameTime);

                // shoot
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && GameState.Instance.RequestShoot()) {
                    var pos = _player.TopCenter();
                    var shoot = new Shoot {
                        X = {
                            Value = _player.X.Value + pos.X
                        },
                        Y = {
                            Value = _player.Y.Value - 5
                        },
                        Boundary = _viewport
                    };
                    shoot.Y.Speed[Direction.Up] = _player.Y.GetSpeed(Direction.Up) * 2.0f;
                    shoot.Y.Speed[Direction.Down] = shoot.Y.Speed[Direction.Up];

                    _scene.Add(shoot);
                }

                // move player
                _player.MoveByDirections(Input.KeyboardToDirections(Keyboard.GetState()));
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.LightSkyBlue);

            spriteBatch.Begin();

            if (GameState.Instance.GameOver) {
                spriteBatch.DrawString(_font, "Game Over", new Vector2(200, 100), Color.Red);
            }

            spriteBatch.Draw(_player.GetTexture(GraphicsDevice), _player.GetPosition());
            
            // scene
            _scene.Draw(spriteBatch, GraphicsDevice);

            // hud
            spriteBatch.DrawString(_font, string.Format("Shots: {0}", GameState.Instance.ShootsAvailable.ToString("000")), new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(_font, string.Format("{0}:{1}", gameTime.TotalGameTime.Minutes.ToString("00"), gameTime.TotalGameTime.Seconds.ToString("00")), new Vector2(_viewport.Width - 70, 10), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
