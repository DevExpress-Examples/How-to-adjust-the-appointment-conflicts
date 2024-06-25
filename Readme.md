<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/397979009/20.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1023102)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# WinForms Scheduler - Resolve appointment conflicts

This example handles the [SchedulerControl.AllowAppointmentConflicts](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AllowAppointmentConflicts) event to prevent appointments from crossing (when the user moves an appointment within the Scheduler). The [SchedulerOptionsCustomization.AllowAppointmentConflicts](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.SchedulerOptionsCustomization.AllowAppointmentConflicts) property is set to `AppointmentConflictsMode.Custom` to handle appointment conflicts manually.
  
The code in the `AllowAppointmentConflicts` event handler checks whether the time interval of the modified appointment intersects with other appointments, including recurrent series and exceptions. If such an appointment is found, the appointment is added to the `e.Conflicts` collection. If the collection has at least one element, a conflict occurs and the Scheduler control cancels changes.

The example also handles the [CustomDrawAppointmentBackground](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.CustomDrawAppointmentBackground) event to highlight conflicts. The [AppointmentConflictsCalculator.CalculateConflicts](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.Native.AppointmentConflictsCalculator.CalculateConflicts%28DevExpress.XtraScheduler.Appointment-DevExpress.XtraScheduler.TimeInterval%29) method is used to obtain appointment conflicts.
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-resolve-appointment-conflicts&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-resolve-appointment-conflicts&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
