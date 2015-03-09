using LinuxDoku.GameJam1.Game.Contracts;
using LinuxDoku.GameJam1.Game.Entities;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Logic {
    public class Boundary : IHaveASize {
        public int Width { get; set; }
        public int Height { get; set; }

        public bool Validate(Vector2 position, float width = 1, float height = 1) {
            return position.X >= 0 && position.X <= Width - width &&
                   position.Y >= 0 && position.Y <= Height - height;
        }

        public bool Validate(PixelBase pixel) {
            return Validate(pixel.GetPosition());
        }
        
        public bool ValidateDirection(PixelBase pixel, Direction direction, float distance) {
            return GetMaxMovement(pixel, direction) >= distance;
        }

        public float GetMaxMovement(PixelBase pixel, Direction direction) {
            switch (direction) {
                case Direction.Up:
                    return pixel.Y.Value;
                case Direction.Down:
                    return Height - pixel.Y.Value - pixel.Height;
                case Direction.Left:
                    return pixel.X.Value;
                case Direction.Right:
                    return Width - pixel.X.Value - pixel.Width;
            }

            return 0;
        }
    }
}