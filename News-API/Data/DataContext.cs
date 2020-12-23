using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using News_API.Models;

namespace News_API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<New> News{ get; set; }
            public DbSet<Photo> Photos{ get; set; }


    }
}