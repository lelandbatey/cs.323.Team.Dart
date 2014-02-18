
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SadCL
{
    class Program
    {
		enum hardVar{
			vertMaxtick = 700,
			horizonMaxtick = 6000,
			vertMaxAngle = 45,
			horizonMaxAngle = 270
		}
        static void Main(string[] args) {



            // // Various setup stuff
            Target.TargetManager tMan = Target.TargetManager.Instance;
            bool doneFlag = false;
            string inLine, givenAct, givenMod; // Input-line, given-action, given-modifier

            MissileLauncher.MissileLauncherController mMan = new MissileLauncher.MissileLauncherController();
            //MissileLauncher.MissileLauncherController mMan = MissileLauncher.MissileLauncherController.Instance;

            //Should reset the turret before we even begin.
			mMan.reset();

            // // Print that we're actually ready to go!
            Console.WriteLine("Status: OPERATIONAL");
            Console.WriteLine("Gimme somethin' t' shoot!");

            while (!doneFlag) {

                // Get the line and do some string transforms
                System.Console.Write("> ");
                inLine = Console.ReadLine();

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

                } else if (givenAct == "scoundrels") {
                    tMan.printEnemies();
                } else if (givenAct == "friends") {
                    tMan.printFriends();
                } else if (givenAct == "kill") {
                    // RIGHT NOW JUST A STUB, EXPAND THIS LATER ONCE WE HAVE THE MISSILE LAUNCHER!

                    bool validTarget = true;
                    double X = 0.0;
                    double Y = 0.0;
                    double Z = 0.0;

                    try {
                        Tuple<double, double, double> targCoords = tMan.takeAim(givenMod);
                        //Tuple<double, double, double> targCoords = tMan.takeAim(givenMod);
                        tMan.printAll();

                        X = targCoords.Item1;
                        Y = targCoords.Item2;
                        Z = targCoords.Item3;
                    }
                    catch {
                        Console.WriteLine("List doesn't exist.");
                        validTarget = false;
                    }

                    if (validTarget == true) {
                        double r = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
                        double Theta = (Math.PI / 2) - Math.Acos(Z / r);
                        double Phi = Math.Atan2(Y, X);

                        // Uses non-relative tick conversion
                        Phi = sphToTick(Phi);
                        Theta = vertSphToTick(Theta);

                        mMan.move(Phi, Theta);
                        mMan.fire();
                    }

                } else if (givenAct == "fire") {
                    mMan.fire();
                } else if (givenAct == "exit") { // Peace yo, we out
                    doneFlag = true;
                } else if (givenAct == "moveby") {

                    //In the event we encounter an error, we do not
                    //want anything to happen to the turret.
                    bool dontMove = false;

					double Phi = 0.0, Theta = 0.0;
                    try {
                        List<double> input = getPhiTheta(givenMod);
                        Phi = input[0];
                        Theta = input[1];
                    }
                    catch {
                        dontMove = true;
                    }

					// uses relative tick-conversion for naive rotation
                    if (dontMove == false) {
                        Phi = sphToTickRel(Phi);
                        Theta = vertSphToTick(Theta);

                        mMan.moveBy(Phi, Theta);
                    }
					

				} else if (givenAct == "move") {

                    //In the event we encounter an error, we do not
                    //want anything to happen to the turret.
                    bool dontMove = false;
					
					double Phi = 0.0, Theta = 0.0;
                    try {
                        List<double> input = getPhiTheta(givenMod);
                        Phi = input[0];
					    Theta = input[1];
                    }
                    catch {
                        dontMove = true;
                    }
					
					// Uses non-relative tick conversion
                    if (dontMove == false) {
                        Phi = sphToTick(Phi);
                        Theta = vertSphToTick(Theta);

                        mMan.move(Phi, Theta);
                    }

				} else if (givenAct == "status") {
					mMan.status();
				} else if (givenAct == "reload") {
					mMan.reload();
				} else if (givenAct == "reset") {
					mMan.reset();
				} else {
					System.Console.WriteLine("Unknown Command Entered.");
				}
            }
        }

		// Convenience method to make testing easier
		public static void rotate(double amount) {
			amount = amount * 22.22222222;
			MissileLauncher.MissileLauncherController mMan = new MissileLauncher.MissileLauncherController();
			mMan.moveBy(amount,0.0);
			Thread.Sleep(800);
		}

		public static double sphToTick(double amount) {
			// Returns the absolute position where we need to go.
			return (sphToTickRel(amount) + 1000);
		}

		public static double sphToTickRel(double amount) {
			// Doesn't add the absolute 1000 
			return (radToDegrees(amount) * 22.22222);
		}
		public static double vertSphToTick(double amount) {
			// Used to make the conversion between Z amount and 
			return (radToDegrees(amount) * 15.55555);
		}

		public static double radToDegrees(double amount) {
			// Converts radians to degrees for easier math
			return (amount * 180 / Math.PI);
		}

		// Gets the user to input data
		public static List<double> getPhiTheta(string modifier){
			//bool kickOut = false;
			// LET'S DO SOME STRING FLOGGING, YEAH!
			modifier = modifier.Trim();

			if (!(modifier.Split(' ').Length == 2)) {
				Console.WriteLine("NAH MAN, ur typin ur doubles real bad man. FIX IT FIX IT FIX IT!");
                throw new Exception();
			}

			double Theta = 0.0, Phi = 0.0;

			// This does actual checking to see if they're actually valid doubles
			//if (!double.TryParse(modifier.Split(' ')[0], out Phi)) {
			//	Console.WriteLine("Yeaaaaa.... Phi's not really a double dude. ");
			//	kickOut = true;
			//}

			try {
				Phi = double.Parse(modifier.Split(' ')[0]);
			}
			catch {
				Console.WriteLine("Yeaaaaa.... Phi's not really a double dude. ");
                throw;
			}

			try {
				Theta = double.Parse(modifier.Split(' ')[1]);
			}
			catch {
				Console.WriteLine("Sorry man, that Theta's not a proper double");
                throw;
			}
			//if (!
			//	Console.WriteLine("Sorry man, that Theta's not a proper double");
			//	kickOut = true;
			//}

            //if (kickOut) {
            //    throw new ArgumentException("Unnacceptable input given.");
            //}

			List<double> toReturn = new List<double>();

			toReturn.Add(Phi);
			toReturn.Add(Theta);

			return toReturn;
		}

    }
}
