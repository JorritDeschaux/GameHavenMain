using GameHavenMain.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IApplicationDbContext
	{
		public DbSet<UserDTO> Users { get; set; }
	}
}
