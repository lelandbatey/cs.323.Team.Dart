using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetBase
{
	/// <summary>
	/// Exists to convert `OldTarget`s into `Target`s. This is a pretty gnarly way to handle legacy code, but we gotta do it.
	/// It's ugly, but for right now it's what we have.
	/// </summary>
	public static class TargetConverter
	{
	
		public static Target old2new(OldTarget old){
			Target newt = new Target();

			newt.x = old.X;
			newt.y = old.Y;
			newt.z = old.Z;

			newt.status = Convert.ToInt32(old.Friend);
			newt.points = old.Points;
			newt.led = old.Flashrate;
			newt.name = old.Name;
			newt.hit = Convert.ToInt32(old.dead);

			return newt;

		}

		public static List<Target> OldTargetList2NewTargetList(List<OldTarget> old) {
			List<Target> toReturn = new List<Target>();
			foreach (var item in old) {
				toReturn.Add(old2new(item));
			}
			return toReturn;
		}
	}

}
