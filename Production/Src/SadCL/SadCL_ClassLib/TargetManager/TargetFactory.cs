using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Target
{
    public class TargetFactory
    {
        public TargetFactory() { }

        // Maaaaaaan, I was right the first time I built this freaking thing... 
        public static List<Target> BuildTargetList(string fileLocation){
            string fExtension = Path.GetExtension(fileLocation).ToLower();

            if (fExtension.Length == 0) { // We've not been given a proper file path (it doesn't have any kind of a file extension).
                throw new ArgumentException("Improper path to file provided.");
            }

            TargetBuilder targetBuilder;
            
            // Big hairy 'if' block to determine which kind of object to return.
            if (fExtension == ".ini") {
                targetBuilder = new IniBuilder();
            } else if (fExtension == ".txt") {
                targetBuilder = new IniBuilder();
            } else {
                throw new ArgumentException(String.Format("Don't know how to handle file of given file extension:{0}", fExtension));
            }

            List<Target> toReturn = targetBuilder.ProductBuilder(fileLocation);

            return toReturn;
        }
        
        
    }

    abstract public class TargetBuilder
    {
        abstract public List<Target> ProductBuilder(string fileLocation);
    }

}
