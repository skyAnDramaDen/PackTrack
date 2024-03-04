using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbcParcel.Data
{
    public class ApplicationDbContext : IdentityDbContext<Applicationuser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Tracking> Trackings { get; set; }
        public DbSet<Applicationuser> Applicationusers { get; set; }
    }
}
