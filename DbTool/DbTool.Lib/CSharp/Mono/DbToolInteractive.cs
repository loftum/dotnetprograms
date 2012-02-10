using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.Configuration;
using Mono.CSharp;

namespace DbTool.Lib.CSharp.Mono
{
    public class DbToolInteractive
    {
        private static readonly DynamicSqlQuery DynamicSql = new DynamicSqlQuery();

        public static Evaluator Evaluator;
        public static TextWriter Output = new StringWriter();

        public static IEnumerable<dynamic> Schema(string collection)
        {
            return DynamicSql.Schema(collection);
        }

        public static IEnumerable<dynamic> Query(string sql)
        {
            return DynamicSql.Query(sql);
        }

        public static void SetDb(DbToolDatabase db)
        {
            DynamicSql.ConnectionData = db.GetConnectionData();
        }

        public static void SetConnection(DbConnection connection)
        {
            DynamicSql.DbConnection = connection;
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