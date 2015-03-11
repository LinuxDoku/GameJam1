using LinuxDoku.GameJam1.Game.Entities;
using LinuxDoku.GameJam1.Game.Logic;
using NUnit.Framework;

namespace LinuxDoku.GameJam1.Tests {
    [TestFixture]
    public class CollisionTest {
        private class TestPixel : PixelBase {
            public TestPixel(int width, int height, int x, int y) : base(null) {
                Width = width;
                Height = height;
                X.Value = x;
                Y.Value = y;
            }
        }

        [Test]
        public void Should_Not_Collide_To_Each_Other() {
            var result = Collision.AreColliding(
                new TestPixel(100, 100, 10, 10),
                new TestPixel(100, 100, 200, 10)
            );

            Assert.IsFalse(result);
        }

        [Test]
        public void Should_Not_Collide_To_Direct_Neighbour() {
            var result = Collision.AreColliding(
                new TestPixel(100, 100, 300, 10),
                new TestPixel(100, 100, 200, 10)
            );
            
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_Collide_In_Bigger_One() {
            var result = Collision.AreColliding(
                new TestPixel(1000, 1000, 10, 10),
                new TestPixel(100, 100, 200, 100)
            );

            Assert.IsTrue(result);
        }

        [Test]
        public void Should_Collide_On_Overlap() {
            var pixelA = new TestPixel(100, 100, 0, 0);
            var pixelB = new TestPixel(100, 50, 0, 0);

            Assert.IsTrue(Collision.AreColliding(pixelA, pixelB));
        }
    }
}
