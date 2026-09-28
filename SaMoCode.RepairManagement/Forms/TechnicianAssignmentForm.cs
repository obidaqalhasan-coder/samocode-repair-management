using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    public class TechnicianAssignmentForm : Form
    {
        private readonly int _deviceId;
        private readonly TechnicianAssignment _current;
        private readonly TechnicianAssignmentService _service;
        private readonly ComboBox _technicians;
        private readonly TextBox _reason;
        private readonly Button _save;

        public TechnicianAssignmentForm(int deviceId, TechnicianAssignment current)
        {
            _deviceId = deviceId;
            _current = current;
            _service = new TechnicianAssignmentService();
            bool transfer = current != null;
            Text = transfer ? "Transfer Technician" : "Assign Technician";
            Font = new Font("Segoe UI", 9F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(570, 440);
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            Panel header = new Panel { Dock = DockStyle.Top, Height = 110,
                BackColor = Color.FromArgb(248, 250, 252) };
            header.Controls.Add(new Label { Text = Text, AutoSize = true,
                Location = new Point(30, 20), Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42) });
            header.Controls.Add(new Label { Text = transfer
                    ? "Current technician: " + current.TechnicianName
                    : "Choose an active technician for this device.",
                AutoEllipsis = true, Location = new Point(32, 72), Size = new Size(505, 25),
                ForeColor = Color.FromArgb(100, 116, 139) });
            Controls.Add(header);

            Controls.Add(new Label { Text = "Technician", AutoSize = true,
                Location = new Point(32, 130), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            _technicians = new ComboBox { Location = new Point(35, 157), Size = new Size(500, 30),
                DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11F),
                DisplayMember = "DisplayName", ValueMember = "TechnicianId", TabIndex = 0 };
            Controls.Add(_technicians);
            Controls.Add(new Label { Text = "Transfer reason (required)", AutoSize = true,
                Location = new Point(32, 208), Visible = transfer,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            _reason = new TextBox { Location = new Point(35, 237), Size = new Size(500, 80),
                Multiline = true, MaxLength = 300, Visible = transfer,
                ScrollBars = ScrollBars.Vertical, BorderStyle = BorderStyle.FixedSingle, TabIndex = 1 };
            Controls.Add(_reason);

            Panel footer = new Panel { Dock = DockStyle.Bottom, Height = 90,
                BackColor = Color.FromArgb(248, 250, 252) };
            Button cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel,
                Location = new Point(315, 22), Size = new Size(105, 44),
                FlatStyle = FlatStyle.Flat, BackColor = Color.White, TabIndex = 1 };
            cancel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _save = new Button { Text = transfer ? "Transfer" : "Assign",
                Location = new Point(430, 22), Size = new Size(105, 44),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), TabIndex = 0 };
            _save.FlatAppearance.BorderSize = 0;
            _save.Click += Save_Click;
            footer.Controls.Add(cancel);
            footer.Controls.Add(_save);
            Controls.Add(footer);
            AcceptButton = _save;
            CancelButton = cancel;
            Load += LoadTechnicians;
        }

        private void LoadTechnicians(object sender, EventArgs e)
        {
            try
            {
                var choices = new TechnicianService().GetActiveTechnicians()
                    .Where(t => _current == null || t.TechnicianId != _current.TechnicianId)
                    .Select(t => new { t.TechnicianId, DisplayName = t.Name +
                        (string.IsNullOrWhiteSpace(t.Specialization) ? "" : " — " + t.Specialization) })
                    .ToList();
                _technicians.DataSource = choices;
                _technicians.SelectedIndex = -1;
                _save.Enabled = choices.Count > 0;
                if (choices.Count == 0)
                    MessageBox.Show(this, "No eligible active technicians are available. Add a technician from the Technicians page first.",
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _save.Enabled = false;
                MessageBox.Show(this, ex.Message, "Unable to load technicians",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (_technicians.SelectedValue == null)
            {
                MessageBox.Show(this, "Please select a technician.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _save.Enabled = false;
            try
            {
                int technicianId = (int)_technicians.SelectedValue;
                if (_current == null)
                    _service.Assign(_deviceId, technicianId);
                else
                    _service.Transfer(_deviceId, technicianId, _reason.Text,
                        _current.TechnicianAssignmentId);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Unable to save assignment",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _save.Enabled = true;
            }
        }
    }
}
