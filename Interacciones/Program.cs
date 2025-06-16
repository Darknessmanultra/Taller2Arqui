var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<RatingRequestConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("Likes-queue", e =>
        {
            e.ConfigureConsumer<RatingRequestConsumer>(ctx);
        });
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();