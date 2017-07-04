using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Configuration;
using System.Linq.Expressions;
using MongoDB.Linq;

namespace XKPI.Utils
{
    /// <summary>
    /// samus驱动 db通用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDBHelper<T> where T : class
    {
        #region property
        private const string connectionString = "mongodb://127.0.0.1:27017";
        private const string databaseName = "xcdr";
        private Mongo mongo;
        private MongoDatabase mongoDatabase;
        private MongoCollection<T> mongoCollection;
        #endregion

        #region 构造
        public MongoDBHelper(string name)
        {
            mongo = GetMongo();
            mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            mongoCollection = mongoDatabase.GetCollection<T>(name) as MongoCollection<T>;
            mongo.Connect();
        }
        public MongoDBHelper()
        {
            mongo = GetMongo();
            mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            mongoCollection = mongoDatabase.GetCollection<T>() as MongoCollection<T>;
            mongo.Connect();
        }
        ~MongoDBHelper()
        {
            mongo.Disconnect();
        }
        #endregion

        #region 配置Mongo,将类UserInfo映射到集合

        /// <summary>
        /// 配置Mongo,将类UserInfo映射到集合
        /// </summary>
        private Mongo GetMongo()
        {
            var config = new MongoConfigurationBuilder();
            config.Mapping(mapping =>
            {
                mapping.DefaultProfile(profile =>
                {
                    profile.SubClassesAre(t => t.IsSubclassOf(typeof(T)));
                });
                mapping.Map<T>();
            });
            config.ConnectionString(connectionString);
            return new Mongo(config.BuildConfiguration());
        }
        #endregion

        #region methods

        #region add
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        public void Insert(T t)
        {
            mongoCollection.Insert(t);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        public void Insert(T t, bool b)
        {
            mongoCollection.Insert(t, b);
        }
        #endregion

        #region update
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        public void Insert(T t, Expression<Func<T, bool>> func)
        {
            mongoCollection.Update(t, func, true);
        }
        #endregion

        #region delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        public void Delete(Expression<Func<T, bool>> func)
        {
            mongoCollection.Remove(func);
        }

        #endregion

        #region search


        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns></returns>
        public T SelectOne(Document spec)
        {
            return mongoCollection.FindOne(spec);

        }
        public T SelectOne(Expression<Func<T, bool>> func)
        {
            return mongoCollection.Linq().FirstOrDefault(func);
        }

        /// <summary>
        /// 查询集合数据
        /// </summary>
        /// <returns></returns>
        public List<T> SelectMore(Document spec)
        {
            return mongoCollection.Find(spec).Documents.ToList();
        }
        public List<T> SelectMore(Expression<Func<T, bool>> func)
        {
            return mongoCollection.Linq().Where(func).ToList();
        }

        /// <summary>
        /// Cursor查询所有
        /// </summary>
        /// <returns></returns>
        public Cursor SelectALl2Cursor()
        {
            return (Cursor)mongoCollection.FindAll();
        }
        /// <summary>
        /// linq查询所有
        /// </summary>
        /// <returns></returns>
        public List<T> SelectALl()
        {
            return mongoCollection.Linq().ToList();
        }

        /// <summary>
        /// 求最大最小值
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public T selectMaxMin(string fieldname, IndexOrder order)
        {
            return mongoCollection.FindAll().Sort(fieldname, order).Limit(1).Documents.FirstOrDefault();
        }

        /// <summary>
        /// MapReduce
        /// </summary>
        /// <param name="MapFunc"></param>
        /// <param name="ReduceFunc"></param>
        /// <returns></returns>
        public void CreateMapReduce(string MapFunc, string ReduceFunc, string outcollection)
        {
            mongoCollection.MapReduce()
            .Map(MapFunc)
            .Reduce(ReduceFunc)
            .Out(outcollection);
        }

        #endregion

        #endregion
    }
}