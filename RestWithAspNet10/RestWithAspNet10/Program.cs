using RestWithAspNet10.Configurations;
using RestWithAspNet10.Repository;
using RestWithAspNet10.Repository.Impl;
using RestWithAspNet10.Service;
using RestWithAspNet10.Service.Impl;

var builder = WebApplication.CreateBuilder(args);


builder.AddSerilogLogging();
// Add services to the container.

builder.Services.AddControllers().AddContentNegotiation();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddEvolveConfiguration(builder.Configuration,builder.Environment);

builder.Services.AddScoped<IBookService, BookServiceImpl>();
builder.Services.AddScoped<IPersonService,PersonServiceImpl>();
builder.Services.AddScoped<PersonServiceImplV2>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
