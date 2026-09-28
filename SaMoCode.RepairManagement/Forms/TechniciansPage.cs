using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class TechniciansPage : UserControl
    {
        private readonly TechnicianService _technicianService;

        public TechniciansPage() : this(true) { }
        internal TechniciansPage(bool loadData)
        {
            InitializeComponent();

            _technicianService = new TechnicianService();

            btnAddTechnician.Click += btnAddTechnician_Click;

            btnAddTechnician.Text="+ Add Technician";
            PageLayout.Header(pnlHeader,lblTitle,lblSubtitle,btnAddTechnician);
            PageLayout.List(pnlList,lblListTitle,lblCount,dgvTechnicians);
            if(loadData) LoadTechnicians();
        }

        private void LoadTechnicians()
        {
            try
            {
                List<Technician> technicians =
                    _technicianService.GetActiveTechnicians();

                dgvTechnicians.AutoGenerateColumns = false;
                dgvTechnicians.Columns.Clear();

                dgvTechnicians.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "ID",
                        DataPropertyName = "TechnicianId",
                        FillWeight = 15
                    });

                dgvTechnicians.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Technician",
                        DataPropertyName = "Name",
                        FillWeight = 40
                    });

                dgvTechnicians.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Specialization",
                        DataPropertyName = "Specialization",
                        FillWeight = 40
                    });

                dgvTechnicians.DataSource = technicians;

                lblCount.Text =
                    technicians.Count == 1
                        ? "1 technician"
                        : $"{technicians.Count} technicians";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to load technicians",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAddTechnician_Click(
            object sender,
            EventArgs e)
        {
            using (TechnicianForm form = new TechnicianForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadTechnicians();
                }
            }
        }
    }
}
