using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News_API.Helpers;
using News_API.Models;

namespace News_API.Data
{
    public class NewsRepo : INewsRepo
    {
        private readonly DataContext _context;

        public NewsRepo(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<PagedList<New>> GetAllNews(NewsParams newsParams)
        {
            var news =  _context.News.Include(x => x.Photos);
            return await PagedList<New>.CreateAsync(news,newsParams.PageNumber,newsParams.PageSize);
           
        }

        public async Task<Photo> GetMainPhotoForUser(int newsId)
        {
            return await _context.Photos.Where(i=>i.NewId==newsId).FirstOrDefaultAsync(p=>p.IsMain);
        }

        public async Task<New> GetNews(int id)
        {
            var news = await _context.News.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
            return news;
        }

        public async Task<Photo> GetPhotoForNews(int id)
        {
           var photo=await _context.Photos.FirstOrDefaultAsync(u=>u.Id==id);
           return photo;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}