namespace SaMoCode.RepairManagement.Forms
{
    partial class RepairDetailsPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Button btnBack;

        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.Label lblCustomerSection;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerPhone;

        private System.Windows.Forms.Panel pnlDevice;
        private System.Windows.Forms.Label lblDeviceSection;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.Label lblDeviceStatus;
        private System.Windows.Forms.Label lblDeviceDetails;

        private System.Windows.Forms.Panel pnlTechnician;
        private System.Windows.Forms.Label lblTechnicianSection;
        private System.Windows.Forms.Label lblCurrentTechnician;
        private System.Windows.Forms.Label lblAssignedAt;
        private System.Windows.Forms.Button btnAssignTechnician;

        private System.Windows.Forms.Panel pnlIssues;
        private System.Windows.Forms.Label lblIssuesSection;
        private System.Windows.Forms.DataGridView dgvIssues;

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
            this.lblCreated = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();

            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.lblCustomerSection = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerPhone = new System.Windows.Forms.Label();

            this.pnlDevice = new System.Windows.Forms.Panel();
            this.lblDeviceSection = new System.Windows.Forms.Label();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.lblDeviceStatus = new System.Windows.Forms.Label();
            this.lblDeviceDetails = new System.Windows.Forms.Label();

            this.pnlTechnician = new System.Windows.Forms.Panel();
            this.lblTechnicianSection = new System.Windows.Forms.Label();
            this.lblCurrentTechnician = new System.Windows.Forms.Label();
            this.lblAssignedAt = new System.Windows.Forms.Label();
            this.btnAssignTechnician = new System.Windows.Forms.Button();
            this.pnlIssues = new System.Windows.Forms.Panel();
            this.lblIssuesSection = new System.Windows.Forms.Label();
            this.dgvIssues = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvIssues)).BeginInit();

            this.SuspendLayout();

            // Page
            this.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.AutoScroll = true;
            this.Padding = new System.Windows.Forms.Padding(8);

            // =====================================================
            // Header
            // =====================================================

            this.pnlHeader.Location =
                new System.Drawing.Point(8, 8);

            this.pnlHeader.Size =
                new System.Drawing.Size(1100, 110);

            this.pnlHeader.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblCreated);
            this.pnlHeader.Controls.Add(this.btnBack);

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

            this.lblTitle.Text = "Repair Order";

            this.lblCreated.AutoSize = true;

            this.lblCreated.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.lblCreated.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblCreated.Location =
                new System.Drawing.Point(3, 58);

            this.lblCreated.Text = "Created";

            this.btnBack.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnBack.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnBack.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(203, 213, 225);

            this.btnBack.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnBack.Location =
                new System.Drawing.Point(960, 15);

            this.btnBack.Size =
                new System.Drawing.Size(140, 42);

            this.btnBack.Text = "← Back";

            // =====================================================
            // Customer
            // =====================================================

            this.pnlCustomer.BackColor =
                System.Drawing.Color.White;

            this.pnlCustomer.Location =
                new System.Drawing.Point(8, 125);

            this.pnlCustomer.Size =
                new System.Drawing.Size(535, 160);

            this.pnlCustomer.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left;

            this.pnlCustomer.Controls.Add(this.lblCustomerSection);
            this.pnlCustomer.Controls.Add(this.lblCustomerName);
            this.pnlCustomer.Controls.Add(this.lblCustomerPhone);

            this.lblCustomerSection.AutoSize = true;

            this.lblCustomerSection.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblCustomerSection.Location =
                new System.Drawing.Point(25, 20);

            this.lblCustomerSection.Text = "Customer";

            this.lblCustomerName.AutoSize = true;

            this.lblCustomerName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    13F,
                    System.Drawing.FontStyle.Bold);

            this.lblCustomerName.ForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.lblCustomerName.Location =
                new System.Drawing.Point(25, 60);

            this.lblCustomerName.Text = "Customer Name";

            this.lblCustomerPhone.AutoSize = true;

            this.lblCustomerPhone.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.lblCustomerPhone.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblCustomerPhone.Location =
                new System.Drawing.Point(27, 105);

            this.lblCustomerPhone.Text = "Phone";

            // =====================================================
            // Device
            // =====================================================

            this.pnlDevice.BackColor =
                System.Drawing.Color.White;

            this.pnlDevice.Location =
                new System.Drawing.Point(558, 125);

            this.pnlDevice.Size =
                new System.Drawing.Size(550, 160);

            this.pnlDevice.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.pnlDevice.Controls.Add(this.lblDeviceSection);
            this.pnlDevice.Controls.Add(this.lblDeviceName);
            this.pnlDevice.Controls.Add(this.lblDeviceStatus);
            this.pnlDevice.Controls.Add(this.lblDeviceDetails);

            this.lblDeviceSection.AutoSize = true;

            this.lblDeviceSection.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblDeviceSection.Location =
                new System.Drawing.Point(25, 20);

            this.lblDeviceSection.Text = "Device";

            this.lblDeviceName.AutoSize = true;

            this.lblDeviceName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    13F,
                    System.Drawing.FontStyle.Bold);

            this.lblDeviceName.ForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.lblDeviceName.Location =
                new System.Drawing.Point(25, 60);

            this.lblDeviceName.Text = "Device";

            this.lblDeviceStatus.AutoSize = true;

            this.lblDeviceStatus.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblDeviceStatus.ForeColor =
                System.Drawing.Color.FromArgb(37, 99, 235);

            this.lblDeviceStatus.Location =
                new System.Drawing.Point(350, 65);

            this.lblDeviceStatus.Text = "Received";

            this.lblDeviceDetails.AutoSize = true;

            this.lblDeviceDetails.Font =
                new System.Drawing.Font("Segoe UI", 9.5F);

            this.lblDeviceDetails.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblDeviceDetails.Location =
                new System.Drawing.Point(27, 108);

            this.lblDeviceDetails.Text =
                "Color / Serial";

            // =====================================================
            // Issues
            // =====================================================

            this.pnlIssues.BackColor =
                System.Drawing.Color.White;

            this.pnlIssues.Location =
                new System.Drawing.Point(8, 455);

            this.pnlIssues.Size =
                new System.Drawing.Size(1100, 235);

            this.pnlIssues.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.pnlIssues.Controls.Add(this.lblIssuesSection);
            this.pnlIssues.Controls.Add(this.dgvIssues);

            this.lblIssuesSection.AutoSize = true;

            this.lblIssuesSection.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    12F,
                    System.Drawing.FontStyle.Bold);

            this.lblIssuesSection.Location =
                new System.Drawing.Point(25, 20);

            this.lblIssuesSection.Text =
                "Reported Issues";

            this.dgvIssues.Location =
                new System.Drawing.Point(25, 65);

            this.dgvIssues.Size =
                new System.Drawing.Size(1050, 140);

            this.dgvIssues.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.dgvIssues.AllowUserToAddRows = false;
            this.dgvIssues.AllowUserToDeleteRows = false;
            this.dgvIssues.AllowUserToResizeRows = false;

            this.dgvIssues.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvIssues.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvIssues.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvIssues.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvIssues.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvIssues.ColumnHeadersHeight = 45;

            this.dgvIssues.EnableHeadersVisualStyles = false;

            this.dgvIssues.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);

            this.dgvIssues.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.dgvIssues.DefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9.5F);

            this.dgvIssues.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(219, 234, 254);

            this.dgvIssues.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.dgvIssues.GridColor =
                System.Drawing.Color.FromArgb(226, 232, 240);

            this.dgvIssues.ReadOnly = true;
            this.dgvIssues.RowHeadersVisible = false;
            this.dgvIssues.RowTemplate.Height = 42;

            this.dgvIssues.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // Technician custody for the displayed device.
            this.pnlTechnician.BackColor = System.Drawing.Color.White;
            this.pnlTechnician.Location = new System.Drawing.Point(8, 300);
            this.pnlTechnician.Size = new System.Drawing.Size(1100, 140);
            this.pnlTechnician.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTechnicianSection.Text = "Current Technician";
            this.lblTechnicianSection.AutoSize = true;
            this.lblTechnicianSection.Location = new System.Drawing.Point(25, 18);
            this.lblTechnicianSection.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentTechnician.Text = "Not assigned";
            this.lblCurrentTechnician.Location = new System.Drawing.Point(25, 52);
            this.lblCurrentTechnician.Size = new System.Drawing.Size(810, 32);
            this.lblCurrentTechnician.AutoEllipsis = true;
            this.lblCurrentTechnician.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblCurrentTechnician.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblCurrentTechnician.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblAssignedAt.Location = new System.Drawing.Point(27, 96);
            this.lblAssignedAt.Size = new System.Drawing.Size(800, 25);
            this.lblAssignedAt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAssignedAt.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnAssignTechnician.Text = "Assign Technician";
            this.btnAssignTechnician.Enabled = false;
            this.btnAssignTechnician.Location = new System.Drawing.Point(850, 48);
            this.btnAssignTechnician.Size = new System.Drawing.Size(225, 44);
            this.btnAssignTechnician.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnAssignTechnician.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignTechnician.FlatAppearance.BorderSize = 0;
            this.btnAssignTechnician.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAssignTechnician.ForeColor = System.Drawing.Color.White;
            this.btnAssignTechnician.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAssignTechnician.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlTechnician.Controls.Add(this.lblTechnicianSection);
            this.pnlTechnician.Controls.Add(this.lblCurrentTechnician);
            this.pnlTechnician.Controls.Add(this.lblAssignedAt);
            this.pnlTechnician.Controls.Add(this.btnAssignTechnician);
            this.Controls.Add(this.pnlTechnician);

            // Page controls
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.pnlDevice);
            this.Controls.Add(this.pnlIssues);

            this.Name = "RepairDetailsPage";

            this.Size =
                new System.Drawing.Size(1120, 720);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvIssues)).EndInit();

            this.ResumeLayout(false);
        }
    }
}
