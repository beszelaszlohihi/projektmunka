using F1Zone.BlazorApp.Components;
using F1Zone.BlazorApp.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using F1ZoneLibrary.Dto; 

var builder = WebApplication.CreateBuilder(args);

//Blazor Server komponensek hozzáadása
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//LocalStorage
builder.Services.AddBlazoredLocalStorage();

//Hitelesítési alapok
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

//AuthProvider regisztrálás
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthStateProvider>());

//HttpClient az API-hoz
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5007/")
});

//Az AuthService regisztrálása
builder.Services.AddScoped<AuthService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();