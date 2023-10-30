﻿using CTN4_Serv.Service;
using CTN4_Serv.Service.IService;
using CTN4_Serv.Service.Service;
using CTN4_Serv.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ILoginService, LoginServices>();
builder.Services.AddTransient<ITokenService, TokenServices>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddTransient<IKhachHangService, KhachHangService>();
builder.Services.AddTransient<ISanPhamService, SanPhamService>();
builder.Services.AddSession(option =>
{
	//option.IdleTimeout = TimeSpan.FromSeconds(60);
	// Định hình Session này tồn tại trong 30 giây
}); // Thêm cái này để dùng Session


builder.Services.AddAuthentication(auth =>
 {
     auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
 })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Nhân viên", policy => { policy.RequireAuthenticatedUser(); policy.RequireRole("Nhân viên"); });
    options.AddPolicy("Quản lý", policy => { policy.RequireAuthenticatedUser(); policy.RequireRole("Quản lý"); });


});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.Use(async (context, next) =>
{
	var token = context.Session.GetString("Token");
	if (!string.IsNullOrEmpty(token))
	{
		context.Request.Headers.Add("Authorization", "Bearer " + token);
	}
	await next();
});
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=DangNhap}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
