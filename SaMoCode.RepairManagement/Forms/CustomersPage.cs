using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class CustomersPage : UserControl
    {
        private readonly CustomerService _customerService;

        public CustomersPage()
        {
            InitializeComponent();

            _customerService = new CustomerService();

            btnAddCustomer.Click += btnAddCustomer_Click;

            LoadCustomers();
        }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (CustomerForm form = new CustomerForm())
            {
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
        }
        private void LoadCustomers()
        {
            try
            {
                List<Customer> customers =
                    _customerService.GetAllCustomers();

                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.Columns.Clear();

                dgvCustomers.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        Name = "CustomerId",
                        HeaderText = "ID",
                        DataPropertyName = "CustomerId",
                        FillWeight = 15
                    });

                dgvCustomers.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Name",
                        HeaderText = "Customer",
                        DataPropertyName = "Name",
                        FillWeight = 35
                    });

                dgvCustomers.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        Name = "PhoneNumber",
                        HeaderText = "Phone",
                        DataPropertyName = "PhoneNumber",
                        FillWeight = 30
                    });

                dgvCustomers.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        Name = "CreatedAt",
                        HeaderText = "Created",
                        DataPropertyName = "CreatedAt",
                        FillWeight = 25,
                        DefaultCellStyle =
                        {
                            Format = "dd MMM yyyy"
                        }
                    });

                dgvCustomers.DataSource = customers;

                lblCustomerCount.Text =
                    customers.Count == 1
                        ? "1 customer"
                        : $"{customers.Count} customers";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to load customers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}