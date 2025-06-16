using MassTransit;
using Interacciones.src.Contracts;

namespace Interacciones.src.Consumers
{
    public class LikesRequestConsumer : IConsumer<LikesRequest>
    {
        public async Task Consume(ConsumeContext<LikesRequest> context)
        {
            var LikesId = context.Message.VideoId;

            var likes = await GetLikes(LikesId);

            await context.RespondAsync(new LikesResponse
            {
                VideoId = LikesId,
                Likes = likes.Likes
            });
        }

        private Task<LikesResponse> GetLikes(int videoId)
        {
            return Task.FromResult(new LikesResponse
            {
                VideoId = videoId,
                Likes = 42,
            });
        }
    }
}