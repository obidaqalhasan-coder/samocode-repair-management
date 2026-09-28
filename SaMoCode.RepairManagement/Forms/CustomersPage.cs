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

            txtSearch.TextChanged += txtSearch_TextChanged;

            dgvCustomers.CellDoubleClick+=(s,e)=> { if(e.RowIndex>=0) EditSelectedCustomer(); };
            var menu=new ContextMenuStrip();
            menu.Items.Add("Edit customer",null,(s,e)=>EditSelectedCustomer());
            menu.Items.Add("Repair history",null,(s,e)=>ShowCustomerHistory());
            dgvCustomers.ContextMenuStrip=menu;
            dgvCustomers.CellMouseDown+=(s,e)=> {if(e.RowIndex>=0 && e.Button==MouseButtons.Right)dgvCustomers.CurrentCell=dgvCustomers.Rows[e.RowIndex].Cells[0];};
            LoadCustomers();
        }
        private void EditSelectedCustomer()
        {
            var customer=dgvCustomers.CurrentRow==null?null:dgvCustomers.CurrentRow.DataBoundItem as Customer;
            if(customer==null)return;
            using(var form=new CustomerForm(customer))if(form.ShowDialog(this)==DialogResult.OK)SearchCustomers();
        }
        private void ShowCustomerHistory()
        {
            var customer=dgvCustomers.CurrentRow==null?null:dgvCustomers.CurrentRow.DataBoundItem as Customer;
            if(customer==null)return;
            try
            {
                using(var form=new Form {Text=customer.Name+" — Repair history (double-click to open)",Width=1000,Height=600,StartPosition=FormStartPosition.CenterParent})
                {
                    var grid=WorkflowDialog.Grid();grid.Dock=DockStyle.Fill;
                    grid.DataSource=new RepairWorkflowService().CustomerHistory(customer.CustomerId);
                    grid.CellDoubleClick+=(s,e)=> {
                        if(e.RowIndex<0)return;
                        var row=grid.Rows[e.RowIndex].DataBoundItem as System.Data.DataRowView;
                        if(row==null)return;
                        int order=(int)row["Order"];form.Close();
                        var parent=Parent;if(parent!=null){parent.Controls.Clear();parent.Controls.Add(new RepairDetailsPage(order){Dock=DockStyle.Fill});Dispose();}
                    };
                    form.Controls.Add(grid);form.ShowDialog(this);
                }
            }
            catch(Exception ex){MessageBox.Show(this,ex.Message,"Unable to load history");}
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        private void SearchCustomers()
        {
            try
            {
                List<Customer> customers =
                    _customerService.SearchCustomers(txtSearch.Text);

                dgvCustomers.DataSource = null;
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
                    "Search Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
