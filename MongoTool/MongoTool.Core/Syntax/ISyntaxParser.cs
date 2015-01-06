using System.Collections.Generic;

namespace MongoTool.Core.Syntax
{
    public interface ISyntaxParser
    {
        IEnumerable<Tag> Tags { get; }
        void Parse(string text, int start, int end);
        IEnumerable<Suggestion> Suggestions { get; }
        void FindSuggestions(string text, int cursor);
    }
}