using GlizzyGlobal;
using MortgageCalculatorLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the currency conversion services.
builder.Services.AddSingleton<ICurrencyRateProvider, CurrencyServiceRateProvider>();
builder.Services.AddSingleton<ICurrencyConverter, CurrencyConverter>();

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

app.UseAuthorization();

<<<<<<< HEAD
app.UseRequestIP();
=======
app.UseRequestIP(); 
>>>>>>> Added currency conversion to API

app.MapControllers();

app.Run();
