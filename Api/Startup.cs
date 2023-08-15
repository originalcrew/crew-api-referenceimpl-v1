using System.Reflection;
using AutoMapper;
using Crew.Api.ReferenceImpl.V1.Options;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OwaspHeaders.Core.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Crew.Api.ReferenceImpl.V1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<ForwardedHeadersOptions>().Configure(
                opt =>
                {
                    opt.ForwardedHeaders = ForwardedHeaders.All;
                    /* only loopback proxies are allowed by default, so clear that restriction */
                    opt.KnownNetworks.Clear();
                    opt.KnownProxies.Clear();
                });

            services.AddOptions<DiagnosticsOptions>().Bind(Configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddCors(
                opt =>
                {
                    opt.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .WithOrigins(Configuration["Cors:Origins"].Split(','))
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });

            services
                .AddControllers(
                    opt =>
                    {
                        opt.CacheProfiles.Add(
                            "No",
                            new CacheProfile
                            {
                                NoStore = true,
                                Location = ResponseCacheLocation.None
                            });
                    })
                .AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .AddFluentValidation(
                    config =>
                    {
                        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                        config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        config.ImplicitlyValidateChildProperties = true;
                    });

            services
                .AddSwaggerGen(
                    opt =>
                    {
                        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "v1-referenceimpl-api", Version = "v1" });
                        opt.CustomSchemaIds(type => type.FullName);
                        opt.CustomOperationIds(apiDesc => apiDesc.ActionDescriptor.Id);
                    })
                .AddSwaggerGenNewtonsoftSupport();

            services.AddHttpClient();

            /*
             * impl Refit clients to start calling other crew APIs
             */
            ////services
            ////    .AddRefitClient<IDocumentsApi>(
            ////        new RefitSettings
            ////        {
            ////            ContentSerializer = new JsonContentSerializer(
            ////                new JsonSerializerSettings
            ////                {
            ////                    Converters =
            ////                    {
            ////                        new StringEnumConverter()
            ////                    }
            ////                })
            ////        })
            ////    .ConfigureHttpClient(client => client.BaseAddress = new Uri(Configuration["DocumentsApiBaseAddress"]))
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            app.UseSecureHeadersMiddleware(Extensions.SecureHeadersMiddlewareExtensions.BuildCrewConfiguration());
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(
                opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1-referenceimpl-api");
                    opt.DocExpansion(DocExpansion.List);
                    opt.DefaultModelExpandDepth(3);
                    opt.DefaultModelsExpandDepth(0);
                });
        }
    }
}