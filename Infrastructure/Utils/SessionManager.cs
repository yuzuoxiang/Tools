using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utils
{
    /// <summary>
    /// ** 描述：session操作类
    /// ** 创始时间：2015-6-9
    /// ** 修改时间：-
    /// ** 作者：sunkaixuan
    /// </summary>
    /// <typeparam name="K">键</typeparam>
    /// <typeparam name="V">值</typeparam>
    public class SessionManager<V>
    {
        private static readonly object _instancelock = new object();
        private static SessionManager<V> _instance = null;
        private static HttpContext context = HttpContext.Current;

        public static SessionManager<V> GetInstance()
        {
            if (_instance == null)
            {
                lock (_instancelock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager<V>();
                    }
                }

            }
            return _instance;
        }

        public void Add(string key, V value)
        {
            context.Session.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return context.Session[key] != null;
        }

        public V Get(string key)
        {
            return (V)context.Session[key];
        }

        public IEnumerable<string> GetAllKey()
        {
            foreach (var key in context.Session.Keys)
            {
                yield return key.ToString();
            }
        }

        public void Remove(string key)
        {
            context.Session[key] = null;
            context.Session.Remove(key);
        }

        public void RemoveAll()
        {
            foreach (var key in GetAllKey())
            {
                Remove(key);
            }
        }

        public void RemoveAll(Func<string, bool> removeExpression)
        {
            var allKeyList = GetAllKey().ToList();
            var removeKeyList = allKeyList.Where(removeExpression).ToList();
            foreach (var key in removeKeyList)
            {
                Remove(key);
            }
        }

        public V this[string key]
        {
            get { return (V)context.Session[key]; }
        }

        public void Add(string key, V value, int cacheDurationInSeconds)
        {
            throw new NotImplementedException("session无法设置过期时间,请到webconfig更改设置");
        }
    }
}
