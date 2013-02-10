using System.Collections.Generic;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Text
{
    public class TextBlock
    {
        public int Number { get; private set; }
        public string Text { get; private set; }
        public int StartIndex { get; private set; }
        public int Length { get; private set; }
        public int EndIndex { get { return StartIndex + Length; } }

        public IList<string> Lines
        {
            get { return Text.SplitLines(true); }
        }

        public TextBlock(int number, string block, int startIndex)
        {
            Number = number;
            Text = block;
            StartIndex = startIndex;
            Length = block.Length;
        }
    }
}