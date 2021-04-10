using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace weathremvc.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoverDocument;

        public TokenService(ILogger<TokenService> logger, IOptions<IdentityServerSettings> identityServerSettings)
        {
            this._logger = logger;
            this._identityServerSettings = identityServerSettings;

            using var httpClient = new HttpClient();
            this._discoverDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoverUrl).Result;

            if (this._discoverDocument.IsError)
            {
                logger.LogError($"Unable to get discovery document. Error is: {this._discoverDocument.Error}");
                throw new Exception("Unable to get discovery document", this._discoverDocument.Exception);
            }

        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            using var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = this._discoverDocument.TokenEndpoint,

                ClientId = this._identityServerSettings.Value.ClientName,
                ClientSecret = this._identityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                this._logger.LogError($"Unable to get token. Error is: {tokenResponse.Error}");
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
