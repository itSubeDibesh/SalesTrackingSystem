using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ProductCategory_Interface
    {
        List<ProductCategory_Model> ListAllData();
        bool Save(ProductCategory_Model model);
        bool Update(ProductCategory_Model model);
        int GetNewProductCategoryID();
        ProductCategory_Model GetProductCatgoryById(long id);
        bool Delete(long ProductCategoryId);
        bool ProductCategoryExist(long id);
    }
}
