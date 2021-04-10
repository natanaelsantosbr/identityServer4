using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weathremvc.Services
{
    public class IdentityServerSettings
    {
        public string DiscoverUrl { get; set; }

        public string ClientName { get; set; }

        public string ClientPassword { get; set; }

        public bool UseHttps { get; set; }
    }
}
