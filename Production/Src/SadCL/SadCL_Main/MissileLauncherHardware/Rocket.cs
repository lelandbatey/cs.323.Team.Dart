using BuildDefender;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UsbLibrary;

namespace MissileLauncherExample
{


    public class RocketProgram
    {
        [STAThread]
        public static void Main(string[] args)
        {
            MissileLauncher missile = new MissileLauncher();            

            missile.command_switchLED(true);
            missile.command_reset();

            string input = "";
            while (input != "quit")
            {
                Console.Write("Enter Command: ");
                input = Console.ReadLine();

                string[] data = input.Split();

                if (data.Length < 2)
                {
                    if (input.TrimEnd().TrimStart().ToLower() == "reset")
                    {
                        Console.WriteLine("Resetting the launcher...please wait");
                        missile.command_reset();
                        continue;
                    }

                    Console.WriteLine("The command needs to be longer than two arguments {0}", input);
                    continue;
                }

                int movement = 0;
                bool works = int.TryParse(data[1].ToLower(), out movement);
                if (works == false)
                {
                    Console.WriteLine("The value was incorrect. {0}", data[1]);
                    continue;
                }
                Console.WriteLine("{0} - {1}", data[0], data[1]);

                switch (data[0].ToLower())
                {
                    case "f":
                        missile.command_Fire();
                        break;
                    case "l":
                        missile.command_Left(movement);
                        break;
                    case "r":
                        missile.command_Right(movement);
                        break;
                    case "u":
                        missile.command_Up(movement);
                        break;
                    case "d":
                        missile.command_Down(movement);
                        break;
                    case "q":
                        Console.WriteLine("Quitting.");
                        break;
                    default:
                        Console.WriteLine("The command was not understood.");
                        break;
                }
            }
            missile.command_switchLED(false);
            
        }

    }
}
  

