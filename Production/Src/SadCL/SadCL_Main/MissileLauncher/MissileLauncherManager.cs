using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherManager
    {
        private MissileLauncherAdapter MissileTurret = null;
        private MissileLauncherFactory Factory = new MissileLauncherFactory();
        public MissileLauncherManager()
        {
            MissileTurret = Factory.create_Launcher(LauncherTypes.DreamCheeky);
        }
        
        public void fire()
        {
            MissileTurret.fire();
        }
    }    

   
}
