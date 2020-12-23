namespace News_API.Helpers
{
        //client اللي هيرجع لل  response header داخل ال  Paginationتمرير المعلومات الخاصه بال 

    public class PaginationHeader
    {
        // هي دي الاترييوت اللي هستقبلها ف الانجلر
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public PaginationHeader(int currentPage,int itemsPerPage,int TotalItems,int totalPages)
        {
            this.CurrentPage=currentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItems = TotalItems;
            this.TotalPages = totalPages;
        }
    }
}