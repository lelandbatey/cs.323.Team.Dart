using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadCL.MissileLauncher;
using System.Windows.Input;
using SadCL.MissileLauncher;
using SadCL.

namespace SadGui.ViewModel
{
    class MissileLauncherControlsViews : ViewModelBase
    {

        private MissileLauncherController mMan = new MissileLauncherController();

        public MissileLauncherControlsViews(IMissileLauncher launcher) {
            FireCommand = new DelegateCommand(Fire);
            moveUpCommand = new DelegateCommand(moveUp);
            moveDownCommand = new DelegateCommand(moveDown);
            moveLeftCommand = new DelegateCommand(moveLeft);
            moveRightCommand = new DelegateCommand(moveRight);
        }

        public void Fire() {
            mMan.fire();
            updateLauncherStatus();
        }

        public void moveUp() {
            mMan.moveBy(0, CoordConvert.vertSphToTick(moveAmnt));
            updateLauncherStatus();
        }

        public void moveDown() {
            mMan.moveBy(0, CoordConvert.vertSphToTick(-moveAmnt));
            updateLauncherStatus();
        }

        public void moveLeft() {
            mMan.moveBy(CoordConvert.sphToTickRel(moveAmnt), 0);
            updateLauncherStatus();
        }
        public void moveRight() {
            mMan.moveBy(CoordConvert.sphToTickRel(-moveAmnt), 0);
            updateLauncherStatus();
        }

        public ICommand moveLeftCommand { get; set; }
        public ICommand moveRightCommand { get; set; }
        public ICommand moveDownCommand { get; set; }
        public ICommand moveUpCommand { get; set; }
        public ICommand FireCommand { get; set; }

    }
}
