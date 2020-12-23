namespace News_API.Helpers
{
    public class NewsParams
    {
                // علشان احدد اقصى عدد لظهور اليوزرز 
        private const int MaxPageSize = 50;// هو ده العدد اللي مش هيتخطاه
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value>MaxPageSize)?MaxPageSize:value;}
        }

    }
}