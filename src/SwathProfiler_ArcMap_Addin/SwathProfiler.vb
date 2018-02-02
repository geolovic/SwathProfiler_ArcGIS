Public Class SwathProfiler
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
    '
    '  TODO: Sample code showing how to access button host
    '
    My.ArcMap.Application.CurrentTool = Nothing
    Dim select_data_form As SelDataForm = New SelDataForm
    select_data_form.Show()
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
