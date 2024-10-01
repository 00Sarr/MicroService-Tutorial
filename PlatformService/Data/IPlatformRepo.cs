using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        void CreatePlatform(Platform platform);
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
    }
}
