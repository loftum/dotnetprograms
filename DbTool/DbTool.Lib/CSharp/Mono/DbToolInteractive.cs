using System.Collections.Generic;
using System.IO;
using DbTool.Lib.Communication.DbCommands.WebMatrix;
using DbTool.Lib.Configuration;
using Mono.CSharp;

namespace DbTool.Lib.CSharp.Mono
{
    public class DbToolInteractive
    {
        private static readonly WebMatrixQuery WebMatrix = new WebMatrixQuery();

        public static Evaluator Evaluator;
        public static TextWriter Output = new StringWriter();
        
        public static void Hest()
        {
            Output.WriteLine("Hest!");
            Output.Flush();
        }

        public static IEnumerable<dynamic> Schema(string collection)
        {
            return WebMatrix.Schema(collection);
        }

        public static IEnumerable<dynamic> Query(string sql)
        {
            return WebMatrix.Query(sql);
        }

        public static IEnumerable<T> Query<T>(string sql)
        {
            return WebMatrix.Query<T>(sql);
        }

        public static void SetDb(DbToolDatabase db)
        {
            WebMatrix.ConnectionString = db.GetConnectionData().GetConnectionString();
            WebMatrix.ProviderName = db.GetConnectionData().ProviderName;
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