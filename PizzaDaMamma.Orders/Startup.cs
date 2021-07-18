using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDaMamma.Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; private set; }
        public IServiceCollection Services { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Services = services;

            services.AddControllers().AddMvcOptions(option =>
            {
                //option.OutputFormatters.Clear();
                //option.OutputFormatters.Add(new Utf8JsonOutputFormatter(StandardResolver.Default));
                //option.InputFormatters.Clear();
                //option.InputFormatters.Add(new Utf8JsonInputFormatter());
            });
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("unknwn.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            
            {
                if (context.Request.Path.ToString() == "/" || context.Request.Path.ToString().ToLower() == "/index.html")
                {
                    string html = Pages.Index.Load(env);
                    await context.Response.WriteAsync(html);
                    //System.Type t = typeof(Startup);
                    //string file = string.Concat(t.Assembly.Location.Replace(t.Assembly.ManifestModule.Name, string.Empty), env.IsDevelopment() ? @"..\..\..\" : string.Empty, @"wwwroot", context.Request.Path.ToString() == "/" ? @"\index.html" : context.Request.Path.ToString().Replace(@"/", @"\"));
                    //if (File.Exists(file))
                    //{
                    //    //file = string.Concat(t.Assembly.Location.Replace(t.Assembly.ManifestModule.Name, string.Empty), @"wwwroot\app\index.html");
                    //    string html = ReadFileAsString(file);
                    //    file = context.Request.Path.ToString();
                    //    if (file[0] == '/')
                    //        file = file.Substring(1);
                    //    html = html.Replace("<!--location-->", "<script>localStorage.setItem('startupPage','" + file + "');</script>");
                    //    await context.Response.WriteAsync(html);
                    //}
                    //else
                    //    await context.Response.WriteAsync("Auto parts site - page " + context.Request.Path.ToString() + " not found!");
                }
            });
        }

        
    }
}
