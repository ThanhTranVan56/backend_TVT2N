using backend_TVT2N.Controllers;
using Libs;
using Libs.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

}, ServiceLifetime.Transient);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
                };
            });

builder.Services.AddAuthorization(options => {

    options.AddPolicy("Product.View", policy => {
        policy.RequireAssertion(context => context.User.IsInRole("admin") ||
        context.User.HasClaim(s => s.Type == "Product.View"));
    });
    options.AddPolicy("Product.Create", policy => {
        policy.RequireAssertion(context => context.User.IsInRole("admin") ||
        context.User.HasClaim(s => s.Type == "Product.Create"));
    });
    options.AddPolicy("Product.Edit", policy => {
        policy.RequireAssertion(context => context.User.IsInRole("admin") ||
        context.User.HasClaim(s => s.Type == "Product.Edit"));
    });
    options.AddPolicy("Product.Delete", policy => {
        policy.RequireAssertion(context => context.User.IsInRole("admin") ||
        context.User.HasClaim(s => s.Type == "Product.Delete"));
    });
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ShoppingCartService>();
builder.Services.AddTransient<ProfileService>();
builder.Services.AddTransient<AddressService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<OrderDetailService>();
builder.Services.AddTransient<ReviewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
