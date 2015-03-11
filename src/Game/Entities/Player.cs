using LinuxDoku.GameJam1.Game.Logic;
using LinuxDoku.GameJam1.Game.Texture;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Entities {
    public class Player : PixelBaseBitmapBase {
        public Player(int speed, Vector2 startPosition, bool centerStartPosition = true) {
            // bitmap
            Bitmap = new Bitmap(6, 11, 8);

            Bitmap.Map[0] = new short[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
            Bitmap.Map[1] = new short[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
            Bitmap.Map[2] = new short[] { 0, 2, 0, 0, 1, 1, 1, 0, 0, 2, 0 };
            Bitmap.Map[3] = new short[] { 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1 };
            Bitmap.Map[4] = new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            Bitmap.Map[5] = new short[] { 1, 0, 3, 0, 0, 0, 0, 0, 3, 0, 1 };

            Bitmap.Colors.Add(0, Color.Transparent);
            Bitmap.Colors.Add(1, Color.Black);
            Bitmap.Colors.Add(2, Color.Red);
            Bitmap.Colors.Add(3, Color.Orange);
            Bitmap.Colors.Add(4, Color.DarkGray);

            // size
            Width = Bitmap.GetWidth();
            Height = Bitmap.GetHeight();

            // speed
            Y.Speed.Add(Direction.Up, speed);
            Y.Speed.Add(Direction.Down, speed / 1.7f);
            X.Speed.Add(Direction.Left, speed / 1.3f);
            X.Speed.Add(Direction.Right, speed / 1.3f);

            // position
            X.Value = startPosition.X;
            Y.Value = startPosition.Y;

            if (centerStartPosition) {
                X.Value -= Width / 2;
                Y.Value -= Height / 2;
            }
        }

        protected override Bitmap Bitmap { get; set; }
    }
}