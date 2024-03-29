using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderingAPizza.DataAccess;
using OrderingAPizza.DataAccess.CQRS;
using OrderingAPizza.ApplicationServices.API.Domain;
using OrderingAPizza.ApplicationServices.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using OrderingAPizza.ApplicationServices.API.Validators;
using Microsoft.AspNetCore.Authentication;
using OrderingAPizza.Authentication;

namespace OrderingAPizza
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
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddMvcCore()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddPizzaTypeRequestValidator>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddTransient<IQueryExecutor, QueryExecutor>();
            services.AddTransient<ICommandExecutor, CommandExecutor>();

            services.AddAutoMapper(typeof(PizzasProfile).Assembly);

            services.AddMediatR(typeof(ResponseBase<>));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddDbContext<RestaurantStorageContext>(
                opt => 
                opt.UseSqlServer(this.Configuration.GetConnectionString("RestaurantDatabaseConnection")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderingAPizza", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderingAPizza v1"));
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
