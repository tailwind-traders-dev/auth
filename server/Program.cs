
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
 
var app = builder.Build();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");


app.UseCors(builder => builder
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
);

//load the routes
app.Run();

//this is for tests
public partial class Program { }