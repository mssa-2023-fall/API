using GlizzyGlobal;
using GlizzyGlobal.Controllers;
using MortgageCalculatorLibrary;

var builder = WebApplication.CreateBuilder(args);

//Adding services to the container.
builder.Services.AddControllers();

//Waiting for MortgageCalculatorLibrary 1.1 Release, build error for now.
builder.Services.AddSingleton<ICurrencyRateProvider, CurrencyServiceRateProvider>();
builder.Services.AddSingleton<ICurrencyConverter, CurrencyConverter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle //NO
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

app.UseRequestIP(); 

app.MapControllers();

app.Run();
