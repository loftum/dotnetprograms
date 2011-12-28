using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.RegularExpressions;
using DbTool.Lib.Exceptions;
using Mono.CSharp;
using WebMatrix.Data;

namespace DbTool.Lib.Communication.DbCommands
{
    public class LinqExecutor : IDbCommandExecutor
    {
        private readonly DbConnection _dbConnection;
        private readonly int _maxRows;

        public LinqExecutor(DbConnection dbConnection, int maxRows)
        {
            _dbConnection = dbConnection;
            _maxRows = maxRows;
        }

        public IDbCommandResult Execute(string command)
        {
            try
            {
                var report = new Report(new ConsoleReportPrinter());
                var parser = new CommandLineParser(report);
                var settings = parser.ParseArguments(new string[0]);
                var evaluator = new Evaluator(settings, report);

                evaluator.Run("using WebMatrix.Data;");
                evaluator.Run("using System.Collections.Generic;");

                var rawSql = Regex.Match(command, @"\({1}[\s\S]+\)").Value;
                var linqExpression = command.Replace(rawSql, "data");
                var sql = rawSql.Replace(")", string.Empty).Replace("(", string.Empty);

                var data = new string[] {"a", "b", "c"};
                var methodResult = evaluator.Evaluate(linqExpression);
                var result = new MessageResult("Result: " + methodResult);
                return result;
            }
            catch (Exception ex)
            {
                throw new DbToolException(ex.ToString(), ex);
            }
        }

        private IEnumerable<dynamic> Query(string sql)
        {
            using (var db = Database.OpenConnectionString(_dbConnection.ConnectionString, "SQL Server Data Provider"))
            {
                return db.Query(sql);
            }
        }
    }
}