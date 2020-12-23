using System.Collections.Generic;
using System.Threading.Tasks;
using News_API.Helpers;
using News_API.Models;

namespace News_API.Data
{
    public interface INewsRepo
    {
        void Add <T> (T entity) where T:class;// Add Any entity
        void Delete <T> (T entity) where T:class;
        Task <bool> SaveAll();
        Task<New> GetNews(int id);
        Task<PagedList<New>> GetAllNews(NewsParams newsParams);

        //photos
        Task <Photo> GetPhotoForNews(int id);
        Task <Photo>GetMainPhotoForUser(int newsId);
    }
}