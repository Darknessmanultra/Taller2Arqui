using MassTransit;
using VideosMS.src.Mappings;
using VideosMS.src.Repositories;
using VideosMS.src.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Mapper));
builder.Services.AddSingleton<VideoRepository>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(Program).Assembly);

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(ctx);
        
    });
});
var app = builder.Build();
app.MapGrpcService<VideoGrpcService>();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();