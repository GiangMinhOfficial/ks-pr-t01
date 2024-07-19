using Microsoft.EntityFrameworkCore;
using PFR_NEFC_KS_Practice_T01.Domain.Service;
using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Context;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository.IRepository;
using PFR_NEFC_KS_Practice_T01.Infrastructure.UnitOfWork;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(PFR_NEFC_KS_Practice_T01.Domain.Mapper.AutoMapperProfile));
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
