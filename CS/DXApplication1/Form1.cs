using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1 {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {

        BindingList<CustomResource> CustomResourceCollection = new BindingList<CustomResource>();
        public Form1() {
            InitializeComponent();

            InitHelper.InitResources(CustomResourceCollection);

            ResourceMappingInfo mappingsResource = this.schedulerDataStorage1.Resources.Mappings;
            mappingsResource.Id = "ResID";
            mappingsResource.Caption = "Name";

            AppointmentMappingInfo mappingsAppointment = this.schedulerDataStorage1.Appointments.Mappings;
            mappingsAppointment.Start = "StartTime";
            mappingsAppointment.End = "EndTime";
            mappingsAppointment.Subject = "Subject";
            mappingsAppointment.AllDay = "AllDay";
            mappingsAppointment.Description = "Description";
            mappingsAppointment.Label = "Label";
            mappingsAppointment.Location = "Location";
            mappingsAppointment.RecurrenceInfo = "RecurrenceInfo";
            mappingsAppointment.ReminderInfo = "ReminderInfo";
            mappingsAppointment.ResourceId = "OwnerId";
            mappingsAppointment.Status = "Status";
            mappingsAppointment.Type = "EventType";

            schedulerDataStorage1.Resources.DataSource = CustomResourceCollection;

            schedulerControl1.Start = DateTime.Now;

            schedulerControl1.OptionsCustomization.AllowAppointmentConflicts = AppointmentConflictsMode.Custom;
            schedulerControl1.AllowAppointmentConflicts += SchedulerControl1_AllowAppointmentConflicts;
            schedulerControl1.CustomDrawAppointmentBackground += SchedulerControl1_CustomDrawAppointmentBackground;

            schedulerControl1.DataStorage.Appointments.Clear();
            schedulerControl1.GroupType = SchedulerGroupType.Resource;
            Appointment apt1 = schedulerControl1.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, DateTime.Now, DateTime.Now.AddHours(2));
            apt1.ResourceId = schedulerControl1.DataStorage.Resources[0].Id;
            apt1.Subject = "Test1";
            schedulerControl1.DataStorage.Appointments.Add(apt1);
            Appointment apt2 = schedulerControl1.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, DateTime.Now, DateTime.Now.AddHours(2));
            apt2.ResourceId = schedulerControl1.DataStorage.Resources[1].Id;
            apt2.Subject = "Test2";
            schedulerControl1.DataStorage.Appointments.Add(apt2);

            schedulerControl1.Views.TimelineView.AppointmentDisplayOptions.SnapToCellsMode = AppointmentSnapToCellsMode.Always;
            schedulerControl1.Views.MonthView.AppointmentDisplayOptions.SnapToCellsMode = AppointmentSnapToCellsMode.Always;
        }

        private void SchedulerControl1_CustomDrawAppointmentBackground(object sender, CustomDrawObjectEventArgs e) {
            SchedulerControl scheduler = sender as SchedulerControl;
            AppointmentViewInfo viewInfo = (e.ObjectInfo as DevExpress.XtraScheduler.Drawing.AppointmentViewInfo);
            Appointment apt = viewInfo.Appointment;
            AppointmentBaseCollection allAppointments = scheduler.ActiveView.GetAppointments();
            AppointmentConflictsCalculator aCalculator = new AppointmentConflictsCalculator(allAppointments);
            TimeInterval visibleInterval = scheduler.ActiveView.GetVisibleIntervals().Interval;
            bool isConflict = aCalculator.CalculateConflicts(apt, visibleInterval).Count != 0;
            // Paint conflict appointments with a red and white hatch brush.
            if(isConflict) {
                Rectangle rect = e.Bounds;
                Brush brush = e.Cache.GetSolidBrush(scheduler.DataStorage.Appointments.Labels.GetById(apt.LabelKey).GetColor());
                e.Cache.FillRectangle(brush, rect);
                rect.Inflate(-3, -3);
                using(var _hatchBrush = new HatchBrush(HatchStyle.WideUpwardDiagonal, Color.Red, Color.White))
                    e.Cache.FillRectangle(_hatchBrush, rect);
                e.Handled = true;
            }
        }

        private void SchedulerControl1_AllowAppointmentConflicts(object sender, AppointmentConflictEventArgs e) {
            e.Conflicts.Clear();
            FillConflictedAppointmentsCollection(e.Conflicts, e.Interval, ((SchedulerControl)sender).DataStorage.Appointments.Items, e.AppointmentClone);
        }

        void FillConflictedAppointmentsCollection(AppointmentBaseCollection conflicts, TimeInterval interval,
           AppointmentBaseCollection collection, Appointment currApt) {
            for(int i = 0; i < collection.Count; i++) {
                Appointment apt = collection[i];
                if(new TimeInterval(apt.Start, apt.End).IntersectsWith(interval) & !(apt.Start == interval.End || apt.End == interval.Start)) {
                    if(apt.ResourceId == currApt.ResourceId) {
                        conflicts.Add(apt);
                    }
                }
                if(apt.Type == AppointmentType.Pattern) {
                    FillConflictedAppointmentsCollection(conflicts, interval, apt.GetExceptions(), currApt);
                }
            }
        }
    }
}
