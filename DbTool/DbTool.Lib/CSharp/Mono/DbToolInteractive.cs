using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.Configuration;
using Mono.CSharp;

namespace DbTool.Lib.CSharp.Mono
{
    public class DbToolInteractive
    {
        public static readonly SqlDatabase Db = new SqlDatabase();

        public static Evaluator Evaluator;
        public static TextWriter Output = new StringWriter();

        public static IEnumerable<dynamic> Schema(string collection)
        {
            return Db.Schema(collection);
        }

        public static IEnumerable<dynamic> Query(string sql)
        {
            return Db.Query(sql);
        }

        public static IQueryable<T> Query<T>()
        {
            return Db.Query<T>();
        }

        public static void Insert<T>(T item)
        {
            Db.Insert(item);
        }

        public static void SetDb(DbToolDatabase db)
        {
            Db.ConnectionData = db.GetConnectionData();
        }

        public static void SetConnection(DbConnection connection)
        {
            Db.DbConnection = connection;
        }

        public static string vars
        {
            get { return Evaluator.GetVars(); }
        }

        public static string usings
        {
            get { return Evaluator.GetUsing(); }
        }
    }
}