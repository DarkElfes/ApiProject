using ApiProject.Domain.PhoneCases;
using ApiProject.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<PhoneCase> PhoneCases { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            CreatedAt = seedDate,
            Email = "admin@caseshop.com",
            Username = "Admin",
            Role = "Admin",
            PasswordHash = "AQAAAAIAAYagAAAAEOMiV4vZOmimSUP3whM3EdotmdNNlNiPUySw18aeStR3+nVzltjeGeoRqT93FbCtVw==",
        });

        modelBuilder.Entity<PhoneCase>().HasData(
              new()
              {
                  Id = 1,
                  CreatedAt = seedDate,
                  Name = "Shockproof Armor Matte Case",
                  Model = PhoneModelType.Iphone16,
                  Color = "Green",
                  ImgUrl = "https://ae-pic-a1.aliexpress-media.com/kf/S0082d2ca5c48496c8df2d0b69a3edaddq.jpg_960x960q75.jpg_.avif",
                  Sold = 0,
                  Price = 81,
                  Stock = 10
              },
              new()
              {
                  Id = 2,
                  CreatedAt = seedDate,
                  Name = "Luxury Liquid Silicone Phone Case ",
                  Model = PhoneModelType.Iphone16ProMax,
                  Color = "Black",
                  ImgUrl = "https://ae-pic-a1.aliexpress-media.com/kf/S10f49ffaa7ea434ca62700fa04444087l.jpg_960x960q75.jpg_.avif",
                  Sold = 0,
                  Price = 92,
                  Stock = 10
              },
              new()
              {
                  Id = 3,
                  CreatedAt = seedDate,
                  Name = "Luxury Liquid Silicone For Magsafe Phone Case",
                  Model = PhoneModelType.Iphone16ProMax,
                  Color = "Red Brown",
                  ImgUrl = "https://ae-pic-a1.aliexpress-media.com/kf/S4fc26c62c8104b288029a0852d1117cc6.jpg_960x960q75.jpg_.avif",
                  Sold = 0,
                  Price = 255,
                  Stock = 10
              }
        );

    }
}