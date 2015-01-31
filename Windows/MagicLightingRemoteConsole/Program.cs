using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MagicLightingRemoteCore;

namespace MagicLightingRemoteConsole
{
    /// <summary>
    /// Using the console, send color codes to a RGB LED lamp by emulating
    /// the Magic remote control.
    /// Usage: appname color_code [/Q] | [/V]
    /// - color_code can be a hex or decimal number or color name
    /// - /Q Do not display any output to the console
    /// - /V show version information
    /// </summary>
    class Program
    {
        private static bool _quiet;

        static void Main(string[] args)
        {
            // Check arguments
            if ((args.Length != 1 && args.Length != 2) ||
                (args.Length == 1 && args[0] == "/?"))
            {
                WriteApplicationUsage();
                Exit(1);
            }

            if (args[0].ToLower() == "/v")
            {
                ShowVersionInfo();
                Exit(0);
            }

            // Parse color code from argument
            MagicColor magicColor;
            if (!MagicColor.TryParse(args[0], out magicColor))
            {
                WriteLine("'{0}' is not a valid color code.", args[0]);
                WriteApplicationUsage();
                Exit(1);
            }

            _quiet = ParseOptionalArgumentQuiet(args);

            // Send the color to the arduino
            string message;
            if (ArduinoCommunication.SendColorCode(magicColor, Settings.Default.PortName))
            {
                // Store the port used for successful communication as optimization
                Settings.Default.PortName = ArduinoCommunication.PortName;
                Settings.Default.Save();
                message = "Successfully send the color code.";
            }
            else
            {
                message = "Failed to send the color code.";
            }
            WriteLine(string.Format("{0} (0x{1} / {2} / {3})", message, magicColor.Code.ToString("X2"), magicColor.Code.ToString("D3"), magicColor.Name));

            Exit(0);
        }

        private static void ShowVersionInfo()
        {
            // Show Assembly version
            Console.WriteLine("1.0.0");
        }

        private static void Exit(int exitCode)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine();
                Console.WriteLine("Press key to exit.");
                Console.ReadKey();
            }
            Environment.Exit(exitCode);
        }

        private static bool ParseOptionalArgumentQuiet(string[] args)
        {
            return args.Length == 2 && args[1].ToLower() == "/q";
        }

        private static void WriteApplicationUsage()
        {
            WriteLine("Send a color code to a Magic Lighting RGB LED light using arduino as transmitter.");
            WriteLine(" {0} color_code [/Q]", Path.GetFileName(Environment.GetCommandLineArgs()[0]));
            WriteLine("     Valid color codes:");

            MagicColors.All.ToList().ForEach(magicColor => WriteLine("                  0x{0:X2} / {0,3} / {1}", magicColor.Code, magicColor.Name, magicColor.Code));

            WriteLine("{0}/Q       Do not display any output to the console.", Environment.NewLine);
        }

        private static void WriteLine(string format, params object[] arg)
        {
            if(_quiet) return;

            Console.WriteLine(format, arg);
        }
    }
}
