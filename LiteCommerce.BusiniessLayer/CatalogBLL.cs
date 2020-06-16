using LiteCommerce.DataLayer;
using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.BusiniessLayer
{
    /// <summary>
    /// Các mục lục
    /// </summary>
    public static class CatalogBLL
    {
        #region Declare variable

        private static ISupplierDAL SupplierDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }
        private static IProductDAL ProductDB { get; set; }
        private static IProductAttributeDAL ProductAttributeDB { get; set; }
        private static IOrderDAL OrderDB { get; set; }
        private static ICountryDAL CountryDB { get; set; }

        private static IOrderDetailDAL OrderDetailDB { get; set; }

        #endregion Declare variable

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
            ProductDB = new DataLayer.SqlServer.ProductDAL(connectionString);
            ProductAttributeDB = new DataLayer.SqlServer.ProductAttributeDAL(connectionString);
            OrderDB = new DataLayer.SqlServer.OrderDAL(connectionString);
            CountryDB = new DataLayer.SqlServer.CountryDAL(connectionString);
            OrderDetailDB = new DataLayer.SqlServer.OrderDetailDAL(connectionString);
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

        public static List<Supplier> Suppliers_CompanyName()
        {
            return SupplierDB.List();
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
        /// Lấy thông tin của một khách hàng
        /// </summary>
        /// <returns></returns>
        public static List<Customer> Customer_List()
        {
            return CustomerDB.List();
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
        /// Danh sách các người giao hàng theo phân trang
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
        /// Thông tin của người giao hàng
        /// </summary>
        /// <returns></returns>
        public static List<Shipper> Shipper_List()
        {
            return ShipperDB.List();
        }

        /// <summary>
        /// Đếm số lượng truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Shipper_Count(string searchValue)
        {
            return ShipperDB.Count(searchValue);
        }

        /// <summary>
        /// Lấy thông tin của một người giao hàng theo ID
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }

        /// <summary>
        /// Thêm thông tin của một người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Shipper_Add(Shipper data)
        {
            return ShipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của một người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Shipper_Update(Shipper data)
        {
            return ShipperDB.Update(data);
        }

        /// <summary>
        /// Xóa một hoặc nhiều người giao hàng theo ID
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
        /// Danh sách thông tin các thể loại theo phân trang
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
        /// Đếm số lượng các truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Category_Count(string searchValue)
        {
            return CategoryDB.Count(searchValue);
        }

        /// <summary>
        /// Lấy thông tin của một thể loại theo ID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryId)
        {
            return CategoryDB.Get(categoryId);
        }

        /// <summary>
        /// Thêm một thể loại
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Category_Add(Category data)
        {
            return CategoryDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của một thể loại
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Category_Update(Category data)
        {
            return CategoryDB.Update(data);
        }

        /// <summary>
        /// Xóa một hoặc nhiều thể loại theo ID
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        public static bool Category_Delete(int[] categoryIDs)
        {
            return CategoryDB.Delete(categoryIDs);
        }

        /// <summary>
        /// Lấy tên thể loại
        /// </summary>
        /// <returns></returns>
        public static List<Category> Category_CategoryName()
        {
            return CategoryDB.List();
        }

        #endregion Category

        #region Product

        /// <summary>
        /// Danh sách các sản phẩm theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Product> Product_List(int page, int pageSize, string searchValue, int categoryName, int companyName)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return ProductDB.List(page, pageSize, searchValue, categoryName, companyName);
        }

        /// <summary>
        /// Đếm số lượng kết quả tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Product_Count(string searchValue, int categoryName, int companyName)
        {
            return ProductDB.Count(searchValue, categoryName, companyName);
        }

        /// <summary>
        /// Lấy thông tin của một sản phẩm theo ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Product GetProduct(int productId)
        {
            return ProductDB.Get(productId);
        }

        /// <summary>
        /// Bổ sung thông tin một sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Product_Add(Product data)
        {
            return ProductDB.Add(data);
        }

        /// <summary>
        /// Xóa một hoặc nhiều sản phẩm theo ID
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        public static bool Product_Delete(int[] productIDs)
        {
            return ProductDB.Delete(productIDs);
        }

        /// <summary>
        /// Cập nhật thông tin của một sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Product_Update(Product data)
        {
            return ProductDB.Update(data);
        }

        #endregion Product

        #region ProductAttribute

        /// <summary>
        /// Dánh sách thuộc tính của mốt sản phẩm theo phân trang
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductAttributes> ProductAttribute_List(int productID)
        {
            return ProductAttributeDB.List(productID);
        }

        /// <summary>
        /// Thêm thông tin một thuộc tính cho sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ProductAttribute_Add(ProductAttributes data)
        {
            return ProductAttributeDB.Add(data);
        }

        /// <summary>
        /// Lấy thông tin của một thuộc tính trong sản phẩm
        /// </summary>
        /// <param name="productAttributeId"></param>
        /// <returns></returns>
        public static ProductAttributes GetProductAttributes(int productAttributeId)
        {
            return ProductAttributeDB.Get(productAttributeId);
        }

        /// <summary>
        /// Xóa một hoặc nhiều thuộc tính của sản phẩm
        /// </summary>
        /// <param name="productAttributesIDs"></param>
        /// <returns></returns>
        public static bool ProductAttributes_Delete(int[] productAttributesIDs)
        {
            return ProductAttributeDB.Delete(productAttributesIDs);
        }

        /// <summary>
        /// Cập nhật thuộc tính cho sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ProductAttributes_Update(ProductAttributes data)
        {
            return ProductAttributeDB.Update(data);
        }

        #endregion ProductAttribute

        #region Country

        /// <summary>
        /// Danh sách các đất nước trong database
        /// </summary>
        /// <returns></returns>
        public static List<Country> Country_List()
        {
            return CountryDB.ListCountries();
        }

        #endregion Country

        #region Order

        /// <summary>
        /// Thêm một đơn đặt hàng
        /// </summary>
        /// <param name="data">Dữ liệu của một đơn hàng</param>
        /// <returns>Mã đơn hàng sau khi thêm</returns>
        public static int Order_Add(Order data)
        {
            return OrderDB.Add(data);
        }

        /// <summary>
        /// Cập nhật một đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Order_Update(Order data)
        {
            return OrderDB.Update(data);
        }

        /// <summary>
        /// Xóa một hay nhiều đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public static bool Order_Delete(int[] orderIDs)
        {
            return OrderDB.Delete(orderIDs);
        }

        /// <summary>
        /// Lấy thông tin của một đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderID">Mã đơn hàng</param>
        /// <returns></returns>
        public static Order GetOrder(int orderID)
        {
            return OrderDB.Get(orderID);
        }

        /// <summary>
        /// Danh sách các đơn hàng theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="customerId"></param>
        /// <param name="employeeId"></param>
        /// <param name="shipperId"></param>
        /// <returns></returns>
        public static List<Order> Order_List(int page, int pageSize, string searchValue, string customerId, int employeeId, int shipperId)
        {
            return OrderDB.List(page, pageSize, searchValue, customerId, employeeId, shipperId);
        }

        /// <summary>
        /// Đếm số truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <param name="customerId">Mã khách hàng</param>
        /// <param name="employeeId">Mã nhân viên</param>
        /// <param name="shipperId">Mã người giao hàng</param>
        /// <returns>Số kết quả tìm được</returns>
        public static int Order_Count(string searchValue, string customerId, int employeeId, int shipperId)
        {
            return OrderDB.Count(searchValue, customerId, employeeId, shipperId);
        }

        #endregion Order

        #region OrderDetail
        public static List<OrderDetail> OrderDetails_List(int orderID)
        {
            return OrderDetailDB.List(orderID);
        }
        #endregion OrderDetail
    }
}