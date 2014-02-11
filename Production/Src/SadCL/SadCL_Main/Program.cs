using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL
{
    class Program
    {
        static void Main(string[] args) {

            Target.TargetManager tMan = Target.TargetManager.Instance;

            bool doneFlag = false;

            string inLine, givenAct, givenMod; // Input-line, given-action, given-modifier

            while (!doneFlag) {

                // Get the line and do some string transforms
                inLine = Console.ReadLine();
                givenAct = inLine.Split(' ')[0].ToLower();
                if (inLine.Split(' ').Length > 1 ) 
                    givenMod = inLine.Substring(inLine.IndexOf(' '));
                else 
                    givenMod = "";
                
                // Giant hairy if-elseif statement
                if (givenAct == "load"){

                    if (File.Exists(givenMod))
                        tMan.load(givenMod);
                    else
                        Console.WriteLine("File specified doesn't exist.");
                    
                } else if (givenAct == "scoundrels" ) {
                    tMan.printEnemies();
                } else if (givenAct == "friends" ) {
                    tMan.printFriends();
                } else if (givenAct == "exit") { // Peace yo, we out
                    return;
                }

            }


        }
    }
}
