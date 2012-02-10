using System.Text;
using System.Text.RegularExpressions;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.Modifiers
{
    public class CommandModifier
    {
        private const string Pattern = @"\${1}\({1}([^\(^\)]*)\){1}";

        public static string Modify(string command)
        {
            if (command.IsNullOrEmpty())
            {
                return command;
            }
            var builder = new StringBuilder(command);
            var regex = new Regex(Pattern);
            foreach (Match match in regex.Matches(command))
            {
                var outside = match.Groups[0].Value;
                var content = match.Groups[1].Value;
                builder.Replace(outside, Wrap(Fnuttify(ToSelect(content))));
            }
            return builder.ToString();
        }

        private static string ToSelect(string text)
        {
            return text.IsSingleWord()
                ? string.Format("select * from {0}", text)
                : text;
        }

        private static string Wrap(string content)
        {
            return string.Format("Query({0})", content);
        }

        private static string Fnuttify(string text)
        {
            return string.Format("\"{0}\"", text.Trim('"'));
        }
    }
}