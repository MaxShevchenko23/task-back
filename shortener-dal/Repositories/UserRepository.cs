using Microsoft.EntityFrameworkCore;
using url_shortener_server.Helpers.Models;
using url_shortener_server.shortener_dal.Entities;

namespace url_shortener_server.shortener_dal.Repositories
{
    public class UserRepository
    {
        private readonly ShortenerDbContext context;

        public UserRepository(ShortenerDbContext context)
        {
            this.context = context;
        }

        public async Task<User> CreateUser(AuthModel model)
        {

            User entity = new()
            {
                Email = model.Email,
                Password = model.Password,
                UserRoleId = 1
            };

            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync(true);

            var maxId = context
                .Users
                .Max(e => e.Id);

            return context
                .Users
                .First(e => e.Id == maxId);
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            return await context.Users.FirstOrDefaultAsync(e => e.Password == password && e.Email == email);
        }




    }
}
