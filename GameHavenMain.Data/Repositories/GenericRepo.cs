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


		public async Task<IEnumerable<TEntity>> GetAll()
		{
			return await _table.ToListAsync();
		}

		public async Task<TEntity> GetById(object id)
		{
			return await _table.FindAsync(id);
		}

		public async Task<TEntity> GetByName(object name)
		{
			return await _table.FindAsync(name);
		}

		public async Task Create(TEntity obj)
		{
			await _table.AddAsync(obj);

			await Save();
		}

		public async Task Update(TEntity obj)
		{
			_table.Update(obj);

			await Save();
		}

		public async Task Delete(object id)
		{
			TEntity existing = await _table.FindAsync(id);
			_table.Remove(existing);

			await Save();
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

	
	}
}
