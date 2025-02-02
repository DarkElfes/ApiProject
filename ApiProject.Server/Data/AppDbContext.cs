using ApiProject.Server.Users;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Server.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}