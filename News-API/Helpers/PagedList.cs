using System.Linq;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace News_API.Helpers
{
    public class PagedList<T>:List<T>
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } // Total Of Items in DB
 
        public PagedList(List<T> Items,int count,int pageNumber,int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages=(int)Math.Ceiling(count/(double)pageSize);
            this.AddRange(Items);
        }

         //IQueryable is suitable for querying data from database
        // IEnumerable: database loads all of the productsexecutes select query on server side, load data in-memory on client side and then filter data.
        public static async Task <PagedList<T>> CreateAsync(IQueryable<T> source,int pageNumber , int pageSize){
            var count = await source.CountAsync();
            var items= await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items,count,pageNumber,pageSize);
        }


    }
}