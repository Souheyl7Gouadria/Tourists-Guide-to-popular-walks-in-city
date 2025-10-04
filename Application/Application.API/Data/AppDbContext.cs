using Application.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions){}

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data for Difficulty
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("a2f4c3d8-1e5b-4c3a-9f1e-1b2c3d4e5f60"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("b3f5d4e9-2f6c-5d4b-af2f-2c3d4e5f6a70"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("c4a6e5f0-3a7d-6e5c-1b3a-3d4e5f6a7b80"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            
            // Seed data for Region
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("d5b7f6a1-4b8e-7f6d-2c4b-4e5f6a7b8c90"),
                    Code = "US-W",
                    Name = "West Coast",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("e6c8a7b2-5c9f-8a7e-3d5c-5f6a7b8c9d01"),
                    Code = "US-E",
                    Name = "East Coast",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("f7d9b8c3-6d0a-9b8f-4e6d-6a7b8c9d0e12"),
                    Code = "US-M",
                    Name = "Midwest",
                    RegionImageUrl = null
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
