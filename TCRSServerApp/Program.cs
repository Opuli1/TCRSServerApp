using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<UserService>()
                .AddTransient<CategoryService>()
                .AddTransient<ContentPostService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AppAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(serviceProvider =>
    serviceProvider.GetRequiredService<AppAuthenticationStateProvider>());
builder.Services.AddScoped<SearchService>();
builder.Services.AddScoped<BannerService>();

var appConnectionString = builder.Configuration.GetConnectionString("TCRS");

builder.Services.AddDbContext<TCRSContext>(options =>
    options.UseSqlServer(appConnectionString), ServiceLifetime.Transient);

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
