using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayer
{
    public interface ICategoryDAL
    {
        int Add(Category data);
        bool Update(Category data);
        bool Delete(Category data);
        Category Get(int catogoryID);
        List<Category> List(int page, int pageSize, string searchValue);
    }
}
