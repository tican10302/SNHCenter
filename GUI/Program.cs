using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddSessionStateTempDataProvider();

builder.Services.AddSession(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("*").AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true) // allow any origin 
                .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
        });
});

builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null); // Không viết thường tên biến khi trả dữ liệu về
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(DTO.Common.CommonConst.ExpireTime);
        options.LoginPath = "/Account/Index";
    });

builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10;config.IsDismissable = true;config.Position = NotyfPosition.BottomRight; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowSpecificOrigin");

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
        response.Redirect("/Account/Logout");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();
app.UseNotyf();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();