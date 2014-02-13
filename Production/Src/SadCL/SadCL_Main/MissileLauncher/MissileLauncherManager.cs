using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherManager
    {
        //Turret faces dead center, bisecting the X-axis and the Y-axis
        //into its positive and negative components.
        private double currentTheta = 90.0;
        private double currentPhi = 90.0;

        // Singleton: http://msdn.microsoft.com/en-us/library/ff650316.aspx
        // Need to look at this later.  Some pretty wild stuff.
        private static MissileLauncherManager instance; // Our private instance of ourself
        public static MissileLauncherManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MissileLauncherManager();
                }
                return instance;
            }
        }
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
            //Math.
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
    }
}
