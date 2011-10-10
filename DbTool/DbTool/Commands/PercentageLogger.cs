using System.Text;
using DbTool.Lib.Logging;

namespace DbTool.Commands
{
    public class PercentageLogger
    {
        private readonly IDbToolLogger _logger;
        private int _lastDots;

        public PercentageLogger(IDbToolLogger logger)
        {
            _logger = logger;
        }

        public void Log(int percent)
        {
            if (_lastDots == 0)
            {
                _logger.Write("[");
            }
            int dots = percent/10*4;
            if (dots > _lastDots)
            {
                if (percent < 100)
                {
                    _logger.Write(GetDots(dots - _lastDots));
                }
                else
                {
                    _logger.WriteLine(GetDots(dots - _lastDots) + "]");
                }
                _lastDots = dots;
            }
        }

        private static string GetDots(int number)
        {
            var builder = new StringBuilder();
            for (int ii = 0; ii < number; ii++)
            {
                builder.Append(".");
            }
            return builder.ToString();
        }
    }
}