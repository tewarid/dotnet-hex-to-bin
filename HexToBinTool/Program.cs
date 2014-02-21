using HexToBinLib;
using System;
using System.Text;

namespace HexToBinTool
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: cmd <input filename> <output filename>");
                return;
            }

            try
            {
                int count = HexToBin.Convert(args[0], args[1], Encoding.ASCII);
                Console.WriteLine("{0} bytes written to output file", count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
	}
}
