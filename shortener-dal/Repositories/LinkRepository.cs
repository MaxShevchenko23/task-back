using Microsoft.EntityFrameworkCore;
using url_shortener_server.Helpers;
using url_shortener_server.shortener_dal;
using url_shortener_server.shortener_dal.Entities;

namespace url_shortener_server.Controllers
{
    public class LinkRepository
    {
        private readonly ShortenerDbContext context;

        public LinkRepository(ShortenerDbContext context)
        {
            this.context = context;
        }

        public async Task<Link?> GetInfo(string code)
        {
            var entity = await context.Links.Include(e => e.User).FirstOrDefaultAsync(e => e.Shortened == code);

            if (entity != null)
            {
                entity.User.Links = new List<Link>();
            }

            return entity;
        }
        public async Task<string?> GetSourceLink(string shortened)
        {
            var entity = await context.Links.FirstOrDefaultAsync(e => e.Shortened == shortened);
            return entity.Source;
        }

        //temp
        public async Task<IEnumerable<Link>> GetAllLinks(int? userId)
        {
            return userId.HasValue ? await context.Links.Where(e => e.UserId == userId).ToListAsync() :
                await context.Links.ToListAsync();
        }


        public async Task<string?> GetShortenedLink(string source)
        {
            var entity = await context.Links.FirstOrDefaultAsync(e => e.Source == source);
            return entity == null ? null : entity.Shortened;
        }

        public async Task<Link?> GetFullInfo(string shortened)
        {
            return await context.Links
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Shortened == shortened);
        }

        public async Task<string> CreateShortenedLink(string source, int? userId)
        {
            string shortened = source.GetCode();

            Link link = new()
            {
                CreatedAt = DateTime.Now,
                Shortened = shortened,
                Source = source,
                UserId = userId,
                Views = 0,
            };
            await context.Links.AddAsync(link);
            await context.SaveChangesAsync(true);

            return shortened;

        }
    }
}