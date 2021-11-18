using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.HelperClasses
{
	public class PagedList<GameDTO> : List<GameDTO>
	{

		public int CurrentPage { get; private set; }
		
		public int TotalPages { get; private set; }
		
		public int PageSize { get; private set; }
		
		public int TotalCount{ get; private set; }


		public bool HasPrevious => CurrentPage > 1;

		public bool HasNext => CurrentPage < TotalPages;


		public PagedList(List<GameDTO> items, int count, int pageNumber, int pageSize)
		{

			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			
			AddRange(items);
		
		}

		
		public static PagedList<GameDTO> ToPagedList(IQueryable<GameDTO> source, int pageNumber, int pageSize)
		{

			var count = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			return new PagedList<GameDTO>(items, count, pageNumber, pageSize);

		}

	}
}
