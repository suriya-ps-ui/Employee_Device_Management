using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services;
using Microsoft.OpenApi.Models;

var builder=WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();

// Add appsettings.Development.json as the primary configuration file
builder.Configuration
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
// Add DbContext
builder.Services.AddDbContext<AssetManagementContext>(options=>options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

// Add services
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<IDeviceService,DeviceService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddControllers();
// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>{
        options.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuer=false,
            ValidateAudience=false,
            ValidateLifetime=true,
            ValidateIssuerSigningKey=true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]??throw new Exception("Key is Null")))
        };
    });
builder.Services.AddAuthorization();
// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset Management API", Version = "v1" });
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer",
        BearerFormat="JWT",
        In=ParameterLocation.Header,});

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{{
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }}
                ,Array.Empty<string>()}});
                });

var app = builder.Build();
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c =>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asset Management API V1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();