<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/397979009/20.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1023102)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to adjust the appointment conflicts

This example handles the [SchedulerControl.AllowAppointmentConflicts](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AllowAppointmentConflicts) event to prevent movement of an appointment to a range where another appointment exists. 
The [SchedulerOptionsCustomization.AllowAppointmentConflicts](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.SchedulerOptionsCustomization.AllowAppointmentConflicts) property is set to **AppointmentConflictsMode.Custom** to process appointment conflicts in a custom manner.
  
The code in the **AllowAppointmentConflicts** event handler checks whether the time interval of the modified appointment intersects with other appointments, including recurrent series and exceptions. If such an appointment is found, it is added to the **AppointmentConflictEventArgs.Conflicts** collection. If the collection has at least one element, a conflict occurs and the Scheduler cancels changes.
  
To paint conflicts, the example handles the [CustomDrawAppointmentBackground](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.CustomDrawAppointmentBackground) event. The [AppointmentConflictsCalculator.CalculateConflicts](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.Native.AppointmentConflictsCalculator.CalculateConflicts%28DevExpress.XtraScheduler.Appointment-DevExpress.XtraScheduler.TimeInterval%29) method is called in this event handler to get the current conflicts.

