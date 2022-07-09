using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Data
{
    public class StringParsingFunctions 
    {
        public static Dictionary<string,string> TurnIntoDictionary(string[] values, char splitElement)
        {
            Dictionary<string, string> result = new();
            foreach (string pairs in values)
            {
                string[] bits = pairs.Split(splitElement);
                if (bits.Length == 2)
                {
                    string key = bits[0];
                    string value = bits[1];
                    result.Add(key, value);
                }
            }
            return result;
        }
        public static string[] ClearCommentLines(string [] materialToClean)
        {
            List<string> result = new();
            foreach(string line in materialToClean)
            {
                if (line[0] == '/' && line[1] == '/')
                {
                    continue;
                }
                result.Add(line);
            }
            return result.ToArray();
        }
    }
}
