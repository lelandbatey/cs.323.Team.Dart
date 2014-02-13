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
        enum userInput
        {
            userCommand,
            userTheta,
            userPhi
        }
        static void Main(string[] args) {



            // // Various setup stuff
            Target.TargetManager tMan = Target.TargetManager.Instance;
            bool doneFlag = false;
            string inLine, givenMod; // Input-line, given-action, given-modifier

            //List<string> givenAct; // It's a list for multi-part statements.

            MissileLauncher.MissileLauncherManager mMan = MissileLauncher.MissileLauncherManager.Instance;

            //Should reset the turret before we even begin.
            mMan.reset();

            // // Print that we're actually ready to go!
            Console.WriteLine("Status: OPERATIONAL");
            Console.WriteLine("Gimme somethin' t' shoot!");

            while (!doneFlag) {

                // Get the line and do some string transforms
                System.Console.WriteLine("Enter Command:");
                inLine = Console.ReadLine();

                List<string> givenAct = new List<string>(inLine.Split(' '));

                givenAct[(int)userInput.userCommand].ToLower();

                //givenAct = inLine.Split(' ')[0].ToLower();

                // We have get the modifier in this way because the line may contain many spaces. This way we go from the first space till the end of the line and set that to be our modifier
                if (inLine.Split(' ').Length > 1 ) 
                    givenMod = inLine.Substring(inLine.IndexOf(' ')+1); // '+1' is so we don't include the space in the string we get
                else 
                    givenMod = "";
                
                // Giant hairy if-elseif statement
                if (givenAct[(int)userInput.userCommand] == "load"){

                    if (File.Exists(givenMod))
                        tMan.load(givenMod);
                    else
                        Console.WriteLine("File specified doesn't exist.");

                }
                else if (givenAct[(int)userInput.userCommand] == "scoundrels")
                {
                    tMan.printEnemies();
                }
                else if (givenAct[(int)userInput.userCommand] == "friends")
                {
                    tMan.printFriends();
                }
                else if (givenAct[(int)userInput.userCommand] == "kill")
                {
                    // RIGHT NOW JUST A STUB, EXPAND THIS LATER ONCE WE HAVE THE MISSILE LAUNCHER!

                    tMan.takeAim(givenMod);
                    tMan.printAll();

                }
                else if (givenAct[(int)userInput.userCommand] == "fire")
                {
                    mMan.fire();
                }
                else if (givenAct[(int)userInput.userCommand] == "exit")
                { // Peace yo, we out
                    doneFlag = true;
                }
                else if (givenAct[(int)userInput.userCommand] == "moveby")
                {
                    try
                    {
                        double Theta = Convert.ToDouble(givenAct[(int)userInput.userTheta]);
                        double Phi = Convert.ToDouble(givenAct[(int)userInput.userPhi]);
                        mMan.moveBy(Theta, Phi);
                    }
                    catch (FormatException Error)
                    {
                        System.Console.WriteLine(Error.Message);
                        System.Console.WriteLine("Need to enter both Theta and Phi, and both must be of type double.");
                    }
                }
                else if (givenAct[(int)userInput.userCommand] == "move")
                {
                    System.Console.WriteLine("I'mma movin!");
                }
                else if (givenAct[(int)userInput.userCommand] == "status")
                {
                    mMan.status();
                }
                else if (givenAct[(int)userInput.userCommand] == "reload")
                {
                    mMan.reload();
                }
                else if (givenAct[(int)userInput.userCommand] == "reset")
                {
                    mMan.reset();
                }
                else
                {
                    System.Console.WriteLine("Unknown Command Entered.");
                }
            }
        }
    }
}
