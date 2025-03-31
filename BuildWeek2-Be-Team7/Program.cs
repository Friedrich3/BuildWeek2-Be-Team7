using System.Text;
using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.Models.Auth;
using BuildWeek2_Be_Team7.Services;
using BuildWeek2_Be_Team7.Settings;
using FluentEmail.MailKitSmtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(a => a.File("Log/log_txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(a => a.Console())
    .CreateLogger();
try
{
    Log.Information("Starting application...");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddFluentEmail(builder.Configuration.GetSection("MailSettings").GetValue<string>("FromDefault"))
        .AddRazorRenderer()
        .AddMailKitSender(new SmtpClientOptions()
        {
            Server = builder.Configuration.GetSection("MailSettings").GetValue<string>("Server"),
            Port = builder.Configuration.GetSection("MailSettings").GetValue<int>("Port"),
            User = builder.Configuration.GetSection("MailSettings").GetValue<string>("User"),
            Password = builder.Configuration.GetSection("MailSettings").GetValue<string>("Password"),
            UseSsl = builder.Configuration.GetSection("MailSettings").GetValue<bool>("UseSsl"),
            RequiresAuthentication = builder.Configuration.GetSection("MailSettings").GetValue<bool>("RequiresAuthentication")
        });
    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = builder.Configuration.GetSection("Identity").GetValue<bool>("SignInRequireConfirmedAccount");
        options.Password.RequiredLength = builder.Configuration.GetSection("Identity").GetValue<int>("RequiredLength");
        options.Password.RequireDigit = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireDigit");
        options.Password.RequireLowercase = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireLowercase");
        options.Password.RequireUppercase = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireUppercase");
        options.Password.RequireNonAlphanumeric = builder.Configuration.GetSection("Identity").GetValue<bool>("RequireNonAlphanumeric");
    })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Issuer"),
                    ValidAudience = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("SecurityKey")))
                };
            });

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<UserManager<ApplicationUser>>();
    builder.Services.AddScoped<SignInManager<ApplicationUser>>();
    builder.Services.AddScoped<RoleManager<ApplicationRole>>();
    builder.Services.AddScoped<EmailServices>();
    builder.Services.AddScoped<PetServices>();
    builder.Services.AddScoped<ClientServices>();
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddScoped<MedicalExamServices>();


    var app = builder.Build();

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex.Message);
}
finally
{
    await Log.CloseAndFlushAsync();
}