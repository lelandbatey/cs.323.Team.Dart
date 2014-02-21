using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{

    //The Adapter inherits from the legacy hardware to provide necessary functionality.
    public class MissileLauncherAdapter : MissileLauncherHardware
    {
        //Whereas the legacy hardware can move in 4 directions, the Interface provides
        //only 2 values:  Theta and Phi.  Need to translate those coordinates into directions.
        public void moveBy(double phi, double theta)
        {
			
            //As Phi increases, the Turret turns counterclockwise.
            //As Phi decreases, the Turret turns clockwise.
            if (phi < 0.0)  //Don't know if magic numbers are still a thing here.
            {
				command_Right(Math.Abs((int)phi));
            }
            else
            {
                command_Left(Math.Abs((int)phi));
            }

            //As Theta increases, the Turret head descends.
            //As Theta decreases, the Turret head elevates.
            if (theta < 0.0)
            {
                command_Down(Math.Abs((int)theta));
            }
            else
            {
                command_Up((int)theta);
            }
        }
    }

    

    public class MockLauncher : IMissileLauncher
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
