using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Data;

namespace Business
{
    public class BProduct
    {

        public List<Product> Read()
        {
            var data = new DProduct();
            return data.Read();
        }
    }
}
