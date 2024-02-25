using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using StoriesProject.Common.Cache;
using StoriesProject.Common.Repository;
using StoriesProject.Data;
using StoriesProject.MiddleWare;
using StoriesProject.Model.ViewModel;
using StoriesProject.Repositories.Base;
using System.Globalization;

#region Config service
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Khởi tạo IConfiguration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

services.AddSingleton(configuration);

#region Razor page config
services.AddRazorPages();
services.AddServerSideBlazor();
#endregion

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

#region Đăng kí lifetime cho các Service, Respository
services.AddScoped<RestOutput>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Config AutoMapper
//services.AddAutoMapper(typeof(Startup));
#endregion

#region Config http requets
// cấu hình phiên 
// TODO: ntthe => đánh giá lại xem blazor app có cần quản lý session kiểu này không, hay chỉ cần quản lý phiên author thôi
//services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//});
// lấy thông tin từ http request
services.AddHttpContextAccessor();
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



services.AddSingleton<WeatherForecastService>();
#endregion

#region Run service pipleline
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

#region CORS
app.UseCors("AllowAnyOrigin");
#endregion

#region DDOS
app.UseIpRateLimiting();
#endregion

#region Authen, Author
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response != null && response.StatusCode == 401)
    {
        await Task.Run(() => {
            response.Redirect("/Account/Login");
        });
    }
});
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.MapFallbackToPage("/_Host");

#region Localize
app.UseRequestLocalization();
app.UseMiddleware<LanguageMiddleware>();
#endregion

app.Run();
#endregion