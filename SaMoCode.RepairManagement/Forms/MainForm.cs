using System;
using System.Drawing;
using System.Windows.Forms;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class MainForm : Form
    {
        private readonly Color _normalNavColor =
            Color.FromArgb(15, 23, 42);

        private readonly Color _activeNavColor =
            Color.FromArgb(30, 41, 59);

        public MainForm()
        {
            InitializeComponent();

            btnDashboard.Click += btnDashboard_Click;
            btnRepairs.Click += btnRepairs_Click;
            btnCustomers.Click += btnCustomers_Click;
            btnTechnicians.Click += btnTechnicians_Click;

            ShowDashboard();
        }

        private void SetActiveButton(Button activeButton)
        {
            btnDashboard.BackColor = _normalNavColor;
            btnRepairs.BackColor = _normalNavColor;
            btnCustomers.BackColor = _normalNavColor;
            btnTechnicians.BackColor = _normalNavColor;

            activeButton.BackColor = _activeNavColor;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            CustomersPage page = new CustomersPage
            {
                Dock = DockStyle.Fill
            };

            pnlContent.Controls.Add(page);

            SetActiveButton(btnCustomers);
        }
        private void btnRepairs_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            RepairsPage page = new RepairsPage
            {
                Dock = DockStyle.Fill
            };

            pnlContent.Controls.Add(page);

            SetActiveButton(btnRepairs);
        }
        private void btnTechnicians_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(new TechniciansPage { Dock = DockStyle.Fill });
            SetActiveButton(btnTechnicians);
        }

        private void ShowDashboard()
        {
            pnlContent.Controls.Clear();
            SetActiveButton(btnDashboard);
        }
    }
}
