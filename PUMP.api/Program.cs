using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PUMP.core.BL.Interfaces;
using PUMP.core.BL.Services;
using PUMP.helpers;
using PUMP.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// JWT Setup
Settings.Key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
Settings.Issuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Settings.Issuer,
            ValidAudience = Settings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Settings.Key)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("General",
        policy => policy.RequireRole("Employee", "Admin"));
    options.AddPolicy("Administrator",
        policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
    });
    
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

});

// // Get string connection
Settings.ConnectionString = builder.Configuration.GetConnectionString("path");

// Add dependency injection
builder.Services.AddTransient<IPatient, PatientServices>();
builder.Services.AddTransient<IDoctor, DoctorServices>();
builder.Services.AddTransient<IAuth, AuthServices>();
builder.Services.AddTransient<IAppointment, AppointmentServices>();
builder.Services.AddTransient<IMeasurement, MeasurementServices>();


var app = builder.Build();

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
