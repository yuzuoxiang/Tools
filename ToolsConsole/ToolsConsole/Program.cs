using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var a = new ObservableCollection<int>();

            //string result = Tools.StrClass.StrMid(str,5,7);
            string result = Tools.Common.ChineseCharactersToPinyin.GetFirst("黄开始考试德克士里");
            result.ToString();
        }
        
    }
}
