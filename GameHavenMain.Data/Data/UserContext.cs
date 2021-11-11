
using GameHavenMain.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Data
{
	public class UserContext : DbContext
	{
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<UserDTO> User { get; set; }

    }
}
