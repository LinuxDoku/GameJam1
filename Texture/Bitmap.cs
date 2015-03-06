using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LinuxDoku.GameJam1.Game.Texture {
    public class Bitmap {
        public Bitmap(int height, int width, int scale=1) {
            Height = height;
            Width = width;
            Scale = scale;
            
            Map = new short[height][];

            Colors = new Dictionary<short, Color>();
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public int Scale { get; protected set; }

        public short[][] Map { get; set; }
        public IDictionary<short, Color> Colors { get; set; }

        public Color[] ToTextureColors() {
            var colors = new Color[GetWidth() * GetHeight()];
            var i = 0;

            for (var x = 0; x < Map.Length; x++) {
                for (var xScale = 0; xScale < Scale; xScale++) {
                    for (var y = 0; y < Map[x].Length; y++) {
                        for (var yScale = 0; yScale < Scale; yScale++) {
                            colors[i] = Colors[Map[x][y]];
                            i++;
                        }
                    }
                }
            }

            return colors;
        }

        public int GetWidth() {
            return Width * Scale;
        }

        public int GetHeight() {
            return Height * Scale;
        }
    }
}