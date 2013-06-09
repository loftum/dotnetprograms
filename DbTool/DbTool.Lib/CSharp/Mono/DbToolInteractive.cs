using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Linq;
using Mono.CSharp;

namespace DbTool.Lib.CSharp.Mono
{
    public class DbToolInteractive
    {
        private static readonly DynamicQuery DynamicQuery = new DynamicQuery();

        public static Evaluator Evaluator;
        public static TextWriter Output = new StringWriter();

        public static IEnumerable<dynamic> Schema(string collection)
        {
            return DynamicQuery.Schema(collection);
        }

        public static IEnumerable<dynamic> Query(string sql)
        {
            return DynamicQuery.Query(sql);
        }

        public static IQueryable<T> Query<T>()
        {
            return new DbToolQueryable<T>(new DbToolQueryProvider(new QueryableToSqlTranslator(), DynamicQuery.DbConnection));
        }

        public static void SetDb(DbToolDatabase db)
        {
            DynamicQuery.ConnectionData = db.GetConnectionData();
        }

        public static void SetConnection(DbConnection connection)
        {
            DynamicQuery.DbConnection = connection;
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