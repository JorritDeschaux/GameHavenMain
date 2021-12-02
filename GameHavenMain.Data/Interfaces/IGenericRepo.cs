using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IGenericRepo<TEntity> where TEntity : class
	{

		Task<IEnumerable<TEntity>> GetAll();
		Task<TEntity> GetById(object id);
		Task Create(TEntity obj);
		Task Update(TEntity obj);
		Task Delete(object id);
		Task Save();

	}
}
