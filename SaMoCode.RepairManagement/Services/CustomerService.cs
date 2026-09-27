using System;
using SaMoCode.RepairManagement.Data;
using SaMoCode.RepairManagement.Models;
using System.Collections.Generic;

namespace SaMoCode.RepairManagement.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public int AddCustomer(string name, string phoneNumber, string notes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.");

            Customer customer = new Customer
            {
                Name = name.Trim(),
                PhoneNumber = phoneNumber.Trim(),
                Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim(),
                CreatedAt = DateTime.Now
            };

            return _customerRepository.Add(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }




    }
}