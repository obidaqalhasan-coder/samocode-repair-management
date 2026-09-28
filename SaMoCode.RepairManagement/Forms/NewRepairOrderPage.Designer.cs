namespace SaMoCode.RepairManagement.Forms
{
    partial class NewRepairOrderPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.Label lblCustomerSection;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;

        private System.Windows.Forms.Panel pnlDevice;
        private System.Windows.Forms.Label lblDeviceSection;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.TextBox txtSerial;

        private System.Windows.Forms.Panel pnlIssue;
        private System.Windows.Forms.Label lblIssueSection;
        private System.Windows.Forms.Label lblProblem;
        private System.Windows.Forms.TextBox txtReportedProblem;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCreateRepair;

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
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.lblCustomerSection = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.pnlDevice = new System.Windows.Forms.Panel();
            this.lblDeviceSection = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.pnlIssue = new System.Windows.Forms.Panel();
            this.lblIssueSection = new System.Windows.Forms.Label();
            this.lblProblem = new System.Windows.Forms.Label();
            this.txtReportedProblem = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCreateRepair = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.pnlDevice.SuspendLayout();
            this.pnlIssue.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Location = new System.Drawing.Point(8, 8);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(334, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "New Repair Order";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(3, 53);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(347, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Create a repair intake for a customer device.";
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCustomer.BackColor = System.Drawing.Color.White;
            this.pnlCustomer.Controls.Add(this.lblCustomerSection);
            this.pnlCustomer.Controls.Add(this.lblCustomer);
            this.pnlCustomer.Controls.Add(this.cmbCustomer);
            this.pnlCustomer.Location = new System.Drawing.Point(8, 115);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(1100, 145);
            this.pnlCustomer.TabIndex = 1;
            // 
            // lblCustomerSection
            // 
            this.lblCustomerSection.AutoSize = true;
            this.lblCustomerSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCustomerSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustomerSection.Location = new System.Drawing.Point(28, 20);
            this.lblCustomerSection.Name = "lblCustomerSection";
            this.lblCustomerSection.Size = new System.Drawing.Size(102, 28);
            this.lblCustomerSection.TabIndex = 0;
            this.lblCustomerSection.Text = "Customer";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustomer.Location = new System.Drawing.Point(29, 59);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(120, 20);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Select customer";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbCustomer.Location = new System.Drawing.Point(32, 86);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(600, 31);
            this.cmbCustomer.TabIndex = 2;
            // 
            // pnlDevice
            // 
            this.pnlDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDevice.BackColor = System.Drawing.Color.White;
            this.pnlDevice.Controls.Add(this.lblDeviceSection);
            this.pnlDevice.Controls.Add(this.lblBrand);
            this.pnlDevice.Controls.Add(this.txtBrand);
            this.pnlDevice.Controls.Add(this.lblModel);
            this.pnlDevice.Controls.Add(this.txtModel);
            this.pnlDevice.Controls.Add(this.lblColor);
            this.pnlDevice.Controls.Add(this.txtColor);
            this.pnlDevice.Controls.Add(this.lblSerial);
            this.pnlDevice.Controls.Add(this.txtSerial);
            this.pnlDevice.Location = new System.Drawing.Point(8, 275);
            this.pnlDevice.Name = "pnlDevice";
            this.pnlDevice.Size = new System.Drawing.Size(1100, 240);
            this.pnlDevice.TabIndex = 2;
            // 
            // lblDeviceSection
            // 
            this.lblDeviceSection.AutoSize = true;
            this.lblDeviceSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDeviceSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDeviceSection.Location = new System.Drawing.Point(28, 20);
            this.lblDeviceSection.Name = "lblDeviceSection";
            this.lblDeviceSection.Size = new System.Drawing.Size(76, 28);
            this.lblDeviceSection.TabIndex = 0;
            this.lblDeviceSection.Text = "Device";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBrand.Location = new System.Drawing.Point(29, 62);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(51, 20);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "Brand";
            // 
            // txtBrand
            // 
            this.txtBrand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBrand.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtBrand.Location = new System.Drawing.Point(32, 89);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(250, 31);
            this.txtBrand.TabIndex = 2;
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblModel.Location = new System.Drawing.Point(310, 62);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(53, 20);
            this.lblModel.TabIndex = 3;
            this.lblModel.Text = "Model";
            // 
            // txtModel
            // 
            this.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModel.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtModel.Location = new System.Drawing.Point(313, 89);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(300, 31);
            this.txtModel.TabIndex = 4;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblColor.Location = new System.Drawing.Point(641, 62);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(46, 20);
            this.lblColor.TabIndex = 5;
            this.lblColor.Text = "Color";
            // 
            // txtColor
            // 
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtColor.Location = new System.Drawing.Point(644, 89);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(220, 31);
            this.txtColor.TabIndex = 6;
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSerial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSerial.Location = new System.Drawing.Point(29, 151);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(168, 20);
            this.lblSerial.TabIndex = 7;
            this.lblSerial.Text = "Serial / IMEI (optional)";
            // 
            // txtSerial
            // 
            this.txtSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerial.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSerial.Location = new System.Drawing.Point(32, 178);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(581, 31);
            this.txtSerial.TabIndex = 8;
            // 
            // pnlIssue
            // 
            this.pnlIssue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIssue.BackColor = System.Drawing.Color.White;
            this.pnlIssue.Controls.Add(this.lblIssueSection);
            this.pnlIssue.Controls.Add(this.lblProblem);
            this.pnlIssue.Controls.Add(this.txtReportedProblem);
            this.pnlIssue.Controls.Add(this.lblNotes);
            this.pnlIssue.Controls.Add(this.txtNotes);
            this.pnlIssue.Location = new System.Drawing.Point(8, 530);
            this.pnlIssue.Name = "pnlIssue";
            this.pnlIssue.Size = new System.Drawing.Size(1100, 260);
            this.pnlIssue.TabIndex = 3;
            // 
            // lblIssueSection
            // 
            this.lblIssueSection.AutoSize = true;
            this.lblIssueSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblIssueSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblIssueSection.Location = new System.Drawing.Point(28, 20);
            this.lblIssueSection.Name = "lblIssueSection";
            this.lblIssueSection.Size = new System.Drawing.Size(185, 28);
            this.lblIssueSection.TabIndex = 0;
            this.lblIssueSection.Text = "Reported Problem";
            // 
            // lblProblem
            // 
            this.lblProblem.AutoSize = true;
            this.lblProblem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProblem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblProblem.Location = new System.Drawing.Point(29, 62);
            this.lblProblem.Name = "lblProblem";
            this.lblProblem.Size = new System.Drawing.Size(301, 20);
            this.lblProblem.TabIndex = 1;
            this.lblProblem.Text = "What problem was reported or observed?";
            // 
            // txtReportedProblem
            // 
            this.txtReportedProblem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportedProblem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportedProblem.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtReportedProblem.Location = new System.Drawing.Point(32, 89);
            this.txtReportedProblem.Name = "txtReportedProblem";
            this.txtReportedProblem.Size = new System.Drawing.Size(900, 31);
            this.txtReportedProblem.TabIndex = 2;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNotes.Location = new System.Drawing.Point(29, 144);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(170, 20);
            this.lblNotes.TabIndex = 3;
            this.lblNotes.Text = "Intake notes (optional)";
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(32, 171);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(900, 60);
            this.txtNotes.TabIndex = 4;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.pnlFooter.Controls.Add(this.btnCreateRepair);
            this.pnlFooter.Location = new System.Drawing.Point(8, 805);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1100, 85);
            this.pnlFooter.TabIndex = 4;
            // 
            // btnCreateRepair
            // 
            this.btnCreateRepair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateRepair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnCreateRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateRepair.FlatAppearance.BorderSize = 0;
            this.btnCreateRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRepair.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateRepair.ForeColor = System.Drawing.Color.White;
            this.btnCreateRepair.Location = new System.Drawing.Point(875, 16);
            this.btnCreateRepair.Name = "btnCreateRepair";
            this.btnCreateRepair.Size = new System.Drawing.Size(225, 48);
            this.btnCreateRepair.TabIndex = 0;
            this.btnCreateRepair.Text = "Create Repair Order";
            this.btnCreateRepair.UseVisualStyleBackColor = false;
            // 
            // NewRepairOrderPage
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.pnlDevice);
            this.Controls.Add(this.pnlIssue);
            this.Controls.Add(this.pnlFooter);
            this.Name = "NewRepairOrderPage";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(1120, 900);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlDevice.ResumeLayout(false);
            this.pnlDevice.PerformLayout();
            this.pnlIssue.ResumeLayout(false);
            this.pnlIssue.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}