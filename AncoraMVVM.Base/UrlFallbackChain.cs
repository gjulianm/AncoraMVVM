using System;
using System.Collections.Generic;
using System.Linq;

namespace AncoraMVVM.Base
{
    public class StringFallbackChain
    {
        public IEnumerable<string> Chain { get; private set; }

        public Func<string, string> PreprocessFunction { get; set; }
        public Func<string, bool> PreprocessFunctionFilter { get; set; }
        public Func<string, bool> Condition { get; set; }

        public StringFallbackChain(IEnumerable<string> chain)
        {
            Chain = chain;
        }

        public StringFallbackChain(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public string FinalString
        {
            get
            {
                return Chain
                    .Select(x => x != null && PreprocessFunctionFilter != null && PreprocessFunctionFilter(x)
                        ? PreprocessFunction(x)
                        : x)
                     .FirstOrDefault(Condition) ?? Chain.Last();
            }
        }
    }

    public class UrlFallbackChain : StringFallbackChain
    {
        public UrlFallbackChain(string hostForRelativeUrls, params string[] values)
            : base((IEnumerable<string>)values)
        {
            PreprocessFunction = x => hostForRelativeUrls.TrimEnd('/') + x;
            PreprocessFunctionFilter = x => x.StartsWith("/");
            Condition = x => x != null && (x.StartsWith("http://") || x.StartsWith("https://")); // Uri.IsWellFormedUriString was doing the hell it wanted to do.
        }
    }
}
