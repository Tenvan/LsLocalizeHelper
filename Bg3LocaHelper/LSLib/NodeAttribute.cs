using System;
using System.Collections.Generic;
using System.Linq;

namespace LSLib.LS
{
    public class TranslatedString
    {
        public UInt16 Version = 0;
        public string Value;
        public string Handle;

        public override string ToString()
        {
            return Value;
        }
    }

    public class TranslatedFSStringArgument
    {
        public string Key;
        public TranslatedFSString String;
        public string Value;
    }

    public class TranslatedFSString : TranslatedString
    {
        public List<TranslatedFSStringArgument> Arguments;
    }

    public class NodeSerializationSettings
    {
        public bool DefaultByteSwapGuids = true;
        public bool ByteSwapGuids = true;

        public void InitFromMeta(string meta)
        {
            if (meta.Length == 0)
            {
                // No metadata available, use defaults
                ByteSwapGuids = DefaultByteSwapGuids;
            }
            else
            {
                var tags = meta.Split(',');
                ByteSwapGuids = tags.Contains("bswap_guids");
            }
        }

        public string BuildMeta()
        {
            List<string> tags = new List<string> { "v1" };
            if (ByteSwapGuids)
            {
                tags.Add("bswap_guids");
            }

            return String.Join(",", tags);
        }
    }

}
