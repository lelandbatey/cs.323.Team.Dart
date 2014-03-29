using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SadCLGUI.GUI_ViewModels
{
    class TargetBriefListViewModel
    {
        public TargetViewModel TargetsViewModel { get; set; }
        public ObservableCollection<TargetViewModel> Targets {get; private set; }
		public DelegateCommand getTargetFileLocationCommand { get; set; }

		private Target.TargetManager tMan;


        public TargetBriefListViewModel(List<Target.Target> RawList) {
			tMan = Target.TargetManager.Instance; // Initialize our target manager


			Action loadZeTargets = loadTargets; // Sets up function to run when button gets clicked
			getTargetFileLocationCommand = new DelegateCommand(loadZeTargets);
            
            Targets = new ObservableCollection<TargetViewModel>();
            foreach (Target.Target target in RawList) {
                Targets.Add(new TargetViewModel(target));    
            }
        }

		string getTargetFilePath() {
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.FileName = "Target Config";
			dlg.DefaultExt = ".ini";
			dlg.Filter = "Target Config File (.ini)|*.ini|All files - may not load correctly (*.*)|*.*"; // Regexes for filtering files. First `.ini`, then anything

			string fileName = "";
			Nullable<bool> result = dlg.ShowDialog();
			if (result == true) {
				fileName = dlg.FileName;
			}
			return fileName;
		}

		void loadTargets() {
			string filePath = getTargetFilePath();
			try {
				tMan.load(filePath);
				rebuildTargetsViewList(tMan.getAll());
			}
			catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		void clearViewTargetsList() {
			// This exists because it's a common opperation, and makes it so you 
			// don't have to call the list of targets by name. Might be too much 
			// abstraction, but we're just gonna do this anyway lolololol
			Targets.Clear();
		}

		// Given a list of targets, rebuilds the observable collection with them
		// Rather ugly
		void rebuildTargetsViewList(List<Target.Target> targList) {
			clearViewTargetsList();
			foreach (var item in targList) {
				Targets.Add(new TargetViewModel(item));
			}
		}
    }
}
