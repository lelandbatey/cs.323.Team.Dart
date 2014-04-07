using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadCL.MissileLauncher;
using SadCLGUI.ViewModels;
using System.Windows;

namespace SadCLGUI.GUI_ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MissileControlViewModel m_selectedLauncher;
        private MenuBarViewModel menubar;
        private TargetBriefListViewModel brieflist;

        public MainWindowViewModel(List<Target.Target> RawList) {
            MissileTurret = new MissileControlViewModel(this);
            BriefList = new TargetBriefListViewModel(this);
            MenuBar = new MenuBarViewModel();
        }

        public MenuBarViewModel MenuBar {
            get {
                return menubar;
            }

            private set {
                menubar = value;
            }
        }

        public TargetBriefListViewModel BriefList {
            get {
                return brieflist;
            }
            set {
                brieflist = value;
            }
        } 

        public MissileControlViewModel MissileTurret
        {
            get
            {
                return m_selectedLauncher;
            }
            private set
            {
                m_selectedLauncher = value;
                OnPropertyChanged("MissileLauncher");
            }
        }

		// Implements the mediator pattern
		// Example of "blind" mediation. Coordinates without the "sender" specifying a destination
		public void kill(SadCL.MissileLauncher.MissileLauncherManager CurrentLauncher) {
			TargetViewModel curTarg = BriefList.SelectedTarget;
			
			if (!curTarg.IsFriend && BriefList.FileIsLoaded) {
				double X = curTarg.X;
				double Y = curTarg.Y;
				double Z = curTarg.Z;

				CurrentLauncher.killCoords(X, Y, Z);
				MessageBox.Show("Killed target: " + curTarg.Name);
				BriefList.killTarg(curTarg);
			} else {
				MessageBox.Show("Can't fulfill operation :(");
			}
		}

    }
}
