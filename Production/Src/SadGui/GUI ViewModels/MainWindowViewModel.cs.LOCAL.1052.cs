﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadCL.MissileLauncher;
using SadCLGUI.ViewModels;
using System.Windows;
using TweetSharp;
using SadCLGUI.GUI_ViewModels;


namespace SadCLGUI.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MissileControlViewModel m_selectedLauncher;
        private MenuBarViewModel menubar;
        private TargetBriefListViewModel brieflist;
		private ServerConnectionViewModel serverCon;
        private KillSwitchesViewModel _KillWhat;

        public MainWindowViewModel(List<Target.Target> RawList) {
            MissileTurret = new MissileControlViewModel(this);
            BriefList = new TargetBriefListViewModel(this);
            MenuBar = new MenuBarViewModel();
			ServerConnection = new ServerConnectionViewModel(this);
            KillWhat = new KillSwitchesViewModel();
        }

        public KillSwitchesViewModel KillWhat {
            get { return _KillWhat; }
            set { _KillWhat= value; }
        }

		public ServerConnectionViewModel ServerConnection {
			get { return serverCon; }
			set { serverCon = value; }
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
			
			if (curTarg != null && !curTarg.IsFriend && BriefList.FileIsLoaded) {
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

		public void setTargets(List<TargetBase.Target> targList) {
			BriefList.rebuildTargetsViewList(targList);
		}

		public void killTargets(List<TargetBase.Target> tList) {
			MissileLauncherManager launcher = MissileTurret.GetLauncher();

			TaskQueue.Add_Task(() => {
				launcher.reset();
			});
			//launcher.killCoords(0, 0, 0);
			//launcher.killCoords(1, 1, 0);

            if (KillWhat.KillAll) {
                foreach (var targ in tList) {
                    if (targ.status == 0) {
                        launcher.killCoords(targ.x, targ.y, targ.z);
                        //TwitterMachine.TwitterManager m_twitter = new TwitterMachine.TwitterMachine();

                        // Whereas before we would just blindly execute the killCoord command,
                        // this makes an action out of it, and passes that action to the scheduler.
                        // The scheduler then executes this code in it's own thread.
                        TaskQueue.Add_Task(() => {
                            launcher.killCoords(targ.x, targ.y, targ.z);
                        });

                        // launcher.killCoords(targ.x, targ.y, targ.z);
                    }
                }
            }
			else if (KillWhat.KillFriend) {
                foreach (var targ in tList) {
				    if (targ.status == 1) {
					    launcher.killCoords(targ.x, targ.y, targ.z);
                        //TwitterMachine.TwitterManager m_twitter = new TwitterMachine.TwitterMachine();
					
					    // Whereas before we would just blindly execute the killCoord command,
					    // this makes an action out of it, and passes that action to the scheduler.
					    // The scheduler then executes this code in it's own thread.
					    TaskQueue.Add_Task(() => {
						    launcher.killCoords(targ.x, targ.y, targ.z);
					    });
					
					    // launcher.killCoords(targ.x, targ.y, targ.z);
				        }
			        }
                }
            else { //For should be by default.
                foreach (var targ in tList) {
				    if (targ.status == 0 || targ.status == 1) {
					    launcher.killCoords(targ.x, targ.y, targ.z);
                        //TwitterMachine.TwitterManager m_twitter = new TwitterMachine.TwitterMachine();
					
					    // Whereas before we would just blindly execute the killCoord command,
					    // this makes an action out of it, and passes that action to the scheduler.
					    // The scheduler then executes this code in it's own thread.
					    TaskQueue.Add_Task(() => {
						    launcher.killCoords(targ.x, targ.y, targ.z);
					    });
					
					    // launcher.killCoords(targ.x, targ.y, targ.z);
				    }
			    }
            }
		}
    }
}
