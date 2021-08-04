using System;
using MoviesAPI.Model;
using MoviesAPI.Services;
using MoviesAPI.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace MoviesAPI
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
            services.AddControllers();
            services.AddScoped<IMovies, MoviesRepository>();
            services.AddScoped<IRentals, RentalRepository>();
            services.AddScoped<IPasswordHasher<Users>,PasswordHasher<Users>>();
            services.AddScoped<IBlob, Blob>();
            services.AddScoped<IUserImage, UserImageRespository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddIdentity<Users, IdentityRole>().AddEntityFrameworkStores<IdentityDb>().AddSignInManager().AddDefaultTokenProviders();
            services.AddDbContext<MovieDb>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Movies")).EnableSensitiveDataLogging();
            });
            services.AddDbContext<IdentityDb>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("Movies")).EnableSensitiveDataLogging();
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.User.RequireUniqueEmail = false;

            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;

                options.LoginPath = PathString.Empty;
                options.AccessDeniedPath = PathString.Empty;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
