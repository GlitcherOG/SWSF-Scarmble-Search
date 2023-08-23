using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWSF_Scarmble_Search
{
    public static class HashSearch
    {
        public static void MainSearch(string Code, int Length)
        {
            int HashTest = 0;
            try
            {
                HashTest = int.Parse(Code, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                Console.WriteLine("Error parsing hash code");
                return;
            }
            long MaxWords = (long)Math.Pow(26.0, Length);

            long[] POWArray = new long[Length];

            for (int a = 0; a < POWArray.Length; a++)
            {
                POWArray[a] = (long)Math.Pow(26, a);
            }

            int[] WordArray = new int[Length];
            var hash = File.CreateText(System.IO.Directory.GetCurrentDirectory() + "\\PossibleText.txt");
            for (long i = 0; i < MaxWords; i++)
            {
                for (int a = 0; a < Length; a++)
                {
                    //97 == a
                    WordArray[a] = 97 + (int)((i / POWArray[a]) % 26);
                }

                if (SWSFScamble.ArrayScambleInt(WordArray) == HashTest)
                {
                    string StringCode = "";

                    for (int a = 0; a < WordArray.Length; a++)
                    {
                        StringCode += char.ConvertFromUtf32(WordArray[a]);
                    }
                    hash.WriteLine(StringCode);
                    Console.WriteLine(StringCode);
                }

                if (i % 50000000 == 0)
                {
                    float percent = (float)i / (float)MaxWords * 100f;
                    Console.Title = i + "/" + MaxWords + " " + percent + "%";
                }
            }
            Console.Title = "Search Completed";
            hash.Close();
        }
    }
}
