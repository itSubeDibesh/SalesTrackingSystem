using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Product_Interface
    {
        List<Products_Model> ListAllData();
        bool Save(Products_Model model);        
        bool Update(Products_Model model);
        int GetNewProductID();
        bool Delete(long ProductId);
        Products_Model GetProductById(long id);
        bool ProductExist(long id);
    }
}
