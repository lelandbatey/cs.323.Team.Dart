using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TargetFileIO;

namespace SadCLGUI.ViewModels
{
    class TargetBriefListViewModel
    {
        public TargetViewModel TargetsViewModel { get; set; }
        public ObservableCollection<TargetViewModel> Targets {get; private set; }
		public DelegateCommand getTargetFileLocationCommand { get; set; }

		// Target manager
		private TargetFileIO.TargetManager tMan;


		private SadCLGUI.ViewModels.MainWindowViewModel MainWindowVM;

		// Allows for the tracking of the indexes of the selected targets
		private int _targIndex;
		public int TargetIndex {
			get { return _targIndex; }
			set { _targIndex = value; }
		}

		// Bound the specific target selected in the view
		private TargetViewModel _selectTarg;
		public TargetViewModel SelectedTarget {
			get { return _selectTarg; }
			set { _selectTarg = value; }
		}

		

		public bool FileIsLoaded { get; private set; }

        public TargetBriefListViewModel(SadCLGUI.ViewModels.MainWindowViewModel MWVM) {
			tMan = TargetManager.Instance; // Initialize our target manager

			MainWindowVM = MWVM;

			Action loadZeTargets = loadTargets; // Sets up function to run when button gets clicked
			getTargetFileLocationCommand = new DelegateCommand(loadZeTargets);
            
            Targets = new ObservableCollection<TargetViewModel>();
        }

		// Coordinates getting file path, then loading file
		void loadTargets() {
			string filePath = getTargetFilePath();
			try {
				tMan.load(filePath);
				rebuildTargetsViewList(tMan.getAll());
				FileIsLoaded = true;
			}
			catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		// Gets file path from user
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

		void clearViewTargetsList() {
			// This exists because it's a common opperation, and makes it so you 
			// don't have to call the list of targets by name. Might be too much 
			// abstraction, but we're just gonna do this anyway lolololol
			Targets.Clear();
			FileIsLoaded = false;
		}

		// Given a list of targets, rebuilds the observable collection with them
		// Rather ugly
		public void rebuildTargetsViewList(List<TargetBase.Target> targList) {
			clearViewTargetsList();
			foreach (var item in targList) {
				Targets.Add(new TargetViewModel(item));
			}
			if (tMan.getAll().Count == 0) {
				tMan.setMasterList(targList);// Exists so that the master list will be rebuilt from the given list. Useful when receiving an external list (such as from the server				
			}
			FileIsLoaded = true;
		}

		public void killTarg(TargetViewModel targ) {
			tMan.setToDead(targ.Name);
			rebuildTargetsViewList(tMan.getAll());
		}
    }
}
