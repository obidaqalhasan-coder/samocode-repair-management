namespace SaMoCode.RepairManagement.Forms
{
    partial class CustomerForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;

        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhoneNumber;

        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;

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

            this.pnlBody = new System.Windows.Forms.Panel();

            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();

            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();

            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();

            this.SuspendLayout();


            // =====================================================
            // CustomerForm
            // =====================================================

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Dpi;

            this.BackColor =
                System.Drawing.Color.White;

            this.ClientSize =
                new System.Drawing.Size(600, 590);

            this.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Name = "CustomerForm";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Add Customer";


            // =====================================================
            // Header
            // =====================================================

            this.pnlHeader.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);

            this.pnlHeader.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlHeader.Height = 105;

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);


            // Title
            this.lblTitle.AutoSize = true;

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    18F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(15, 23, 42);

            this.lblTitle.Location =
                new System.Drawing.Point(32, 22);

            this.lblTitle.Text =
                "Add customer";


            // Subtitle
            this.lblSubtitle.AutoSize = true;

            this.lblSubtitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.lblSubtitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblSubtitle.Location =
                new System.Drawing.Point(35, 64);

            this.lblSubtitle.Text =
                "Create a customer profile for repair orders.";


            // =====================================================
            // Body
            // =====================================================

            this.pnlBody.BackColor =
                System.Drawing.Color.White;

            this.pnlBody.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlBody.Controls.Add(this.lblName);
            this.pnlBody.Controls.Add(this.txtName);

            this.pnlBody.Controls.Add(this.lblPhone);
            this.pnlBody.Controls.Add(this.txtPhoneNumber);

            this.pnlBody.Controls.Add(this.lblNotes);
            this.pnlBody.Controls.Add(this.txtNotes);


            // =====================================================
            // Customer Name
            // =====================================================

            this.lblName.AutoSize = true;

            this.lblName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblName.ForeColor =
                System.Drawing.Color.FromArgb(51, 65, 85);

            this.lblName.Location =
                new System.Drawing.Point(34, 32);

            this.lblName.Text =
                "Customer name";


            this.txtName.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F);

            this.txtName.Location =
                new System.Drawing.Point(38, 61);

            this.txtName.Size =
                new System.Drawing.Size(524, 32);

            this.txtName.TabIndex = 0;


            // =====================================================
            // Phone Number
            // =====================================================

            this.lblPhone.AutoSize = true;

            this.lblPhone.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblPhone.ForeColor =
                System.Drawing.Color.FromArgb(51, 65, 85);

            this.lblPhone.Location =
                new System.Drawing.Point(34, 125);

            this.lblPhone.Text =
                "Phone number";


            this.txtPhoneNumber.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtPhoneNumber.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F);

            this.txtPhoneNumber.Location =
                new System.Drawing.Point(38, 154);

            this.txtPhoneNumber.Size =
                new System.Drawing.Size(524, 32);

            this.txtPhoneNumber.TabIndex = 1;


            // =====================================================
            // Notes
            // =====================================================

            this.lblNotes.AutoSize = true;

            this.lblNotes.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblNotes.ForeColor =
                System.Drawing.Color.FromArgb(51, 65, 85);

            this.lblNotes.Location =
                new System.Drawing.Point(34, 220);

            this.lblNotes.Text =
                "Notes  (optional)";


            this.txtNotes.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtNotes.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtNotes.Location =
                new System.Drawing.Point(38, 250);

            this.txtNotes.Multiline = true;

            this.txtNotes.ScrollBars =
                System.Windows.Forms.ScrollBars.Vertical;

            this.txtNotes.Size =
                new System.Drawing.Size(524, 110);

            this.txtNotes.TabIndex = 2;


            // =====================================================
            // Footer
            // =====================================================

            this.pnlFooter.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);

            this.pnlFooter.Dock =
                System.Windows.Forms.DockStyle.Bottom;

            this.pnlFooter.Height = 90;

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);


            // =====================================================
            // Cancel Button
            // =====================================================

            this.btnCancel.BackColor =
                System.Drawing.Color.White;

            this.btnCancel.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnCancel.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(203, 213, 225);

            this.btnCancel.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCancel.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnCancel.ForeColor =
                System.Drawing.Color.FromArgb(51, 65, 85);

            this.btnCancel.Location =
                new System.Drawing.Point(320, 22);

            this.btnCancel.Size =
                new System.Drawing.Size(110, 44);

            this.btnCancel.TabIndex = 4;

            this.btnCancel.Text =
                "Cancel";

            this.btnCancel.UseVisualStyleBackColor = false;

            this.btnCancel.Click +=
                new System.EventHandler(this.btnCancel_Click);


            // =====================================================
            // Save Button
            // =====================================================

            this.btnSave.BackColor =
                System.Drawing.Color.FromArgb(37, 99, 235);

            this.btnSave.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnSave.FlatAppearance.BorderSize = 0;

            this.btnSave.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnSave.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnSave.ForeColor =
                System.Drawing.Color.White;

            this.btnSave.Location =
                new System.Drawing.Point(442, 22);

            this.btnSave.Size =
                new System.Drawing.Size(120, 44);

            this.btnSave.TabIndex = 3;

            this.btnSave.Text =
                "Add Customer";

            this.btnSave.UseVisualStyleBackColor = false;

            this.btnSave.Click +=
                new System.EventHandler(this.btnSave_Click);


            // =====================================================
            // Useful Form Behavior
            // =====================================================

            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;


            // =====================================================
            // Add Controls
            // =====================================================

            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);


            // =====================================================
            // Resume
            // =====================================================

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();

            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();

            this.pnlFooter.ResumeLayout(false);

            this.ResumeLayout(false);
        }
    }
}