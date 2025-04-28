using System.Net;
using System.Text;
using CloudinaryDotNet;
using Coursery.Application.UseCases.Add;
using Coursery.Application.UseCases.Get;
using Coursery.Infrastucture.Data;
using CourseryPL.Controllers;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

Cloudinary cloudinary = new Cloudinary("cloudinary://424864418716181:mKI-uUjsPbq7Y5ogkO3UnDTkCN0@pick-n-play");

builder.Services.AddDbContext<CourseryDbContext>();

builder.Services.AddScoped<AddUser>();
builder.Services.AddScoped<GetUser>();
builder.Services.AddScoped<GetUserByEmailAndPassword>();
builder.Services.AddScoped<GetCourses>();
builder.Services.AddScoped<GetCourse>();
builder.Services.AddScoped<AddCourse>();
builder.Services.AddScoped<EnrollCourse>();
builder.Services.AddScoped<GetEnrolledCourses>();
builder.Services.AddScoped<GetLessonsForUser>();
builder.Services.AddScoped<GetModulesByCourseId>();
builder.Services.AddScoped<AddLesson>();
builder.Services.AddScoped<AddModule>();
builder.Services.AddScoped<GetModule>();
builder.Services.AddScoped<UpdateModule>();
builder.Services.AddScoped<UpdateLesson>();
builder.Services.AddScoped<DeleteModule>();
builder.Services.AddScoped<DeleteLesson>();
builder.Services.AddScoped<GetLessonsByModuleId>();
builder.Services.AddScoped<UpdateUser>();
builder.Services.AddScoped<AddHistoryRecord>();
builder.Services.AddScoped<GetHistoryRecord>();
builder.Services.AddScoped<AddReview>();
builder.Services.AddScoped<GetReviewsByCourseId>();
builder.Services.AddScoped<GetCreatedCourses>();
builder.Services.AddScoped<DeleteCourse>();
builder.Services.AddScoped<UpdateCourse>();



// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var key = Encoding.ASCII.GetBytes("secretnotsosecretsecretnotsosecretsecretnotsosecretsecretnotsosecretsecretnotsosecret");
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "Coursery API for course project",
        TermsOfService = new Uri("https://coursery.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://coursery.com/contact")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://coursery.com/license")
        }
    });
    
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    
    options.SchemaFilter<EnumSchemaFilter>();
});


builder.Services.AddMapster();
builder.Services.AddDbContext<CourseryDbContext>(); 

var app = builder.Build();

app.UseCors("AllowLocalhost3000");


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Coursery API v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "WTF is this");
});

app.MapOpenApi();
app.UseHttpsRedirection();


app.Run();