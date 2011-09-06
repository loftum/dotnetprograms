using System.Text;

namespace DbTool.Lib.Logging
{
    public class MemoryLogger : IDbToolLogger
    {
        public string Text { get { return _builder.ToString(); } }
        private readonly StringBuilder _builder = new StringBuilder();

        public void Write(string message, params object[] args)
        {
            _builder.AppendFormat(message, args);
        }

        public void WriteLine(string message, params object[] args)
        {
            _builder.AppendFormat(message, args).AppendLine();
        }

        public void WriteLine(object text)
        {
            _builder.Append(text).AppendLine();
        }
    }
}