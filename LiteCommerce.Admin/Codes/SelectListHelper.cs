using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> ListOfCountries()
        {
            List<SelectListItem> listCountries = new List<SelectListItem>();
            listCountries.Add(new SelectListItem() { Value = "USA", Text = "United State" });
            return listCountries;
        }
    }
}
