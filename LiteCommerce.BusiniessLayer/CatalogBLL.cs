using LiteCommerce.DomainModels;
using LiteCommerce.DataLayer;
using System.Collections.Generic;

namespace LiteCommerce.BusiniessLayer
{
    /// <summary>
    ///
    /// </summary>
    public static class CatalogBLL
    {
        private static ISupplierDAL SupplierDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static IProductDAL ProductDB { get; set; }
        private static IOrderDAL OrderDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; } 

        /// <summary>
        /// Hàm này được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Innitialize(string connectionString)
        {
            SupplierDB = new DataLayer.SqlServer.SupplierDAL(connectionString);
        }

        /// <summary>
        /// Hien thi danh sach cac Supplier theo dang phan trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Tổng số trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns></returns>
        public static List<Supplier> Supplier_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return SupplierDB.List(page, pageSize, searchValue);
        }
        public static int Supplier_Count(string searchValue)
        {
            return SupplierDB.Count(searchValue);
        }
    }
}