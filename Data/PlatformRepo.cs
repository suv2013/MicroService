using PlatformService.Models;
using System.Linq;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public Platform GetPlatformById(int? id)
        {
            if (id != null && id > 0)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return _context.Platforms.FirstOrDefault(p => p.Id == id);
#pragma warning restore CS8603 // Possible null reference return.

            }

            return (Platform)Enumerable.Empty<Platform>();
        }

        public IEnumerable<Platform> GetPlatforms()
        { 
            return _context.Platforms.ToList();            
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePlatform(Platform platform)
        {
            throw new NotImplementedException();
        }
    }
}
