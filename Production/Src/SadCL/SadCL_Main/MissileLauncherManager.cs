﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL
{
    public class MissileLauncherManager
    {
        //Tells when the MissileLauncher to "Fire".
        //Tells how much the MissileLauncher should "MoveBy".
        //Tells where the MissileLauncher should "Move".
        //Tells MissileLauncher to "Reload".
        //Returns details about the MissileLauncher's "Status".
        
        //Converts from Cartesian to Spherical.
        //Tracks number of missiles in launcher.
        //Has launcher instance.
        //Has launcher factory.
        //Has launcher class definition - I think.

    }    

    public class MissileLauncherAdapter : MissileLauncherHardware
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


    interface IMissileLauncher
    {
        void fire();
        void moveBy();
        void reload();
        void status();
    }

    interface IDreamLauncher : IMissileLauncher
    {
        void battleCry();
    }
}
