using SaMoCode.RepairManagement.Services;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;

        public CustomerForm()
        {
            InitializeComponent();

            _customerService = new CustomerService();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = _customerService.AddCustomer(
                    txtName.Text,
                    txtPhoneNumber.Text,
                    txtNotes.Text);

                MessageBox.Show(
                    $"Customer saved successfully.\nCustomer ID: {customerId}",
                    "Customer Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Tells CustomersPage that saving succeeded.
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to save customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}