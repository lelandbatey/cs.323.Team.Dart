using System;
namespace SadCL.MissileLauncher
{
    public interface IMissileLauncher
    {
        void fire();
        void moveBy(int phi, int theta);
        void reload();
        void status();
    }
}
