using System.Collections.Generic;
using Encoder.Core.Encoders;
using Encoder.ExtensionMethods;
using Encoder.ViewModels;

namespace Encoder.Core
{
    public class TextEncoder : ITextEncoder
    {
        private readonly IDictionary<StringEncoding, IStringEncoder> _encoders = new Dictionary<StringEncoding, IStringEncoder>();

        public TextEncoder()
        {
            _encoders[StringEncoding.None] = new NoneEncoder();
            _encoders[StringEncoding.Base64] = new Base64Encoder();
        }

        public string Encode(string decoded, StringEncoding encoding, bool urlEncode)
        {
            if (decoded == null)
            {
                return null;
            }
            var encoded = _encoders[encoding].Encode(decoded);
            return urlEncode ? encoded.UrlEncoded() : encoded;
        }

        public string Decode(string encoded, StringEncoding encoding, bool urlEncode)
        {
            if (encoded == null)
            {
                return null;
            }
            var toDecode = urlEncode ? encoded.UrlDecoded() : encoded;
            return _encoders[encoding].Decode(toDecode);
        }
    }
}