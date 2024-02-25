using System.Collections.Generic;

namespace AbcParcel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Tracking> Trackings { get; set; }
    }
}
