using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace TodoApp.Identity.Services
{
    internal static class IdentityConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "TodoAPI",
                    DisplayName = "To-do application resource API",
                    Description = "Resource of tasks and projects",
                    Scopes = new List<string> {"TodoAPI"},
                    ApiSecrets = new List<Secret> {new Secret("ResourceSecretKey".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource // Как ASP.NET Core Identity распознаёт это?
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientName = "React front-end application",
                    ClientId = "react_frontend_application",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("react_app_secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "TodoAPI"
                    },
                    AllowOfflineAccess = false, // Если true, то будет refresh-token,
                    AccessTokenLifetime = 60 * 60 * 24 * 7
                }
            };
        }

        public static IEnumerable<ApiScope> GetScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("TodoAPI")
            };
        }
    }
}