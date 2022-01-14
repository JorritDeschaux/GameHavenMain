using GameHavenMain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Repositories
{
	public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
	{
		internal readonly ApplicationDbContext _context;

		public DbSet<TEntity> Table { get; private set; } = null;

		public GenericRepo(ApplicationDbContext context)
		{
			_context = context;
			Table = _context.Set<TEntity>();
		}


		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await Table.ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(object id)
		{
			return await Table.FindAsync(id);
		}

		public async Task CreateAsync(TEntity obj)
		{
			await Table.AddAsync(obj);

			await SaveAsync();
		}

		public async Task UpdateAsync(TEntity obj)
		{
			Table.Update(obj);

			await SaveAsync();
		}

		public async Task DeleteAsync(object id)
		{
			TEntity existing = await Table.FindAsync(id);
			Table.Remove(existing);

			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

	
	}
}
