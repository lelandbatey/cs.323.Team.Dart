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

        // Gets the user to input data
        public static List<double> getPhiTheta(string modifier) {
            // LET'S DO SOME STRING FLOGGING, YEAH!
            modifier = modifier.Trim();

            if (!(modifier.Split(' ').Length == 2)) {
                Console.WriteLine("NAH MAN, ur typin ur doubles real bad man. FIX IT FIX IT FIX IT!");
                throw new Exception();
            }

            double Theta = 0.0, Phi = 0.0;

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

            List<double> toReturn = new List<double>();

            toReturn.Add(Phi);
            toReturn.Add(Theta);

            return toReturn;
        }
    }
}
