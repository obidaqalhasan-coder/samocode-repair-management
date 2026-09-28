using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    public partial class NewRepairOrderPage : UserControl
    {
        private readonly CustomerService _customerService = new CustomerService();
        private readonly MultiDeviceIntakeService _intake = new MultiDeviceIntakeService();
        private readonly List<IntakeDevice> _additional = new List<IntakeDevice>();
        private IntakeDevice _first = new IntakeDevice();
        private Button _review;
        public event EventHandler RepairCreated;
        public NewRepairOrderPage() : this(true) { }
        internal NewRepairOrderPage(bool loadData)
        {
            InitializeComponent();
            txtBrand.MaxLength=50; txtModel.MaxLength=100; txtColor.MaxLength=50; txtSerial.MaxLength=100; txtNotes.MaxLength=500;
            lblProblem.Text="Issues — one problem per line";
            txtReportedProblem.Multiline=true; txtReportedProblem.Height=50; txtReportedProblem.ScrollBars=ScrollBars.Vertical;
            var details=WorkflowDialog.Button("Conditions / accessories"); details.Location=new Point(650,168);
            details.Click+=(s,e)=>Guard(()=> {
                using(var form=new IntakeDeviceForm(CurrentFirst()))
                    if(form.ShowDialog(this)==DialogResult.OK) { _first=form.Device; ShowFirst(); }
            });
            pnlDevice.Controls.Add(details);
            var add=WorkflowDialog.Button("+ Additional device"); add.Location=new Point(0,16);
            add.Click+=(s,e)=>Guard(()=> {
                using(var form=new IntakeDeviceForm()) if(form.ShowDialog(this)==DialogResult.OK) { _additional.Add(form.Device); UpdateCount(); }
            });
            _review=WorkflowDialog.Button("Review devices (1)"); _review.Location=new Point(230,16);
            _review.Click+=(s,e)=>Review(); pnlFooter.Controls.Add(add); pnlFooter.Controls.Add(_review);
            var customer=WorkflowDialog.Button("+ New customer"); customer.Location=new Point(660,80);
            customer.Click+=(s,e)=> { using(var form=new CustomerForm()) if(form.ShowDialog(this)==DialogResult.OK) LoadCustomers(); };
            pnlCustomer.Controls.Add(customer);
            btnCreateRepair.Click+=Create;
            BuildIntakeLayout(details,add,customer);
            if(loadData)LoadCustomers();
        }
        private void BuildIntakeLayout(Button details,Button add,Button newCustomer)
        {
            SuspendLayout();Controls.Clear();Padding=new Padding(16);AutoScroll=true;
            var stack=PageLayout.Stack();stack.Padding=new Padding(0,0,16,20);
            lblTitle.AutoSize=true;lblSubtitle.AutoSize=true;stack.Controls.Add(lblTitle);stack.Controls.Add(lblSubtitle);
            var customer=PageLayout.Stack();customer.Padding=new Padding(18);customer.BackColor=Color.White;customer.Margin=new Padding(0,12,0,12);
            lblCustomerSection.AutoSize=true;customer.Controls.Add(lblCustomerSection);
            var customerRow=new TableLayoutPanel {AutoSize=true,Dock=DockStyle.Top,ColumnCount=2};
            customerRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));customerRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            cmbCustomer.Dock=DockStyle.Fill;cmbCustomer.Margin=new Padding(0,8,12,8);newCustomer.Anchor=AnchorStyles.Right;
            customerRow.Controls.Add(cmbCustomer,0,0);customerRow.Controls.Add(newCustomer,1,0);customer.Controls.Add(customerRow);stack.Controls.Add(customer);
            var device=new TableLayoutPanel {AutoSize=true,Dock=DockStyle.Top,ColumnCount=2,BackColor=Color.White,Padding=new Padding(18),Margin=new Padding(0,0,0,12)};
            device.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));device.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            lblDeviceSection.Text="First device";lblDeviceSection.AutoSize=true;device.Controls.Add(lblDeviceSection,0,0);device.SetColumnSpan(lblDeviceSection,2);
            AddField(device,lblBrand,txtBrand,0,1);AddField(device,lblModel,txtModel,1,1);
            AddField(device,lblColor,txtColor,0,3);AddField(device,lblSerial,txtSerial,1,3);
            details.Anchor=AnchorStyles.Left;details.Margin=new Padding(0,10,0,0);device.Controls.Add(details,0,5);device.SetColumnSpan(details,2);stack.Controls.Add(device);
            var issues=PageLayout.Stack();issues.Padding=new Padding(18);issues.BackColor=Color.White;
            lblProblem.AutoSize=true;lblNotes.AutoSize=true;lblNotes.Text="Order notes (optional)";
            txtReportedProblem.Dock=DockStyle.Top;txtReportedProblem.Height=80;txtNotes.Dock=DockStyle.Top;txtNotes.Height=70;
            issues.Controls.Add(lblProblem);issues.Controls.Add(txtReportedProblem);issues.Controls.Add(lblNotes);issues.Controls.Add(txtNotes);stack.Controls.Add(issues);
            var footer=new FlowLayoutPanel {AutoSize=true,Dock=DockStyle.Top,WrapContents=true,Padding=new Padding(0,12,0,0)};
            foreach(var button in new[]{add,_review,btnCreateRepair}){button.Anchor=AnchorStyles.None;button.Margin=new Padding(0,4,12,4);footer.Controls.Add(button);}
            AutoScroll=false;
            var scroll=new Panel {Dock=DockStyle.Fill,AutoScroll=true};scroll.Controls.Add(stack);
            footer.Dock=DockStyle.Bottom;footer.AutoSizeMode=AutoSizeMode.GrowAndShrink;
            Controls.Add(scroll);Controls.Add(footer);
            pnlHeader.Dispose();pnlCustomer.Dispose();pnlDevice.Dispose();pnlIssue.Dispose();pnlFooter.Dispose();ResumeLayout(true);
        }
        private static void AddField(TableLayoutPanel layout,Label label,Control input,int column,int row)
        {
            label.AutoSize=true;label.Dock=DockStyle.Fill;label.Margin=new Padding(0,12,12,6);
            input.Dock=DockStyle.Fill;input.Margin=new Padding(0,0,12,0);
            layout.Controls.Add(label,column,row);layout.Controls.Add(input,column,row+1);
        }        private void Guard(Action action)
        { try { action(); } catch(Exception ex) { MessageBox.Show(this,ex.Message,"Repair intake",MessageBoxButtons.OK,MessageBoxIcon.Warning); } }
        private void LoadCustomers()
        {
            Guard(()=> {
                cmbCustomer.DisplayMember="Display"; cmbCustomer.ValueMember="CustomerId";
                cmbCustomer.DataSource=_customerService.GetAllCustomers().Select(c=>new {c.CustomerId,Display=c.Name+" — "+c.PhoneNumber}).ToList();
                cmbCustomer.SelectedIndex=-1;
            });
        }
        private IntakeDevice CurrentFirst()
        {
            return new IntakeDevice {Brand=txtBrand.Text,Model=txtModel.Text,Color=txtColor.Text,SerialNumber=txtSerial.Text,
                Problems=txtReportedProblem.Lines.Where(p=>!string.IsNullOrWhiteSpace(p)).Select(p=>p.Trim()).ToList(),
                ConditionNotes=_first.ConditionNotes,ConditionIds=new List<int>(_first.ConditionIds),AccessoryIds=new List<int>(_first.AccessoryIds),OtherAccessory=_first.OtherAccessory};
        }
        private void ShowFirst()
        {
            txtBrand.Text=_first.Brand; txtModel.Text=_first.Model; txtColor.Text=_first.Color; txtSerial.Text=_first.SerialNumber;
            txtReportedProblem.Lines=_first.Problems.ToArray();
        }
        private void UpdateCount() { _review.Text="Review devices ("+(_additional.Count+1)+")"; }
        private void Review()
        {
            Guard(()=> {
                using(var dialog=new WorkflowDialog("Devices in this order","Done"))
                {
                    var list=new ListBox {Height=220,DisplayMember="DisplayName"};
                    Action refresh=()=> { list.DataSource=null; list.DataSource=new[]{CurrentFirst()}.Concat(_additional).ToList(); };
                    refresh(); dialog.Add("Select a device to edit. The first device is also shown on the intake page.",list);
                    var edit=WorkflowDialog.Button("Edit selected"); var remove=WorkflowDialog.Button("Remove additional device");
                    edit.Click+=(s,e)=>Guard(()=> {
                        if(list.SelectedIndex<0)return; int index=list.SelectedIndex;
                        using(var form=new IntakeDeviceForm((IntakeDevice)list.SelectedItem))
                            if(form.ShowDialog(dialog)==DialogResult.OK) { if(index==0){_first=form.Device;ShowFirst();}else _additional[index-1]=form.Device; refresh(); }
                    });
                    remove.Click+=(s,e)=> {if(list.SelectedIndex>0){_additional.RemoveAt(list.SelectedIndex-1);refresh();UpdateCount();}};
                    dialog.Add("",edit);dialog.Add("",remove);dialog.OnSave(()=>{});dialog.ShowDialog(this);
                }
            });
        }
        private void Create(object sender,EventArgs e)
        {
            btnCreateRepair.Enabled=false;
            try
            {
                if(cmbCustomer.SelectedValue==null)throw new ArgumentException("Select a customer.");
                int order=_intake.Create((int)cmbCustomer.SelectedValue,new[]{CurrentFirst()}.Concat(_additional).ToList(),txtNotes.Text);
                // Clear only after the entire order has committed successfully.
                _additional.Clear();_first=new IntakeDevice();ShowFirst();txtNotes.Clear();UpdateCount();
                if(RepairCreated!=null){RepairCreated(this,EventArgs.Empty);return;}
                var parent=Parent;
                if(parent!=null){parent.Controls.Clear();parent.Controls.Add(new RepairDetailsPage(order){Dock=DockStyle.Fill});Dispose();}
            }
            catch(Exception ex){MessageBox.Show(this,ex.Message,"Unable to create repair",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
            finally{if(!IsDisposed)btnCreateRepair.Enabled=true;}
        }
    }
}




