using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
	{
	}
	public DbSet<TeamMember> TeamMembers { get; set; } = null!;
}
