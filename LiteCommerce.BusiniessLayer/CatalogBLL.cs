using LiteCommerce.DataLayer;
using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.BusiniessLayer
{
    /// <summary>
    ///
    /// </summary>
    public static class CatalogBLL
    {
        private static ISupplierDAL SupplierDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }
        private static IProductDAL ProductDB { get; set; }
        private static IOrderDAL OrderDB { get; set; }

        /// <summary>
        /// Hàm này được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Innitialize(string connectionString)
        {
            SupplierDB = new DataLayer.SqlServer.SupplierDAL(connectionString);
            CustomerDB = new DataLayer.SqlServer.CustomerDAL(connectionString);
            ShipperDB = new DataLayer.SqlServer.ShipperDAL(connectionString);
            CategoryDB = new DataLayer.SqlServer.CategoryDAL(connectionString);
        }

        #region Supplier

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

        /// <summary>
        /// Hàm đếm số lượng truy vấn tìm được trong database
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns>Số lượng truy vấn tìm được</returns>
        public static int Supplier_Count(string searchValue)
        {
            return SupplierDB.Count(searchValue);
        }

        /// <summary>
        /// Hàm lấy thông tin của một Supplier
        /// </summary>
        /// <param name="supplierID">ID của supplier cần lấy</param>
        /// <returns>Thông tin toàn bộ Supplier theo ID</returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }

        /// <summary>
        /// Thêm một Supplier mới
        /// </summary>
        /// <param name="data">tất cả các dữ liệu của 1 supplier</param>
        /// <returns></returns>
        public static int Supplier_Add(Supplier data)
        {
            return SupplierDB.Add(data);
        }

        /// <summary>
        /// Cập nhật một supplier mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Supplier_Update(Supplier data)
        {
            return SupplierDB.Update(data);
        }

        /// <summary>
        /// Xóa một hoặc nhiều supplier
        /// </summary>
        /// <param name="supplierIDs">Mảng các supplier ID</param>
        /// <returns></returns>
        public static bool Supplier_Delete(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }

        #endregion Supplier

        #region Customer

        /// <summary>
        /// Danh sách các customer
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> Customer_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return CustomerDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        /// Đếm số lượng các bản ghi tìm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Customer_Count(string searchValue)
        {
            return CustomerDB.Count(searchValue);
        }

        public static Customer GetCustomer(string customerID)
        {
            return CustomerDB.Get(customerID);
        }

        /// <summary>
        /// Thêm một Supplier mới
        /// </summary>
        /// <param name="data">tất cả các dữ liệu của 1 supplier</param>
        /// <returns></returns>
        public static int Customer_Add(Customer data)
        {
            return CustomerDB.Add(data);
        }

        /// <summary>
        /// Cập nhật một supplier mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Customer_Update(Customer data)
        {
            return CustomerDB.Update(data);
        }

        /// <summary>
        /// Xóa một hoặc nhiều supplier
        /// </summary>
        /// <param name="supplierIDs">Mảng các supplier ID</param>
        /// <returns></returns>
        public static bool Customer_Delete(int[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }

        #endregion Customer

        #region Shipper

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> Shipper_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 9999999;
            return ShipperDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Shipper_Count(string searchValue)
        {
            return ShipperDB.Count(searchValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Shipper_Add(Shipper data)
        {
            return ShipperDB.Add(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Shipper_Update(Shipper data)
        {
            return ShipperDB.Update(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipperIDs"></param>
        /// <returns></returns>
        public static bool Shipper_Delete(int[] shipperIDs)
        {
            return ShipperDB.Delete(shipperIDs);
        }

        #endregion Shipper

        #region Category

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> Category_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 9999999;
            return CategoryDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Category_Count(string searchValue)
        {
            return CategoryDB.Count(searchValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryId)
        {
            return CategoryDB.Get(categoryId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Category_Add(Category data)
        {
            return CategoryDB.Add(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Category_Update(Category data)
        {
            return CategoryDB.Update(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        public static bool Category_Delete(int[] categoryIDs)
        {
            return CategoryDB.Delete(categoryIDs);
        }

        #endregion Category
    }
}