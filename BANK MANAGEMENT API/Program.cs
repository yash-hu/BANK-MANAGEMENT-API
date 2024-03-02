using BANK_MANAGEMENT_API.Managers;
using BANK_MANAGEMENT_API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
  
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2",new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description="Standard Authorization header using the Bearer scheme",
        In=ParameterLocation.Header,
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(MapperProfile));
//builder.Services.AddCors();

//DATABASE CONNECTION...
builder.Services.AddDbContext<BankManagementContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));


//add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//adding DI
builder.Services.AddTransient<ICustomersManager, CustomersManager>();
builder.Services.AddTransient<IAccountsManager, AccountsManager>();
builder.Services.AddTransient<ITransactionsManager, TransactionsManager>();
builder.Services.AddTransient<IInterestManager, InterestManager>();
builder.Services.AddTransient<IAuthManager, AuthManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//using authentication
app.UseAuthentication();

app.UseAuthorization();

//adding cors ...




app.MapControllers();

app.Run();
