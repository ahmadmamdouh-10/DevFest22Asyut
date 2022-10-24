using DevFest22Asyut.Configurations;
using DevFest22Asyut.Data;
using DevFest22Asyut.Helpers;
using DevFest22Asyut.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DevFest22Asyut.Data.DbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddGenericServices();
builder.Services.AddRepositories();

builder.Services.AddIdentity<User, Role>(setup =>
{
    setup.Password.RequireDigit = false;
    setup.Password.RequireLowercase = false;
    setup.Password.RequireNonAlphanumeric = false;
    setup.Password.RequireUppercase = false;
    setup.Password.RequiredUniqueChars = 0;
    setup.Password.RequiredLength = 3;

    setup.User.RequireUniqueEmail = true;

    setup.Lockout.AllowedForNewUsers = false;
    setup.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
})
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DevFest22Asyut.Data.DbContext>();


builder.Services.ConfigureApplicationCookie(config =>
{
    config.AccessDeniedPath = new PathString("/Admin/AccessDenied");
    config.LoginPath = new PathString("/Admin/Login");
    config.LogoutPath = new PathString("/Admin/Logout");
});

builder.Services.AddAutoMapper(typeof(DbContextProfile).Assembly);

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy", config => { config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});


builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});



builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Version = "v1",
        Title = "DevFest22Asyut Api"
    });
});

//builder.Services.AddMvc();


builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
#if DEBUG
    //for development
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFest22Asyut Api");
#else
    //for production  /DevFest22Asyut
     c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFest22Asyut Api");
#endif

});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
