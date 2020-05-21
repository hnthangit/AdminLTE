using System;
using System.Configuration;

namespace LiteCommerce.Admin
{
    /// <summary>
    ///
    /// </summary>
    public static class AppSetting
    {
        /// <summary>
        /// Lấy giá trị Default Page Size từ web config ra
        /// </summary>
        public static int DefaultPageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPageSize"]);
            }
        }
    }
}