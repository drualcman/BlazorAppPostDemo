using BlazorAppPostDemo.Data;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddCors( options => 
{
    options.AddDefaultPolicy(policy => 
    {
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
    });
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

app.UseCors();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapPost("create-data", (WeatherForecast data) =>
{
    string json = JsonSerializer.Serialize(data);
    StringBuilder stringBuilder = new StringBuilder("I can receive a post with data:");
    stringBuilder.AppendLine("---");
    stringBuilder.AppendLine(json);
    stringBuilder.AppendLine("---");
    stringBuilder.AppendLine("So I can create resources");
    return Results.Ok(stringBuilder.ToString());
});

app.Run();
