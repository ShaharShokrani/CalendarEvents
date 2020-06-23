// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace CalendarEvents.IDP
{
    public static class Config
    {
        private static readonly List<string> Claims = new List<string>
        {              
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            "calendareventsapi.post"
        };

        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {            
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("calendareventsapi", "Calendar Events API")
                {
                    Scopes =  { 
                        new Scope("calendareventsapi.post")
                    },
                    //UserClaims = Claims
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },
                new Client()
                {
                    RequireConsent = false,
                    ClientName = "Calendar Events UI",
                    ClientId = "calendareventsui",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:4200/auth-callback"
                    },
                    AllowedScopes = Claims,
                    //ClientSecrets =
                    //{
                    //    new Secret("secret")
                    //},
                    //AllowedCorsOrigins = {"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                },
                //// SPA client using code flow + pkce
                //new Client
                //{
                //    ClientId = "spa",
                //    ClientName = "SPA Client",
                //    ClientUri = "http://identityserver.io",

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RequireClientSecret = false,

                //    RedirectUris =
                //    {
                //        "http://localhost:5002/index.html",
                //        "http://localhost:5002/callback.html",
                //        "http://localhost:5002/silent.html",
                //        "http://localhost:5002/popup.html",
                //    },

                //    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                //    AllowedCorsOrigins = { "http://localhost:5002" },

                //    AllowedScopes = { "openid", "profile", "api1" }
                //}
            };
    }
}