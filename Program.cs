using System;

namespace SWSF_Scarmble_Search // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Test
            //HashSearch.MainSearch("02F03E16", 4);

            //Unknown Hash
            //HashSearch.MainSearch("2C73AF55", 8);

            //Console.WriteLine(SWSFScamble.Scamble("Test"));
            if (args.Length != 0)
            {
                if (args[0].ToLower() == "-h")
                {
                    Console.WriteLine(SWSFScamble.Scamble(args[1]));
                }

                if (args[0].ToLower() == "-s")
                {
                    HashSearch.MainSearch(args[1], int.Parse(args[2]));
                }
            }
            else
            {
                Console.WriteLine("How to use. \n Hash Mode \n -h [EncodeText] \n\n Search Mode \n -s [Hash] [EncodeLength]");
            }
            Console.ReadLine();
        }
    }
}