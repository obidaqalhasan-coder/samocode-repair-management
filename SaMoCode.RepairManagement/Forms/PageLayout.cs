using System.Drawing;
using System.Windows.Forms;
namespace SaMoCode.RepairManagement.Forms
{
    internal static class PageLayout
    {
        internal static TableLayoutPanel Stack()
        {
            var table=new TableLayoutPanel {AutoSize=true,AutoSizeMode=AutoSizeMode.GrowAndShrink,Dock=DockStyle.Top,ColumnCount=1,Margin=Padding.Empty};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));return table;
        }
        internal static void Header(Panel panel,Label title,Label subtitle,Button action,Control filters=null)
        {
            panel.Controls.Clear();panel.Dock=DockStyle.Top;panel.AutoSize=true;panel.AutoSizeMode=AutoSizeMode.GrowAndShrink;
            var table=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=2,Padding=new Padding(4,6,4,12)};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            title.AutoSize=true;title.Anchor=AnchorStyles.Left;title.Margin=new Padding(0,0,10,6);
            subtitle.AutoSize=true;subtitle.Dock=DockStyle.Fill;subtitle.Margin=new Padding(0,0,8,6);
            action.AutoSize=true;action.Anchor=AnchorStyles.Right;action.Margin=new Padding(8);action.MinimumSize=new Size(160,44);
            table.Controls.Add(title,0,0);table.Controls.Add(action,1,0);table.Controls.Add(subtitle,0,1);table.SetColumnSpan(subtitle,2);
            if(filters!=null){filters.Dock=DockStyle.Fill;table.Controls.Add(filters,0,2);table.SetColumnSpan(filters,2);}
            panel.Controls.Add(table);
        }
        internal static void List(Panel panel,Label title,Label count,DataGridView grid)
        {
            panel.Controls.Clear();panel.Dock=DockStyle.Fill;panel.Padding=new Padding(20);
            var table=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=3};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));table.RowStyles.Add(new RowStyle(SizeType.AutoSize));table.RowStyles.Add(new RowStyle(SizeType.Percent,100));
            title.AutoSize=true;title.Margin=new Padding(0,0,0,6);count.AutoSize=true;count.Margin=new Padding(0,0,0,12);
            grid.Dock=DockStyle.Fill;grid.Margin=Padding.Empty;
            table.Controls.Add(title,0,0);table.Controls.Add(count,0,1);table.Controls.Add(grid,0,2);panel.Controls.Add(table);
        }
    }
}
