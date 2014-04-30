using Encoder.ViewModels;

namespace Encoder.Core
{
    public interface ITextEncoder
    {
        string Encode(string decoded, StringEncoding encoding, bool urlEncode);
        string Decode(string encoded, StringEncoding encoding, bool urlEncode);
    }
}