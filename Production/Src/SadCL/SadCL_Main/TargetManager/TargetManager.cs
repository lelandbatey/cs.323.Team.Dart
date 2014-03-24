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

        public List<Target> find(string name) { // Searches for all targets that have a given name
            //Process string
            name = name.ToLower();

            List<Target> toRet = new List<Target>();

            //Returns List of all matches or empty list.
            if (name.Length != 0)
                toRet = masterList.FindAll(s => s.Name == name);

            return toRet;
        }

        // While "find" naively returns all targets with a given name,
        //"findPrey" does some checking to make sure the target we're looking for is one we're actually allowed to shoot at.
        public Target findPrey(string name) {
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

        // Returns tuple of named targets X, Y, and Z coordinates, in that order.
        public Tuple<double, double, double> takeAim(string name) {

            Tuple<double, double, double> targCoord = null;

            //toRet is either Target or null
            Target toRet = findPrey(name);

            if (toRet != null) {
                targCoord = Tuple.Create<double, double, double>(toRet.X, toRet.Y, toRet.Z);
                setToDead(name);
            }

            return targCoord;
        }

        public void printEnemies() {
            print(listEnemies());
        }
        public void printFriends() {
            print(listFriends());
        }
        public void printAll() { print(masterList); }

        // // Private method declarations
        private void print(List<Target> targList ) {
            foreach (var item in targList) {
                Console.WriteLine(funnyTargetStr(item));
                //Console.WriteLine(item);

            }
        }
        private List<Target> listEnemies() {

            List<Target> toRet = new List<Target>();
            
            toRet = masterList.FindAll(s => s.Friend == false);

            if (toRet.Count == 0) {
                throw new ArgumentException("No enemies available.");
            }
            
            return toRet;
        }
        private List<Target> listFriends() {
            List<Target> toRet = new List<Target>();

            toRet = masterList.FindAll(s => s.Friend == true);

            if (toRet.Count == 0) {
                throw new ArgumentException("No friends available.");
            }

            return toRet;
        }

        // Makes a humorous string representation of a target.
        private string funnyTargetStr(Target inTarg) {
            string toRet = "";

            string name, friend, position, points, status;
            //string friend;
            //string position;
            //string points;

            name = String.Format("Target: {0}\n", inTarg.Name);

            if (inTarg.Friend) {
                friend = String.Format("Friend: ONE OF US\n");
            } else {
                friend = String.Format("Friend: Wrong, regretably a reprobate\n");
            }

            position = String.Format("Position: x={0}, y={1}, z={2}\n", inTarg.X, inTarg.Y, inTarg.Z);

            points = String.Format("Points: {0}\n", inTarg.Points);

            if (inTarg.dead) {
                status = "Status: Bit the big one\n";
            } else {
                status = "Status: On the lam\n";
            }

            toRet = String.Format("{0}{1}{2}{3}{4}", name, friend, position, points, status);
            return toRet;

        }

        private void setToDead(string name) {
            // Marks a given target as dead
            List<Target> oldList = masterList;
            List<Target> newList = masterList;

            int replaceIndex = masterList.FindIndex(s => s.Name == name);

            // We have to create a new mutableTarget out of the old one, set it to dead, then replace the old on in the masterlist
            // We have to do it this way because Targets are read only, so we have to destroy the old one to make a change to an existing one

            Target tmpTarg = masterList[replaceIndex];
            Target toReplace = new Target(tmpTarg);
            toReplace.dead = true;
            masterList[replaceIndex] = new Target(toReplace);
        }
        
    }    
}

