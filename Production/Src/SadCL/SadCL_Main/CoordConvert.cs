using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL
{
    public static class CoordConvert
    {
        // Convenience method to make testing easier
        public static void rotate(double amount) {
            amount = amount * 22.22222222;
            MissileLauncher.MissileLauncherController mMan = new MissileLauncher.MissileLauncherController();
            mMan.moveBy(amount, 0.0);
            System.Threading.Thread.Sleep(800);
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


        //Input:  String of Two Doubles
        //Req:  Two values must be generated from split.
        //Req:  Both values must successfully convert to be added.
        //Return Success:  List of Two Values
        //Return Failure:  List of No Values
        public static List<double> getPhiTheta(string modifier) {
            // LET'S DO SOME STRING FLOGGING, YEAH!
            modifier = modifier.Trim();

            //Variables that will valid values generated from modifier string.
            double Theta = 0.0, Phi = 0.0;

            //Our return variable.
            List<double> toReturn = new List<double>();

            //Two values must be generated from split.
            if (modifier.Split(' ').Length == 2) {

                bool PhiFlag = Double.TryParse(modifier.Split(' ')[0], out Phi);
                bool ThetaFlag = Double.TryParse(modifier.Split(' ')[1], out Theta);

                //Both values must successfully convert to be added.
                if (PhiFlag && ThetaFlag) {
                    toReturn.Add(Phi);
                    toReturn.Add(Theta);
                } else if (!PhiFlag) {
                    Console.WriteLine("The Phi you entered wasn't valid.");
                } else {
                    Console.WriteLine("The Theta you entered wasn't valid.");
                }
            } else {
                Console.WriteLine("A Phi or Theta wasn't entered.");
            }

            return toReturn;
        }
    }
}
