using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IService.IUserService;
using Application.Service.UserService;
using Common.BaseInterfaces.IBaseRepository;
using Common.BaseInterfaces.IBaseRepository.IRepository;
using Common.CommonHellper.Ext;
using Domain.IRepository;
using Infrastruct.Context;
using Infrastruct.Repository.BaseRepository;
using Infrastruct.Repository.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Common.CommonHellper;
using Swashbuckle.AspNetCore.Filters;
using MediatR;
using Domain.Cmds;
using Domain.CmdHandler;
using Common.IService;
using Common.Service;
using Common.Notice;
using Domain.Events;
using Domain.EventHandler;
using Common.Consul;
using ClientDependency.Core;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace CoreApl
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //���controller����(webapi)
            services.AddControllers();//.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            //������ݿ�����
            services.AddDbContext<CoreDemoDBContext>(o =>
            {
                o.UseSqlServer(Configuration.GetSection("ConnectionStrings:Default").Value);
            });



            #region ���JWT

            services.AddOptions();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtToken"));

            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind("JwtToken", jwtSettings);
            
            services.AddAuthentication(opertion =>
            {
                opertion.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opertion.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                //����JWT����
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤������
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,//������
                                                     //�Ƿ���֤������
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,//������
                                                         //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),

                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = true, //����ʱ��
                };
                c.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        var payload = JsonConvert.SerializeObject(new CusResult
                        {
                            code = 401,
                            msg = "�ܱ�Ǹ������Ȩ���ʸýӿڣ�",
                            data = null
                        });
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 200;
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
            });
            #endregion

            #region ����Swagger���� 
            //����Swagger
            services.AddSwaggerGen(c =>
            {
                var version = "v1";
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = $"{Configuration.GetSection("BasicSettings:apiName").Value} CoreAPI�ӿ��ĵ�����dotnetcore 3.1",//�༭����
                    Version = version,//�汾��
                    Description = $"{Configuration.GetSection("BasicSettings:apiName").Value} HTTP API V1",//�༭����
                    Contact = new OpenApiContact { Name = $"{ Configuration.GetSection("BasicSettings:apiName").Value }-���Ҹ�����Ա���ʼ�", Email = "929013002@qq.com" },//�༭��ϵ��ʽ
                    License = new OpenApiLicense { Name = Configuration.GetSection("BasicSettings:apiName").Value }//�༭���֤
                });
                c.OrderActionsBy(o => o.RelativePath);//����˳��

                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CoreApl.xml");// ���ýӿ��ĵ��ļ�·��
                c.IncludeXmlComments(xmlPath, true); // �ѽӿ��ĵ���·�����ý�ȥ���ڶ���������ʾ�����Ƿ���������Controller��ע������

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // ����Ҫ���������ð�ȫУ�飬��֮ǰ�İ汾��һ��
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // ���� oauth2 ��ȫ������������ oauth2 �������
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion

            //ע�뻺��
            services.AddSingleton<IMemoryCache, MemoryCache>();

            //services.AddTransient<>
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            //ע��ִ�
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository<>));

            #region ����MediatR

            //����MediatR
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorService, MediatorService>();
            services.AddScoped<INotificationHandler<Notification>, Noticehandler>();
            #endregion

            //������� Mediatr  Request/Response
            services.AddScoped<IRequestHandler<AddUserCommand, Unit>, UserCmdHandler>();
            //�����¼��� Notification 
            services.AddScoped<INotificationHandler<InitUserRoleEvent>, InitUserRoleEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          
            
            app.UseRouting();
            //1.�ȿ�����֤
            app.UseAuthentication();
            //2.�ٿ�����Ȩ
            app.UseAuthorization();

   


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #region ���Swagger�м��

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
            #endregion
        }
    }
}
