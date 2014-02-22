using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public enum DreamAmmoCount
    {
        EmptyAmmo = 0,
        MaxAmmo = 4
    }

    public class DreamCheekyLauncher : MissileLauncherAdapter, IMissileLauncher
    {
        public string launcherName { get; private set; }
        public int launcherAmmo { get; private set; }

        public DreamCheekyLauncher(string passedName) {
            launcherName = passedName;
            launcherAmmo = (int)DreamAmmoCount.MaxAmmo;
        }

        public void fire() {
            if (launcherAmmo == (int)DreamAmmoCount.EmptyAmmo) {
                System.Console.WriteLine("We're out of ammunition!");
            } else {
                System.Console.WriteLine("PEW!");
                command_Fire();
                --launcherAmmo;
            }
        }

        public void reload() {
            System.Console.WriteLine("We're reloaded!");
            launcherAmmo = (int)DreamAmmoCount.MaxAmmo;
        }

        public void status() {
            System.Console.WriteLine("Name: {0}", launcherName);
            System.Console.WriteLine("Ammo: {0}", launcherAmmo);
        }

        public void reset() {
            System.Console.WriteLine("Please wait while we return to origin.");
            command_reset();
            System.Console.WriteLine("Reset Complete!");
        }
    }
}
