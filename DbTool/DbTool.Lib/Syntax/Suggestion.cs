namespace DbTool.Lib.Syntax
{
    public class Suggestion
    {
        public string Prefix { get; private set; }
        public string Completion { get; private set; }
        public string Text { get { return string.Format("{0}{1}", Prefix, Completion); } }

        public Suggestion(string text) : this(string.Empty, text)
        {
        }

        public Suggestion(string prefix, string completion)
        {
            Prefix = prefix;
            Completion = completion;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}