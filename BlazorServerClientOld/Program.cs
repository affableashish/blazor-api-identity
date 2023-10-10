using BlazorServerClientOld.Components;
using BlazorServerClientOld;
using BlazorServerClientOld.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

// Stuff that I added 👇
// Configure the HttpClient for the identity service
var identityApiUrl = builder.Configuration["IdentityWebAPI:BaseUrl"]!;
builder.Services.AddHttpClient<IdentityClient>(client =>
{
    client.BaseAddress = new Uri(identityApiUrl);
});
// Configure the HttpClient for the forecast service
var protectedApiUrl = builder.Configuration["ProtectedWebAPI:BaseUrl"]!;
builder.Services.AddHttpClient<WeatherForecastService>(client =>
{
    client.BaseAddress = new Uri(protectedApiUrl);
});
// Stuff that I added 👆

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

app.MapRazorComponents<App>();

app.Run();