using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherAdapter : MissileLauncherHardware, IMissileLauncher
    {
        public string launcherName { get; private set; }
        public int launcherAmmo { get; private set; }

        public MissileLauncherAdapter(string passedName, int passedAmmo)
        {
            launcherName = passedName;
            launcherAmmo = passedAmmo;
        }

        public void fire()
        {
            command_Fire();
        }

        public void moveBy()
        {
            System.Console.WriteLine("Fuel is pumping!");
        }

        public void reload()
        {
            System.Console.WriteLine("Insert Sound of Shotgun Reloading.");
        }

        public void status()
        {
            System.Console.WriteLine("My status is that I have no status.");
        }
    }
}
