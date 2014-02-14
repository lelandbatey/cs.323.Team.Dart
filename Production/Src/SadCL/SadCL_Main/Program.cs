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
            string inLine, givenAct, givenMod; // Input-line, given-action, given-modifier

            //List<string> givenAct; // It's a list for multi-part statements.

            MissileLauncher.MissileLauncherManager mMan = MissileLauncher.MissileLauncherManager.Instance;

            //Should reset the turret before we even begin.
            mMan.reset();

            // // Print that we're actually ready to go!
            Console.WriteLine("Status: OPERATIONAL");
            Console.WriteLine("Gimme somethin' t' shoot!");

            while (!doneFlag) {

                // Get the line and do some string transforms
                System.Console.Write("> ");
                inLine = Console.ReadLine();

                //List<string> givenAct = new List<string>(inLine.Split(' '));


                givenAct = inLine.Split(' ')[0].ToLower();

                // We have get the modifier in this way because the line may contain many spaces. This way we go from the first space till the end of the line and set that to be our modifier
                if (inLine.Split(' ').Length > 1 ) 
                    givenMod = inLine.Substring(inLine.IndexOf(' ')+1); // '+1' is so we don't include the space in the string we get
                else 
                    givenMod = "";
                
                // Giant hairy if-elseif statement
                if (givenAct == "load"){

                    if (File.Exists(givenMod))
                        tMan.load(givenMod);
                    else
                        Console.WriteLine("File specified doesn't exist.");

                }
                else if (givenAct == "scoundrels")
                {
                    tMan.printEnemies();
                }
                else if (givenAct == "friends")
                {
                    tMan.printFriends();
                }
                else if (givenAct == "kill")
                {
                    // RIGHT NOW JUST A STUB, EXPAND THIS LATER ONCE WE HAVE THE MISSILE LAUNCHER!

                    tMan.takeAim(givenMod);
                    tMan.printAll();

                }
                else if (givenAct == "fire")
                {
                    mMan.fire();
                }
                else if (givenAct == "exit")
                { // Peace yo, we out
                    doneFlag = true;
                }
                else if (givenAct == "moveby")
                {

                    //For the time being, we are assuming the Physics representation of Spherical Coordinates:
                    //Theta is the angle from the z-axis, and phi is the angle from the postive x-axis.

                    bool kickOut = false;
                    // LET'S DO SOME STRING FLOGGING, YEAH!
                    givenMod = givenMod.Trim();
                    
                    if (! (givenMod.Split(' ').Length == 2) ) {
                        Console.WriteLine("NAH MAN, ur typin ur doubles real bad man. FIX IT FIX IT FIX IT!");
                        kickOut = true; // AAAAaaaaaand we out
                    }

                    double Theta = 0.0, Phi = 0.0;

                    // This does actual checking to see if they're actually valid doubles
                    if (! double.TryParse(givenMod.Split(' ')[0], out Theta) ) {
                        Console.WriteLine("Yeaaaaa.... Theta's not really a double dude. ");
                        kickOut = true;
                    }

                    if (! double.TryParse(givenMod.Split(' ')[1], out Phi)) {
                        Console.WriteLine("Sorry man, that Phi's not a proper double");
                        kickOut = true;
                    }

                    // If we got any trouble, we "kick out" by using the continue statement to go back to the top of the while loop
                    if (kickOut) {
                        continue;
                    }
                    mMan.moveBy(Theta, Phi);

                }
                else if (givenAct == "move")
                {
                    System.Console.WriteLine("I'mma movin!");
                }
                else if (givenAct == "status")
                {
                    mMan.status();
                }
                else if (givenAct == "reload")
                {
                    mMan.reload();
                }
                else if (givenAct == "reset")
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
