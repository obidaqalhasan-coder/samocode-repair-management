using SaMoCode.RepairManagement.Services;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class TechnicianForm : Form
    {
        private readonly TechnicianService
            _technicianService;

        public TechnicianForm()
        {
            InitializeComponent();
            txtName.MaxLength=100;txtSpecialization.MaxLength=100;

            _technicianService =
                new TechnicianService();

            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                int technicianId =
                    _technicianService.AddTechnician(
                        txtName.Text,
                        txtSpecialization.Text);

                MessageBox.Show(
                    $"Technician added successfully.\nID: {technicianId}",
                    "Technician Added",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
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
                    "Unable to add technician",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
