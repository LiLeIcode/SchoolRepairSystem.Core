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
            services.AddScoped<IRoleReportForRepairService, RoleReportForRepairService>();
            services.AddScoped<IRoleReportForRepairRepository, RoleReportForRepairRepository>();


            services.AddSingleton(new Appsettings(Configuration));

            #region 授权注册服务


            //var Issurer = "lilei.Auth";
            //var Audience = "api.auth";
            //var secret = "zxcvbnmasdfghjklqwertyuiop";

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
                        //是否验证发行人
                        ValidateIssuer = true,
                    ValidIssuer = issuer, //发行人
                                           //是否验证受众人
                        ValidateAudience = true,
                    ValidAudience = audience, //受众人
                                              //是否验证密钥
                        ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true, //验证生命周期
                        RequireExpirationTime = true, //过期时间
                    };
            });



            #endregion


            #region 基于自定义策略授权

            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
           
            //var keyByteArray = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
            //var signingCredentials = new SigningCredentials(keyByteArray, SecurityAlgorithms.HmacSha256);
            //PermissionRequirement permissionRequirement = new PermissionRequirement(
            //    roleName: "管理员",
            //    claimType: ClaimTypes.Role,
            //    audience: audience,
            //    issuer: issuer,
            //    signingCredentials: signingCredentials,
            //    timeSpan: TimeSpan.FromSeconds(60 * 60));
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("Admin", policy => policy.Requirements.Add(PolicyType.AdminPolicy()));
                options.AddPolicy("Carpentry", policy => policy.Requirements.Add(PolicyType.CarpentryPolicy()));
                options.AddPolicy("Electrician", policy => policy.Requirements.Add(PolicyType.ElectricianPolicy()));
                options.AddPolicy("Ordinary", policy => policy.Requirements.Add(PolicyType.OrdinaryPolicy()));
            });


            #endregion


            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = $"SchoolRepairSystem.Core 接口文档-.NetCore 3.1",
                    Description = $"SchoolRepairSystem.Core WebApi v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "LiLeiCode",
                        Email = "2424117373@qq.com",
                        Url = new Uri("http://www.baidu.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "LiLei许可证",
                        Url = new Uri("http://www.baidu.com")
                    }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath,
                    "D:\\CSharperWeb\\SchoolRepairSystem\\SchoolRepairSystem.Api\\SchoolRepairSystem.Api.xml");
                c.IncludeXmlComments(xmlPath);

                #region Swagger加锁
                var apiSecurityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt 默认参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置（请求头）
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", apiSecurityScheme);//这里的方案名称必须是oauth2
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                #endregion
            });



            #endregion

            #region Cors跨域

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
