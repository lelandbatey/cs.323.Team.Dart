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
            masterList = TargetFactory.GetBuilder(filepath).ProductBuilder(filepath);
        }

        public List<Target> find(string name) { // Searches for all targets that have a given name
            name = name.ToLower();
            List<Target> toRet = new List<Target>();
            toRet = masterList.FindAll(s => s.Name == name);
            return toRet;
        }

        // While "find" naively returns all targets with a given name, "takeAim" does some checking to make sure the target we're looking for is one we're actually allowed to shoot at.
        public Target takeAim(string name) {
            List<Target> toRet = new List<Target>();
            if (find(name).Count == 0) {
                throw new ArgumentException("No target by that name");
            }
            Target tempTarg = find(name)[0];

            if (tempTarg.Friend == true) {
                throw new ArgumentOutOfRangeException("Target is friendly");
            }
            return tempTarg;
        }

        public void printEnemies() { }
        public void printFriends() { }

        // Private method declarations
        private void print(List<Target> targList ) {
            foreach (var item in targList) {
                Console.WriteLine(item);
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

        private void setToDead(string name) {
            List<Target> oldList = masterList;
            List<Target> newList = masterList;

            int replaceIndex = masterList.FindIndex(s => s.Name == name);
        }
        
    }    
}

