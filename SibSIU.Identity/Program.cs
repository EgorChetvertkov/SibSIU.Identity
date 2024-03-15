using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Rewrite;

using SibSIU.Auth.Database;
using SibSIU.Core.MailService;
using SibSIU.CORS.Database;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean;
using SibSIU.Domain.ExternalApplication;
using SibSIU.Domain.ExternalApplication.CORSes.Validations;
using SibSIU.Domain.SendMail;
using SibSIU.Domain.UserManager;
using SibSIU.Identity.Database;
using SibSIU.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new()
    {
        Description = "API SibSIU SSO",
        Title = "SibSIU.SSO",
        Version = "v1"
    }));

builder.AddCORS();

var userDataConnectionString = builder.GetConnectionStringByName("UserData");
var oidcConnectionString = builder.GetConnectionStringByName("Identity");
var deanConnectionString = builder.GetConnectionStringByName("Dean");
var outBoxConnectionString = builder.GetConnectionStringByName("OutBox");
var corsConnectionString = builder.GetConnectionStringByName("CORS");

builder.Services.AddDeanDatabase(deanConnectionString);
builder.Services.AddOpenIdConnectDatabase(
    oidcConnectionString,
    builder.GetSymmetricSecurityKey("Encryption"),
    builder.GetCertificate("SignIn"));
builder.Services.AddAuthDatabase(userDataConnectionString);
builder.Services.AddMailerWithOutBoxDatabase<OutBoxContext>(builder.Configuration, outBoxConnectionString);
builder.Services.AddCORSDatabase(corsConnectionString);

builder.Services.AddDomainDeanServices();
builder.Services.AddUserManagerServices();
builder.Services.AddApplicationServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMemoryCache();

builder.Services.AddAuthorization(); //TODO : Add policies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "sso";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Error/Index/401";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });
builder.Services.AddRazorPages(option => option.Conventions
    .AllowAnonymousToFolder("/Account")
    .AllowAnonymousToFolder("/Error")
    .AuthorizeFolder("/Admin")
    .AuthorizeFolder("/Home")); // TODO : add authorize folders

builder.Services.AddProblemDetails();

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/Error/Index/{0}");

RewriteOptions options = new RewriteOptions().AddRedirectToHttps().AddRedirectToWwwPermanent();
app.UseRewriter(options);

app.UseHsts();

app.UseHttpsRedirection();
app.UseCORSPolicy();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();

app.MapControllers();
app.MapRazorPages();

app.Run();
