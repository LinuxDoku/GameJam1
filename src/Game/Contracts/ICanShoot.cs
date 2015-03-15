using System;
using LinuxDoku.GameJam1.Game.Entities;

namespace LinuxDoku.GameJam1.Game.Contracts {
    public interface ICanShoot {
        void OnShotDestroyed(PixelBase pixel);
    }
}