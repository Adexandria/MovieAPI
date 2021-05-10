using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
