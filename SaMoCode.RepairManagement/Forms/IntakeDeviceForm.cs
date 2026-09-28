using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SaMoCode.RepairManagement.Models;
using SaMoCode.RepairManagement.Services;

namespace SaMoCode.RepairManagement.Forms
{
    internal class IntakeDeviceForm : WorkflowDialog
    {
        public IntakeDevice Device {get;private set;}
        public IntakeDeviceForm(IntakeDevice initial = null) : base("Device intake")
        {
            initial=initial??new IntakeDevice();
            var brand=Input("Brand",initial.Brand,50); var model=Input("Model",initial.Model,100);
            var color=Input("Color (optional)",initial.Color,50); var serial=Input("Serial / IMEI (optional)",initial.SerialNumber,100);
            var problems=Input("Issues — one problem per line",string.Join(Environment.NewLine,initial.Problems),20000,true);
            var notes=Input("Condition notes",initial.ConditionNotes,500,true);
            var service=new MultiDeviceIntakeService();
            var conditions=Checks(service.Conditions(),initial.ConditionIds); Add("Visible conditions",conditions);
            var accessories=Checks(service.Accessories(),initial.AccessoryIds); Add("Accessories received",accessories);
            var other=Input("Other accessory",initial.OtherAccessory,200);
            OnSave(()=> {
                var result=new IntakeDevice {Brand=brand.Text,Model=model.Text,Color=color.Text,SerialNumber=serial.Text,
                    Problems=problems.Lines.Where(p=>!string.IsNullOrWhiteSpace(p)).Select(p=>p.Trim()).ToList(),
                    ConditionNotes=notes.Text,OtherAccessory=other.Text,
                    ConditionIds=conditions.CheckedItems.Cast<DataRowView>().Select(r=>(int)r["Id"]).ToList(),
                    AccessoryIds=accessories.CheckedItems.Cast<DataRowView>().Select(r=>(int)r["Id"]).ToList()};
                MultiDeviceIntakeService.ValidateDevice(result); Device=result;
            });
        }
        private static CheckedListBox Checks(DataTable source,List<int> selected)
        {
            var list=new CheckedListBox {DataSource=source,DisplayMember="Name",ValueMember="Id",CheckOnClick=true,Height=130};
            for(int i=0;i<list.Items.Count;i++)
                if(selected.Contains((int)((DataRowView)list.Items[i])["Id"])) list.SetItemChecked(i,true);
            return list;
        }
    }
}
