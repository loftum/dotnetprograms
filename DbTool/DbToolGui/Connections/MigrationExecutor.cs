﻿using System;
using DbTool.Lib.Configuration;
using DbTool.Lib.Extensions;
using DbTool.Lib.Logging;
using DbTool.Lib.Migrating;

namespace DbToolGui.Connections
{
    public class MigrationExecutor : IDbCommandExecutor
    {
        private readonly ConnectionData _connectionData;

        public MigrationExecutor(ConnectionData connectionData)
        {
            _connectionData = connectionData;
        }

        public IDbCommandResult Execute(string statement)
        {
            var version = GetVersionFrom(statement);
            var logger = new MemoryLogger();
            var runner = new MigrationRunner(_connectionData, logger);
            if (version.HasValue)
            {
                runner.MigrateTo(version.Value);
            }
            else
            {
                runner.MigrateToLatest();
            }
            return new MigrationResult(logger.Text);
        }

        private static long? GetVersionFrom(string statement)
        {
            var parts = statement.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2 || parts[1].EqualsIgnoreCase("up"))
            {
                return null;
            }
            return long.Parse(parts[1]);
        }
    }
}