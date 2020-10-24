using AutoMapper;
using GoodShoppingRD.Models;
using GoodShoppingRD.Models.Auth;
using GoodShoppingRD.Repository;
using GoodShoppingRD.Repository.Contracts;
using GoodShoppingRD.Services;
using GoodShoppingRD.Services.Contracts;
using GoodShoppingRD.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace GoodShoppingRD
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
            var jwtSettings = Configuration.GetSection("Jwt");

            services.AddDbContext<GoodShoppingRdDbContext>(opt => opt.UseSqlServer(Configuration["ConnectionString"]));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<GoodShoppingRdDbContext>()
                .AddDefaultTokenProviders();

            #region Repositories

            services.AddScoped<IBaseRepository<Supermarket>, BaseRepository<Supermarket>>();
            services.AddScoped<IBaseRepository<SupermarketAddress>, BaseRepository<SupermarketAddress>>();
            services.AddScoped<IBaseRepository<Catalog>, BaseRepository<Catalog>>();
            services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IBaseRepository<ProductCatalog>, BaseRepository<ProductCatalog>>();
            services.AddScoped<IBaseRepository<Sale>, BaseRepository<Sale>>();
            services.AddScoped<IBaseRepository<ShoppingCart>, BaseRepository<ShoppingCart>>();
            services.AddScoped<IBaseRepository<ShoppingCartDetail>, BaseRepository<ShoppingCartDetail>>();

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.Configure<JwtSettings>(jwtSettings);

            services.AddScoped<IAuthService, AuthService>();

            services.AddAuth(jwtSettings.Get<JwtSettings>());

            services.AddSwagger();

            services.AddControllers();
        }

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Good Shopping RD API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
