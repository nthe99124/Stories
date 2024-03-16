using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Mappings;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel;
using System.Text;

#region Config service
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
#region IConfiguration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

services.AddSingleton(configuration);
#endregion

#region config http
// Add services to the container.
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddHttpContextAccessor();
#endregion

#region Swagger
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Bearer Auth"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[]{}
                    }
                });
});
#endregion

#region config Author, Authen
// sử dụng jwt bear token
var secretKey = configuration.GetSection("Jwt").GetSection("SecretKey").Value;
if (secretKey != null)
{
    var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
    services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(opt =>
    {
        opt.SaveToken = true;
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            // các mã xác thực thông báo
            //grant token
            ValidateIssuer = false,
            ValidateAudience = false,

            //sign token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "Role"
        };
    });
}


#endregion

#region config CORS
// TODO: ntthe => xác định lại cần truy cập từ đâu nữa để config thêm nhé -> hiện tại defined allow all rồi
services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
#endregion

#region Xử lý DDOS
services.AddMemoryCache();
services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IPRateLimiting").Bind(options));
services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
#endregion

#region Config cache
//TODO: hiện tại đang làm cache distributed => sau có cần cache khác thì custom thêm
services.AddDistributedMemoryCache();
services.AddSingleton<IDistributedCacheCustom, DistributedCacheCustom>();
#endregion

#region Context DB
services.AddDbContext<StoriesContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Đăng kí lifetime cho các Service, Respository

//Common
services.AddScoped<IRestOutput, RestOutput>();

services.AddScoped<StoriesContext>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

// mỗi lần gọi 1 phát tạo insert nên dùng luôn Transient

//Repository
services.AddTransient<ILogEntryRepository, LogEntryRepository>();
services.AddTransient<IAccountantsRepository, AccountantRepository>();
services.AddTransient<IStoriesRepository, StoriesRepository>();
services.AddTransient<ITopicRepository, TopicRepository>();
services.AddTransient<IRoleAccountantRepository, RoleAccountantRepository>();
services.AddTransient<IRoleRepository, RoleRepository>();
services.AddTransient<IAuthorRegisterRepository, AuthorRegisterRepository>();
services.AddTransient<ITopicStoryRepository, TopicStoryRepository>();

//Service
services.AddTransient<ILogEntryService, LogEntryService>();
services.AddTransient<IAccoutantsService, AccoutantsService>();
services.AddTransient<IStoriesService, StoriesService>();
services.AddTransient<ITopicService, TopicService>();

#endregion

#region Config AutoMapper
services.AddAutoMapper(typeof(MappingProfile));
#endregion

#endregion


#region Run service pipleline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region CORS
app.UseCors("AllowAnyOrigin");
#endregion

#region DDOS
app.UseIpRateLimiting();
#endregion

#region Authen, Author
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.MapControllers();

app.Run();
#endregion