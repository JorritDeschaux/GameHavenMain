using GameHavenMain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Data
{
	interface IApplicationDbContext
	{
		public DbSet<Test> Test { get; set; }
	}
}
