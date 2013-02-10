using Caliburn.Micro;
using CodeGenerator.Lib.Generating;
using CodeGenerator.Lib.Syntax;

namespace CodeGenerator.ViewModels
{
    public class GeneratorViewModel : PropertyChangedBase
    {
        private readonly IOutputGenerator _generator;

        private string _input;
        public string Input
        {
            get { return _input; }
            set { _input = value; NotifyOfPropertyChange(() => Input); }
        }

        private string _template;
        public string Template
        {
            get { return _template; }
            set { _template = value; NotifyOfPropertyChange(() => Template); }
        }

        private string _output;
        public string Output
        {
            get { return _output; }
            set { _output = value; NotifyOfPropertyChange(() => Output); }
        }

        private string _delimiter;
        public string Delimiter
        {
            get { return _delimiter; }
            set { _delimiter = value; NotifyOfPropertyChange(() => Delimiter); }
        }

        private int _linesPerRecord;
        public int LinesPerRecord
        {
            get { return _linesPerRecord; }
            set { _linesPerRecord = value; NotifyOfPropertyChange(() => LinesPerRecord); }
        }

        public ISyntaxParser SyntaxParser { get; private set; }
        

        public GeneratorViewModel(IOutputGenerator generator,
            ISyntaxParser syntaxParser)
        {
            _generator = generator;
            SyntaxParser = syntaxParser;
            LinesPerRecord = 1;
            Delimiter = @"[\s]+";
        }

        public void Generate()
        {
            Output = _generator.Generate(Input, Template, LinesPerRecord, Delimiter);
        }
    }
}