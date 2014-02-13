using System;
namespace SadCL.MissileLauncher
{
    public interface IMissileLauncher
    {
        void fire();
        void moveBy();
        void reload();
        void status();
    }
}
