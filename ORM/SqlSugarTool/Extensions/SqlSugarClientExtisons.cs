using SqlSugar;

namespace SqlSugarTool
{
    //public class SugarBase<T> : SugarCommon where T : class, new()
    //{
    //    private static string _dbName { get; set; }

    //    /// <summary>
    //    /// 初始化类
    //    /// </summary>
    //    /// <param name="DBName">连接的数据库名</param>
    //    public SugarBase(string dbName)
    //    {
    //        _dbName = dbName;
    //    }

    //    public SqlSugarClient GetDB()
    //    {
    //        return SugarDao.GetInstance(_dbName);
    //    }
    //}


    public static class SqlSugarClientExtisons
    {
        public static int NewDelete<T>(this SqlSugarClient db, int id) where T : class, new()
        {
            return db.Deleteable<T>().In(id).ExecuteCommand();
        }
    }
}
