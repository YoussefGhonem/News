namespace News_API.Models
{
    public class Photo
    {
        public int Id { get; set; }
       
        public string Url { get; set; }
        public bool IsMain { get; set; }
         public string PublicId { get; set; }
        public New New { get; set; }
        public int NewId { get; set; }

        
        
        
        
        
        
        
    }
}