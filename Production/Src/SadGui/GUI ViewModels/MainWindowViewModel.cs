using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadCL.MissileLauncher;
using SadCLGUI.ViewModels;

namespace SadCLGUI.GUI_ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MissileControlViewModel m_selectedLauncher;
        private MenuBarViewModel menubar;
        private TargetBriefListViewModel brieflist;

        public MainWindowViewModel(List<Target.Target> RawList) {
            MissileTurret = new MissileControlViewModel();
            BriefList = new TargetBriefListViewModel(RawList);
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

    }
}
