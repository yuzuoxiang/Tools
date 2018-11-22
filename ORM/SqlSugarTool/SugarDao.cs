using SqlSugar;
using SqlSugarTool.Extensions.DataCache;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SqlSugarTool
{
    public class SugarDao
    {
        private const string ParamName = "SqlSugar的连接字符串不能为空";

        private static bool IsDebug => (ConfigurationManager.AppSettings["IsDebug"] ?? "False") == "Ture";
      

        public static string GetConfigurationManager(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn">连接名称</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="shardSameThread"></param>
        /// <param name="autoCloseConnection"></param>
        /// <returns></returns>
        public static SqlSugarClient GetInstance(string conn = "", DbType dbType = DbType.SqlServer, bool shardSameThread = false, bool autoCloseConnection = true)
        {
            if (string.IsNullOrEmpty(conn))
                throw new ArgumentNullException(ParamName);

            var db = new SqlSugarClient(new ConnectionConfig()
            {
                //连接字符串
                ConnectionString = conn,
                //数据库类型
                DbType = dbType,
                //是否自动释放数据库，设为true时不需要close或者Using的操作，比较推荐
                IsAutoCloseConnection = autoCloseConnection,
                //设为true相同线程是同一个SqlSugarClient
                IsShardSameThread = shardSameThread,
                //初始化主键和自增列信息的方式
                //InitKeyType = InitKeyType.SystemTable
                //设置数据库读写分离，所有的写入删除更新都走主库，查询走从库，事务内都走主库，HitRate表示权重 值越大执行的次数越高，如果想停掉哪个连接可以把HitRate设为0
                //SlaveConnectionConfigs = new List<SlaveConnectionConfig>() {
                //     new SlaveConnectionConfig() { HitRate=10, ConnectionString="111" },
                //     new SlaveConnectionConfig() { HitRate=30, ConnectionString="222" }
                //}
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    DataInfoCacheService = new RedisCache()
                }
            });

            if (IsDebug)
            {
                //SQL执行前事件
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Logs.Log.Debug($"SQL执行前事件，sql:{sql},pars:{pars}");

                    if (db.TempItems == null) db.TempItems = new Dictionary<string, object>();
                    db.TempItems.Add("logTime", DateTime.Now);
                };

                //SQL执行完事件
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    ////打印SQL参数
                    //Console.WriteLine(sql + " " + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));

                    //SQL执行时间
                    if (db.TempItems.ContainsKey("logTime"))
                    {
                        var startintTime = db.TempItems["logTime"].ObjToDate();

                        db.TempItems.Remove("logTime");
                        var completedTime = DateTime.Now;
                        var totalTime = (completedTime - startintTime).Milliseconds;
                        Logs.Log.Debug($"SQL执行时间：{totalTime} 毫秒");
                    }

                    Logs.Log.Debug($"SQL执行完事件，sql:{sql},pars:{pars}");
                };

                //执行SQL错误事件
                db.Aop.OnError = (exp) =>
                {
                    Logs.Log.Error($"执行SQL错误事件，exp:{exp}");
                };

            }
                          
            //下面写的语句都会进上面的回调函数 

            return db;
        }
    }
}
