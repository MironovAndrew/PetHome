using PetHome.Accounts.Infrastructure.Inject.Auth;
using PetHome.API.DependencyInjections;
using PetHome.API.DependencyInjections.AppExtentions;
using PetHome.API.MinimumApi;
using PetHome.Core.Response.Loggers;
using PetHome.Core.Response.Validation;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PetHome.API;
public partial class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        //�������� ������ �� Serilog
        builder.Services.AddSerilog();
        //����������� ����� Seq 
        Log.Logger = SeqLogger.InitDefaultSeqConfiguration(builder.Configuration);


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
        builder.Services.AddSwaggerGetWithAuthentication();

        //����������� ��������������
        builder.Services.ApplyAuthenticationAuthorizeConfiguration(builder.Configuration);

        //����������� infrastructures
        builder.Services.AddModulesInfrastructures(builder.Configuration);

        //����������� handlers
        builder.Services.AddModulesServices(builder.Configuration);



        var app = builder.Build();

        //�������� CORS
        app.AddCORS("http://localhost:5173");

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

        //����������� ��������
        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        //�������� �������-api
        app.AddMinimumApi();


        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

}
public partial class Program;
