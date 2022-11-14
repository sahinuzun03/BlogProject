using KD12BlogProject.DataAccess.EntityFramework.EntityTypeConfiguration;
using KD12BlogProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.DataAccess.EntityFramework.Context
{
    public class KD12BlogDbContext : IdentityDbContext<AppUser>
    {
        //Identity kullandığımız için IdentityDbContext üzerinden DbContext'imizi oluşturuyoruz.
        public KD12BlogDbContext(DbContextOptions<KD12BlogDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AuthorConfig());
            builder.ApplyConfiguration(new GenreConfig());
            builder.ApplyConfiguration(new PostConfig());
            base.OnModelCreating(builder);
        }
    }
}
