using SaMoCode.RepairManagement.Services;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        private readonly SaMoCode.RepairManagement.Models.Customer _original;

        public CustomerForm(SaMoCode.RepairManagement.Models.Customer original = null)
        {
            InitializeComponent();

            _customerService = new CustomerService();
            _original=original;
            txtName.MaxLength=100;txtPhoneNumber.MaxLength=30;txtNotes.MaxLength=500;
            if(original!=null)
            {
                Text="Edit Customer";lblTitle.Text=Text;lblSubtitle.Text="Update customer contact details.";
                txtName.Text=original.Name;txtPhoneNumber.Text=original.PhoneNumber;txtNotes.Text=original.Notes;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId;
                if(_original!=null)
                {
                    _customerService.UpdateCustomer(_original,txtName.Text,txtPhoneNumber.Text,txtNotes.Text);
                    customerId=_original.CustomerId;
                }
                else customerId = _customerService.AddCustomer(
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
