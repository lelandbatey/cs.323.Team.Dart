using System;
namespace SadCL.MissileLauncher
{
    interface IMissileLauncher
    {
        void fire();
        void moveBy();
        void reload();
        void status();
    }
}
