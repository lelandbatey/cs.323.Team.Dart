using System;
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

    public class MissileLauncherAdapter
    {

    }


    interface IMissileLauncher
    {
        void fire();
        void moveBy();
        void reload();
        void status();
    }
}
