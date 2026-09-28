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

        public void UpdateCustomer(Customer original, string name, string phone, string notes)
        {
            if(original == null || original.CustomerId <= 0) throw new ArgumentException("Select a customer.");
            Validate(name,phone,notes);
            _customerRepository.Update(original,name.Trim(),phone.Trim(),string.IsNullOrWhiteSpace(notes)?null:notes.Trim());
        }
        private static void Validate(string name,string phone,string notes)
        {
            RepairWorkflowService.Text(name,"Customer name",100);
            RepairWorkflowService.Text(phone,"Phone number",30);
            RepairWorkflowService.Text(notes,"Notes",500,false);
        }
        public int AddCustomer(string name, string phoneNumber, string notes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.");

            Validate(name,phoneNumber,notes);
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

        public List<Customer> SearchCustomers(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return _customerRepository.GetAll();

            return _customerRepository.Search(searchText.Trim());
        }


    }
}
