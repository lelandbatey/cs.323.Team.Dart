using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherController
    {


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

        //public MissileLauncherController(){
        //    currentPhi = 3000.0;
        //}

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
            MissileTurret.moveBy(phi, theta);
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
