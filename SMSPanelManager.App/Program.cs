using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SMSPanelManager.Core.Services;
using SMSPanelManager.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SMSPanelContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SMSPanelConection"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
}
);
builder.Services.AddTransient<ISMSService, SMSServices>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
).AddCookie(options =>
{
    options.LoginPath = "/Login";

    options.LogoutPath = "/Logout";

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
