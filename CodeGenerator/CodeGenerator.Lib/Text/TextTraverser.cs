using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Text
{
    public class TextTraverser
    {
        private readonly string _text;

        public TextTraverser(string text)
        {
            _text = text;
        }

        public IEnumerable<TextBlock> ReadBlocksOfLines(int linesPerBlock)
        {
            var textIndex = 0;
            var number = 0;
            foreach (var text in GetBlocks(linesPerBlock))
            {
                yield return new TextBlock(number++, text, textIndex);
                textIndex += text.Length;
            }
        }

        private IEnumerable<string> GetBlocks(int linesPerBlock)
        {
            return _text.SplitLines(true)
                .InChunksOf(linesPerBlock)
                .Select(c => string.Join(Environment.NewLine, c));
        }
    }
}