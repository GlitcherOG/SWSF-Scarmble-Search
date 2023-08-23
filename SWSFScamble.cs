using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWSF_Scarmble_Search
{
    public class SWSFScamble
    {
        public static string Scamble(string Code)
        {
            int hash_Result = 78850;
            byte[] data = Encoding.Unicode.GetBytes(Code.ToLower());
            for (int i = 0; i < data.Length/2; i++)
            {
                hash_Result = (hash_Result * 5) + BitConverter.ToInt16(data, i * 2);
            }
            return hash_Result.ToString("X8");
        }

        public static int ScambleInt(string Code)
        {
            int hash_Result = 78850;
            byte[] data = Encoding.Unicode.GetBytes(Code.ToLower());
            for (int i = 0; i < data.Length / 2; i++)
            {
                hash_Result = (hash_Result * 5) + BitConverter.ToInt16(data, i * 2);
            }
            return hash_Result;
        }

        public static int ArrayScambleInt(int[] Code)
        {
            int hash_Result = 78850;
            for (int i = 0; i < Code.Length; i++)
            {
                hash_Result = (hash_Result * 5) + Code[i];
            }
            return hash_Result;
        }
    }
}
