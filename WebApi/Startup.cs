using Autofac;
using Autofac.Core;
using Ioc.Interface;
using Ioc.Registrador;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using WebApi.Base;

namespace WebApi
{
    public class Startup : IControllerRegistrador
    {
        public ILifetimeScope AutofacContainer { get; set; }
        public IConfiguration Configuration { get; }
        public HttpContextAccessor HttpContextAccessor { get => new HttpContextAccessor(); }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        ///// <summary>
        ///// Configuração da autoridade de autenticação
        ///// </summary>
        ///// <param name="services">Service Collection .Net Core</param>
        //private void ConfigurarAutenticacao(IServiceCollection services)
        //{
        //    this.ConfigurarIdentityServer(services);
        //    this.ConfigurarCliente(services);
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseIdentityServer();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zup Recrutamento API V1");
            });
        }

        /// <summary>
        /// Chamada da implementação para configurar container (chamada efetuada pelo módulo Ioc)
        /// </summary>
        /// <param name="builder">Container builder autofac</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            Registrador.Registrar(builder, this);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            services.AddHttpContextAccessor();

            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(ExceptionFilter));
                //options.Filters.Add(typeof(SessionFilter));
            })
            .AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Zup Recrutamento",
                    Description = "Sistema para recrutamento da Zup",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Moyle",
                        Email = "rafael.moyle@gmail.com"
                    }
                });

                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new string[] { }
                    }
                };

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Copie 'Bearer ' + token'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                c.AddSecurityRequirement(security);

                foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.SwaggerDoc.XML", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(name);
                }
            });

            //this.ConfigurarAutenticacao(services);
        }

        /// <summary>
        /// Método para ações customizadas ao ativar um tipo já cadastrado na injeção de dependencia (caso necessário)
        /// </summary>
        /// <typeparam name="TypeOf">Tipo</typeparam>
        /// <param name="e">Tipo que está sendo instanciado</param>
        public void OnActivatingInstance<TypeOf>(IActivatingEventArgs<TypeOf> e)
        {

        }

        /// <summary>
        /// Registro de controllers
        /// </summary>
        /// <param name="builder">Container builder autofac</param>
        public void RegistrarComponentesInternos(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseApiController).Assembly.GetName()))
                   .PropertiesAutowired();
        }
    }
}
