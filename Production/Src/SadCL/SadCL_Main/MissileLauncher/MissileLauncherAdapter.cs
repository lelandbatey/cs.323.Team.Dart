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
                command_Up(Math.Abs((int)theta));
            }
        }
    }
}
