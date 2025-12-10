using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatProcessor;

internal static class Utils
{
    public static string ReplaceTimeTokens(string input)
    {
        return Regex.Replace(input, @"\{DATETIME:(.*?)\}", match =>
        {
            string format = match.Groups[1].Value;
            return DateTime.Now.ToString(format);
        });
    }  
}
