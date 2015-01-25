using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMapp.Utils
{
    public static class Utility
    {
        public static int StringToNumber(string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits. Err: " + e.Message);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32. Err: " + e.Message);
            }

            return -1;
        }
    }
}
