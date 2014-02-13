using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{

    //The Adapter inherits from both the legacy hardware, and the current format for
    //all missile launchers (Interface).
    public abstract class MissileLauncherAdapter : MissileLauncherHardware
    {
        public void moveBy(double phi, double theta)
        {
            if (phi < 0.0)
            {
                command_Right((int)phi);
            }
            else
            {
                command_Left((int)phi);
            }

            if (theta < 0.0)
            {
                command_Down((int)theta);
            }
            else
            {
                command_Up((int)theta);
            }
        }
    }

    public class DreamCheekyLauncher : MissileLauncherAdapter, IMissileLauncher
    {
        public string launcherName { get; private set; }
        public int launcherAmmo { get; private set; }

        public DreamCheekyLauncher(string passedName, int passedAmmo)
        {
            launcherName = passedName;
            launcherAmmo = passedAmmo;
        }

        public void fire()
        {
            if (launcherAmmo == (int)AmmoCount.EmptyAmmo)
            {
                System.Console.WriteLine("You must acquire more Vespene Gas! (Ammo)");
            }
            else
            {
                System.Console.WriteLine("PEW!");
                command_Fire();
                --launcherAmmo;
            }     
        }

        public void reload()
        {
            launcherAmmo = (int)AmmoCount.MaxAmmo;
        }

        public void status()
        {
            System.Console.WriteLine("Name: {0}", launcherName);
            System.Console.WriteLine("Ammo: {0}", launcherAmmo);
        }

        public void reset()
        {
            System.Console.WriteLine("Please wait while we return to origin.");
            command_reset();
        }
    }

    public class MockLauncher : MissileLauncherAdapter, IMissileLauncher
    {
        public string launcherName { get; private set; }
        public int launcherAmmo { get; private set; }

        public MockLauncher()
        {
            launcherName = "King Henry";
            launcherAmmo = 5;
        }
        public void fire()
        {
            System.Console.WriteLine("Cry 'God for Harry! England and Saint George!'");
        }
        new public void moveBy(double phi, double theta)
        {
            System.Console.WriteLine("Theta: {0}", theta);
            System.Console.WriteLine("Phi: {0}", phi);
        }
        public void reload()
        {
            System.Console.WriteLine("Once more unto the breach, dear friends, once more.");
        }
        public void status()
        {
            System.Console.WriteLine("I was once known as {0}", launcherName);
            System.Console.WriteLine("There are only {0} arrows left in my quiver!", launcherAmmo);
        }
        public void reset()
        {
            System.Console.WriteLine("To reset.  Or not to reset.  That.. is the question.");
        }
    }
}
