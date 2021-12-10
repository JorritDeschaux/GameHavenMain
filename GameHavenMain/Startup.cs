using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace GameHavenMain
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
			services.AddApiVersioning(o =>
			{
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.DefaultApiVersion = new ApiVersion(1, 0);
				o.ReportApiVersions = true;
				o.ApiVersionReader = new HeaderApiVersionReader("Version");
			});

			services.AddScoped<IUserRepo, UserRepo>();
			services.AddScoped<IGameRepo, GameRepo>();

			services.AddTransient<TokenHelper>();

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.WithOrigins("http://localhost:3000");
					builder.AllowAnyHeader();
					builder.AllowAnyMethod();
					builder.AllowCredentials();
				});
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer("JwtBearer", jwtOptions =>
			{
				jwtOptions.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Secret"])),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameHavenMain", Version = "v1" });
				c.SwaggerDoc("v2", new OpenApiInfo { Title = "GameHavenMain", Version = "v2" });
			});
		}



		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameHavenMain v1"));
			}

			app.UseCors();
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
