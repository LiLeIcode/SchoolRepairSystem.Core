using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolRepairSystem.Common.Helper;
using SchoolRepairSystem.Extensions;
using SchoolRepairSystem.Extensions.Authorizations;
using SchoolRepairSystem.Extensions.AutoMapper;
using SchoolRepairSystem.Extensions.Policy;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Repository;
using SchoolRepairSystem.Service;
using Swashbuckle.AspNetCore.Filters;

namespace SchoolRepairSystem.Api
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
            services.AddControllers();


            services.AddAutoMapper(typeof(CustomProfile));

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IReportForRepairService, ReportForRepairService>();
            services.AddScoped<IReportForRepairRepository, ReportForRepairRepository>();
            services.AddScoped<IWareHouseService, WareHouseService>();
            services.AddScoped<IWareHouseRepository, WareHouseRepository>();
            services.AddScoped<IUserWareHouseService, UserWareHouseService>();
            services.AddScoped<IUserWareHouseRepository, UserWareHouseRepository>();
            services.AddScoped<IReportForRepairService, ReportForRepairService>();
            services.AddScoped<IReportForRepairRepository, ReportForRepairRepository>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuRepository, MenuRepository>();


            services.AddSingleton(new Appsettings(Configuration));
            services.AddDbContext<SchoolRepairSystemDbContext>();

            #region ????????????


            

            string issuer = Appsettings.app(new[] { "PermissionRequirement", "Issuer" });
            string audience = Appsettings.app(new[] { "PermissionRequirement", "Audience" });
            string signingKey = Appsettings.app(new[] { "PermissionRequirement", "SigningCredentials" });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                        //??????????????
                        ValidateIssuer = true,
                    ValidIssuer = issuer, //??????
                                           //??????????????
                        ValidateAudience = true,
                    ValidAudience = audience, //??????
                                              //????????????
                        ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true, //????????????
                        RequireExpirationTime = true, //????????
                    };
            });



            #endregion


            #region ??????????????????

            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
           
            //var keyByteArray = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
            //var signingCredentials = new SigningCredentials(keyByteArray, SecurityAlgorithms.HmacSha256);
            //PermissionRequirement permissionRequirement = new PermissionRequirement(
            //    roleName: "??????",
            //    claimType: ClaimTypes.Role,
            //    audience: audience,
            //    issuer: issuer,
            //    signingCredentials: signingCredentials,
            //    timeSpan: TimeSpan.FromSeconds(60 * 60));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.Requirements.Add(PolicyType.AdminPolicy()));
                options.AddPolicy("Carpentry", policy => policy.Requirements.Add(PolicyType.CarpentryPolicy()));
                options.AddPolicy("Electrician", policy => policy.Requirements.Add(PolicyType.ElectricianPolicy()));
                options.AddPolicy("Ordinary", policy => policy.Requirements.Add(PolicyType.OrdinaryPolicy()));
                options.AddPolicy("AdminAndOrdinary", policy => policy.Requirements.Add(PolicyType.AdminAndOrdinaryPolicy()));
                options.AddPolicy("ElectricianAndCarpentry", policy => policy.Requirements.Add(PolicyType.ElectricianAndCarpentryPolicy()));
                options.AddPolicy("AdminAndOrdinaryAndElectrician", policy => policy.Requirements.Add(PolicyType.AdminAndOrdinaryAndElectricianPolicy()));



                //options.AddPolicy("test", policy=>policy.AddRequirements(new []{  PolicyType.AdminPolicy(), PolicyType.OrdinaryPolicy() }));
            });


            #endregion


            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = $"SchoolRepairSystem.Core ????????-.NetCore 3.1",
                    Description = $"SchoolRepairSystem.Core WebApi v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "LiLeiCode",
                        Email = "2424117373@qq.com",
                        Url = new Uri("https://github.com/LiLeIcode/SchoolRepairSystem.Core.git")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "LiLei??????",
                        Url = new Uri("https://github.com/LiLeIcode/SchoolRepairSystem.Core.git")
                    }
                });
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //Console.WriteLine(basePath);
                //var xmlPath = Path.Combine(basePath,
                //    "D:\\CSharperWeb\\SchoolRepairSystem\\SchoolRepairSystem.Api\\SchoolRepairSystem.Api.xml");
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory,
                    xmlFile);
                //Console.WriteLine("AppContext.BaseDirectory:" + AppContext.BaseDirectory);
                //Console.WriteLine("xmlFile" + xmlFile);
                c.IncludeXmlComments(xmlPath);

                #region Swagger????
                var apiSecurityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT??????????????????????????????Bearer {token}??????????????????????????\"",
                    Name = "Authorization",//jwt ????????????
                    In = ParameterLocation.Header,//jwt????????Authorization????????????????????
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", apiSecurityScheme);//????????????????????oauth2
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                #endregion
            });



            #endregion

            #region Cors????

            services.AddCors(options =>
            {
                options.AddPolicy("cors", policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
            });

            #endregion

        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new AutofacModuleRegister());
        //}


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"policy WebApi v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
