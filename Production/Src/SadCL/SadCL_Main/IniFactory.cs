using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Target
{
	// TargetFactory is similar in spirit to a factory
	public class IniBuilder
	{

		private string joiner(string thing1, object thing2) {
			return String.Format(thing1, thing2);
		}

		// Nice little pointer to our original print-wrapper
        //private static void print(params object[] var) { SAD_Console_Reader.Program.print(var); }
        //private static void inputWait() { SAD_Console_Reader.Program.inputWait(); }
        //private static string input() { return SAD_Console_Reader.Program.input(); }

		// Method to help de-clutter the fileReader loop.
		private static void route(string tempLine, mutableTarget tempTarget) {
			IniBuilder iniFactory = new IniBuilder();
			string[] words = tempLine.Split('=');

			iniFactory.router[words[0]](words[1], tempTarget);
		}

		// Will be used to make our lookups nice and short
		private Dictionary<string, Action<string, mutableTarget>> router;

		public IniBuilder() {
			// Here we instantiate our dictionary 'router'. We have to do it here instead of in the declaration because otherwise the compiler whines at us.
			router = new Dictionary<string,Action<string,mutableTarget>>() {
				{ "name", setName },
				{ "x", setX },
				{ "y",   setY },
				{ "z", setZ },
				{ "friend", setFriend },
				{ "flashrate", setFlash },
				{ "points", setPoints}
			};
		}

		// Reads the given file, returns a list of targets
		public static List<Target> ProductBuilder(string fileLocation) {
			StreamReader myFile = new StreamReader(@fileLocation);

			string tempLine = "";
			int counter = 0;
			List<mutableTarget> targList = new List<mutableTarget>();
			mutableTarget tempTarget = new mutableTarget();

			// We begin reading the file.
			while ((tempLine = myFile.ReadLine()) != null) {
				tempLine = tempLine.ToLower(); // Since names are case-insensitive, we can just "toLower" everything.
				tempLine = tempLine.Split('#')[0].Trim(); // Strips out comments and extraneous whitespace.

				if (tempLine.Length < 1) { // Skip this line
					++counter;
					continue;
				}
				
				// Check if our current line is a new target declaration
				if (tempLine == "[target]") {

					if (tempTarget.finished || targList.Count > 0) {
						targList.Add(tempTarget);
					}
					tempTarget = new mutableTarget();

				} else if (tempLine.Contains('=')) { // Alright, our line is something else!
					try { // Here we attempt to route data to the appropriate fields.
						route(tempLine, tempTarget);
					} catch (ArgumentException e) {
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
			if (tempTarget.finished) {
				targList.Add(tempTarget);
			}

			for (int i = 0; i < targList.Count; i++) {
				if (!targList[i].finished) {
					throw new InvalidOperationException("Not enough target attributes specified for at least one of your targets.");
				}
			}

			List<Target> staticTargList = new List<Target>();
			Target curTarg;
			// Convert list of mutableTargets to list of Targets(non-mutable)
			for (int i = 0; i < targList.Count; i++) {
				curTarg = new Target(targList[i]);
				staticTargList.Add(curTarg);
			}

			myFile.Close();
			return staticTargList;
		}


		// Name handler
		public void setName(string input, mutableTarget inTarget) {
			string tmpName = input;
			if (input.Contains(' ')) {
				throw new System.ArgumentException("Name field contains spaces.");
			}
			tmpName = String.Join("", tmpName.Split('"'));
			inTarget.Name = tmpName;
		}

		// Points handler
		public void setPoints(string input, mutableTarget inTarget) {
			// print(input);
			int tmpIn = 0;
			if (int.TryParse(input, out tmpIn)) {
				inTarget.Points = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Points' not valid: '{0}'", input));
			}
		}

		// X handler
		public void setX(string input, mutableTarget inTarget) {
			// print(input);
			double tmpIn = 0;
			if (double.TryParse(input, out tmpIn)) {
				inTarget.X = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'X' not valid: '{0}'", input));
			}
		}

		// Y handler
		public void setY(string input, mutableTarget inTarget) {
			// print(input);
			double tmpIn = 0;
			if (double.TryParse(input, out tmpIn)) {
				inTarget.Y = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Y' not valid: '{0}'", input));
			}
		}

		// Z handler
		public void setZ(string input, mutableTarget inTarget) {
			// print(input);
			double tmpIn = 0;
			if (double.TryParse(input, out tmpIn)) {
				inTarget.Z = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Z' not valid: '{0}'", input ));
			}
		}

		// Friend handler
		public void setFriend(string input, mutableTarget inTarget) {
			// print(input);
			bool tmpIn = false;
			if (bool.TryParse(input, out tmpIn)){
				inTarget.Friend = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'Friend' not valid: '{0}'", input));
			}
		}

		// Flash handler
		public void setFlash(string input, mutableTarget inTarget) {
			// print(input);
			int tmpIn = 0;
			if (int.TryParse(input, out tmpIn)) {
				inTarget.FlashRate = tmpIn;
			} else {
				throw new System.ArgumentException(joiner("Value of 'FlashRate' not valid: '{0}'", input));
			}
		}

	}	
}
