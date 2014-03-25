using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherController
    {
        private IMissileLauncher MissileTurret = MissileLauncherFactory.create_Launcher(LauncherTypes.DreamCheeky);
        
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
	}
}
