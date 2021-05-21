using MoviesAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MoviesAPI.UserService
{
    public class IdentityDb : IdentityDbContext<Users>
    {
        public IdentityDb(DbContextOptions<IdentityDb> options) : base(options)
        {

        }
        public DbSet<Users> User { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
