using System;
using System.Collections.Generic;

// namespace "Target" is going to contain all our various things related to Targets
namespace Target
{
    public class TargetManager
    {
        // SINGLETON STUFF
        // taken from here: http://msdn.microsoft.com/en-us/library/ff650316.aspx
        private static TargetManager instance; // Our private instance of ourself
        public static TargetManager Instance{
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


        // // Field Declarations

        // Public field declarations

        // Private field declarations
        List<Target> masterList; // The "master" list of all the targets.
        string currentFilePath; // Path to the file that's currently loaded

        // // Method Declarations

        // Public method declarations
        public void load(string filepath) { // Given a filepath, loads the data into a master-list of targets
            currentFilePath = filepath;
            masterList = IniBuilder.ProductBuilder(filepath);
        }

        public List<Target> find(string name) { // Searches for all targets that have a given name
            List<Target> toRet = new List<Target>();
            toRet = masterList.FindAll(s => s.Name == name);
            return toRet;
        }
        public void printEnemies() { }
        public void printFriends() { }

        // Private method declarations
        private void print(List<Target> targList ) {
            foreach (var item in targList) {
                
            }
        }
        private List<Target> listEnemies() {
            List<Target> toRet = new List<Target>();
            toRet = masterList.FindAll(s => s.Friend == false);
            return toRet;
        }
        private List<Target> listFriends() {
            List<Target> toRet = new List<Target>();
            toRet = masterList.FindAll(s => s.Friend == true);
            return toRet;
        }
        
    }    
}

