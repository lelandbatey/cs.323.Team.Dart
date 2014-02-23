using System;
namespace SadCL.MissileLauncher
{
    public interface IMissileLauncher
    {
        void fire();

        void move(double phi, double theta);
        void moveBy(double phi, double theta);
        void reload();
        void status();
        void reset();   //Though it isn't a requirement, it's convenient to have.
    }
}
