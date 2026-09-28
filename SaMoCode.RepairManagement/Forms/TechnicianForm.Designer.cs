namespace SaMoCode.RepairManagement.Forms
{
    partial class TechnicianForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSpecialization;
        private System.Windows.Forms.TextBox txtSpecialization;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

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
            this.lblSpecialization = new System.Windows.Forms.Label();
            this.txtSpecialization = new System.Windows.Forms.TextBox();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // =====================================================
            // Header
            // =====================================================

            this.pnlHeader.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);

            this.pnlHeader.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlHeader.Height = 110;

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
                new System.Drawing.Point(30, 22);

            this.lblTitle.Text =
                "Add Technician";

            // Subtitle
            this.lblSubtitle.AutoSize = true;

            this.lblSubtitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.lblSubtitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);

            this.lblSubtitle.Location =
                new System.Drawing.Point(33, 68);

            this.lblSubtitle.Text =
                "Add a technician to the repair team.";

            // =====================================================
            // Body
            // =====================================================

            this.pnlBody.BackColor =
                System.Drawing.Color.White;

            this.pnlBody.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlBody.Controls.Add(this.lblName);
            this.pnlBody.Controls.Add(this.txtName);
            this.pnlBody.Controls.Add(this.lblSpecialization);
            this.pnlBody.Controls.Add(this.txtSpecialization);

            // Technician Name Label
            this.lblName.AutoSize = true;

            this.lblName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblName.ForeColor =
                System.Drawing.Color.FromArgb(71, 85, 105);

            this.lblName.Location =
                new System.Drawing.Point(32, 38);

            this.lblName.Text =
                "Technician name";

            // Technician Name TextBox
            this.txtName.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F);

            this.txtName.Location =
                new System.Drawing.Point(35, 68);

            this.txtName.Size =
                new System.Drawing.Size(500, 32);

            // Specialization Label
            this.lblSpecialization.AutoSize = true;

            this.lblSpecialization.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblSpecialization.ForeColor =
                System.Drawing.Color.FromArgb(71, 85, 105);

            this.lblSpecialization.Location =
                new System.Drawing.Point(32, 135);

            this.lblSpecialization.Text =
                "Specialization (optional)";

            // Specialization TextBox
            this.txtSpecialization.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtSpecialization.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F);

            this.txtSpecialization.Location =
                new System.Drawing.Point(35, 165);

            this.txtSpecialization.Size =
                new System.Drawing.Size(500, 32);

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

            // Cancel
            this.btnCancel.BackColor =
                System.Drawing.Color.White;

            this.btnCancel.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnCancel.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCancel.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(203, 213, 225);

            this.btnCancel.FlatAppearance.BorderSize = 1;

            this.btnCancel.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnCancel.ForeColor =
                System.Drawing.Color.FromArgb(71, 85, 105);

            this.btnCancel.Location =
                new System.Drawing.Point(315, 22);

            this.btnCancel.Size =
                new System.Drawing.Size(105, 44);

            this.btnCancel.Text =
                "Cancel";

            this.btnCancel.UseVisualStyleBackColor = false;

            // Save
            this.btnSave.BackColor =
                System.Drawing.Color.FromArgb(37, 99, 235);

            this.btnSave.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnSave.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnSave.FlatAppearance.BorderSize = 0;

            this.btnSave.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnSave.ForeColor =
                System.Drawing.Color.White;

            this.btnSave.Location =
                new System.Drawing.Point(430, 22);

            this.btnSave.Size =
                new System.Drawing.Size(105, 44);

            this.btnSave.Text =
                "Save";

            this.btnSave.UseVisualStyleBackColor = false;

            // =====================================================
            // TechnicianForm
            // =====================================================

            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 20F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.White;

            this.ClientSize =
                new System.Drawing.Size(570, 430);

            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Name =
                "TechnicianForm";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Add Technician";

            this.ResumeLayout(false);
        }
    }
}