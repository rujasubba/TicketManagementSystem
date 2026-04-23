using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;
using TicketManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApiConnection")));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";

    options.ExpireTimeSpan = TimeSpan.FromMinutes(500);
    options.SlidingExpiration = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IcategoryService, CategoryService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ticket}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
