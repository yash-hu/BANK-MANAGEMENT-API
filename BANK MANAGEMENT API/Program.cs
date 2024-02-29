using BANK_MANAGEMENT_API.Managers;
using BANK_MANAGEMENT_API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperProfile));

//DATABASE CONNECTION...
builder.Services.AddDbContext<BankManagementContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));


//adding DI
builder.Services.AddTransient<ICustomersManager, CustomersManager>();
builder.Services.AddTransient<IAccountsManager, AccountsManager>();
builder.Services.AddTransient<ITransactionsManager, TransactionsManager>();
builder.Services.AddTransient<IInterestManager, InterestManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//adding cors ...
app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();

app.Run();
