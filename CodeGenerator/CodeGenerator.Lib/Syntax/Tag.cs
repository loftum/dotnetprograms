namespace CodeGenerator.Lib.Syntax
{
    public struct Tag
    {
        public TagType Type { get; set; }
        public int StartPosition { get; set; }
        public int Length { get; set; }
    }
}