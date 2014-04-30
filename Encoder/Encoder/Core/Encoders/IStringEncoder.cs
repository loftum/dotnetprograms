namespace Encoder.Core.Encoders
{
    public interface IStringEncoder
    {
        string Encode(string decoded);
        string Decode(string encoded);
    }
}