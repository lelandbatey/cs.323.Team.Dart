using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Linq;
//using System.Text;

namespace Target
{
    public class TargetFactory
    {
        public TargetFactory() { }

        public static TargetBuilder GetBuilder(string fileLocation){
            string fExtension = Path.GetExtension(fileLocation).ToLower();

            if (fExtension.Length == 0) { // We've not been given a proper file path (it doesn't have any kind of a file extension).
                throw new ArgumentException("Improper path to file provided.");
            }

            TargetBuilder toReturn;
            // Big hairy 'if' block to determine which kind of object to return.
            if (fExtension == ".ini") {
                toReturn = new IniBuilder();
            } else if (fExtension == ".txt") {
                toReturn = new IniBuilder();
            } else {
                throw new ArgumentException(String.Format("Don't know how to handle file of given file extension:{0}", fExtension));
            }

            return toReturn;
        }
        
        
    }

    abstract public class TargetBuilder
    {
        abstract public List<Target> ProductBuilder(string fileLocation);
    }

}
