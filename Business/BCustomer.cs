using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Data;

namespace Business
{
    public class BCustomer
    {
        public List<Customer> Read()
        {
            var data = new DCustomer();
            return data.Read();
        }

        public void Create(Customer customer)
        {
            var data = new DCustomer();
            data.Create(customer);
        }

        public void Update(Customer customer)
        {
            var data = new DCustomer();
            data.Update(customer);
        }

        public void Delete(int customerId)
        {
            var data = new DCustomer();
            data.Delete(customerId);
        }
    }
}