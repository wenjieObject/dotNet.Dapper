using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using Autofac;
using IRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoapCore;
using Utils;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //autofac ����
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.TryAddSingleton<IContract, StudentService>();
            
        }

        ////autofac ����
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ֱ����Autofacע�������Զ���� 
            //builder.RegisterType<DeptRepository>().As<IDeptRepository>();//ע�� 

            var assemblies = Assembly.Load("Repositories");
  
            //ע��ִ� && Service
            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc => cc.Name.EndsWith("Repository") |//ɸѡ
                             cc.Name.EndsWith("Service"))
                .PublicOnly()//ֻҪpublic����Ȩ�޵�
                .Where(cc => cc.IsClass)//ֻҪclass�ͣ���ҪΪ���ų�ֵ��interface���ͣ�
                .AsImplementedInterfaces();//�Զ�����ʵ�ֵ����нӿ����ͱ�¶������IDisposable�ӿڣ�

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseSoapEndpoint<IContract>("/StudentService.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);

        }



    }
}
