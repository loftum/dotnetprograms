namespace Encoder.Core.Encoders
{
    public class NoneEncoder : IStringEncoder
    {
        public string Encode(string decoded)
        {
            return decoded;
        }

        public string Decode(string encoded)
        {
            return encoded;
        }
    }
}