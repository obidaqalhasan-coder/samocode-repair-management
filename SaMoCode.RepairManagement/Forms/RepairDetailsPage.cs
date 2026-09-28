using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class RepairDetailsPage : UserControl
    {
        private readonly int _repairOrderId;
        private readonly RepairDetailsService _details=new RepairDetailsService();
        private readonly RepairWorkflowService _workflow=new RepairWorkflowService();
        private readonly TechnicianAssignmentService _assignments=new TechnicianAssignmentService();
        private readonly ComboBox _devices=new ComboBox();
        private readonly DataGridView _timeline=WorkflowDialog.Grid();
        private readonly DataGridView _work=WorkflowDialog.Grid();
        private readonly DataGridView _intake=WorkflowDialog.Grid();
        private readonly Button _primary=WorkflowDialog.Button("Continue");
        private readonly Button _more=WorkflowDialog.Button("More options");
        private readonly Label _nextTitle=new Label {AutoSize=true,Font=new Font("Segoe UI",12F,FontStyle.Bold)};
        private readonly Label _nextText=new Label {AutoSize=true,Dock=DockStyle.Fill};
        private readonly ContextMenuStrip _options=new ContextMenuStrip();
        private readonly List<WorkflowIssue> _issueStates=new List<WorkflowIssue>();
        private RepairGuidance _guidance;
        private bool _loaded;
        private string _deviceStatus;
        private int? _deviceId;
        private bool _loading;
        private TechnicianAssignment _current;
        public RepairDetailsPage(int repairOrderId) : this(repairOrderId,true) { }
        public RepairDetailsPage(int repairOrderId,int deviceId) : this(repairOrderId,false) { _deviceId=deviceId;Reload(); }
        internal RepairDetailsPage(int repairOrderId,bool loadData)
        {
            InitializeComponent();_repairOrderId=repairOrderId;
            components=components??new System.ComponentModel.Container();components.Add(_options);
            btnBack.Click+=(s,e)=>Navigate(new RepairsPage());
            btnAssignTechnician.Click+=(s,e)=>Guard(AssignTechnician);
            _primary.Click+=(s,e)=>Guard(ContinueWorkflow);
            _more.Click+=(s,e)=>_options.Show(_more,new Point(0,_more.Height));
            _devices.DropDownStyle=ComboBoxStyle.DropDownList;_devices.DisplayMember="DisplayName";
            _devices.SelectedIndexChanged+=(s,e)=>{if(!_loading)LoadDevice();};
            dgvIssues.SelectionChanged+=(s,e)=>UpdateGuidance();
            BuildGuidedLayout();
            if(loadData)Reload();
        }
        private static void AddTab(TabControl tabs,string title,Control control)
        {var page=new TabPage(title);control.Dock=DockStyle.Fill;page.Controls.Add(control);tabs.TabPages.Add(page);}
        private static void InStack(TableLayoutPanel stack,Control control)
        {control.Dock=DockStyle.Fill;control.Margin=new Padding(0,3,0,6);stack.Controls.Add(control);}
        private void BuildGuidedLayout()
        {
            SuspendLayout();Controls.Clear();Padding=new Padding(12);AutoScroll=true;
            var root=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=5,MinimumSize=new Size(0,650)};
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            for(int i=0;i<4;i++)root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
            var topButtons=new FlowLayoutPanel {AutoSize=true,FlowDirection=FlowDirection.RightToLeft,WrapContents=false};
            btnBack.AutoSize=true;btnBack.Anchor=AnchorStyles.None;topButtons.Controls.Add(btnBack);
            var refresh=WorkflowDialog.Button("Refresh");refresh.Click+=(s,e)=>Reload();topButtons.Controls.Add(refresh);
            var top=new TableLayoutPanel {AutoSize=true,Dock=DockStyle.Fill,ColumnCount=2};
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));top.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            var headings=PageLayout.Stack();InStack(headings,lblTitle);InStack(headings,lblCreated);top.Controls.Add(headings,0,0);top.Controls.Add(topButtons,1,0);root.Controls.Add(top,0,0);
            var overview=new TableLayoutPanel {AutoSize=true,Dock=DockStyle.Fill,ColumnCount=2,Margin=new Padding(0,8,0,8)};
            overview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));overview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            var customer=PageLayout.Stack();customer.BackColor=Color.White;customer.Padding=new Padding(16);
            InStack(customer,lblCustomerSection);InStack(customer,lblCustomerName);InStack(customer,lblCustomerPhone);
            var device=PageLayout.Stack();device.BackColor=Color.White;device.Padding=new Padding(16);
            InStack(device,lblDeviceSection);InStack(device,_devices);InStack(device,lblDeviceStatus);InStack(device,lblDeviceDetails);
            lblCustomerName.AutoSize=false;lblCustomerName.Height=32;lblCustomerName.AutoEllipsis=true;
            lblDeviceDetails.AutoSize=false;lblDeviceDetails.Height=28;lblDeviceDetails.AutoEllipsis=true;
            overview.Controls.Add(customer,0,0);overview.Controls.Add(device,1,0);root.Controls.Add(overview,0,1);
            var custody=new TableLayoutPanel {Dock=DockStyle.Fill,AutoSize=true,ColumnCount=2,BackColor=Color.White,Padding=new Padding(16,8,16,8)};
            custody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));custody.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            var custodyText=PageLayout.Stack();InStack(custodyText,lblTechnicianSection);InStack(custodyText,lblCurrentTechnician);InStack(custodyText,lblAssignedAt);
            lblCurrentTechnician.AutoSize=false;lblCurrentTechnician.Height=30;lblCurrentTechnician.AutoEllipsis=true;
            lblAssignedAt.AutoSize=true;btnAssignTechnician.AutoSize=true;btnAssignTechnician.Anchor=AnchorStyles.Right;
            custody.Controls.Add(custodyText,0,0);custody.Controls.Add(btnAssignTechnician,1,0);root.Controls.Add(custody,0,2);
            var next=PageLayout.Stack();next.BackColor=Color.FromArgb(239,246,255);next.Padding=new Padding(16);next.Margin=new Padding(0,8,0,8);
            InStack(next,_nextTitle);InStack(next,_nextText);
            var actions=new FlowLayoutPanel {AutoSize=true,Dock=DockStyle.Fill};actions.Controls.Add(_primary);actions.Controls.Add(_more);InStack(next,actions);root.Controls.Add(next,0,3);
            var tabs=new TabControl {Dock=DockStyle.Fill,MinimumSize=new Size(0,180),Font=new Font("Segoe UI",9.5F)};
            dgvIssues.AutoGenerateColumns=true;dgvIssues.Columns.Clear();dgvIssues.MultiSelect=false;
            AddTab(tabs,"Issues — select one to continue",dgvIssues);AddTab(tabs,"Timeline / custody",_timeline);AddTab(tabs,"Work & parts",_work);AddTab(tabs,"Intake / accessories",_intake);
            root.Controls.Add(tabs,0,4);Controls.Add(root);_primary.Visible=false;_more.Visible=false;btnAssignTechnician.Visible=false;
            // Designer panels no longer own visible controls; dispose the empty containers.
            pnlHeader.Dispose();pnlCustomer.Dispose();pnlDevice.Dispose();pnlTechnician.Dispose();pnlIssues.Dispose();
            ResumeLayout(true);
        }
        private void AssignTechnician()
        {
            if(!_deviceId.HasValue)return;
            _current=_assignments.GetCurrentAssignment(_deviceId.Value);
            using(var form=new TechnicianAssignmentForm(_deviceId.Value,_current))form.ShowDialog(this);
            Reload();
        }
        private void ContinueWorkflow()
        {
            if(!_loaded||_guidance==null)return;
            switch(_guidance.Action)
            {
                case RepairAction.Assign:AssignTechnician();break;
                case RepairAction.AddIssue:IssueAction("Add");break;
                case RepairAction.Diagnose:IssueAction("Diagnosis & Quote");break;
                case RepairAction.Contact:IssueAction("Contact");break;
                case RepairAction.Start:IssueAction("Start");break;
                case RepairAction.Complete:IssueAction("Complete");break;
                case RepairAction.Ready:_workflow.Ready(_deviceId.Value);Reload();break;
                case RepairAction.Deliver:Delivery();break;
                case RepairAction.SelectIssue:
                    foreach(DataGridViewRow row in dgvIssues.Rows)
                        if((int)((DataRowView)row.DataBoundItem)["Id"]==_guidance.NextIssueId){dgvIssues.CurrentCell=row.Cells["Problem"];break;}
                    break;
            }
        }
        private void UpdateGuidance()
        {
            if(!_loaded||_loading)return;
            var row=dgvIssues.CurrentRow==null?null:dgvIssues.CurrentRow.DataBoundItem as DataRowView;
            int? selected=row==null?(int?)null:(int)row["Id"];
            _guidance=RepairGuidanceService.Next(_deviceStatus,_current!=null,_issueStates,selected);
            _nextTitle.Text=_guidance.Action==RepairAction.None?"Repair complete":"Next: "+_guidance.Caption;
            _nextText.Text=_guidance.Explanation;_primary.Text=_guidance.Caption;
            _primary.Visible=_guidance.Action!=RepairAction.None;_primary.Enabled=true;
            btnAssignTechnician.Visible=_current!=null&&_deviceStatus!="Delivered"&&_deviceStatus!="Ready for Pickup";
            btnAssignTechnician.Text="Transfer Technician";btnAssignTechnician.Enabled=true;
            _options.Items.Clear();
            if(_deviceStatus!="Delivered")
            {
                _options.Items.Add("Add another issue",null,(s,e)=>Guard(()=>IssueAction("Add")));
                if(_guidance.CanReviseQuote)_options.Items.Add("Revise quote / request new approval",null,(s,e)=>Guard(()=>IssueAction("Quote")));
            }
            _more.Visible=_options.Items.Count>0;
        }        private void Navigate(UserControl page)
        {var parent=Parent;if(parent==null){page.Dispose();return;}parent.Controls.Clear();page.Dock=DockStyle.Fill;parent.Controls.Add(page);Dispose();}
        private void Guard(Action action)
        {try{action();}catch(Exception ex){MessageBox.Show(this,ex.Message,"Repair management",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
        private void Reload()
        {
            _loaded=false;_primary.Enabled=false;_more.Visible=false;btnAssignTechnician.Enabled=false;
            _nextTitle.Text="Loading repair";_nextText.Text="If loading fails, use Refresh to try again.";
            Guard(()=> {
                var repair=_details.GetRepairDetails(_repairOrderId);
                lblTitle.Text="Repair Order #"+repair.RepairOrderId;
                lblCreated.Text=$"Created {repair.CreatedAt:dd MMM yyyy - hh:mm tt} • {repair.OrderStatus}";
                lblCustomerName.Text=repair.CustomerName;lblCustomerPhone.Text=repair.PhoneNumber;
                int previous=_deviceId??0;
                _loading=true;
                try { _devices.DataSource=repair.Devices; _devices.SelectedItem=repair.Devices.FirstOrDefault(d=>d.DeviceId==previous)??repair.Devices.FirstOrDefault(); }
                finally{_loading=false;}
                LoadDevice();
            });
        }
        private void LoadDevice()
        {
            _loaded=false;_primary.Enabled=false;_more.Visible=false;btnAssignTechnician.Enabled=false;
            _nextTitle.Text="Loading repair";_nextText.Text="If loading fails, use Refresh to try again.";_deviceId=null;
            Guard(()=> {
                var device=_devices.SelectedItem as RepairDeviceDetails;if(device==null)return;
                _deviceId=device.DeviceId;lblDeviceStatus.Text=device.Status;
                lblDeviceDetails.Text=(string.IsNullOrWhiteSpace(device.Color)?"No color":device.Color)+" • "+(string.IsNullOrWhiteSpace(device.SerialNumber)?"No serial / IMEI":device.SerialNumber);
                _current=_assignments.GetCurrentAssignment(device.DeviceId);
                lblCurrentTechnician.Text=_current==null?(device.Status=="Delivered"?"Delivered to customer":device.Status=="Ready for Pickup"?"At reception":"Not assigned"):_current.TechnicianName;
                lblAssignedAt.Text=_current==null?"":$"Assigned {_current.AssignedAt:dd MMM yyyy - hh:mm tt}";
                btnAssignTechnician.Text=_current==null?"Assign Technician":"Transfer Technician";
                var oldSelection=dgvIssues.CurrentRow==null?null:dgvIssues.CurrentRow.DataBoundItem as DataRowView;
                int priorIssue=oldSelection==null?0:(int)oldSelection["Id"];
                dgvIssues.DataSource=_workflow.Issues(device.DeviceId);
                foreach(string column in new[]{"Id","DeviceId","QuoteVersion","Notes","WorkDone","Diagnosis","Minimum","Maximum"}) if(dgvIssues.Columns.Contains(column))dgvIssues.Columns[column].Visible=false;
                _timeline.DataSource=_workflow.Timeline(device.DeviceId);_work.DataSource=_workflow.Work(device.DeviceId);_intake.DataSource=_workflow.IntakeDetails(device.DeviceId);
                _deviceStatus=device.Status;
                _issueStates.Clear();
                foreach(DataRow r in ((DataTable)dgvIssues.DataSource).Rows)
                    _issueStates.Add(new WorkflowIssue {Id=(int)r["Id"],Problem=(string)r["Problem"],Diagnosis=r["Diagnosis"] as string,Status=(string)r["Status"],
                        Minimum=r.Field<decimal?>("Minimum"),Maximum=r.Field<decimal?>("Maximum"),Approval=(string)r["Approval"]});
                var preferred=_issueStates.FirstOrDefault(i=>i.Id==priorIssue&&!RepairWorkflowService.Terminal(i.Status))??_issueStates.FirstOrDefault(i=>!RepairWorkflowService.Terminal(i.Status));
                if(preferred!=null)
                    foreach(DataGridViewRow row in dgvIssues.Rows)
                        if((int)((DataRowView)row.DataBoundItem)["Id"]==preferred.Id){dgvIssues.CurrentCell=row.Cells["Problem"];break;}
                _loaded=true;UpdateGuidance();
                btnAssignTechnician.Enabled=device.Status!="Delivered"&&device.Status!="Ready for Pickup";
            });
        }
        private void IssueAction(string action)
        {
            int device=_deviceId.Value;
            DataRowView selected=dgvIssues.CurrentRow==null?null:dgvIssues.CurrentRow.DataBoundItem as DataRowView;
            if(action!="Add"&&selected==null)throw new ArgumentException("Select an issue first.");
            int issue=selected==null?0:(int)selected["Id"];
            if(action=="Start") {_workflow.StartIssue(device,issue);Reload();return;}
            using(var dialog=new WorkflowDialog(action=="Add"?"Add issue":action+" — Issue #"+issue))
            {
                if(selected!=null)dialog.Add("Issue",new Label {Text=(string)selected["Problem"],AutoSize=true,MaximumSize=new Size(620,0)});
                if(action=="Add"||action=="Diagnosis"||action=="No repair")
                {
                    var text=dialog.Input(action=="Add"?"Reported problem":action=="Diagnosis"?"Diagnosis":"Reason",
                        action=="Diagnosis"?selected["Diagnosis"] as string:null,500,true);
                    dialog.OnSave(()=> {if(action=="Add")_workflow.AddIssue(device,text.Text);else if(action=="Diagnosis")_workflow.DiagnoseIssue(device,issue,text.Text);else _workflow.NoRepairIssue(device,issue,text.Text);});
                }
                else if(action=="Diagnosis & Quote")
                {
                    var diagnosis=dialog.Input("Diagnosis",selected["Diagnosis"] as string,500,true);
                    var noRepair=new CheckBox {Text="No repair is required for this issue",AutoSize=true};dialog.Add("",noRepair);
                    var min=dialog.Amount("Quote minimum (AED)",selected.Row.Field<decimal?>("Minimum")??0);
                    var max=dialog.Amount("Quote maximum (AED)",selected.Row.Field<decimal?>("Maximum")??0);
                    noRepair.CheckedChanged+=(s,e)=>{min.Enabled=max.Enabled=!noRepair.Checked;};
                    dialog.OnSave(()=>_workflow.DiagnoseAndQuote(device,issue,diagnosis.Text,min.Value,max.Value,noRepair.Checked));
                }
                else if(action=="Quote")
                {
                    dialog.Add("Reception pricing",new Label {Text="For a fixed price, enter the same minimum and maximum. A changed quote needs new approval.",AutoSize=true,MaximumSize=new Size(620,0)});
                    var min=dialog.Amount("Minimum (AED)",selected.Row.Field<decimal?>("Minimum")??0);
                    var max=dialog.Amount("Maximum (AED)",selected.Row.Field<decimal?>("Maximum")??0);
                    dialog.OnSave(()=>_workflow.QuoteIssue(device,issue,min.Value,max.Value));
                }
                else if(action=="Contact")
                {
                    dialog.Add("Quote discussed",new Label {Text=string.Format("{0}–{1} AED / version {2}",selected["Minimum"],selected["Maximum"],selected["QuoteVersion"]),AutoSize=true});
                    var method=dialog.Choice("Contact method","Phone","In person","WhatsApp");
                    var result=dialog.Choice("Customer response","Approved","Rejected","No Answer");
                    var notes=dialog.Input("Contact notes",null,500,true); int version=(int)selected["QuoteVersion"];
                    dialog.OnSave(()=>_workflow.ContactCustomer(device,issue,method.Text,result.Text,notes.Text,version));
                }
                else if(action=="Complete")
                {
                    var technician=new ComboBox {DropDownStyle=ComboBoxStyle.DropDownList,DisplayMember="Name",ValueMember="TechnicianId",DataSource=new TechnicianService().GetActiveTechnicians()};
                    technician.SelectedIndex=-1;dialog.Add("Technician who performed this work",technician);
                    var text=dialog.Input("Work done",null,500,true);
                    var price=dialog.Amount("Final price (AED)",selected.Row.Field<decimal?>("Minimum")??0);
                    var parts=new DataGridView {Height=160,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,RowHeadersVisible=false,BackgroundColor=Color.White};
                    parts.Columns.Add("Part","Part used");parts.Columns.Add("Quantity","Quantity");dialog.Add("Parts used (optional)",parts);
                    dialog.OnSave(()=> {
                        parts.EndEdit();var used=new List<UsedPart>();
                        foreach(DataGridViewRow row in parts.Rows)
                        {
                            if(row.IsNewRow)continue;
                            string name=Convert.ToString(row.Cells[0].Value);string qty=Convert.ToString(row.Cells[1].Value);
                            if(string.IsNullOrWhiteSpace(name)&&string.IsNullOrWhiteSpace(qty))continue;
                            decimal quantity;if(!decimal.TryParse(qty,out quantity))throw new ArgumentException("Enter a valid quantity for each part.");
                            used.Add(new UsedPart {Name=name,Quantity=quantity});
                        }
                        _workflow.CompleteIssue(device,issue,technician.SelectedValue==null?0:(int)technician.SelectedValue,text.Text,price.Value,used);
                    });
                }
                if(dialog.ShowDialog(this)==DialogResult.OK)Reload();
            }
        }
        private void Delivery()
        {
            int device=_deviceId.Value;decimal amount=_workflow.AmountDue(device);
            using(var dialog=new WorkflowDialog("Deliver device","Record delivery"))
            {
                dialog.Add("Amount to collect",new Label {Text=amount.ToString("N2")+" AED",AutoSize=true,Font=new Font("Segoe UI",20F,FontStyle.Bold)});
                dialog.Add("",new Label {Text="Confirm the customer has paid and received this device. This records payment; it does not charge a card.",AutoSize=true,MaximumSize=new Size(620,0)});
                var method=amount==0?dialog.Choice("Payment method","No charge"):dialog.Choice("Payment method","Cash","Visa");
                var confirmed=new CheckBox {Text="Payment and device handover are complete",AutoSize=true};dialog.Add("",confirmed);
                dialog.OnSave(()=> {if(!confirmed.Checked)throw new ArgumentException("Confirm payment and handover first.");_workflow.Deliver(device,method.Text,amount);});
                if(dialog.ShowDialog(this)==DialogResult.OK)Reload();
            }
        }
    }
}



