namespace DbTool.Lib.Ui.Syntax
{
    public struct Tag
    {
        public TagType Type { get; set; }
        public string Word { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public int Length { get { return EndPosition - StartPosition; } }
    }
}