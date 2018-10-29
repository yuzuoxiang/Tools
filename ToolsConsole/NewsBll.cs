using Models;
using SqlSugarTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsConsole
{
    public class NewsBll : DbContext<News>
    {
        public NewsBll():base("PaGame")
        { 
        }

        public List<News> GetList()
        {
            var db= NewDb("PaGame");

            db = Db;

             

            return ModelDb.GetList();
        }
    }
}
