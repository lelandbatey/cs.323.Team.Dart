using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherManager
    {
        private IMissileLauncher MissileTurret = MissileLauncherFactory.create_Launcher(LauncherTypes.DreamCheeky);

        public MissileLauncherManager() { }
        
        public void fire()
        {
            MissileTurret.fire();
        }
    }
}
