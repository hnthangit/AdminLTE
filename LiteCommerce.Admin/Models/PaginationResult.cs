namespace LiteCommerce.Admin.Models
{
    public class PaginationResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                int pageCount = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                {
                    pageCount += 1;
                }
                return pageCount;
            }
        }

        public string searchValue { get; set; }
        public int categoryName { get; set; }
        public int companyName { get; set; }
    }
}