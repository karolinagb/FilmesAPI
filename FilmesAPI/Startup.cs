using FilmesAPI.Data;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace FilmesAPI
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
            services.AddDbContext<FilmeDbContext>(opt => opt
                .UseLazyLoadingProxies()
                .UseMySQL(Configuration
                .GetConnectionString("FilmeConnection")));

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //desafio para validar o token
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = true;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters()
                {
                    //validar quem est� assinando/gerou esse token
                    ValidateIssuerSigningKey = true,

                    //definir como ele vai validar quem assinou esse token
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("aaafdfdsfjdsfskdjbbbfjdfsdfksdfdjs")),

                    //N�o vamos nos preocupar quem � o issuer desse token
                    //nem vamos valida a audience
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    //O rel�gio de contagem de tempo de expira��o do token vai ser a partir de zero
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<FilmeService, FilmeService>();
            services.AddScoped<CinemaService, CinemaService>();

            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); //Ignora a referencia ciclica

            //Par�metros = informa que queremos usar o AutoMapper dentro do Assembly da nossa aplica��o
            //AppDomain = Dom�nio da nossa aplica��o
            //CurrentDomain = Dom�nio atual
            //GetAssemblies = Obter o assembly
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
