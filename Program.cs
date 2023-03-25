using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();



// Add Blazor default authentication state provider
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();



// Add authentication services
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {

//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            //ValidateIssuerSigningKey = true,
//            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("GDJO6baHwsOz7kPBlHgf")),
//            //ValidateIssuer = false,
//            //ValidateAudience = false,

//            ValidateIssuer = false,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"], //"http://localhost:44302/",
//            ValidAudience = builder.Configuration["Jwt:Issuer"], //"http://localhost:44302/",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token"]))  //"GDJO6baHwsOz7kPBlHgf"


//        };
//    })
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Login";
//        options.LogoutPath = "/Logout";
//        options.Cookie.Name = "prefs_userID";
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.Cookie.Name = "prefs_userID";
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Line to enable authentication

app.UseAuthorization();

app.MapRazorPages();

app.Run();
