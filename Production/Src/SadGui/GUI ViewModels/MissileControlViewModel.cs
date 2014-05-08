using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SadCL.MissileLauncher;

namespace SadCLGUI.ViewModels
{
    class MissileControlViewModel
    {
        private MissileLauncherManager m_launcher = new MissileLauncherManager();
		private SadCLGUI.ViewModels.MainWindowViewModel MainWindowVM;

        private int distance = 50;

        public MissileControlViewModel(SadCLGUI.ViewModels.MainWindowViewModel MWVM) {
			
			// Done to allow for mediator-type behaviour
			MainWindowVM = MWVM;
            
            FireCommand = new LauncherCommand(Fire);
            LeftCommand = new LauncherCommand(moveLeft);
            RightCommand = new LauncherCommand(moveRight);
            UpCommand = new LauncherCommand(moveUp);
            DownCommand = new LauncherCommand(moveDown);
            KillCommand = new DelegateCommand(kill);
			ResetCommand = new LauncherCommand(reset);
            ReloadCommand = new LauncherCommand(reload);
        }

		// Needed for our autonomous mode.
		public MissileLauncherManager GetLauncher() { return m_launcher; }

        public void Fire() {
            m_launcher.fire();
        }
        public void moveLeft() {
            m_launcher.moveBy(distance, 0);
        }
        public void moveRight() {
            m_launcher.moveBy(-distance, 0);
        }
        public void moveUp() {
            m_launcher.moveBy(0, distance);
        }
        public void moveDown() {
            m_launcher.moveBy(0, -distance);
        }

        public void kill() {
			MainWindowVM.kill(m_launcher);
        }
		public void reset() {
			m_launcher.reset();
		}
        public void reload() {
            m_launcher.reload();
        }

        public ICommand FireCommand { get; set; }
        public ICommand LeftCommand { get; set; }
        public ICommand RightCommand { get; set; }
        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }
        public ICommand KillCommand { get; set; }
		public ICommand ResetCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
    }
}
