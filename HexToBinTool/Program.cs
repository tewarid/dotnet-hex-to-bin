using HexToBinLib;
using System;
using System.Text;

namespace HexToBinTool
{
	static class MainClass
	{
        private static string inFile;
        private static string outFile;
        private static HexToBin converter; 

		public static void Main (string[] args)
		{
            if (!ParseOptions(args))
            {
                PrintUsage();
                Console.WriteLine("Press Enter to quit...");
                Console.Read();
                Environment.Exit(-1);
            }

            try
            {
                int count = converter.Convert(inFile, outFile, Encoding.ASCII);
                Console.WriteLine("{0} bytes written to output file", count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.WriteLine("Press Enter to quit...");
            Console.Read();
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
                    converter = new HexToBin(args[i]);
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
            Console.WriteLine("-i\tlist of characters to ingore e.g. -i \"{},\"");
        }
	}
}
