using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách các Country
        /// </summary>
        /// <returns>List Country</returns>
        public static List<Country> ListOfCountries()
        {
            List<Country> listCountries = new List<Country>();
            listCountries = CatalogBLL.Country_List();
            return listCountries;
        }

        /// <summary>
        /// Danh sách SelectListItem cho Country
        /// </summary>
        /// <param name="allowSelectAll"></param>
        /// <returns>List SelectListItem</returns>
        public static List<SelectListItem> Country(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Countries --" });
            }
            foreach (var countries in CatalogBLL.Country_List())
            {
                list.Add(new SelectListItem()
                {
                    Value = countries.CountryName.ToString(),
                    Text = countries.CountryName
                });
            }
            return list;
        }

        /// <summary>
        /// Danh sách SelectListItem cho Supplier
        /// </summary>
        /// <param name="allowSelectAll"></param>
        /// <returns>List SelectListItem</returns>
        public static List<SelectListItem> Suppliers(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Supplier --" });
            }
            foreach (var supplier in CatalogBLL.Suppliers_CompanyName())
            {
                list.Add(new SelectListItem()
                {
                    Value = supplier.SupplierID.ToString(),
                    Text = supplier.CompanyName
                });
            }
            return list;
        }

        /// <summary>
        /// Danh sách SelectListItem cho Category
        /// </summary>
        /// <param name="allowSelectAll"></param>
        /// <returns>List SelectListItem</returns>
        public static List<SelectListItem> Category(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Category --" });
            }
            foreach (var category in CatalogBLL.Category_CategoryName())
            {
                list.Add(new SelectListItem()
                {
                    Value = category.CategoryID.ToString(),
                    Text = category.CategoryName
                });
            }
            return list;
        }
    }
}