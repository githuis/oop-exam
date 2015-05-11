using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StregSystemProject;

namespace StregExtensions
{
    public static class StregStringExtensions
    {
        public static bool IsAdminCommand(this string str)
        {
            return str[0] == ':';
        }

        public static bool IsBuyCommand(this string str)
        {
            string[] split = str.Split(' ');
            int parsed;

            if (split.Length > 3)
                return false;

            if(split.Length > 2)
                if(!int.TryParse(split[1], out parsed))            
                    return false;
         
            if (split.Length == 3)
                if (!int.TryParse(split[2], out parsed))
                    return false;

            return true;
        }

        public static bool ContainsAmount(this string str)
        {
            string[] split = str.Split(' ');

            if (split.Length >= 3)
                return true;
            else
                return false;
        }
    }
}
