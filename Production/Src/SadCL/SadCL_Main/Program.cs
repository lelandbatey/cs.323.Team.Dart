using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL
{
    class Program
    {
        static void Main(string[] args) {

            Target.TargetManager targMan = Target.TargetManager.Instance;


            //List<string> paths = Target.TargetFactory.getTestPaths();

            //foreach (var item in paths) {
            //    Console.WriteLine(item);
            //    Target.TargetBuilder testBuilder = Target.TargetFactory.GetBuilder(item);
            //    Console.WriteLine(testBuilder is Target.TargetBuilder);
            //}

        }
    }
}
