using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace HexToBinLib
{
    public class HexToBin
    {
        /// <summary>
        /// Specifies a string containing characters that will be ignored
        /// in the supplied hex input. This may be useful to ignore characters
        /// such as {}' that are commonly found in a C array initialization
        /// e.g. {0xDE, 0xAD}.
        /// </summary>
        public string IgnoredChars { get; private set; }

        /// <summary>
        /// An instance of HexToBin with an empty IgnoredChars.
        /// </summary>
        public static HexToBin DefaultInstance
        {
            get;
            private set;
        } = new HexToBin(string.Empty);

        /// <summary>
        /// An instance of HexToBin that ignores characters found in C style 
        /// array initialization.
        /// </summary>
        public static HexToBin CArrayInstance
        {
            get;
            private set;
        } = new HexToBin("{},");

        /// <summary>
        /// Creates a new instance of hex to binary converter.
        /// </summary>
        /// <param name="ignoredChars"><see cref="IgnoredChars"/></param>
        public HexToBin(string ignoredChars)
        {
            IgnoredChars = ignoredChars;
        }

        /// <summary>
        /// Read hex stream from input file and write corresponding binary to output file.
        /// </summary>
        /// <param name="infile">Input file name.</param>
        /// <param name="outfile">Output file name</param>
        /// <param name="encoding">Characetr encoding of data in input file.</param>
        /// <returns>Number of bytes written or a negative value on error.</returns>
        public int Convert(string infile, string outfile, Encoding encoding)
        {
            StreamReader inf = new StreamReader(File.OpenRead(infile), encoding);
            FileStream outf = File.OpenWrite(outfile);

            int count = Convert(inf, outf);

            inf.Close();
            outf.Close();

            return count;
        }

        /// <summary>
        /// Convert hex stream read from the specified reader, and write corresponding binary
        /// to the specified output stream.
        /// </summary>
        /// <param name="input">Input text reader from where characters are read.</param>
        /// <param name="output">Output stream to where binary data is written.</param>
        /// <returns>Number of bytes written or a negative value on error.</returns>
        public int Convert(TextReader input, Stream output)
        {
            int line = 1;
            int col = 1;
            int colStart = 0;
            int count = 0;
            bool parse = false;
            bool has0x = false;

            StringBuilder val = new StringBuilder();
            while (true)
            {
                int ch = input.Read();
                switch (ch)
                {
                    case '\n':
                    case '\r':
                        line++;
                        col = 0;
                        if (ch == '\r' && input.Peek() == '\n')
                        {
                            // just so we don't count the same line twice for dos/windows
                            input.Read();
                        }
                        if (val.Length == 2)
                        {
                            parse = true;
                        }
                        break; // switch

                    case ' ':
                    case '\t':
                        if (val.Length == 2)
                        {
                            parse = true;
                        }
                        break; // switch

                    case -1:
                        if (val.Length > 0)
                        {
                            parse = true;
                        }
                        break; // switch

                    default:
                        if (colStart == 0)
                        {
                            colStart = col;
                        }
                        
                        if (ch == '0')
                        {
                            // strip off 0x
                            int x = input.Peek();
                            if (x == 'x' || x == 'X')
                            {
                                has0x = true;
                                if (val.Length > 0)
                                {
                                    parse = true;
                                }

                                // skip
                                input.Read();
                                col++;
                                break; // switch
                            }
                        }
                        else if(IsIgnoredChar((char)ch))
                        {
                            break; // switch
                        }

                        val.Append((char)ch);
                        if (!has0x && val.Length == 2)
                        {
                            parse = true;
                        }
                        break; // switch
                }

                if (parse)
                {
                    count++;
                    byte result;
                    if (byte.TryParse(val.ToString(), NumberStyles.HexNumber,
                        CultureInfo.InvariantCulture, out result))
                    {
                        output.WriteByte(result);
                    }
                    else
                    {
                        Console.Error.WriteLine("Bad data at line {0} column {1}: {2}",
                            line, colStart, val);
                        return -1;
                    }
                    val.Clear();
                    colStart = 0;
                    parse = false;
                    has0x = false;
                }

                if (ch == -1)
                {
                    break; // while
                }

                col++;
            }
            return count;
        }

        /// <summary>
        /// Determines whether a character will be ignored.
        /// </summary>
        /// <param name="ch">Character to test</param>
        /// <returns>true if character is ignored</returns>
        /// <see cref="IgnoredChars"/>
        public bool IsIgnoredChar(char ch)
        {
            for (int i = 0; i < IgnoredChars.Length; i++)
            {
                if (IgnoredChars[i] == ch)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
