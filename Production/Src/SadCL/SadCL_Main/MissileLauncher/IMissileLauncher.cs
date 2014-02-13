using System;
namespace SadCL.MissileLauncher
{
    public interface IMissileLauncher
    {
        void fire();
        void moveBy(double phi, double theta);
        void reload();
        void status();
        void reset();
    }
}
