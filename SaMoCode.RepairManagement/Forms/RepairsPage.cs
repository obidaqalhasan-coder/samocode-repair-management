using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class RepairsPage : UserControl
    {
        private readonly RepairOrderService _repairOrderService;
        private List<RepairListItem> _all = new List<RepairListItem>();
        private readonly TextBox _search = new TextBox();
        private readonly ComboBox _status = new ComboBox();

        public RepairsPage() : this(true) { }
        internal RepairsPage(bool loadData)
        {
            InitializeComponent();

            _repairOrderService = new RepairOrderService();

            btnNewRepair.Click += btnNewRepair_Click;
            dgvRepairs.CellDoubleClick += dgvRepairs_CellDoubleClick;
            _search.Width=260;
            _status.Width=180;_status.DropDownStyle=ComboBoxStyle.DropDownList;
            _status.Items.AddRange(new object[]{"All statuses","Received","Diagnosis","Waiting Approval","Approved","In Repair","Ready for Pickup","Delivered"});_status.SelectedIndex=0;
            _search.TextChanged+=(s,e)=>Filter();_status.SelectedIndexChanged+=(s,e)=>Filter();
            var filters=new FlowLayoutPanel {AutoSize=true,WrapContents=true,Padding=Padding.Empty,Margin=Padding.Empty};
            filters.Controls.Add(new Label {Text="Search",AutoSize=true,Margin=new Padding(0,8,8,0)});
            filters.Controls.Add(_search);filters.Controls.Add(_status);
            var refresh=WorkflowDialog.Button("Refresh");refresh.Click+=(s,e)=>LoadRepairs();filters.Controls.Add(refresh);
            btnNewRepair.Text="+ New Repair";
            lblSubtitle.Text="Find a repair by order, customer, phone or device. Double-click to open.";
            PageLayout.Header(pnlHeader,lblTitle,lblSubtitle,btnNewRepair,filters);
            PageLayout.List(pnlList,lblRepairList,lblRepairCount,dgvRepairs);
            if(loadData) LoadRepairs();
        }

        private void LoadRepairs()
        {
            try
            {
                List<RepairListItem> repairs =
                    _repairOrderService.GetRepairList();

                dgvRepairs.AutoGenerateColumns = false;
                dgvRepairs.Columns.Clear();

                dgvRepairs.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Order",
                        DataPropertyName = "RepairOrderId",
                        FillWeight = 15
                    });

                dgvRepairs.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Customer",
                        DataPropertyName = "CustomerName",
                        FillWeight = 30
                    });

                dgvRepairs.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Phone",
                        DataPropertyName = "PhoneNumber",
                        FillWeight = 25
                    });

                dgvRepairs.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Device",
                        DataPropertyName = "DeviceName",
                        FillWeight = 30
                    });

                dgvRepairs.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Status",
                        DataPropertyName = "DeviceStatus",
                        FillWeight = 20
                    });

                dgvRepairs.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Created",
                        DataPropertyName = "CreatedAt",
                        FillWeight = 20,
                        DefaultCellStyle =
                        {
                            Format = "dd MMM yyyy"
                        }
                    });

                _all=repairs;
                dgvRepairs.DataSource = repairs;

                lblRepairCount.Text =
                    repairs.Count == 1
                        ? "1 repair"
                        : $"{repairs.Count} repairs";
                Filter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to load repairs",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Filter()
        {
            string query=_search.Text.Trim();string status=_status.Text;
            var rows=_all.Where(r=>(status=="All statuses"||r.DeviceStatus==status) &&
                (r.RepairOrderId.ToString().IndexOf(query,System.StringComparison.OrdinalIgnoreCase)>=0 ||
                 (r.CustomerName??"").IndexOf(query,System.StringComparison.OrdinalIgnoreCase)>=0 ||
                 (r.PhoneNumber??"").IndexOf(query,System.StringComparison.OrdinalIgnoreCase)>=0 ||
                 (r.DeviceName??"").IndexOf(query,System.StringComparison.OrdinalIgnoreCase)>=0)).ToList();
            dgvRepairs.DataSource=rows;lblRepairCount.Text=rows.Count+" device(s)";
        }
        private void btnNewRepair_Click(object sender, EventArgs e)
        {
            using(var dialog=new Form {Text="Receive devices",WindowState=FormWindowState.Maximized,
                StartPosition=FormStartPosition.CenterParent,MinimumSize=new System.Drawing.Size(1000,700)})
            {
                var page=new NewRepairOrderPage {Dock=DockStyle.Fill};
                page.RepairCreated+=(s,args)=>{dialog.DialogResult=DialogResult.OK;dialog.Close();};
                dialog.Controls.Add(page);dialog.ShowDialog(this);
            }
            LoadRepairs();
        }
        private void dgvRepairs_CellDoubleClick(
    object sender,
    DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            RepairListItem selectedRepair =
                dgvRepairs.Rows[e.RowIndex].DataBoundItem
                as RepairListItem;

            if (selectedRepair == null)
                return;

            OpenRepairDetails(
                selectedRepair.RepairOrderId,selectedRepair.DeviceId);
        }

        private void OpenRepairDetails(int repairOrderId,int deviceId)
        {
            RepairDetailsPage page =
                new RepairDetailsPage(repairOrderId,deviceId)
                {
                    Dock = DockStyle.Fill
                };

            Control parent = this.Parent;

            if (parent == null)
                return;

            parent.Controls.Clear();
            parent.Controls.Add(page);
        }

    }
}


