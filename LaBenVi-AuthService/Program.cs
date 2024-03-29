using AutoMapper;
using LaBenVi_AuthService;
using LaBenVi_AuthService.Data;
using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Service;
using LaBenVi_AuthService.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAuthService, AuthService>();


builder.Services.AddDbContext<LaBenViDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MappingConfig.MapsReg().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.Configure<IdentityDefaultOptions>(builder.Configuration.GetSection("IdentityDefaultOptions"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IMessengerService, MailgunMessengerService>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<LaBenViDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IRepository, Repository>();
//builder.Services.AddScoped<IMessageService, MessageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
IncludeMigration();
app.Run();


void IncludeMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _context = scope.ServiceProvider.GetRequiredService<LaBenViDbContext>();

        if (_context.Database.GetPendingMigrations().Count() > 0)
        {
            _context.Database.Migrate();
        }
    }
}