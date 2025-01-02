using PetHome.Core.Controllers;
using PetHome.Core.Response.Loggers;
using PetHome.Core.Response.Validation;
using PetHome.Volunteers.Infrastructure;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PetHome.Volunteers.Application;
using PetHome.Species.Infrastructure;
using PetHome.Species.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using PetHome.Accounts.Domain;
using Microsoft.AspNetCore.Identity;
using PetHome.Accounts.Application;
using PetHome.Accounts.Infrastructure.Inject;

namespace PetHome.API;
public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        //�������� ������ �� Serilog
        builder.Services.AddSerilog();
        //����������� ����� Seq 
        Log.Logger = SeqLogger.InitDefaultSeqConfiguration();


        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        //Auto validation
        builder.Services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        //����������� swagger � ������������ ��������������
        builder.Services.AdddSwaggerGetWithAuthentication();
         
        //����������� infrastructures
        builder.Services
            .AddAccountsInfrastructure()
            .AddSpeciesInfrastructure(builder.Configuration)
            .AddVolunteerInfrastructure(builder.Configuration);

        //����������� handlers
        builder.Services 
            .AddSpeciesServices()
            .AddVolunteerServices();


        var app = builder.Build();

        //Middleware ��� ������ ���������� (-���� �����)
        app.UseExceptionHandler();

        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //Automigration
            //app.ApplyAutoMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

}
public partial class Program;
