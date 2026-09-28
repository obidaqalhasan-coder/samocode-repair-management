namespace SaMoCode.RepairManagement.Forms
{
    partial class TechniciansPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddTechnician;

        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvTechnicians;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnAddTechnician = new System.Windows.Forms.Button();

            this.pnlList = new System.Windows.Forms.Panel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvTechnicians = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTechnicians)).BeginInit();

            this.SuspendLayout();

            // =========================
            // Header
            // =========================

            this.pnlHeader.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlHeader.Height = 110;

            this.pnlHeader.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.btnAddTechnician);

            // Title
            this.lblTitle.AutoSize = true;

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    22F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.lblTitle.Location =
                new System.Drawing.Point(0, 5);

            this.lblTitle.Text = "Technicians";

            // Subtitle
            this.lblSubtitle.AutoSize = true;

            this.lblSubtitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblSubtitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblSubtitle.Location =
                new System.Drawing.Point(3, 55);

            this.lblSubtitle.Text =
                "Manage repair technicians and specializations.";

            // Add Technician button
            //
            // IMPORTANT:
            // Keep it anchored to the LEFT for now.
            // This prevents WinForms from moving it outside
            // the visible page during resizing.
            this.btnAddTechnician.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left;

            this.btnAddTechnician.BackColor =
                System.Drawing.Color.FromArgb(37, 99, 235);

            this.btnAddTechnician.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnAddTechnician.FlatAppearance.BorderSize = 0;

            this.btnAddTechnician.ForeColor =
                System.Drawing.Color.White;

            this.btnAddTechnician.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnAddTechnician.Location =
                new System.Drawing.Point(650, 15);

            this.btnAddTechnician.Size =
                new System.Drawing.Size(190, 46);

            this.btnAddTechnician.Text =
                "+ Add Technician";

            this.btnAddTechnician.UseVisualStyleBackColor = false;

            // =========================
            // List Panel
            // =========================

            this.pnlList.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlList.BackColor =
                System.Drawing.Color.White;

            this.pnlList.Padding =
                new System.Windows.Forms.Padding(25);

            this.pnlList.Controls.Add(this.lblListTitle);
            this.pnlList.Controls.Add(this.lblCount);
            this.pnlList.Controls.Add(this.dgvTechnicians);

            // List title
            this.lblListTitle.AutoSize = true;

            this.lblListTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    12F,
                    System.Drawing.FontStyle.Bold);

            this.lblListTitle.Location =
                new System.Drawing.Point(25, 20);

            this.lblListTitle.Text =
                "Technician List";

            // Count
            this.lblCount.AutoSize = true;

            this.lblCount.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.lblCount.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblCount.Location =
                new System.Drawing.Point(28, 53);

            this.lblCount.Text =
                "0 technicians";

            // =========================
            // Technicians Grid
            // =========================

            this.dgvTechnicians.Location =
                new System.Drawing.Point(25, 85);

            this.dgvTechnicians.Size =
                new System.Drawing.Size(1020, 550);

            this.dgvTechnicians.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.dgvTechnicians.AllowUserToAddRows = false;
            this.dgvTechnicians.AllowUserToDeleteRows = false;
            this.dgvTechnicians.AllowUserToResizeRows = false;

            this.dgvTechnicians.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvTechnicians.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvTechnicians.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvTechnicians.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvTechnicians.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvTechnicians.ColumnHeadersHeight = 45;

            this.dgvTechnicians.EnableHeadersVisualStyles = false;

            this.dgvTechnicians.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);

            this.dgvTechnicians.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.dgvTechnicians.DefaultCellStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.dgvTechnicians.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(219, 234, 254);

            this.dgvTechnicians.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.dgvTechnicians.GridColor =
                System.Drawing.Color.FromArgb(226, 232, 240);

            this.dgvTechnicians.ReadOnly = true;

            this.dgvTechnicians.RowHeadersVisible = false;

            this.dgvTechnicians.RowTemplate.Height = 42;

            this.dgvTechnicians.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // =========================
            // TechniciansPage
            // =========================

            this.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.Padding =
                new System.Windows.Forms.Padding(8);

            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.pnlHeader);

            this.Name = "TechniciansPage";

            this.Size =
                new System.Drawing.Size(1100, 750);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTechnicians)).EndInit();

            this.ResumeLayout(false);
        }
    }
}