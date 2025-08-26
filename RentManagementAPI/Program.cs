using LeaseManagement.Infrastructure.MongoDB.Data;
using LeaseManagement.Infrastructure.MongoDB.Implementation;
using RentManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar serviços
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<MotorcycleImplementation>();
builder.Services.AddSingleton<LeaseImplementation>();
builder.Services.AddSingleton<DeliveryManImplementation>();
builder.Services.AddHostedService<NotificationConsumerService>();

// Add services to the container.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
