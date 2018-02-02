Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Analyst3D
Imports System.IO
Imports System.Drawing
Imports ESRI.ArcGIS.DataSourcesRaster

Public Class SelDataForm
  Dim pMxDoc As IMxDocument
  Dim pMap As IMap
  Dim pEnumLayers As IEnumLayer
  Dim pLayer As ILayer
  Dim pFLayer As IFeatureLayer
  Dim pRLayer As IRasterLayer
  Dim pFClass As IFeatureClass
  Dim pRaster As IRaster
  Dim version As String = "20150828"

  Private Sub SelDataForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      'Reference variables
      pMxDoc = TryCast(My.ArcMap.Document, IMxDocument)
      pMap = TryCast(pMxDoc.FocusMap, IMap)

      If pMap.LayerCount <= 0 Then
        MsgBox("No layers in the map!")
        Me.Close()
      End If

      pEnumLayers = TryCast(pMap.Layers, IEnumLayer)
      pLayer = TryCast(pEnumLayers.Next, ILayer)

      'Fill CmbL with polygon layers and CmbR with raster layers
      Do Until pLayer Is Nothing
        If TypeOf pLayer Is IFeatureLayer Then
          pFLayer = TryCast(pLayer, IFeatureLayer)
          If pFLayer.FeatureClass.ShapeType = esriGeometryType.esriGeometryPolyline Then
            CmbL.Items.Add(pLayer.Name)
          End If
        ElseIf TypeOf pLayer Is IRasterLayer Then
          CmbR.Items.Add(pLayer.Name)
        End If
        pLayer = pEnumLayers.Next
      Loop
    Catch ex As Exception
      MsgBox(ex.Message, vbExclamation, "Swath Profiler")
    End Try
  End Sub

  Private Sub LoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadData.Click
    Dim pFCursor As IFeatureCursor
    Dim QueryFilter As IQueryFilter
    Dim pFeat As IFeature
    Dim pPolyline As IPolyline
    Dim profile_data As List(Of PointF())

    Dim offset As Single
    Dim step_size As Single
    Dim n_profile_points As Integer
    Dim n_lines As Integer
    Dim name As String
    Dim k As Integer

    'Try
    Me.Cursor = Windows.Forms.Cursors.WaitCursor

    'Ensure that both combos have a layer selected
    If CmbR.Text = "" Or CmbL.Text = "" Then
      MsgBox("Please select valid layers")
      Exit Sub
    End If

    'Identify layers in combos and reference pRaster y pFClass
    pEnumLayers = TryCast(pMap.Layers, IEnumLayer)
    pLayer = TryCast(pEnumLayers.Next, ILayer)
    Do Until pLayer Is Nothing
      If pLayer.Name = CmbL.Text Then
        pFLayer = TryCast(pLayer, IFeatureLayer)
        pFClass = TryCast(pFLayer.FeatureClass, IFeatureClass)
      ElseIf pLayer.Name = CmbR.Text Then
        pRLayer = TryCast(pLayer, IRasterLayer)
        pRaster = TryCast(pRLayer.Raster, IRaster)
      End If
      pLayer = pEnumLayers.Next
    Loop

    'Check if the analysis is done only for selected features
    If CheckSelected.Checked Then
      QueryFilter = get_selected(pFLayer)
    Else
      QueryFilter = Nothing
    End If

    'Check the number of profiles (exit if number exceed 10)
    If pFClass.FeatureCount(QueryFilter) > 10 Then
      Dim mess As String
      mess = "FeatureClass contains more than 10 lines." & vbCrLf
      mess &= "It will take long time in computing swath profiles" & vbCrLf
      mess &= "Do you want to Continue?"
      Dim respuesta As Long = MsgBox(mess, vbYesNo, "Swath profiler")
      If respuesta = vbNo Then Exit Sub
    End If

    'Get parameters for Swath profile analysis
    If check_step.Checked Then
      If step_size_textbox.Text = "" Then
        Dim mess As String = "Please introduce a valid step size"
        MsgBox(mess, MsgBoxStyle.Critical)
        Exit Sub
      End If
      step_size = CSng(step_size_textbox.Text)
    Else
      Dim pRasterProp As IRasterProps = TryCast(pRLayer.Raster, IRasterProps)
      step_size = pRasterProp.MeanCellSize.X * 1.5
    End If

    If check_n_profiles.Checked Then
      If n_profiles_textbox.Text = "" Then
        Dim mess As String = "Please introduce a valid number of lines"
        MsgBox(mess, MsgBoxStyle.Critical)
        Exit Sub
      End If
      n_lines = CInt(n_profiles_textbox.Text)
    Else
      n_lines = 50
    End If

    offset = CSng(width_textbox.Text) / (n_lines + 1)

    'Find the Name field (if present)
    Dim nameInd As Integer
    nameInd = pFClass.FindField("Name")

    'Start the iteration over the profiles
    pFCursor = pFClass.Update(QueryFilter, False)
    pFeat = pFCursor.NextFeature
    k = 0

    'Prepare the array for swath_profiles (one per feature)
    swath_profiles = New List(Of SwathProfile)

    Do Until pFeat Is Nothing
      'Get the name for the profile. If field "Name" does not exist, set generic names ("Profile 1", "Profile 2" ...)
      If nameInd > 0 Then
        name = CStr(pFeat.Value(nameInd))
      Else
        name = "Profile " & CStr(k + 1)
      End If

      'Prepare the array to store profile-data as a list of arrays of pointF
      'Each offset line has an associated pointF() array
      profile_data = New List(Of PointF())

      'Get baseline
      pPolyline = TryCast(pFeat.ShapeCopy, IPolyline)
      n_profile_points = CInt(pPolyline.Length / step_size)
      Dim s_size As Single = pPolyline.Length / n_profile_points
      Dim baseline As PointF() = get_profile(pPolyline, pRaster, s_size)
      profile_data.Add(baseline)

      'Check if DEM covers the swath band
      If swath_is_inside(pPolyline, CDbl(width_textbox.Text), pRLayer.VisibleExtent) = False Then
        Dim mess As String = "Selected DEM does not cover the whole swath band." & vbCrLf
        mess &= "Please correct the problem and re-run."
        Exit Sub
      End If

      'Get all the other lines
      Dim offset_lines() As IPolyline = get_offset_lines(pPolyline, offset, n_lines / 2)
      For Each line In offset_lines
        s_size = line.Length / n_profile_points
        Dim profile_line As PointF() = get_profile(line, pRaster, s_size)
        ReDim Preserve profile_line(baseline.GetUpperBound(0))
        For i = 0 To baseline.GetUpperBound(0)
          profile_line(i).X = baseline(i).X
        Next i
        profile_data.Add(profile_line)
      Next

      swath_profiles.Add(New SwathProfile(profile_data, name))

      k += 1
      pFeat = pFCursor.NextFeature
    Loop

    Me.Cursor = Windows.Forms.Cursors.Default
    Dim DispForm As DisplayForm = New DisplayForm
    DispForm.Show()
    Me.Close()

    'Catch ex As Exception
    '  MsgBox(ex.Message, vbCritical, "Profiler")
    'End Try
  End Sub

  Public Function get_distance(ByVal p1 As PointF, ByVal p2 As PointF) As Double
    Return Math.Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)
  End Function

  Public Function get_profile(ByVal in_line As IPolyline, ByVal in_raster As IRaster, ByVal step_size As Single) As PointF()
    'Esta función devuelve un perfil a partir de una línea y un ráster
    'El perfil será devuelto como una matriz de puntos PointF (X = distancia, Y = elevación)
    Dim out_points As List(Of PointF) = New List(Of PointF)

    'Se crea la superficie para el análisis
    Dim pSurface As ISurface
    Dim pRasterSurface As IRasterSurface = New RasterSurface
    pRasterSurface.PutRaster(pRaster, 0)
    pSurface = TryCast(pRasterSurface, ISurface)

    'Se crea el perfil como una colección de puntos
    Dim pGeometry As IGeometry
    Dim pPointColl As IPointCollection
    pGeometry = TryCast(in_line, IGeometry)
    pSurface.GetProfile(pGeometry, pGeometry, step_size)
    pPointColl = TryCast(pGeometry, IPointCollection)

    'Se realiza un loop por el perfil para llenar la matriz de PointF
    Dim distance As Double = 0
    Dim pto_1 As PointF
    Dim pto_2 As PointF
    out_points.Add(New PointF(distance, pPointColl.Point(0).Z))
    For i = 1 To pPointColl.PointCount - 1
      pto_1 = New PointF(CSng(pPointColl.Point(i - 1).X), CSng(pPointColl.Point(i - 1).Y))
      pto_2 = New PointF(CSng(pPointColl.Point(i).X), CSng(pPointColl.Point(i).Y))
      distance += get_distance(pto_1, pto_2)
      out_points.Add(New PointF(distance, pPointColl.Point(i).Z))
    Next

    'Se devuelve la matriz de puntos con el método ToArray de la lista genérica de puntosF
    Return out_points.ToArray
  End Function

  Public Function get_offset_lines(ByVal zero_line As IPolyline, ByVal line_offset As Single, ByVal number_of_lines As Integer) As IPolyline()
    'Esta función retorna una matriz con las líneas del Swath profile
    'La matriz contendrá un número de líneas (number_of_lines) a cada lado de la zero_line
    Dim i As Integer, k As Integer
    Dim out_lines() As IPolyline
    ReDim out_lines(number_of_lines * 2 - 1)
    Dim pConstructCurve As IConstructCurve
    For i = 0 To number_of_lines - 1
      'Se construye una línea a un lado de la línea zero (multiplicando el offset por i)
      pConstructCurve = New Polyline
      pConstructCurve.ConstructOffset(zero_line, line_offset * i, esriConstructOffsetEnum.esriConstructOffsetRounded + esriConstructOffsetEnum.esriConstructOffsetSimple)
      out_lines(k) = CType(pConstructCurve, IPolyline)
      k += 1 'Se construye una línea a un lado de la línea zero (multiplicando el offset por -i)
      pConstructCurve = New Polyline
      pConstructCurve.ConstructOffset(zero_line, line_offset * (-i), esriConstructOffsetEnum.esriConstructOffsetRounded + esriConstructOffsetEnum.esriConstructOffsetSimple)
      out_lines(k) = CType(pConstructCurve, IPolyline)
      k += 1
    Next

    Return (out_lines)
  End Function

  Public Function get_selected(ByVal pFeatureLayer As IFeatureLayer) As IQueryFilter
    'Esta función devuelve un filtro (IQueryFilter) con las entidades seleccionadas.
    'Si no hay ninguna entidad seleccionada, el filtro devuelto es Nothing
    Dim qryString As String
    Dim pQueryFilter As IQueryFilter
    Dim pFeatSel As IFeatureSelection
    Dim pSelSet As ISelectionSet
    Dim pEnumIDs As IEnumIDs
    Dim pID As Long
    pQueryFilter = New QueryFilter

    pFeatSel = pFeatureLayer
    pSelSet = pFeatSel.SelectionSet
    pEnumIDs = pSelSet.IDs
    If pSelSet.Count = 0 Then
      pQueryFilter = Nothing
    Else
      pID = pEnumIDs.Next
      qryString = pFClass.OIDFieldName & Space(1) & "in" & Space(1) & "("
      Do Until pID = -1
        qryString = qryString & Str(pID) & Space(1) & ","
        pID = pEnumIDs.Next
      Loop
      qryString = qryString.Substring(0, Len(qryString) - 1) & ")"
      pQueryFilter.WhereClause = qryString
    End If

    Return pQueryFilter
  End Function

  Public Function swath_is_inside(ByVal zero_line As IPolyline, ByVal width As Double, ByVal env As IEnvelope) As Boolean
    'Esta función retorna verdadero o falso si el modelo cubre totalmente el análisis o no
    Dim in_line1 As IPolyline
    Dim in_line2 As IPolyline
    Dim pConstructCurve As IConstructCurve

    pConstructCurve = New Polyline
    pConstructCurve.ConstructOffset(zero_line, width / 2, esriConstructOffsetEnum.esriConstructOffsetRounded + esriConstructOffsetEnum.esriConstructOffsetSimple)
    in_line1 = CType(pConstructCurve, IPolyline)

    pConstructCurve = New Polyline
    pConstructCurve.ConstructOffset(zero_line, -width / 2, esriConstructOffsetEnum.esriConstructOffsetRounded + esriConstructOffsetEnum.esriConstructOffsetSimple)
    in_line2 = CType(pConstructCurve, IPolyline)

    If is_inside(in_line1.FromPoint, env) And is_inside(in_line1.ToPoint, env) And is_inside(in_line2.FromPoint, env) _
      And is_inside(in_line2.ToPoint, env) Then
      Return True
    Else
      Return False
    End If

  End Function

  Public Function is_inside(ByVal in_point As IPoint, ByVal env As IEnvelope) As Boolean
    'Esta función checkea si un punto está dentro de un envelope
    If in_point.X > env.XMax Or in_point.X < env.XMin Or in_point.Y > env.YMax Or in_point.Y < env.YMin Then
      Return False
    Else
      Return True
    End If
  End Function

  Private Sub check_step_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_step.CheckedChanged
    If check_step.Checked Then
      step_size_textbox.Enabled = True
    Else
      step_size_textbox.Enabled = False
    End If
  End Sub

  Private Sub check_n_profiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_n_profiles.CheckedChanged
    If check_n_profiles.Checked Then
      n_profiles_textbox.Enabled = True
    Else
      n_profiles_textbox.Enabled = False
    End If
  End Sub

  Private Sub DisplayForm_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    If e.X > Me.Width - 25 And e.Y > Me.Height - 45 Then
      MsgBox(version)
    End If
  End Sub

End Class