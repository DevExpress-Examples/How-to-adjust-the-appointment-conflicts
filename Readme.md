<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/397979009/20.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1023102)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to adjust the appointment conflicts

<p></p><p>This example demonstrates how to prevent moving an appointmnets to a range where another appointment is set using the <a href="https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AllowAppointmentConflicts">SchedulerControl.AllowAppointmentConflicts</a> event. 
For this, it is necessary to set the <a href="https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.SchedulerOptionsCustomization.AllowAppointmentConflicts">SchedulerOptionsCustomization.AllowAppointmentConflicts</a> property to <b>AppointmentConflictsMode.Custom</b>. 
The code in the event handler checks whether the time interval of the modified appointment intersects with other appointments, including recurrent series and exceptions. 
  If such an appointment is found, it is added to the <b>AppointmentConflictEventArgs.Conflicts</b> collection. If the collection has at least one element, a conflict occurs and the Scheduler cancels changes.
To paint the conflict appointments, the <a href="https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.CustomDrawAppointmentBackground">CustomDrawAppointmentBackground</a> event is used. The <a href="https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.Native.AppointmentConflictsCalculator.CalculateConflicts%28DevExpress.XtraScheduler.Appointment-DevExpress.XtraScheduler.TimeInterval%29">AppointmentConflictsCalculator.CalculateConflicts</a> method is called in this event handler to get the current conflicts.</p>

<br/>


