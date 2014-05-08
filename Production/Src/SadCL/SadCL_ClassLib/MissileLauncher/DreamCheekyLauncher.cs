using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public enum DreamAmmoCount
    {
        EmptyAmmo = 0,
        MaxAmmo = 4
    }

    public class DreamCheekyLauncher : MissileLauncherHardware, IMissileLauncher 
    {
        public string launcherName { get; private set; }
        public int launcherAmmo { get; private set; }


		// Sorry for the magic numbers :/

		// These are initialized to these particular numbers because they
		// represent the defaults for theta and phi. Phi is in the "middle",
		// and theta is pointed straight ahead.
        private double disTheta = 0.0;

        private double disPhi = 3000.0;


		public double getTheta() {
			return disTheta;
		}

		public double getPhi() {
			return disPhi;
		}
		public int getAmmo() {
			return launcherAmmo;
		}


        // Does bounds checking on horizontal rotation, not allowing it to go
        // past it's maximum, and not allowing it to go below zero
        public double currentPhi {
            get { return disPhi; }
            private set {
                if (value >= 5670) {
                    disPhi = 5670;
                } else if (value <= 0) {
                    disPhi = 0;
                } else {
                    disPhi = value;
                }
            }
        }

        // Doing bounds checking on aiming in the vertical
        public double currentTheta {
            get { return disTheta; }
            private set {
                if (value >= 700) {
                    disTheta = 700;
                } else if (value <= 0) {
                    disTheta = 0;
                } else {
                    disTheta = value;
                }
            }
        }

        public DreamCheekyLauncher(string passedName) {
            launcherName = passedName;
            launcherAmmo = (int)DreamAmmoCount.MaxAmmo;
			//move(5600, 0);
			//moveBy(-5600, 0);

        }

        public void fire() {
            if (launcherAmmo == (int)DreamAmmoCount.EmptyAmmo) {
                throw new IndexOutOfRangeException("We're out of ammunition!");
            } else {
                command_Fire();
                --launcherAmmo;
            }
        }

		// Could also be called "MoveTo", since it moves th launcher to a specified position
        public void move(double phi, double theta) {

			double pDifference = phi - currentPhi;
			double tDifference = theta - currentTheta;

			Debug.WriteLine(String.Format("\nPhi: {0}\ntheta: {1}\npDiff: {2}\ntDiff: {3}\ncurrentPhi: {4}\ncurrentTheta: {5}\n",phi,theta,pDifference,tDifference,currentPhi,currentTheta));

			this.moveBy(pDifference, tDifference);				

			currentPhi = phi;
			currentTheta = theta;
        }


        public void moveBy(double phi, double theta) {

            currentPhi = currentPhi + phi;
            currentTheta = currentTheta + theta;

            //As Phi increases, the Turret head turns left.
            //As Phi decreases, the Turret head turns right.
            if (phi < 0.0)  {
                command_Right(Math.Abs((int)phi));
            } else {
                command_Left(Math.Abs((int)phi));
            }

            //As Theta increases, the Turret head descends.
            //As Theta decreases, the Turret head elevates.
            if (theta < 0.0) {
                command_Down(Math.Abs((int)theta));
            } else {
                command_Up(Math.Abs((int)theta));
            }
        }

        public void reload() {
			//System.Console.WriteLine("We're reloaded!");
            launcherAmmo = (int)DreamAmmoCount.MaxAmmo;
        }

        public void status() {
            System.Console.WriteLine("Name: {0}", launcherName);
            System.Console.WriteLine("Ammo: {0}", launcherAmmo);
        }

        public void reset() {
			//System.Console.WriteLine("Please wait while we return to origin.");
            moveBy(6000, 700);
            moveBy(-2835, -650);
            currentPhi = 2835;
            currentTheta = 0;
			//System.Console.WriteLine("Reset Complete!");
        }
    }
}
