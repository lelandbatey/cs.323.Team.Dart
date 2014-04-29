using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetBase
{
	public class Target
	{
		public int id { get; set; }
		public int status { get; set; } // Indicates the "friendlyness". 0 is enemy; any positive value is freind
		public int hit { get; set; } // Number of times the target has been hit. Non-zero value also indicates that the target is "dead"
		public bool movingState { get; set; }
		public int led { get; set; }
		public string name { get; set; }
		public double spawnRate { get; set; }
		public bool isMoving { get; set; }
		public double points { get; set; }
		public double startTime { get; set; }
		public double x { get; set; }
		public double y { get; set; }
		public double z { get; set; }
		public int input { get; set; }
		public double dutyCycle { get; set; }
	}
}
