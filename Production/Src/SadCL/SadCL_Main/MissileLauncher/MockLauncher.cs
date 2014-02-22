using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadCL.MissileLauncher
{
    public class MockLauncher : IMissileLauncher
    {
        public string launcherName { get; private set; }
        public int launcherAmmo { get; private set; }

        public MockLauncher() {
            launcherName = "King Henry";
            launcherAmmo = 5;
        }
        public void fire() {
            System.Console.WriteLine("Cry 'God for Harry! England and Saint George!'");
        }
        public void moveBy(double phi, double theta) {
            System.Console.WriteLine("Theta: {0}", theta);
            System.Console.WriteLine("Phi: {0}", phi);
        }
        public void reload() {
            System.Console.WriteLine("Once more unto the breach, dear friends, once more.");
        }
        public void status() {
            System.Console.WriteLine("I was once known as {0}", launcherName);
            System.Console.WriteLine("There are only {0} arrows left in my quiver!", launcherAmmo);
        }
        public void reset() {
            System.Console.WriteLine("To reset.  Or not to reset.  That.. is the question.");
        }
    }
}
