using System;

namespace Target
{
    // Target is a read-only class, allowing one to look at these targets, but not to
    // change them. By making it read-only, we are able to ensure that no target can
    // have it's data messed with.

    // Something to note is that the creation of the Target class relies on the
    // "mutableTarget" class. mutableTargets are just that, target classes that are
    // nearly the exact same as the vanilla Target class, but they have public
    // setters. Creating a Target class requires that first you gather all your data
    // into a mutableTarget and ensure that it's internal state is set to "finished".
    // Once that happens, you can create a new Target out of that mutableTarget.
    public class Target
    {

        public Target() {    }

        public Target(mutableTarget inTarget) {
            if (inTarget.finished) {
                Name = inTarget.Name;
                X = inTarget.X;
                Y = inTarget.Y;
                Z = inTarget.Z;
                Friend = inTarget.Friend;
                Points = inTarget.Points;
                FlashRate = inTarget.FlashRate;
                dead = inTarget.isDead;
            } else {
                throw new InvalidOperationException("Not enough target attributes specified for at least one of your targets.");
            }
        }

        public string Name { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public bool Friend { get; private set; }
        public int Points { get; private set; }
        public int FlashRate { get; private set; }
        public bool dead { get; private set; }
        
        // Makes printing these blasted things much easier.
        public override string ToString() {
            string tmpStr = "Name={0}\nX={1}\nY={2}\nZ={3}\nFriend={4}\nPoints={5}\nFlashRate={6}\n";
            tmpStr = String.Format(tmpStr, Name, X, Y, Z, Friend, Points, FlashRate);
            return tmpStr;
        }
        
        
    }



    // mutableTarget is pretty much a normal Target, but it has public getters and
    // setters (for all it's attributes at least). Most of the error checking
    // actually happens in the "TargetFactory" class.
    public class mutableTarget
    {
        private string Nameval;
        private double Xval;
        private double Yval;
        private double Zval;
        private bool Friendval;
        private int Pointsval;
        private int FlashRateval;
        private bool finishedval;

        private int[] setlist;

        //private void print(object var) { SAD_Console_Reader.Program.print(var); }

        public mutableTarget() {
            finishedval = false;
            setlist = new int[7];
            isDead = false;
        }

        public mutableTarget(Target inTarget) {
            isDead = inTarget.dead;
            setlist = new int[7];
            FlashRate = inTarget.FlashRate;
            Friend = inTarget.Friend;
            Points = inTarget.Points;
            X = inTarget.X;
            Y = inTarget.Y;
            Z = inTarget.Z;
            Name = inTarget.Name;
            finishedval = true;
        }

        // Why did I implement all of these manually....
        public string Name {
            get { return Nameval; }
            set {
                Nameval = value;
                setlist[0] = 1;
            }
        }
        public double X {
            get { return Xval; }
            set {
                Xval = value;
                setlist[1] = 1;
            }
        }
        public double Y {
            get { return Yval; }
            set {
                Yval = value;
                setlist[2] = 1;
            }
        }
        public double Z {
            get { return Zval; }
            set {
                Zval = value;
                setlist[3] = 1;
            }
        }
        public bool Friend {
            get { return Friendval; }
            set {
                Friendval = value;
                setlist[4] = 1;
            }
        }
        public int Points {
            get { return Pointsval; }
            set {
                Pointsval = value;
                setlist[5] = 1;
            }
        }
        public int FlashRate {
            get { return FlashRateval; }
            set {
                FlashRateval = value;
                setlist[6] = 1;
            }
        }
        public bool finished {
            get {
                // Checks if all the other flags have been set, then sets finished to true if they've all been called.
                int tmp = 0;
                for (int i = 0; i < 7; i++) {
                    if (setlist[i] == 1) {
                        tmp++;
                    }
                }
                if (tmp >= 7) {
                    finishedval = true;
                }
                return finishedval;
            }
            set { finishedval = value; }
        }

        // Public variable to handle death.
        public bool isDead;

        // Makes printing these blasted things much easier.
        public override string ToString() {
            string tmpStr = "Name={0}\nX={1}\nY={2}\nZ={3}\nFriend={4}\nPoints={5}\nFlashRate={6}\nfinished={7}\n";
            tmpStr = String.Format(tmpStr, Nameval, Xval, Yval, Zval, Friendval, Pointsval, FlashRateval, finished);
            return tmpStr;
        }

        public static string pigConvert(string inString) {
            // Converts a string to it's piglatin representation.
            // Look at it on wikipedia for more information.
            string toReturn = "";
            bool vowelBegin = false;
            inString = inString.Trim();
            // Check if our string begins with a vowel
            for (int i = 0; i < "aeiou".Length; i++) {
                if (inString[0] == "aeiou"[i]) {
                    vowelBegin = true;
                }
            }

            if (vowelBegin) {
                toReturn = inString + "way";
            } else {
                toReturn = inString.Substring(1, inString.Length - 1) + inString[0] + "ay";
            }

            return toReturn;
        }
    }
}

