using HexToBinLib;
using System;
using System.Text;

namespace HexToBinTool
{
	class MainClass
	{
        private static string inFile;
        private static string outFile;

		public static void Main (string[] args)
		{
            if (!ParseOptions(args))
            {
                PrintUsage();
                Environment.Exit(-1);
            }

            try
            {
                int count = HexToBin.Convert(inFile, outFile, Encoding.ASCII);
                Console.WriteLine("{0} bytes written to output file", count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static bool ParseOptions(string[] args)
        {
            if (args.Length == 0)
            {
                return false;
            }

            int i = 0;

            if (args[i].Equals("-i"))
            {
                i++;
                if (args.Length > i + 1)
                {
                    Console.WriteLine("Will ignore the following characters: {0}", args[i]);
                    HexToBin.IgnoredChars = args[i];
                    i++;
                } 
                else
                {
                    return false;
                }
            }

            if (args.Length > i + 1)
            {
                inFile = args[i];
                i++;
            }
            else
            {
                return false;
            }

            if (args.Length == i + 1)
            {
                outFile = args[i];
                i++;
            }
            else
            {
                return false;
            }

            return true;
        }

        static void PrintUsage()
        {
            Console.WriteLine("Options: [-i \"ignore\"] <input filename> <output filename>");
            Console.WriteLine("-i\tlist of charcaters to ingore e.g. -i \"{},\"");
        }
	}
}
