using BusinessObject.Models;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Role CheckLogin(string email, string password)
        {
            return CustomerDAO.Instance.CheckLogin(email, password);
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return CustomerDAO.Instance.GetAllCustomer();
        }

        public Customer? GetCustomerByID(string id)
        {
            return CustomerDAO.Instance.GetCustomerByID(id);
        }

        public bool UpdateCustomer(Customer customerUpdate)
        {
            return CustomerDAO.Instance.UpdateCustomer(customerUpdate);
        }
        public bool CreateCustomer(Customer customerCreate)
        {
            return CustomerDAO.Instance.CreateCustomer(customerCreate);
        }
    }
}
