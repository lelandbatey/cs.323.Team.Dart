using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Target
{
	public class IniBuilder : TargetBuilder
	{

		// Used to make our lookups short
		private Dictionary<string, Action<string, Target>> router;

		public IniBuilder() {
			router = new Dictionary<string, Action<string, Target>>() {
				{ "name", setName },
				{ "x", setX },
				{ "y",   setY },
				{ "z", setZ },
				{ "friend", setFriend },
				{ "flashrate", setFlash },
				{ "points", setPoints}
			};
		}

		// Method to help de-clutter the fileReader loop.
		private void route(string tempLine, Target tempTarget) {
			string[] words = tempLine.Split('=');

			this.router[words[0]](words[1], tempTarget);
		}
		

		public override List<Target> ProductBuilder(string fileLocation) {
			StreamReader myFile = new StreamReader(@fileLocation);

			string tempLine = "";
			int counter = 0;// Tracks line, makes for more usefull errors
			List<Target> targList = new List<Target>();
			Target tempTarg = new Target();

			// Begin reading file
			while ((tempLine = myFile.ReadLine()) != null) {
				tempLine = tempLine.ToLower(); // Since names are case-insensitive, we can just "toLower" everything.
				tempLine = tempLine.Split('#')[0].Trim(); // Strips out comments and extraneous whitespace.

				if (tempLine.Length < 1) { // Skip this line
					++counter;
					continue;
				}

				// Check if our current line is a new target declaration
				if (tempLine == "[target]") {
					if (targetFinished(tempTarg) || targList.Count > 0) {
						targList.Add(tempTarg);
					}
					tempTarg = new Target();
				} else if (tempLine.Contains('=')) { // Alright, our line is something else!
					try { // Here we attempt to route data to the appropriate fields.
						route(tempLine, tempTarg);
					}
					catch (ArgumentException e) {
						Console.WriteLine("\nLine:", counter);
						throw e;
					}
				} else { // Otherwise, somethin janky's goin on with this line...
					throw new InvalidOperationException(String.Format("\n\tLine: {0}\n\tSomething gnarly's going on with the specified file", counter));
				}

				++counter;
			}

			// The final target won't normally get the chance to be added to the
			// list of targets, so we have to do a small manual check.
			if (targetFinished(tempTarg)) {
				targList.Add(tempTarg);
			}

			for (int i = 0; i < targList.Count; i++) {
				if (!targetFinished(targList[i])) {
					throw new InvalidOperationException("Not enough target attributes specified for at least one of your targets.");
				}
			}

			myFile.Close();
			return targList;
		}

		public bool targetFinished(Target inTarg){
			// An EXTEMELY crude thing that tests whether all a target's values have been set or not

			double testDub = 0;
			int testInt = 0;
			bool testBool = true;
			try {
				if (inTarg.Name == "CAPITALLETTERSCAN'THAPPEN!")
					return false;
			} catch (Exception) {
				return false;				
			}
			try {
				if (inTarg.X == testDub)
					testDub = inTarg.X;
			} catch (Exception) {
				return false;
			}
			try {
				if (inTarg.Y == testDub)
					testDub = inTarg.X;
			} catch (Exception) {
				return false;
			}
			try {
				if (inTarg.Z == testDub)
					testDub = inTarg.X;
			} catch (Exception) {
				return false;
			}
			try {
				if (inTarg.Friend == testBool)
					testDub = inTarg.X;
			}catch (Exception) {
				return false;
			}
			try {
				if (inTarg.Points == testInt)
					testDub = inTarg.X;
			} catch (Exception) {
				return false;
			}
			try {
				if (inTarg.Flashrate == testInt)
					testDub = inTarg.X;
			} catch (Exception) {
				return false;
			}
			
			if (inTarg == null) {
				return false;
			} else if (inTarg.X == null) {
				return false;
			} else if (inTarg.Y == null) {
				return false;
			} else if (inTarg.Z == null) {
				return false;
			} else if (inTarg.Friend == null) {
				return false;
			} else if (inTarg.Points == null) {
				return false;
			} else if (inTarg.Flashrate == null) {
				return false;
			}

			return true;
		}

		// This ended up being some-what of a utility method :/
		private string joiner(string thing1, object thing2) {
			return String.Format(thing1, thing2);
		}


		// Name Handler
		public void setName(string input, Target inTarget) {
			string tmpName = input;
			if (input.Contains(' ')) {
				throw new System.ArgumentException("Name field contains spaces.");
			}
			tmpName = String.Join("", tmpName.Split('"'));
			inTarget.Name = tmpName;
		}

		// Points handler
		public void setPoints(string input, Target inTarget) {
			int tmpIn = 0;
			if (int.TryParse(input, out tmpIn)) {
				inTarget.Points = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Points' not valid: '{0}'", input));
			}
		}
		// X handler
		public void setX(string input, Target inTarget) {
			double tmpIn = 0;
			if (double.TryParse(input, out tmpIn)) {
				inTarget.X = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'X' not valid: '{0}'", input));
			}
		}

		// Y handler
		public void setY(string input, Target inTarget) {
			double tmpIn = 0;
			if (double.TryParse(input, out tmpIn)) {
				inTarget.Y = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Y' not valid: '{0}'", input));
			}
		}

		// Z handler
		public void setZ(string input, Target inTarget) {
			double tmpIn = 0;
			if (double.TryParse(input, out tmpIn)) {
				inTarget.Z = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Z' not valid: '{0}'", input));
			}
		}

		// Friend handler
		public void setFriend(string input, Target inTarget) {
			bool tmpIn = false;
			if (bool.TryParse(input, out tmpIn)) {
				inTarget.Friend = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Friend' not valid: '{0}'", input));
			}
		}

		// Flash handler
		public void setFlash(string input, Target inTarget) {
			int tmpIn = 0;
			if (int.TryParse(input, out tmpIn)) {
				inTarget.Flashrate = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'FlashRate' not valid: '{0}'", input));
			}
		}
	}

}
