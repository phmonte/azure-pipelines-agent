// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
//


using System.Collections.Generic;

namespace Microsoft.VisualStudio.Services.Agent
{
    public static partial class AdditionalMaskingRegexes
    {
        public static IEnumerable<string> CredScanPatterns => credScanPatterns;

        // Each pattern or set of patterns has a comment mentioning
        // which CredScan policy it came from. In CredScan, if a pattern
        // contains a named group, then that named group is considered the
        // sensitive part.
        // 
        // For the agent, we don't want to mask the non-sensitive parts, so
        // we wrap lookbehind and lookahead non-match groups around those
        // parts which aren't in the named group.
        // 
        // The non-matching parts are pulled out into separate string
        // literals to make them easier to manually examine.
        private static IEnumerable<string> credScanPatterns =
            new List<string>()
            {
                // AAD client app, most recent two versions.
                  @"\b" // pre-match
                + @"[0-9A-Za-z-_~.]{3}7Q~[0-9A-Za-z-_~.]{31}\b|\b[0-9A-Za-z-_~.]{3}8Q~[0-9A-Za-z-_~.]{34}" // match
                + @"\b", // post-match
                  
                  // Prominent Azure provider 512-bit symmetric keys.
                  @"\b" // pre-match
                + @"[0-9A-Za-z+/]{76}(APIM|ACDb|\+(ABa|AMC|ASt))[0-9A-Za-z+/]{5}[AQgw]==" // match
                + @"", // post-match
                       // 
                  // Prominent Azure provider 256-bit symmetric keys.
                  @"\b" // pre-match
                + @"[0-9A-Za-z+/]{33}(AIoT|\+(ASb|AEh|ARm))[A-P][0-9A-Za-z+/]{5}=" // match
                + @"", // post-match
                       
                  // Azure Function key.
                  @"\b" // pre-match
                + @"[0-9A-Za-z_\-]{44}AzFu[0-9A-Za-z\-_]{5}[AQgw]==" // match
                + @"", // post-match

                  // Azure Search keys.
                  @"\b" // pre-match
                + @"[0-9A-Za-z]{42}AzSe[A-D][0-9A-Za-z]{5}" // match
                + @"\b", // post-match
                  
                  // Azure Container Registry keys.
                  @"\b" // pre-match
                + @"[0-9A-Za-z+/]{42}\+ACR[A-D][0-9A-Za-z+/]{5}" // match
                + @"\b", // post-match
                  
                  // Azure Cache for Redis keys.
                  @"\b" // pre-match
                + @"[0-9A-Za-z]{33}AzCa[A-P][0-9A-Za-z]{5}=" // match
                + @"", // post-match
                  
                  // NuGet API keys.
                  @"\b" // pre-match
                + @"oy2[a-p][0-9a-z]{15}[aq][0-9a-z]{11}[eu][bdfhjlnprtvxz357][a-p][0-9a-z]{11}[aeimquy4]" // match
                + @"\b", // post-match
                  
                  // NPM author keys.
                  @"\b" // pre-match
                + @"npm_[0-9A-Za-z]{36}" // match
                + @"\b", // post-match
            };
    }
}
