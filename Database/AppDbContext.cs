using NewApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            builder.Entity<Category>().ToTable("Category");


            //builder.Entity<Category>().HasOne(n => n.Owner).WithMany(u => u.Notes).HasForeignKey(n => n.UserId);
            //builder.Entity<App>().HasOne(n => n.User).WithMany(u => u.Apps).HasForeignKey(n => n.UserId);
            //builder.Entity<RolePermission>().HasOne(rp => rp.Role).WithMany(u => u.RolePermissions).HasForeignKey(n => n.RoleId);

            //builder.Entity<UserPartner>().HasKey(t => new { t.UserId, t.PartnerId });
            //builder.Entity<UserPartner>().HasOne(ur => ur.User).WithMany(u => u.UserPartners).HasForeignKey(ur => ur.UserId);
            //builder.Entity<UserPartner>().HasOne(ur => ur.Partner).WithMany(r => r.UserPartners).HasForeignKey(ur => ur.PartnerId);
        }
    }
}
