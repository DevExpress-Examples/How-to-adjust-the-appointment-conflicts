Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Native
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace DXApplication1

    Public Partial Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Private CustomResourceCollection As BindingList(Of CustomResource) = New BindingList(Of CustomResource)()

        Public Sub New()
            InitializeComponent()
            InitHelper.InitResources(CustomResourceCollection)
            Dim mappingsResource As ResourceMappingInfo = schedulerDataStorage1.Resources.Mappings
            mappingsResource.Id = "ResID"
            mappingsResource.Caption = "Name"
            Dim mappingsAppointment As AppointmentMappingInfo = schedulerDataStorage1.Appointments.Mappings
            mappingsAppointment.Start = "StartTime"
            mappingsAppointment.End = "EndTime"
            mappingsAppointment.Subject = "Subject"
            mappingsAppointment.AllDay = "AllDay"
            mappingsAppointment.Description = "Description"
            mappingsAppointment.Label = "Label"
            mappingsAppointment.Location = "Location"
            mappingsAppointment.RecurrenceInfo = "RecurrenceInfo"
            mappingsAppointment.ReminderInfo = "ReminderInfo"
            mappingsAppointment.ResourceId = "OwnerId"
            mappingsAppointment.Status = "Status"
            mappingsAppointment.Type = "EventType"
            schedulerDataStorage1.Resources.DataSource = CustomResourceCollection
            schedulerControl1.Start = Date.Now
            schedulerControl1.OptionsCustomization.AllowAppointmentConflicts = AppointmentConflictsMode.Custom
            AddHandler schedulerControl1.AllowAppointmentConflicts, AddressOf SchedulerControl1_AllowAppointmentConflicts
            AddHandler schedulerControl1.CustomDrawAppointmentBackground, AddressOf SchedulerControl1_CustomDrawAppointmentBackground
            schedulerControl1.DataStorage.Appointments.Clear()
            schedulerControl1.GroupType = SchedulerGroupType.Resource
            Dim apt1 As Appointment = schedulerControl1.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, Date.Now, Date.Now.AddHours(2))
            apt1.ResourceId = schedulerControl1.DataStorage.Resources(0).Id
            apt1.Subject = "Test1"
            schedulerControl1.DataStorage.Appointments.Add(apt1)
            Dim apt2 As Appointment = schedulerControl1.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, Date.Now, Date.Now.AddHours(2))
            apt2.ResourceId = schedulerControl1.DataStorage.Resources(1).Id
            apt2.Subject = "Test2"
            schedulerControl1.DataStorage.Appointments.Add(apt2)
            schedulerControl1.Views.TimelineView.AppointmentDisplayOptions.SnapToCellsMode = AppointmentSnapToCellsMode.Always
            schedulerControl1.Views.MonthView.AppointmentDisplayOptions.SnapToCellsMode = AppointmentSnapToCellsMode.Always
        End Sub

        Private Sub SchedulerControl1_CustomDrawAppointmentBackground(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim scheduler As SchedulerControl = TryCast(sender, SchedulerControl)
            Dim viewInfo As AppointmentViewInfo = TryCast(e.ObjectInfo, DevExpress.XtraScheduler.Drawing.AppointmentViewInfo)
            Dim apt As Appointment = viewInfo.Appointment
            Dim allAppointments As AppointmentBaseCollection = scheduler.ActiveView.GetAppointments()
            Dim aCalculator As AppointmentConflictsCalculator = New AppointmentConflictsCalculator(allAppointments)
            Dim visibleInterval As TimeInterval = scheduler.ActiveView.GetVisibleIntervals().Interval
            Dim isConflict As Boolean = aCalculator.CalculateConflicts(apt, visibleInterval).Count <> 0
            ' Paint conflict appointments with a red and white hatch brush.
            If isConflict Then
                Dim rect As Rectangle = e.Bounds
                Dim brush As Brush = e.Cache.GetSolidBrush(scheduler.DataStorage.Appointments.Labels.GetById(apt.LabelKey).GetColor())
                e.Cache.FillRectangle(brush, rect)
                rect.Inflate(-3, -3)
                Using _hatchBrush = New HatchBrush(HatchStyle.WideUpwardDiagonal, Color.Red, Color.White)
                    e.Cache.FillRectangle(_hatchBrush, rect)
                End Using

                e.Handled = True
            End If
        End Sub

        Private Sub SchedulerControl1_AllowAppointmentConflicts(ByVal sender As Object, ByVal e As AppointmentConflictEventArgs)
            e.Conflicts.Clear()
            FillConflictedAppointmentsCollection(e.Conflicts, e.Interval, CType(sender, SchedulerControl).DataStorage.Appointments.Items, e.AppointmentClone)
        End Sub

        Private Sub FillConflictedAppointmentsCollection(ByVal conflicts As AppointmentBaseCollection, ByVal interval As TimeInterval, ByVal collection As AppointmentBaseCollection, ByVal currApt As Appointment)
            For i As Integer = 0 To collection.Count - 1
                Dim apt As Appointment = collection(i)
                If New TimeInterval(apt.Start, apt.End).IntersectsWith(interval) And Not(apt.Start = interval.End OrElse apt.End = interval.Start) Then
                    If apt.ResourceId Is currApt.ResourceId Then
                        conflicts.Add(apt)
                    End If
                End If

                If apt.Type = AppointmentType.Pattern Then
                    FillConflictedAppointmentsCollection(conflicts, interval, apt.GetExceptions(), currApt)
                End If
            Next
        End Sub
    End Class
End Namespace
