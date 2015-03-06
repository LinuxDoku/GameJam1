using System.Collections.Generic;
using LinuxDoku.GameJam1.Game.Contracts;
using LinuxDoku.GameJam1.Game.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LinuxDoku.GameJam1.Game.Entities {
    public abstract class Pixel : IHaveASize {
        protected Pixel() {
            X = new Axis();
            Y = new Axis();
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public Boundary Boundary { get; set; }
        public Axis X { get; set; }
        public Axis Y { get; set; }

        protected Texture2D TextureCache { get; set; }

        public Vector2 GetPosition() {
            return new Vector2(X.Value, Y.Value);
        }

        public virtual Texture2D GetTexture(GraphicsDevice graphicsDevice) {
            if (TextureCache == null) {
                TextureCache = new Texture2D(graphicsDevice, Width, Height, false, SurfaceFormat.Color);
                TextureCache.SetData(GetColorForTexture());
            }
            return TextureCache;
        }

        public virtual void SetPosition(Vector2 position) {
            if (Boundary.Validate(position)) {
                X.Value = (int)position.X;
                Y.Value = (int)position.Y;
            }
        }

        public virtual void Move(Direction direction, float distance) {
            var axis = GetAxis(direction);

            if (axis != null) {
                if (Boundary != null && !Boundary.ValidateDirection(this, direction, distance)) {
                    distance = Boundary.GetMaxMovement(this, direction);
                    if (distance <= 0) {
                        OnBoundaryCollide(direction);
                        return;
                    }
                }

                if (direction == Direction.Up || direction == Direction.Left) {
                    axis.Value -= distance;
                } else {
                    axis.Value += distance;
                }
            }
        }

        public virtual Axis GetAxis(Direction direction) {
            switch (direction) {
                case Direction.Up:
                    return Y;
                case Direction.Down:
                    return Y;
                case Direction.Left:
                    return X;
                case Direction.Right:
                    return X;
            }

            return null;
        }

        public void MoveByDirections(IEnumerable<Direction> directions) {
            foreach (var direction in directions) {
                Move(direction, GetSpeed(direction));
            }
        }

        protected float GetSpeed(Direction direction) {
            return GetAxis(direction).GetSpeed(direction);
        }

        protected virtual Color[] GetColorForTexture() {
            var colors = new Color[Width * Height];
            for (var x = 0; x < colors.Length; x++) {
                colors[x] = Color;
            }
            return colors;
        }

        protected virtual void OnBoundaryCollide(Direction direction) {}
    }
}