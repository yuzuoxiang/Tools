using SqlSugar;
using System.Configuration;

namespace SqlSugarTool
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext(string dbName)
        {
            _dbName = dbName;

            Db = SugarDao.GetInstance(connectionString);
        }

        private static string _dbName { get; set; }

        public SqlSugarClient Db;

        private static string GetConfigurationManager(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// 根据数据名创建一个新连接
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public SqlSugarClient NewDb(string dbName)
        {
            return SugarDao.GetInstance(GetConfigurationManager(dbName));
        }

        /// <summary>
        /// 数据库连接属性
        /// </summary>
        private static string connectionString
        {
            get
            {
                return GetConfigurationManager(_dbName);
            }
        }
        
        public DbSet<T> ModelDb { get { return new DbSet<T>(Db); } }
    }
}
