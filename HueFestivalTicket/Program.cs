using HueFestivalTicket.Contexts;
using HueFestivalTicket.Helpers;
using HueFestivalTicket.Helpers.EmailBuilder;
using HueFestivalTicket.Middlewares;
using HueFestivalTicket.Repositories;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert bearer [token]",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    IConfiguration configuration = builder.Configuration;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "")),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrStaffOrManager", policy => policy.RequireRole(RoleAssignment.ADMIN, RoleAssignment.MANAGER, RoleAssignment.STAFF));
    options.AddPolicy("AdminOrManager", policy => policy.RequireRole(RoleAssignment.ADMIN, RoleAssignment.MANAGER));
    options.AddPolicy("ManagerOrStaff", policy => policy.RequireRole(RoleAssignment.STAFF, RoleAssignment.MANAGER));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(RoleAssignment.ADMIN));
    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole(RoleAssignment.MANAGER));
    options.AddPolicy("StaffPolicy", policy => policy.RequireRole(RoleAssignment.STAFF));
    options.AddPolicy("ReporterPolicy", policy => policy.RequireRole(RoleAssignment.REPORTER));
});

builder.Services.AddAutoMapper(typeof(AutoMapping).Assembly);

builder.Services.AddScoped<IRoleRepository, RoleRepository>()
        .AddScoped<IEventRepository, EventRepository>()
        .AddScoped<IImageEventRepository, ImageEventRepository>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IAccountRepository, AccountRepository>()
        .AddScoped<ITypeLocationRepository, TypeLocationRepository>()
        .AddScoped<ISupportRepository, SupportRepository>()
        .AddScoped<ILocationRepository, LocationRepository>()
        .AddScoped<IEventLocationRepository, EventLocationRepository>()
        .AddScoped<INewsRepository, NewsRepository>()
        .AddScoped<ITypeTicketRepository, TypeTicketRepository>()
        .AddScoped<IPriceTicketRepository, PriceTicketRepository>()
        .AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped<IInvoiceRepository, InvoiceRepository>()
        .AddScoped<ITicketRepository, TicketRepository>()
        .AddScoped<ICheckinRepository, CheckinRepository>()
        .AddScoped<ITokenRepository, TokenRepository>()
        .AddScoped<IVerifyRepository, VerifyRepository>()
        .AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<EmailBuilderWithCloudinary>();
builder.Services.AddScoped<EmailBuilder>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
