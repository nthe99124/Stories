using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Globalization;
#region Config service
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region config Author, Authen
// TODO: ntthe => hiện tại dùng cookie, nếu cần thay đổi thì chuyển BearToken thì cần config lại
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Account/Login"; // đường dẫn tới trang đăng nhập
    //option.AccessDeniedPath = "/"; // đường dẫn tới trang không có quyền
});
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

#region Context DB
//service.AddDbContext<StoriesProjectContext>(options =>
//{
//    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
//});
#endregion

#region Localize

services.AddSingleton<IHtmlLocalizerFactory, HtmlLocalizerFactory>();
services.AddLocalization(options => options.ResourcesPath = "Resources");

// config thêm ngôn ngữ mới
var supportedCultures = new[]
{
    new CultureInfo("vi-VN"),
    new CultureInfo("en-US"),
};
services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("vi-VN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
    };
});
#endregion

#region Đăng kí lifetime cho các BL

#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
