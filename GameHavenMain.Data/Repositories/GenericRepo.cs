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
		public ApplicationDbContext _context;
		public DbSet<TEntity> _table = null;


		public GenericRepo(ApplicationDbContext context)
		{
			_context = context;
			_table = _context.Set<TEntity>();
		}


		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _table.ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(object id)
		{
			return await _table.FindAsync(id);
		}

		public async Task CreateAsync(TEntity obj)
		{
			await _table.AddAsync(obj);

			await SaveAsync();
		}

		public async Task UpdateAsync(TEntity obj)
		{
			_table.Update(obj);

			await SaveAsync();
		}

		public async Task DeleteAsync(object id)
		{
			TEntity existing = await _table.FindAsync(id);
			_table.Remove(existing);

			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

	
	}
}
