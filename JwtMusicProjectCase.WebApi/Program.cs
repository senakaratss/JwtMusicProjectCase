using FluentValidation;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Concrete;
using JwtMusicProjectCase.Business.Dtos.AuthDtos;
using JwtMusicProjectCase.Business.Mapping;
using JwtMusicProjectCase.Business.ValidationRules;
using JwtMusicProjectCase.DataAccess.Context;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.DataAccess.Repositories.Concrete.EntityFramework;
using JwtMusicProjectCase.Entity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.ML;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<MusicContext>()
    .AddDefaultTokenProviders();


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});


builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddDbContext<MusicContext>();

builder.Services.AddScoped<IRecommendationService, RecommendationManager>();
builder.Services.AddSingleton<MLContext>();

builder.Services.AddScoped<IRoleService, RoleManager>();

builder.Services.AddScoped<IGenreDal, EfGenreDal>();
builder.Services.AddScoped<IGenreService, GenreManager>();

builder.Services.AddScoped<IPackageDal, EfPackageDal>();
builder.Services.AddScoped<IPackageService, PackageManager>();

builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IJwtService, JwtManager>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();

builder.Services.AddScoped<IArtistDal, EfArtistDal>();
builder.Services.AddScoped<IArtistService, ArtistManager>();

builder.Services.AddScoped<ISongDal, EfSongDal>();
builder.Services.AddScoped<ISongService, SongManager>();

builder.Services.AddScoped<IListeningHistoryDal, EfListeningHistoryDal>();
builder.Services.AddScoped<IListeningHistoryService, ListeningHistoryManager>();

builder.Services.AddScoped<IPlaylistDal, EfPlaylistDal>();
builder.Services.AddScoped<IPlaylistService, PlaylistManager>();

builder.Services.AddScoped<IPlaylistSongDal, EfPlaylistSongDal>();
builder.Services.AddScoped<IPlaylistSongService, PlaylistSongManager>();

builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddScoped<IDashboardDal, EfDashboardManager>();
builder.Services.AddScoped<IDashboardService, DashboardManager>();


builder.Services.AddControllers();
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Audio")),
    RequestPath = "/Audio"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
