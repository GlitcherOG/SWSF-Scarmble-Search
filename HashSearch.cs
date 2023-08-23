using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWSF_Scarmble_Search
{
    public static class HashSearch
    {

        public static long[] Ammount = new long[1];
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

        public static void ThreadSearch(string Code, int Length)
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
            Ammount = new long[26];
            var hash = File.CreateText(System.IO.Directory.GetCurrentDirectory() + "\\PossibleText.txt");
            Thread[] threadArray = new Thread[26];

            for (int i = 0; i < threadArray.Length; i++)
            {
                int temptest = i;
                threadArray[i] = new Thread(() => Worker(HashTest, temptest, Length, hash));
                threadArray[i].Start();
            }

            bool Sleep = true;
            while (Sleep)
            {
                Console.Clear();
                Sleep = false;
                for (int i = 0; i < threadArray.Length; i++)
                {
                    if (threadArray[i].IsAlive)
                    {
                        Console.WriteLine("Thread " + i + " is working " + Ammount[i] + "/" + POWArray[POWArray.Length - 2]);
                        Sleep = true;
                    }
                    else
                    {
                        Console.WriteLine("Thread " + i + " is done");
                    }
                }
                Thread.Sleep(20000);
            }

            Console.Title = "Search Completed";
            hash.Close();
        }

        public static void Worker(int HashTest, int startLetter,int Length, StreamWriter TextFile)
        {
            long MaxWords = (long)Math.Pow(26.0, Length-1);

            long[] POWArray = new long[Length];

            for (int a = 0; a < POWArray.Length; a++)
            {
                POWArray[a] = (long)Math.Pow(26, a);
            }

            int[] WordArray = new int[Length];
            WordArray[0] = startLetter + 97;
            for (long i = 0; i < MaxWords; i++)
            {
                for (int a = 1; a < Length; a++)
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
                    TextFile.WriteLine(StringCode);
                    Console.WriteLine(StringCode);
                }


                if(i%50000000 == 0)
                {
                    Ammount[startLetter] = i;
                }
            }

        }
    }
}
