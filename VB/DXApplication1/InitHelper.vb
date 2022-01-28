Imports System
Imports System.ComponentModel
Imports System.Drawing

Namespace DXApplication1

    Public Class InitHelper

        Public Shared RandomInstance As Random = New Random()

        Public Shared Sub InitResources(ByVal resources As BindingList(Of CustomResource))
            resources.Add(CreateCustomResource(1, "Peter Dolan", Color.PowderBlue))
            resources.Add(CreateCustomResource(2, "Ryan Fisher", Color.PaleVioletRed))
            resources.Add(CreateCustomResource(3, "Andrew Miller", Color.PeachPuff))
        End Sub

        Public Shared Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String, ByVal ResColor As Color) As CustomResource
            Dim cr As CustomResource = New CustomResource()
            cr.ResID = res_id
            cr.Name = caption
            Return cr
        End Function
    End Class
End Namespace
