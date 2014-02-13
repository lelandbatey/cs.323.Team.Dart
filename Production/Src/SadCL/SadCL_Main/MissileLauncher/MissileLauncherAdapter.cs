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

        public void moveBy(double phi, double theta)
        {
            if (phi < 0)
            {
                command_Right(phi * -1);
            }
            else
            {
                command_Left(phi);
            }

            if (theta < 0)
            {
                command_Down(theta * -1);
            }
            else
            {
                command_Up(theta);
            }
        }

        public void reload()
        {
            System.Console.WriteLine("Insert Sound of Shotgun Reloading.");
        }

        public void status()
        {
            System.Console.WriteLine("My status is that I have no status.");
        }

        public void reset()
        {
            command_reset();
        }
    }
}
