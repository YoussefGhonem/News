using System.Collections.Generic;
using System.Linq;
using News_API.Models;
using Newtonsoft.Json;

namespace News_API.Data
{
    public class TrialNews
    {
        private readonly DataContext _context;

        public TrialNews(DataContext context)
        {
            _context = context;
        }
        public void NewTrialNews()
        {
            if (!_context.News.Any())
            {
                var NewsData = System.IO.File.ReadAllText("Data/TrialNewsData.json");
                var News = JsonConvert.DeserializeObject<List<New>>(NewsData);
                foreach (var user in News)
                {
                    _context.Add(user);
                    _context.SaveChanges();
                }
            }

        }
    }
}