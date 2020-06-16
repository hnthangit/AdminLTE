namespace LiteCommerce.Admin.Models
{
    public class PaginationResult
    {
        /// <summary>
        /// Trang
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Kích thước trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Số lượng mỗi dòng của trang
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Số lượng trang
        /// </summary>
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

        /// <summary>
        /// Giá trị cần tìm kiếm
        /// </summary>
        public string searchValue { get; set; }

        /// <summary>
        /// CategoryID
        /// </summary>
        public int categoryName { get; set; }

        /// <summary>
        /// SupplierID
        /// </summary>
        public int companyName { get; set; }

        /// <summary>
        /// CustomerId
        /// </summary>
        public string customerId { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public int employeeId { get; set; }

        /// <summary>
        /// ShipperID
        /// </summary>
        public int shipperId { get; set; }

        public string country { get; set; }
    }
}