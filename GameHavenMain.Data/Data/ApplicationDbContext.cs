
using GameHavenMain.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserDTO> User { get; set; }

        public DbSet<ReviewDTO> UserReview { get; set; }

        public DbSet<LinkedDTO> GameReview { get; set; }

        public DbSet<ProfileDTO> ProfilePage { get; set; }

        public DbSet<LinkedDTO> FavGame { get; set; }

    }
}
