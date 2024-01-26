using EmailAPI;
using EmailAPI.Data;
using EmailAPI.Models;
using EmailAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IEmailSender, EmailSender>();



builder.Services.AddDbContext<LaBenViDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<LaBenViDbContext>()
    .AddDefaultTokenProviders();


    
//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddTransient<IEmailSender, EmailSender>();
//builder.Services.AddTransient<IMessengerService, MessengerService>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Fast api",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
