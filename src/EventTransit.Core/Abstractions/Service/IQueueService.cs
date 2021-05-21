using System.Threading.Tasks;
using EventTransit.Core.Dto;

namespace EventTransit.Core.Abstractions.Service
{
    public interface IQueueService
    {
        Task<bool> PublishAsync(QueueRequestDto requestDto);
    }
}