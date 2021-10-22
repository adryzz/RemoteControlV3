using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlV3.Utils
{
    public static class ParseUtils
    {
        public static bool? ParseBool(string text)
        {
            switch(text.ToLower())
            {
                case "1":
                case "on":
                case "enable":
                case "enabled":
                case "true":
                    {
                        return true;
                    }
                case "0":
                case "off":
                case "disable":
                case "disabled":
                case "false":
                    {
                        return false;
                    }
            }
            return null;
        }

        public static Size? ParseSize(string text)
        {
            try
            {
                if (text.Contains('x'))
                {
                    string[] a = text.Split('x', StringSplitOptions.RemoveEmptyEntries);
                    if (a.Length == 2)
                    {
                        return new Size(int.Parse(a[0]), int.Parse(a[1]));
                    }
                }
                else if (text.Contains(' '))
                {
                    string[] a = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (a.Length == 2)
                    {
                        return new Size(int.Parse(a[0]), int.Parse(a[1]));
                    }
                }
            }
            catch(Exception)
            {
                
            }
            return null;
        }
    }
}
