﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ToolsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = null;
            Test(str);

            Console.Read();
        }

        static void Test(string str)
        {
            //string result = Tools.StrClass.StrMid(str,5,7);
            int result = Tools.QueryString.F("1").ToInt32(); //Tools.StrClass.Length(str, 5, 7);
            result.ToString();
        }
        
    }
}