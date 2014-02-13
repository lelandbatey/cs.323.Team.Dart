using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MissileLauncherManager
    {
        // SINGLETON STUFF
        // taken from here: http://msdn.microsoft.com/en-us/library/ff650316.aspx
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

        public MissileLauncherManager() { }
        
        public void fire()
        {
            MissileTurret.fire();
        }
        public void moveBy()
        {
            MissileTurret.moveBy();
        }
    }
}
