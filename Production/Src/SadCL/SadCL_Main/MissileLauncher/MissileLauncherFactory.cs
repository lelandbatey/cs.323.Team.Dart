using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    enum LauncherTypes
    {
        Mock = 0,
        DreamCheeky = 1
    }
    enum AmmoCount
    {
        EmptyAmmo = 0,
        MaxAmmo = 4
    }
    public class MissileLauncherFactory
    {
        public static IMissileLauncher create_Launcher(LauncherTypes Product)
        {
            IMissileLauncher generatedLauncher = null;

            if (Product == LauncherTypes.Mock)
            {
                generatedLauncher = new MockLauncher();
            }
            else if (Product == LauncherTypes.DreamCheeky)
            {
                generatedLauncher = new DreamCheekyLauncher("Photon Cannon", (int)AmmoCount.MaxAmmo);
            }
            else
                throw new ArgumentException("The product you want me to create doesn't exist in my database.");

            return generatedLauncher;
        }
    }
}
