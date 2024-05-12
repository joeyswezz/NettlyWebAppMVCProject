var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add authentication services
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.LoginPath = "/Home/SignIn";  // Set the login path
    options.LogoutPath = "/Home/Logout";  // Set the logout path
    options.AccessDeniedPath = "/Home/AccessDenied";  // Set the access denied path
    options.Cookie.Name = "YourAppName.Cookie";
});

// Add logging services for better diagnostics (optional, recommended)
builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
    // Additional logging providers as needed
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Ensure you have an Error view
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();  // Provide detailed errors in development
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

