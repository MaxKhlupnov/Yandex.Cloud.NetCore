using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Yandex.Cloud.NetCore.Sample.Common.Framework;
using Yandex.Cloud.NetCore.Sample.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Yandex.Cloud.NetCore.Sample.MemberCatalogue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AuthContext>(options => options.UseNpgsql(Configuration.GetConnectionString("IdentityServerDb")));
            services.AddIdentity<Member, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            #region Identity
              JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
               services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(o =>
               {
                   o.Authority = Configuration["IdentityServer:Uri"];
                   o.Audience =  Configuration["IdentityServer:Audience"];
                   o.RequireHttpsMetadata = false;
                   o.Events = new JwtBearerEvents
                   {
                       // Check if we need handle authentication events
                       OnMessageReceived = context =>
                       {                        
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = context =>
                       {

                           // check https://leastprivilege.com/2017/10/09/new-in-identityserver4-v2-simplified-configuration-behind-load-balancers-or-reverse-proxies/
                           context.NoResult();
                           return Task.CompletedTask;
                       },
                       OnChallenge = context =>
                       {
                           return Task.CompletedTask;
                       }
                   };
               });


            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("Member").RequireAuthenticatedUser()
                                                                                    .Build();
            });

            #endregion

            services.AddMvc();

            services.AddControllers();

            #region Swagger config
            services.AddOpenApiDocument(document =>
            {
                //inject authorize button for swagger ui
                document.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "oAuth2 System authorization",
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                {Configuration["IdentityServer:Audience"],"MemberCatalogue Api"}
                               
                    },
                            AuthorizationUrl = $"{Configuration["IdentityServer:Uri"]}/connect/authorize",
                            TokenUrl = $"{Configuration["IdentityServer:Uri"]}/connect/token"
                        }
                    }
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();


            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "native",
                    AppName = "MemberCatalogue.Api"                
                };
            });

            app.UseOpenApi(settings =>
            {
                settings.PostProcess = (document, request) =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "demo API";

                    document.Info.License = new OpenApiLicense
                    {
                        Name = "Use under MIT License",
                        Url = "https://github.com/MaxKhlupnov/Yandex.Cloud.NetCore"
                    };
                };
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
