using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherManager
    {
        private IMissileLauncher MissileTurret = MissileLauncherFactory.create_Launcher(LauncherTypes.DreamCheeky);

		private const double HORIZ_RATIO = (5670.0 / 270.0);
        
        public void fire()
        {
            MissileTurret.fire();
        }
        public void moveBy(double phi, double theta)
        {
            MissileTurret.moveBy(phi, theta);
        }
        public void move(double phi, double theta)
        {
            MissileTurret.move(phi, theta);
        }
        public void status()
        {
            MissileTurret.status();
        }
        public void reload()
        {
            MissileTurret.reload();
        }
        public void reset()
        {
            MissileTurret.reset();
        }

		public double getPhi() {
			return MissileTurret.getPhi();
		}
		public double getTheta() {
			return MissileTurret.getTheta();
		}
		public int getAmmo() {
			return MissileTurret.getAmmo();
		}

		// Moves to and kills a series of xyz coordinates
		public void killCoords(double x, double y, double z) {
			// Convert xyz coords to spherical coords
			double r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
			double Theta = (Math.PI / 2) - Math.Acos(z / r); // We subtract from pi/2 because the launcher can only rotate 270 degrees, which is 90 less than 360. 90 degrees is pi/2
			double Phi = Math.Atan2(y, x);

			Theta = radToDeg(Theta);
			Phi = radToDeg(Phi);

			Debug.WriteLine(Theta);
			Debug.WriteLine(Phi);

			Phi = horizontalToTick(Phi);
			Theta = verticalToTick(Theta);

			Debug.WriteLine(Theta);
			Debug.WriteLine(Phi);

			MissileTurret.move(Phi, Theta);
			MissileTurret.fire();

		}

		double radToDeg(double rads) {
			return (rads * 180 / Math.PI);
		}

		double verticalToTick(double degrees) {
			return (degrees * 15.5555555);
		}

		double horizontalToTick(double degrees) {
			return ((degrees * HORIZ_RATIO) + 1000);
			//return (horizontalToTickRel(degrees) + 1000); // The plus 1000 is to compensate for the extra amount the launcher *could* turn
		}

		double horizontalToTickRel(double degrees) {
			return (degrees * HORIZ_RATIO);
		}
	}
}
