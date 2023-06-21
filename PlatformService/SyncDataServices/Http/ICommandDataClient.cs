using System.Threading.Tasks;
using PlatformService.DTOS;

namespace PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDTO plat); 
    }
}