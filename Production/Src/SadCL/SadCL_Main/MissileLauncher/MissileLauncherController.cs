using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherController
    {
        //Turret faces dead center, bisecting the X-axis and the Y-axis
        //into its positive and negative components.
        private double disTheta = 0.0;
		private double disPhi = 3000.0;
		//private double currentPos = 3000.0;

		public double currentPhi {
			get{ return disPhi; }
			private set {
				if (value >= 6000) {
					disPhi = 6000;
				} else if (value <= 0) {
					disPhi = 0;
				} else {
					disPhi = value;
				}

			}
		}
		public double currentTheta {
			get { return disTheta; }
			private set {
				if (value >= 700) {
					disTheta = 700;
				} else if (value <= 0) {
					disTheta = 0;
				} else {
					disTheta = value;
				}

			}
		}

        // Singleton: http://msdn.microsoft.com/en-us/library/ff650316.aspx
        // Need to look at this later.  Some pretty wild stuff.
        //private static MissileLauncherController instance; // Our private instance of ourself
        //public static MissileLauncherController Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new MissileLauncherController();
        //        }
        //        return instance;
        //    }
        //}

		public MissileLauncherController(){
			currentPhi = 3000.0;
		}

        private IMissileLauncher MissileTurret = MissileLauncherFactory.create_Launcher(LauncherTypes.DreamCheeky);
        
        public void fire()
        {
            MissileTurret.fire();
        }
        public void moveBy(double phi, double theta)
        {
			currentPhi = currentPhi + phi;
			currentTheta = currentTheta + theta;
            MissileTurret.moveBy(phi, theta);
        }
        public void move(double phi, double theta)
        {
			//Console.WriteLine(currentPhi);
			//Console.WriteLine("Phi: {0}", phi);
			//Console.WriteLine("Theta: {0}", theta);
			double pDifference = phi - currentPhi;
			double tDifference = theta - currentTheta;
			//Console.WriteLine(pDifference);
			//Console.WriteLine(tDifference);
			
			MissileTurret.moveBy(pDifference, tDifference);				
			
			currentPhi = phi;
			currentTheta = theta;

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
			moveBy(6000, 700);
			moveBy(-3000, -700);
			currentPhi = 3000;
			currentTheta = 0;
			//MissileTurret.reset();
        }
    }
}
