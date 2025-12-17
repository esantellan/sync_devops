using Metrics.Api.Configuration;
using Metrics.Application;
using Metrics.Application.Interfaces;
using Metrics.Infrastructure;
using Metrics.Infrastructure.Queues;
using Metrics.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(_ => builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

// Configuration
builder.Services.Configure<RecurringTaskSettings>(builder.Configuration.GetSection("RecurringTask"));
builder.Services.Configure<AzureDevOpsSettings>(builder.Configuration.GetSection("AzureDevOps"));

// Background Task Services
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>(); // In-memory queue
builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddHostedService<ScheduledHostedService>();

// Database-backed Task Services
builder.Services.AddScoped<IBackgroundTaskService, BackgroundTaskService>();
builder.Services.AddSingleton<IBackgroundTaskHandlerResolver, BackgroundTaskHandlerResolver>();

// Register all background task handlers
builder.Services.Scan(scan => scan
    .FromApplicationDependencies()
    .AddClasses(classes => classes.AssignableTo<IBackgroundTaskHandler>())
    .AsImplementedInterfaces()
    .WithScopedLifetime());

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
