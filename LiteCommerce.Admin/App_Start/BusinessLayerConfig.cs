using LiteCommerce.BusiniessLayer;
using System.Configuration;

namespace LiteCommerce.Admin.App_Start
{
    /// <summary>
    /// Khởi tạo các chức năng tác nghiệp cho ứng dụng
    /// </summary>
    public static class BusinessLayerConfig
    {
        /// <summary>
        ///
        /// </summary>
        public static void Initialize()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;
            //string connectionString = ConfigurationManager.ConnectionStrings[1].ToString();
            CatalogBLL.Innitialize(connectionString);
            HumanResourceBLL.Initialize(connectionString);
            AccountBLL.Initialize(connectionString);
            UserAccountBLL.Initialize(connectionString);
            //TODO: Bổ sung việc khởi tao các BLL khác khi cần sử dụng
        }
    }
}