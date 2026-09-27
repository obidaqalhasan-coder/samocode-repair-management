namespace SaMoCode.RepairManagement.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Panel pnlContent;

        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblSystemName;

        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnRepairs;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnTechnicians;

        private System.Windows.Forms.Label lblVersion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlBrand = new System.Windows.Forms.Panel();

            this.lblBrand = new System.Windows.Forms.Label();
            this.lblSystemName = new System.Windows.Forms.Label();

            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnRepairs = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnTechnicians = new System.Windows.Forms.Button();

            this.lblVersion = new System.Windows.Forms.Label();

            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.SuspendLayout();

            // =========================================================
            // MainForm
            // =========================================================

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Dpi;

            this.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.ClientSize =
                new System.Drawing.Size(1400, 850);

            this.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            // Use the native Windows frame.
            // This gives us Minimize / Maximize / Close automatically.
            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.Sizable;

            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = true;

            // Application opens full screen (maximized).
            this.WindowState =
                System.Windows.Forms.FormWindowState.Maximized;

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.MinimumSize =
                new System.Drawing.Size(1100, 700);

            this.Name = "MainForm";

            this.Text =
                "SaMoCode Repair Management";


            // =========================================================
            // Sidebar
            // =========================================================

            this.pnlSidebar.BackColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.pnlSidebar.Dock =
                System.Windows.Forms.DockStyle.Left;

            this.pnlSidebar.Width = 240;

            this.pnlSidebar.Padding =
                new System.Windows.Forms.Padding(0);

            this.pnlSidebar.Controls.Add(this.lblVersion);
            this.pnlSidebar.Controls.Add(this.btnTechnicians);
            this.pnlSidebar.Controls.Add(this.btnCustomers);
            this.pnlSidebar.Controls.Add(this.btnRepairs);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.pnlBrand);


            // =========================================================
            // Brand Area
            // =========================================================

            this.pnlBrand.BackColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.pnlBrand.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlBrand.Height = 115;

            this.pnlBrand.Controls.Add(this.lblBrand);
            this.pnlBrand.Controls.Add(this.lblSystemName);


            // SaMoCode
            this.lblBrand.AutoSize = true;

            this.lblBrand.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    18F,
                    System.Drawing.FontStyle.Bold);

            this.lblBrand.ForeColor =
                System.Drawing.Color.White;

            this.lblBrand.Location =
                new System.Drawing.Point(24, 25);

            this.lblBrand.Text =
                "SaMoCode";


            // Repair Management
            this.lblSystemName.AutoSize = true;

            this.lblSystemName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Regular);

            this.lblSystemName.ForeColor =
                System.Drawing.Color.FromArgb(148, 163, 184);

            this.lblSystemName.Location =
                new System.Drawing.Point(27, 67);

            this.lblSystemName.Text =
                "Repair Management";


            // =========================================================
            // Dashboard Button
            // =========================================================

            ConfigureNavigationButton(
                this.btnDashboard,
                "Dashboard");

            this.btnDashboard.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnDashboard.BackColor =
                System.Drawing.Color.FromArgb(30, 41, 59);


            // =========================================================
            // Repairs Button
            // =========================================================

            ConfigureNavigationButton(
                this.btnRepairs,
                "Repairs");

            this.btnRepairs.Dock =
                System.Windows.Forms.DockStyle.Top;


            // =========================================================
            // Customers Button
            // =========================================================

            ConfigureNavigationButton(
                this.btnCustomers,
                "Customers");

            this.btnCustomers.Dock =
                System.Windows.Forms.DockStyle.Top;


            // =========================================================
            // Technicians Button
            // =========================================================

            ConfigureNavigationButton(
                this.btnTechnicians,
                "Technicians");

            this.btnTechnicians.Dock =
                System.Windows.Forms.DockStyle.Top;


            // =========================================================
            // Version
            // =========================================================

            this.lblVersion.AutoSize = false;

            this.lblVersion.Dock =
                System.Windows.Forms.DockStyle.Bottom;

            this.lblVersion.Height = 50;

            this.lblVersion.Padding =
                new System.Windows.Forms.Padding(26, 0, 0, 18);

            this.lblVersion.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            this.lblVersion.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblVersion.Text =
                "SaMoCode  •  v1.0";

            this.lblVersion.TextAlign =
                System.Drawing.ContentAlignment.BottomLeft;


            // =========================================================
            // Main Content Area
            // =========================================================

            this.pnlContent.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);

            this.pnlContent.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlContent.Padding =
                new System.Windows.Forms.Padding(
                    40,
                    32,
                    40,
                    32);


            // =========================================================
            // Add Controls to MainForm
            // =========================================================

            // Fill first, sidebar second.
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);


            // =========================================================
            // Resume Layout
            // =========================================================

            this.pnlBrand.ResumeLayout(false);
            this.pnlBrand.PerformLayout();

            this.pnlSidebar.ResumeLayout(false);

            this.ResumeLayout(false);
        }


        // =============================================================
        // Navigation Button Styling
        // =============================================================

        private void ConfigureNavigationButton(
            System.Windows.Forms.Button button,
            string text)
        {
            button.BackColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            button.Cursor =
                System.Windows.Forms.Cursors.Hand;

            button.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            button.FlatAppearance.BorderSize = 0;

            button.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(30, 41, 59);

            button.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(51, 65, 85);

            button.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Regular);

            button.ForeColor =
                System.Drawing.Color.FromArgb(203, 213, 225);

            button.Height = 54;

            button.Padding =
                new System.Windows.Forms.Padding(
                    26,
                    0,
                    0,
                    0);

            button.Text = text;

            button.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            button.UseVisualStyleBackColor = false;

            button.TabStop = false;
        }
    }
}