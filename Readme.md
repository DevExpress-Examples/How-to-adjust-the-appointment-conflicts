# How to adjust the appointment conflicts

<p></p><p>This example demonstrates how to prevent moving an appointmnets to a range where another appointment is set using the [SchedulerControl.AllowAppointmentConflicts](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AllowAppointmentConflicts) event. 
For this, it is necessary to set the [SchedulerOptionsCustomization.AllowAppointmentConflicts](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.SchedulerOptionsCustomization.AllowAppointmentConflicts) property to **AppointmentConflictsMode.Custom**. 
The code in the event handler checks whether the time interval of the modified appointment intersects with other appointments, including recurrent series and exceptions. 
If such an appointment is found, it is added to the **AppointmentConflictEventArgs.Conflicts** collection. If the collection has at least one element, a conflict occurs and the Scheduler cancels changes.
To paint the conflict appointments, the [CustomDrawAppointmentBackground](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.CustomDrawAppointmentBackground) event is used. The [AppointmentConflictsCalculator.CalculateConflicts](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.Native.AppointmentConflictsCalculator.CalculateConflicts%28DevExpress.XtraScheduler.Appointment-DevExpress.XtraScheduler.TimeInterval%29) method is called in this event handler to get the current conflicts.</p>

<br/>


