using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FeatureService
    {
        private readonly ILdClient _ldClient;

        public FeatureService(ILdClient ldClient)
        {
            _ldClient = ldClient;
        }

        public bool IsFeatureEnabled(string featureFlagKey, string userKey)
        {
            var context = Context.Builder(userKey).Build();
            return _ldClient.BoolVariation(featureFlagKey, context, false);
        }
    }
}
