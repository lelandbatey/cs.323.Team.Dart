using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetBase;

namespace TargetFileIO
{
	public class TargetManager
	{
		// SINGLETON STUFF
		// taken from here: http://msdn.microsoft.com/en-us/library/ff650316.aspx
		private static TargetManager instance; // Our private instance of ourself
		public static TargetManager Instance {
			get {
				if (instance == null) {
					instance = new TargetManager();
				}
				return instance;
			}
		}
		// Gonna be a singleton!
		private TargetManager() { // Since our constructor's private, you gotta instantiate it using "Instance"
			currentFilePath = "";
		}
		// // // Field Declarations

		// // Public field declarations

		// // Private field declarations
		List<Target> masterList = new List<Target>(); // The "master" list of all the targets.
		string currentFilePath; // Path to the file that's currently loaded

		// // // Method Declarations

		// // Public method declarations
		public void load(string filepath) { // Given a filepath, loads the data into a master-list of targets
			currentFilePath = filepath;
			masterList = TargetFactory.BuildTargetList(filepath);
		}

		// Returns all the targets in the list.
		// Needed for the GUI
		public List<Target> getAll() {
			return masterList;
		}

		// This is needed for the GUI
		public void deleteAll() {
			masterList.Clear();
		}

		public void setToDead(string name) {
			int replaceIndex = masterList.FindIndex(s => s.name == name);
			masterList[replaceIndex].hit++;
		}
		public void setMasterList(List<Target> inList) {
			masterList.Clear();
			foreach (var item in inList) {
				masterList.Add(item);
			}
		}
	}
}
