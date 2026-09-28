namespace SaMoCode.RepairManagement.Forms
{
    partial class RepairsPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnNewRepair;

        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.Label lblRepairList;
        private System.Windows.Forms.Label lblRepairCount;
        private System.Windows.Forms.DataGridView dgvRepairs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnNewRepair = new System.Windows.Forms.Button();

            this.pnlList = new System.Windows.Forms.Panel();
            this.lblRepairList = new System.Windows.Forms.Label();
            this.lblRepairCount = new System.Windows.Forms.Label();
            this.dgvRepairs = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvRepairs)).BeginInit();

            this.SuspendLayout();

            // Page
            this.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.Padding = new System.Windows.Forms.Padding(8);

            // =====================================================
            // Header
            // =====================================================

            this.pnlHeader.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.pnlHeader.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlHeader.Height = 110;

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.btnNewRepair);

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

            this.lblTitle.Text = "Repairs";

            // Subtitle
            this.lblSubtitle.AutoSize = true;

            this.lblSubtitle.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.lblSubtitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblSubtitle.Location =
                new System.Drawing.Point(3, 55);

            this.lblSubtitle.Text =
                "Track and manage customer repair orders.";

            // New Repair Button
            this.btnNewRepair.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnNewRepair.BackColor =
                System.Drawing.Color.FromArgb(37, 99, 235);

            this.btnNewRepair.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnNewRepair.FlatAppearance.BorderSize = 0;

            this.btnNewRepair.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnNewRepair.ForeColor =
                System.Drawing.Color.White;

            this.btnNewRepair.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnNewRepair.Size =
                new System.Drawing.Size(170, 46);

            this.btnNewRepair.Location =
                new System.Drawing.Point(900, 15);

            this.btnNewRepair.Text =
                "+ New Repair";

            this.btnNewRepair.UseVisualStyleBackColor = false;

            // =====================================================
            // List Card
            // =====================================================

            this.pnlList.BackColor =
                System.Drawing.Color.White;

            this.pnlList.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlList.Padding =
                new System.Windows.Forms.Padding(25);

            this.pnlList.Controls.Add(this.dgvRepairs);
            this.pnlList.Controls.Add(this.lblRepairCount);
            this.pnlList.Controls.Add(this.lblRepairList);

            // Repair List label
            this.lblRepairList.AutoSize = true;

            this.lblRepairList.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    12F,
                    System.Drawing.FontStyle.Bold);

            this.lblRepairList.ForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.lblRepairList.Location =
                new System.Drawing.Point(25, 20);

            this.lblRepairList.Text =
                "Repair Orders";

            // Count
            this.lblRepairCount.AutoSize = true;

            this.lblRepairCount.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            this.lblRepairCount.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblRepairCount.Location =
                new System.Drawing.Point(28, 53);

            this.lblRepairCount.Text =
                "0 repairs";

            // =====================================================
            // DataGridView
            // =====================================================

            this.dgvRepairs.AllowUserToAddRows = false;
            this.dgvRepairs.AllowUserToDeleteRows = false;
            this.dgvRepairs.AllowUserToResizeRows = false;

            this.dgvRepairs.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvRepairs.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvRepairs.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvRepairs.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvRepairs.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvRepairs.ColumnHeadersHeight = 45;

            this.dgvRepairs.EnableHeadersVisualStyles = false;

            this.dgvRepairs.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);

            this.dgvRepairs.ColumnHeadersDefaultCellStyle.ForeColor =
                System.Drawing.Color.FromArgb(71, 85, 105);

            this.dgvRepairs.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.dgvRepairs.DefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9.5F);

            this.dgvRepairs.DefaultCellStyle.ForeColor =
                System.Drawing.Color.FromArgb(30, 41, 59);

            this.dgvRepairs.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(219, 234, 254);

            this.dgvRepairs.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.dgvRepairs.GridColor =
                System.Drawing.Color.FromArgb(226, 232, 240);

            this.dgvRepairs.ReadOnly = true;

            this.dgvRepairs.RowHeadersVisible = false;

            this.dgvRepairs.RowTemplate.Height = 42;

            this.dgvRepairs.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvRepairs.MultiSelect = false;

            this.dgvRepairs.Location =
                new System.Drawing.Point(25, 85);

            this.dgvRepairs.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.dgvRepairs.Size =
                new System.Drawing.Size(1020, 550);

            // =====================================================
            // RepairsPage
            // =====================================================

            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.pnlHeader);

            this.Name = "RepairsPage";
            this.Size = new System.Drawing.Size(1100, 750);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvRepairs)).EndInit();

            this.ResumeLayout(false);
        }
    }
}