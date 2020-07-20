using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using System.Security;
using System.Security.Claims;

namespace Yandex.Cloud.NetCore.Sample.Auth
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                // backward compat
                new ApiScope("MemberCatalogue"),
                
                // more formal
                new ApiScope("MemberCatalogue.Api"),
                
                // scope without a resource
                new ApiScope("scope2"),
                
                // policyserver
                new ApiScope("policyserver.runtime"),
                new ApiScope("policyserver.management")
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("MemberCatalogue.Api")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },

                    Scopes = { "MemberCatalogue","MemberCatalogue.Api" }
                }
             
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                    {
                        ClientId = "bytheway.api.client",
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false,
                        //AllowedCorsOrigins = 
                        AllowedGrantTypes = {
                            "client_credentials",
                            "internal_service"
                        },
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256()) //todo:
                        },
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "MemberCatalogue","MemberCatalogue.Api"
                        },
                        ClientClaimsPrefix = String.Empty
                        //Claims = {new Claim("permission", "Admin")}
                    },
                new Client
                    {
                        ClientId = "native",
                        ClientName = "MemberCatalogue.Api",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        RequireClientSecret = false,
                        RequireConsent = false,
                        AllowAccessTokensViaBrowser = true,
                        RedirectUris =
                        {
                            "http://localhost:6002/swagger/oauth2-redirect.html"

                        },
                        PostLogoutRedirectUris =
                        {
                            "http://localhost:6002/swagger/oauth2-redirect.html"
                        },
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "MemberCatalogue","MemberCatalogue.Api"
                        },
                        AllowOfflineAccess = true,
                        RefreshTokenUsage = TokenUsage.ReUse,
                        ClientClaimsPrefix = String.Empty
                    },
                new Client
                    {
                        ClientId = "mvc",
                        ClientName = "MVC Client",
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                        RequireConsent = false,

                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },

                        RedirectUris           = { "http://localhost:5002/signin-oidc" },
                        PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "MemberCatalogue","MemberCatalogue.Api"
                        },
                        AllowOfflineAccess = true
                    },
                
                // oidc login only
                new Client
                {
                    ClientId = "login",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email" },
                }
            };
        }
    }
}
