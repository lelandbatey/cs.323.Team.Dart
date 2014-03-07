using System;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace Target
{
	public class Target 
	{
		public Target(Target inTarg) {
			dead = inTarg.dead;
			Flashrate = inTarg.Flashrate;
			Friend = inTarg.Friend;
			Points = inTarg.Points;
			X = inTarg.X;
			Y = inTarg.Y;
			Z = inTarg.Z;
			Name = inTarg.Name;
			nameSet = true;
		}
		public Target(){}

		private string internalName;
		public double X;
		public double Y;
		public double Z;
		public bool Friend;
		public int Points;
		public int Flashrate;
		public bool dead = false;

		private bool nameSet = false;

		// A simple way to do "set once" for name
		public string Name {
			get {
				if (!nameSet) {
					throw new InvalidOperationException();
				}
				return internalName;
			}
			set {
				if (nameSet) {
					throw new InvalidOperationException();
				}
				this.internalName = value;
				this.nameSet = true;
			}
		}
		// Makes printing these blasted things much easier.
		public override string ToString() {
			string tmpStr = "Name={0}\nX={1}\nY={2}\nZ={3}\nFriend={4}\nPoints={5}\nFlashRate={6}\n";
			tmpStr = String.Format(tmpStr, Name, X, Y, Z, Friend, Points, Flashrate);
			return tmpStr;
		}
	}
}
