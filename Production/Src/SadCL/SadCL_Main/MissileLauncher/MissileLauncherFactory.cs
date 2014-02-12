using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherFactory
    {
        public enum LauncherTypes
        {
            Mock = 0,
            DreamCheeky = 1
        }

        public MissileLauncherAdapter create_Launcher(LauncherTypes Product)
        {
            //if (Product == LauncherTypes.DreamCheeky)
            //    return new DreamCheekyLauncher("HULK", 4);
            //else if (Product == LauncherTypes.Mock)
            //    producedObject = new Mock;
            //else
            //    System.Console.WriteLine("Unknown enumeration passed to factory.");

            return new MissileLauncherAdapter ("Sauron", 1);
        }
    }
}
