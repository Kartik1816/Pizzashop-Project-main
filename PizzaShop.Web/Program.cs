using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PizzaShop.Domain.DBContext;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.Repository;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;
using PIzzaShop.Service.Services;

var builder = WebApplication.CreateBuilder(args);

var conn=builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PizzaShopDbContext>(options => options.UseNpgsql(conn));
// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IGenerateJwt,GenerateJwt>();
builder.Services.AddScoped<IChangePasswordRepository,ChangePasswordRepository>();
builder.Services.AddScoped<IChangePasswordService,ChangePasswordService>();
builder.Services.AddScoped<IEditProfileRepository,EditProfileRepository>();
builder.Services.AddScoped<IEditProfileService,EditProfileService>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<IForgotPasswordRepository,ForgotPasswordRepository>();
builder.Services.AddScoped<IForgotPasswordService,ForgotPasswordService>();
builder.Services.AddScoped<IUserListRepository,UserListRepository>();
builder.Services.AddScoped<IUserListService,UserListService>();
builder.Services.AddScoped<IDashboardService,DashboardService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5066",
            ValidAudience = "http://localhost:5066",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ICZasdklfjhacknHLisdfnLsdhoSfal4k2342s134k2dfcna234023q42e4w4rIFAKn"))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers["Authorization"] = "Bearer " + token;
                }
                return Task.CompletedTask;
            }
        };
    });
    builder.Services.AddAuthorization();
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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
