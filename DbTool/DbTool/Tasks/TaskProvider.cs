using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;

namespace DbTool.Tasks
{
    public class TaskProvider : ITask
    {
        public string Usage { get { return string.Empty; } }
        public string Example { get { return string.Empty; } }
        public string Name { get { return "usage"; } }

        private readonly IDictionary<string, ITask> _tasks = new Dictionary<string, ITask>();

        private readonly IDbToolLogger _logger;
        private readonly IDbToolSettings _settings;

        public TaskProvider(IDbToolConfig config)
        {
            _logger = new ConsoleLogger();
            _settings = config.Settings;
            Add(new DatabaseRestorer(_logger, _settings),
                new DatabaseDeleter(_logger, _settings),
                new DatabaseBackuper(_logger, _settings),
                new DatabaseCreator(_logger, _settings),
                new DatabaseLister(_logger, _settings),
                new DatabaseMigrator(_logger, _settings),
                new DatabaseVersionViewer(_logger, _settings),
                new DbToolVersionPrinter(_logger, _settings),
                new SettingsSetter(_logger, _settings),
                this);
        }

        private void Add(params ITask[] tasks)
        {
            foreach (var task in tasks)
            {
                _tasks.Add(task.Name, task);    
            }
        }

        public ITask GetTask(IList<string> args)
        {
            if (args.Count == 0 || !_tasks.ContainsKey(args[0]))
            {
                return this;
            }
            return _tasks[args[0]];
        }

        public void Execute(IList<string> args)
        {
            _logger.WriteLine(PrintUsage(args));
        }

        private string PrintUsage(IList<string> args)
        {
            if (args.Count == 0 || !_tasks.ContainsKey(args[0]))
            {
                return GenerateUsageText();
            }
            var taskName = args[0];
            return _tasks[taskName].GenerateUsageText();
        }

        public string GenerateUsageText()
        {
            var tasks = _tasks.Keys.OrderBy(t => t);
            return new StringBuilder()
                .AppendLine("Supported tasks: ")
                    .Append(string.Join(Environment.NewLine, tasks))
                    .ToString();
        }
    }
}