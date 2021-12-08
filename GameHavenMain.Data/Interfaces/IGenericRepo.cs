using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IGenericRepo<TEntity> where TEntity : class
	{

		Task<IEnumerable<TEntity>> GetAllAsync();

		Task<TEntity> GetByIdAsync(object id);

		Task CreateAsync(TEntity obj);

		Task UpdateAsync(TEntity obj);

		Task DeleteAsync(object id);

		Task SaveAsync();

	}
}
