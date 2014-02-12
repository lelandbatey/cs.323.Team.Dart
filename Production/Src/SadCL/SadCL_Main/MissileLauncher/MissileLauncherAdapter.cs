using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherAdapter
    {
        public string launcherName { get; set; }
        public int launcherAmmo { get; set; }
        public void fire()
        {
            command_Fire();
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
