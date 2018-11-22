using SqlSugar; 

namespace SqlSugarTool
{
    /// <summary>
    /// Sugar方法扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {
        }


        //你可以在这里扩展自已的方法



        //public List<T> GetByIdsss(dynamic[] ids)
        //{
        //    return Context.Queryable<T>().In(ids).ToList(); ;
        //}
    }
}
