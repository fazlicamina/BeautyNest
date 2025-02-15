using BeautyNest;
using BeautyNest.Data;
using BeautyNest.Models.Domain;
using BeautyNest.Repositories.Implementation;
using BeautyNest.Repositories.Interface;
using BeautyNest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeautyNestConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeautyNestConnectionString")));

builder.Services.AddScoped<OmiljeniSalonService>();
builder.Services.AddScoped<RecenzijaService>();

builder.Services.AddScoped<ReservationService>();

builder.Services.AddScoped<EmailService>();

builder.Services.Configure<BeautyNest.Models.Domain.EmailSettings>(
    builder.Configuration.GetSection("Email"));

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(3); // Token traje 3 sata
});


builder.Services.AddScoped<ISalonRepository, SalonRepository>();
builder.Services.AddScoped<IKategorijaRepository, KategorijaRepository>();
builder.Services.AddScoped<IKategorijaUslugeRepository, KategorijaUslugeRepository>();
builder.Services.AddScoped<IUslugaRepository, UslugaRepository>();
builder.Services.AddScoped<IGradRepository, GradRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});


//logovanje
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var isDevelopment = app.Environment.IsDevelopment();
    ApplicationDbContext.Initialize(services, isDevelopment); // Poziv metode Initialize
}

app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200") 
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials(); 
});




app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
