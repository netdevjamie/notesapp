using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.WebApi
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
      //services.AddControllers();
      services.AddControllersWithViews();
      // services.AddRazorPages();

      services.AddMvc()
        .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      //app.UseMvc(routes =>
      //{
      //    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
      //});
      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
              //endpoints.MapControllers();
              //endpoints.MapRazorPages();
              endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
      });

      // setup app's root folders
      AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
      AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);
    }
  }
}
