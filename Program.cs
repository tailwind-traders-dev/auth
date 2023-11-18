using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//load up the ENV stuff
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
// .AddGoogle(options =>
// {
//     options.ClientId = configuration["Authentication:Google:ClientId"];
//     options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//     options.ClientSecret = "1B8awSbGR5jboG9oG4OmWrat";

//     options.ClaimsIssuer = "https://www.googleapis.com/oauth2/v1/certs";
//     //options.CallbackPath = "https://localhost:5001/Account/Externallogincallback";
//     options.CallbackPath = new PathString("/signin-google");

  
//     options.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
//     options.TokenEndpoint = "https://oauth2.googleapis.com/token";
//     //options.UserInformationEndpoint = "https://api.googleusercontent.com/user";
//     options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
//     options.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
//     options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
//     options.SaveTokens = true;
// }).AddGitHub(options =>
// {
//     options.ClientId = Environment.GetEnvironmentVariable("GITHUB_ID");
//     options.ClientSecret = Environment.GetEnvironmentVariable("GITHUB_SECRET");
// });


var app = builder.Build();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
);

app.MapGet("/", (HttpContext context) => 
{
  var links = new List<Dictionary<string,string>>();
  links.Add(new Dictionary<string,string>(){
    {"rel","self"},
    {"href","/"}
  });
  links.Add(new Dictionary<string,string>(){
    {"rel","signin"},
    {"href","/signin"}
  });
});

app.MapGet("/signin", (HttpContext context) =>
{
    //return a JSON list of providers and the links
    var providers = new List<string>();
    providers.Add("Google");
    providers.Add("GitHub");
    return Results.Json(providers);
});



//load the routes
app.Run();

//this is for tests
public partial class Program { }