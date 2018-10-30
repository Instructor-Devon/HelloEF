using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloEF.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloEF
{
    public class Startup
    {
        public IConfiguration Configuration {get;set;}
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
           Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            // Configuration["DBInfo:ConnectionString"]
            services.AddDbContext<MyContext>(options => options.UseMySQL(Configuration["DBInfo:ConnectionString"]));
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
    }
}
