using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SqlSugarTool
{
    /// <summary>
    /// 通用的增删改查
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SugarBase<T> : SugarCommon where T : class, new()
    {
        private static string _dbName { get; set; }

        /// <summary>
        /// 初始化BllBase类
        /// </summary>
        /// <param name="DBName">连接的数据库名</param>
        public SugarBase(string dbName)
        {
            _dbName = dbName;
        }

        public SqlSugarClient GetDb()
        {
            return SugarDao.GetInstance(_dbName);
        }

        /// <summary>
        /// 封装了常用方法的工具类
        /// </summary>
        /// <returns></returns>
        public SimpleClient GetSimpleDB()
        {
            return new SimpleClient(SugarDao.GetInstance(_dbName));
        }
        #region 通用查询


        /// <summary>
        /// 返回所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            var sq = GetDb().Queryable<T>().ToList();

            return sq.ToList();
        }

        /// <summary>
        /// 将查询到的数据缓存
        /// </summary>
        /// <param name="seconds">设置缓存过期时间，单位秒</param>
        /// <returns></returns>
        public List<T> GetAll(int seconds = 0)
        {
            var sq = GetDb().Queryable<T>().WithCacheIF(seconds > 0, seconds).ToList();

            return sq.ToList();
        }

        /// <summary>
        /// 返回第一条数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T First(Expression<Func<T, bool>> where)
        {
            return GetDb().Queryable<T>().Where(where).First();
        }

        public T FirstSelect(Expression<Func<T, bool>> where, string select = null, Expression<Func<object, object>> orderBy = null)
        {
            var list = GetDb().Queryable<T>().Where(where);
            if (select != null || !string.IsNullOrEmpty(select))
                list.Select(select);
            return list.First();
        }

        /// <summary>
        /// 根据条件判断是否存在数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> where)
        {
            return GetDb().Queryable<T>().Any(where);
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return GetDb().Queryable<T>().Where(where).Count();
        }

        /// <summary>
        /// 通用查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="select"></param>
        /// <param name="top"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="seconds">缓存失效时间，秒</param>
        /// <returns></returns>
        public List<T> Queryable(Expression<Func<T, bool>> where, string orderBy = "", Expression<Func<T, object>> select = null, int top = 0,
            int? pageSize = null, int? pageIndex = null, int seconds = 0)
        {
            var sq = GetDb().Queryable<T>().Where(where);

            if (!string.IsNullOrEmpty(orderBy))
                sq.OrderBy(orderBy);

            if (select != null)
                sq.Select(select);

            if (top > 0)
                sq.Take(top);

            if (seconds > 0)
                sq.WithCache(seconds);

            return (pageIndex != null && pageSize != null)
                ? sq.ToPageList(pageIndex.GetValueOrDefault(), pageSize.GetValueOrDefault())
                : sq.ToList();
        }

        public IEnumerable<T> GetSelectList(int top, string cols, string tableName, string where, string orderBy)
        {
            string selectCols = "*";
            if (cols != "")
                selectCols = cols;

            string topStr = "";
            if (top > 0)
                topStr = "top " + top.ToString();
            string whereStr = "";
            if (where != "")
                whereStr += "where " + where;
            string orderByStr = "";
            if (orderBy != "")
                orderByStr = "order by " + orderBy;

            var sql = string.Format("select {0} {1} from {2} {3} {4}", topStr, selectCols, tableName, whereStr, orderByStr);
            IEnumerable<T> dataList = GetDb().Ado.SqlQuery<T>(sql);
            return dataList;
        }

        /// <summary>
        /// SQL参数化查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sugarParameter"></param>
        /// <returns></returns>
        public List<T> Queryable(string sql, SugarParameter[] sugarParameter = null)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException(string.Format("{0}SQL语句不能为空", nameof(sql)));

            return GetDb().Ado.SqlQuery<T>(sql, sugarParameter);
        }

        /// <summary>
        /// 通用查询，带分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="select"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<T> QueryableByPage(int pageIndex, int pageSize, ref int totalCount, Expression<Func<T, bool>> where, string orderBy = "",
            Expression<Func<T, object>> select = null, int top = 0)
        {
            var sq = GetDb().Queryable<T>().Where(where);

            if (!string.IsNullOrEmpty(orderBy))
                sq.OrderBy(orderBy);

            if (select != null)
                sq.Select(select);

            if (top > 1)
                sq.Take(top);

            return sq.ToPageList(pageIndex, pageSize, ref totalCount);
        }

        #endregion

        #region 通用插入
        /// <summary>
        /// 通用插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(T model)
        {
            return GetDb().Insertable(model).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Insert(List<T> list)
        {
            return GetDb().Insertable(list.ToArray()).ExecuteCommand() > 0;
        }
        #endregion

        #region 通用删除
        /// <summary>
        /// 通用删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> where)
        {
            return GetDb().Deleteable(where).ExecuteCommand() > 0;
        }

        public bool Delete(int id)
        {
            return GetDb().Deleteable<T>().In(id).ExecuteCommand() > 0;
        }
        #endregion 

        #region 通用修改
        /// <summary>
        /// 通用修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="where"></param>
        /// <param name="updateColumns">只修改列</param>
        /// <param name="ignoreColumns">忽略列</param>
        /// <returns></returns>
        public bool Update(T model, Expression<Func<T, bool>> where = null, Expression<Func<T, object>> updateColumns = null,
            Expression<Func<T, object>> ignoreColumns = null)
        {
            var sq = GetDb().Updateable(model);

            if (where != null)
                sq.Where(where);

            if (updateColumns != null)
                sq.UpdateColumns(updateColumns);

            if (ignoreColumns != null)
                sq.IgnoreColumns(ignoreColumns);

            return sq.ExecuteCommand() > 0;
        }

        public bool Update(Expression<Func<T, T>> updateColumns, Expression<Func<T, bool>> where = null)
        {
            var sq = GetDb().Updateable<T>()
                .UpdateColumns(updateColumns)
                .Where(where);

            return sq.ExecuteCommand() > 0;
        }

        public bool UpdateBitField(string tableName, string setField, bool setValue, int id)
        {
            return GetDb().Ado.ExecuteCommand($"update {tableName} set {setField}={(setValue ? 1 : 0)} where Id={id}") > 0;
        }

        public bool UpdateBitField(string setField, bool setValue, int id, string tableName)
        {
            return GetDb().Ado.ExecuteCommand($"update {tableName} set {setField}={(setValue ? 1 : 0)} where Id={id}") > 0;
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="tableName">事务名称</param>
        /// <param name="sugarParameters">参数列</param>
        /// <returns></returns>
        public string StoredProcedureToString(string tableName, SugarParameter[] sugarParameters)
        {
            var db = GetDb();

            var result = db.Ado.UseStoredProcedure(() =>
            {
                return db.Ado.SqlQueryDynamic(tableName, sugarParameters);
            });

            return sugarParameters[sugarParameters.Count() - 1].Value.ToString();
        }

        public bool StoredProcedureToBool(string tableName, SugarParameter[] sugarParameters)
        {
            var result = StoredProcedureToString(tableName, sugarParameters);

            return result == "1";
        }
        #endregion
    }
}
