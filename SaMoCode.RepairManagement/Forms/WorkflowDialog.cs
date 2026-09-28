using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SaMoCode.RepairManagement.Forms
{
    internal class WorkflowDialog : Form
    {
        private readonly TableLayoutPanel _fields;
        private readonly Button _save;
        public WorkflowDialog(string title, string action = "Save")
        {
            Text=title; Font=new Font("Segoe UI",10F); BackColor=Color.White;
            AutoScaleMode=AutoScaleMode.Font; ClientSize=new Size(700,620);
            MinimumSize=new Size(600,420); StartPosition=FormStartPosition.CenterParent;
            MaximizeBox=false; MinimizeBox=false; ShowInTaskbar=false;
            var header=new Label {Text=title,Dock=DockStyle.Top,Height=76,Padding=new Padding(24,20,0,0),
                Font=new Font("Segoe UI",18F,FontStyle.Bold),ForeColor=Color.FromArgb(15,23,42),BackColor=Color.FromArgb(248,250,252)};
            var scroll=new Panel {Dock=DockStyle.Fill,AutoScroll=true,Padding=new Padding(24,12,24,12)};
            _fields=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=1,Padding=new Padding(0,0,10,12)};
            _fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            scroll.Controls.Add(_fields);
            var footer=new FlowLayoutPanel {Dock=DockStyle.Bottom,Height=76,FlowDirection=FlowDirection.RightToLeft,
                Padding=new Padding(16),BackColor=Color.FromArgb(248,250,252)};
            _save=Button(action); var cancel=Button("Cancel"); cancel.DialogResult=DialogResult.Cancel;
            footer.Controls.Add(_save); footer.Controls.Add(cancel);
            Controls.Add(scroll); Controls.Add(footer); Controls.Add(header);
            AcceptButton=_save; CancelButton=cancel;
        }
        public static Button Button(string caption)
        {
            var button=new Button {Text=caption,AutoSize=true,MinimumSize=new Size(110,38),FlatStyle=FlatStyle.Flat,
                BackColor=Color.FromArgb(37,99,235),ForeColor=Color.White,Padding=new Padding(10,3,10,3),Cursor=Cursors.Hand};
            button.FlatAppearance.BorderSize=0; return button;
        }
        public void Add(string caption,Control field)
        {
            if (!string.IsNullOrEmpty(caption))
                _fields.Controls.Add(new Label {Text=caption,AutoSize=true,Margin=new Padding(0,12,0,6),ForeColor=Color.FromArgb(71,85,105)});
            field.Dock=DockStyle.Top; field.Margin=new Padding(0,0,0,6); _fields.Controls.Add(field);
        }
        public TextBox Input(string caption,string value=null,int maximum=500,bool multiline=false)
        {
            var box=new TextBox {Text=value??"",MaxLength=maximum,Multiline=multiline,Height=multiline?80:30,
                ScrollBars=multiline?ScrollBars.Vertical:ScrollBars.None,BorderStyle=BorderStyle.FixedSingle}; Add(caption,box); return box;
        }
        public NumericUpDown Amount(string caption,decimal value=0)
        {
            var field=new NumericUpDown {DecimalPlaces=2,Maximum=99999999.99m,Minimum=0,Value=Math.Max(0,Math.Min(99999999.99m,value)),ThousandsSeparator=true};
            Add(caption,field); return field;
        }
        public ComboBox Choice(string caption,params string[] values)
        {
            var box=new ComboBox {DropDownStyle=ComboBoxStyle.DropDownList}; box.Items.AddRange(values); box.SelectedIndex=-1; Add(caption,box); return box;
        }
        public void OnSave(Action save)
        {
            _save.Click+=(s,e)=> {
                _save.Enabled=false;
                try { save(); DialogResult=DialogResult.OK; Close(); }
                catch(Exception ex) { MessageBox.Show(this,ex.Message,"Unable to save",MessageBoxButtons.OK,MessageBoxIcon.Warning); _save.Enabled=true; }
            };
        }
        public static void ShowTable(IWin32Window owner,string title,DataTable table)
        {
            using(var form=new Form {Text=title,Size=new Size(1000,600),StartPosition=FormStartPosition.CenterParent,Font=new Font("Segoe UI",10F)})
            {
                var grid=Grid(); grid.Dock=DockStyle.Fill; grid.DataSource=table;
                form.Controls.Add(grid); form.ShowDialog(owner);
            }
        }
        public static DataGridView Grid()
        {
            var grid=new DataGridView {ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,RowHeadersVisible=false,
                BackgroundColor=Color.White,BorderStyle=BorderStyle.None,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,EnableHeadersVisualStyles=false};
            grid.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
            grid.DefaultCellStyle.SelectionBackColor=Color.FromArgb(219,234,254);
            grid.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42); grid.RowTemplate.Height=32;
            return grid;
        }
    }
}
