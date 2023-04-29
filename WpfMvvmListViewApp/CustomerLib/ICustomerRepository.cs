using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLib
{
    public interface ICustomerRepository
    {
        bool Add(Customer customer);
        bool Remove(Customer customer);
        bool Commit();
        IEnumerable<Customer> Customers { get; }
    }
}
