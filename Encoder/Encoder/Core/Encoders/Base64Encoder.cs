using Encoder.ExtensionMethods;

namespace Encoder.Core.Encoders
{
    public class Base64Encoder : IStringEncoder
    {
        public string Encode(string decoded)
        {
            return decoded.ToBase64();
        }

        public string Decode(string encoded)
        {
            return encoded.FromBase64();
        }
    }
}