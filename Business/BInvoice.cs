using System.Collections.Generic;
using Entity;
using Data;

namespace Business
{
    public class BInvoice
    {
        public List<Invoice> Read()
        {
            var data = new DInvoice();
            return data.Read();
        }

        public int Create(Invoice invoice)
        {
            var data = new DInvoice();
            return data.Create(invoice);
        }

        public void Update(Invoice invoice)
        {
            var data = new DInvoice();
            data.Update(invoice);
        }

        public void Delete(int invoiceId)
        {
            var data = new DInvoice();
            data.Delete(invoiceId);
        }
    }
}