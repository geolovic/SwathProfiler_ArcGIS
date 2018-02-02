Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Public Class DisplayForm

  Private Sub DisplayForm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
    If ETHI Then
      check_HI.Text = "THi*"
    Else
      check_HI.Text = "THi"
    End If
  End Sub


  'CARGA Y REDIMENSIONADO DEL FORMULARIO
  Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim x_range As Range
    Dim y_range As Range

    'Se comprueba que hay datos para dibujar
    'Si no los hay se dibuja un perfil standard
    If swath_profiles Is Nothing Then
      x_range = New Range(0, 1000)
      y_range = New Range(0, 100)
    Else
      'Se llena la comboBox con los SwathProfiles que haya y se selecciona el primero
      For Each profile In swath_profiles
        combo_lines.Items.Add(profile.name)
      Next
      combo_lines.SelectedIndex = 0
      x_range = round_range(swath_profiles(combo_lines.SelectedIndex).x_range)
      y_range = round_range(swath_profiles(combo_lines.SelectedIndex).y_range)
    End If

    'Create the chart for displaying
    rectangle_01 = New Rectangle(New Point(70, 40), New Size(canvas.Width - 150, canvas.Height - 90))
    rectangle_02 = New Rectangle(rectangle_01.Location, New Size(rectangle_01.Width, rectangle_01.Height * 0.8))
    rectangle_03 = New Rectangle(New Point(rectangle_02.Location.X, rectangle_02.Location.Y + rectangle_02.Size.Height) _
                                 , New Size(rectangle_02.Width, rectangle_01.Height * 0.2))
    draw_rectangle = rectangle_01
    draw_chart = New DrawChart(x_range, y_range, draw_rectangle)
    HI_chart = New DrawChart(draw_chart.x_axis.range, New Range(0, 100), rectangle_03)
    HI_chart.show_title = False
    HI_chart.y_axis.division = 50
    HI_chart.y_axis.show_labels = False
    HI_chart.x_axis.title = "Distance (m)"


    'Initalize drawing variables
    show_min_max = False
    show_q1_q3 = False
    show_mean = False
    show_data = True
    show_relief = False
    show_HI = False
    ETHI = False
    profile_data_color = Color.LightGray
    profile_min_color = Color.FromArgb(208, 224, 152)
    profile_max_color = Color.FromArgb(249, 186, 128)
    profile_mean_color = Color.DarkBlue
    profile_q1_color = Color.DarkCyan
    profile_q3_color = Color.DarkCyan
    profile_relief_color = Color.Red
    profile_HI_color = Color.Chocolate

    profile_data_size = 1.5
    profile_min_size = 2
    profile_max_size = 2
    profile_mean_size = 2
    profile_relief_size = 1.5
    profile_HI_size = 1.5
    profile_q1_size = 2
    profile_q3_size = 2
    draw_chart.x_axis.title = "Distance (m)"
    draw_chart.y_axis.title = "Elevation (m)"
  End Sub

  Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    'Se redimensiona el picturebox y se colocan de nuevo los botones y checkboxes
    canvas.Width = Me.Width - 150
    canvas.Height = Me.Height - 60
    Dim sep1 As Integer = 25
    Dim sep2 As Integer = 20
    Dim bottom As Integer = 71
    Label1.Location = New Point(Me.Width - 130, Label1.Location.Y)
    combo_lines.Location = New Point(Me.Width - 127, combo_lines.Location.Y)
    check_scale.Location = New Point(Me.Width - 127, check_scale.Location.Y)

    If ETHI Then
      check_HI.Text = "THi*"
    Else
      check_HI.Text = "THi"
    End If

    properties_button.Location = New Point(Me.Width - 127, Me.Height - (bottom))
    export_button.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep1))
    save_button.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep1 * 2))
    check_HI.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep1 * 3))
    bottom = bottom + sep1 * 3
    check_relief.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep2))
    check_q1_q3.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep2 * 2))
    check_mean.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep2 * 3))
    check_min_max.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep2 * 4))
    check_data.Location = New Point(Me.Width - 127, Me.Height - (bottom + sep2 * 5))


    'Necesario, pues el formulario se redimensiona nada más crearse (cuando no está creado el objeto draw_chart aún)
    If draw_chart IsNot Nothing Then
      'Se redimensiona el área de dibujo
      rectangle_01 = New Rectangle(New Point(70, 40), New Size(canvas.Width - 150, canvas.Height - 90))
      rectangle_02 = New Rectangle(rectangle_01.Location, New Size(rectangle_01.Width, rectangle_01.Height * 0.8))
      rectangle_03 = New Rectangle(New Point(rectangle_02.Location.X, rectangle_02.Location.Y + rectangle_02.Size.Height) _
                                 , New Size(rectangle_02.Width, rectangle_01.Height * 0.2))
      set_size(show_HI)
      draw_chart.set_draw_area(draw_rectangle)
      HI_chart.set_draw_area(rectangle_03)
      canvas.Invalidate()
    End If

  End Sub

  Private Sub set_size(ByVal is_HI_selected)
    If is_HI_selected Then
      draw_rectangle = rectangle_02
    Else
      draw_rectangle = rectangle_01
    End If
  End Sub

  'Método de dibujo de la picture box
  Private Sub canvas_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles canvas.Paint
    Dim ind As Integer

    If swath_profiles IsNot Nothing Then
      ind = combo_lines.SelectedIndex
      paint_swath_profile(e.Graphics, ind)
    Else
      draw_chart.draw(e.Graphics)
    End If

  End Sub

  'Metodos para los checkboxes
  Private Sub check_min_max_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_min_max.CheckedChanged
    show_min_max = check_min_max.Checked
    canvas.Invalidate()
  End Sub

  Private Sub check_mean_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_mean.CheckedChanged
    show_mean = check_mean.Checked
    canvas.Invalidate()
  End Sub

  Private Sub Check_data_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_data.CheckedChanged
    show_data = check_data.Checked
    canvas.Invalidate()
  End Sub

  Private Sub check_relief_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_relief.CheckedChanged
    show_relief = check_relief.Checked
    canvas.Invalidate()
  End Sub

  Private Sub check_HI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_HI.CheckedChanged
    show_HI = check_HI.Checked
    set_size(show_HI)
    draw_chart.set_draw_area(draw_rectangle)
    canvas.Invalidate()
  End Sub

  Private Sub check_scale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_scale.CheckedChanged
    canvas.Invalidate()
  End Sub

  Private Sub check_q1_q3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_q1_q3.CheckedChanged
    show_q1_q3 = check_q1_q3.Checked
    canvas.Invalidate()
  End Sub

  'Métodos para los botones
  Private Sub save_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save_button.Click
    Dim FileName As String
    Dim extension As Integer
    Try
      SaveFileDialog1.Filter = "BMP (*.bmp)|*.bmp|TIFF (*.tif)|*.tif|EMF (*.emf)|*.emf"
      SaveFileDialog1.ShowDialog()
      FileName = SaveFileDialog1.FileName
      extension = SaveFileDialog1.FilterIndex '1 BMP, 2 TIFF, 3 EMF

      If extension = 1 Or extension = 2 Then
        save_image_file(FileName)
      ElseIf extension = 3 Then
        save_vector_file(FileName)
      Else
        MsgBox("Problem saving the file, File not saved", vbCritical, "Swath profiles")
        Exit Sub
      End If

      MsgBox("Image saved", vbInformation)
    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "N. Profiler")
    End Try
  End Sub

  Private Sub export_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_button.Click
    Dim FileName As String
    Dim sw As System.IO.StreamWriter
    Dim Linea As String
    Dim i As Integer
    Dim ind As Integer
    Try
      'Get the index for selected profile in comboBox
      ind = combo_lines.SelectedIndex
      'Open the OpenfileDialog to select the file to export
      SaveFileDialog1.Filter = "Text file (*.txt)|*.txt"
      SaveFileDialog1.ShowDialog()
      'Get the file name and open a StreamWriter to write the file
      FileName = SaveFileDialog1.FileName
      sw = New System.IO.StreamWriter(FileName)
      sw.WriteLine(CStr(swath_profiles(ind).name))
      Linea = "X;"
      For i = 1 To swath_profiles(ind).count
        Linea &= "Y" & CStr(i) & ";"
      Next
      Linea &= "Min;Max;Mean;Q1;Q3;THi"
      sw.WriteLine(Linea)

      For i = 0 To swath_profiles(ind).max_profile.GetUpperBound(0)
        Linea = CStr(swath_profiles(ind).min_profile(i).X) & ";"
        For Each profile In swath_profiles(ind).profile_data
          If i <= profile.GetUpperBound(0) Then
            Linea &= CStr(profile(i).Y) & ";"
          End If
        Next
        Linea &= CStr(swath_profiles(ind).min_profile(i).Y) & ";"
        Linea &= CStr(swath_profiles(ind).max_profile(i).Y) & ";"
        Linea &= CStr(swath_profiles(ind).mean_profile(i).Y) & ";"
        Linea &= CStr(swath_profiles(ind).q1_profile(i).Y) & ";"
        Linea &= CStr(swath_profiles(ind).q3_profile(i).Y) & ";"
        Linea &= CStr(swath_profiles(ind).THi_profile(ETHI)(i).Y / 100) & ";"
        sw.WriteLine(Linea)
      Next
      sw.Close()
      MsgBox("Saved", vbInformation, "Swath profile")

    Catch ex As Exception
      Dim mess As String = "Error saving the file"
      MsgBox(mess)
    End Try
  End Sub

  Private Sub properties_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles properties_button.Click
    Dim prop_form As PropertiesForm = New PropertiesForm
    prop_form.ShowDialog()
  End Sub

  Private Sub close_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Me.Close()
  End Sub

  'Otros métodos del formulario
  Private Sub DisplayForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    canvas.Invalidate()
  End Sub

  Private Sub combo_lines_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles combo_lines.SelectedIndexChanged
    canvas.Invalidate()
  End Sub

  'Funciones auxiliares para salvar imágenes
  Private Sub save_image_file(ByVal filename As String)
    'Esta función permite salvar el gráfico en formato de imagen
    'Es llamada desde el Save_button, y toma el estado del SaveFileDialog1 actual (una vez seleccionado el filter index en el saveFileDialog)
    Try
      'Se crea un BitMap con las dimensiones del canvas
      Dim BMap As Bitmap = New Bitmap(canvas.Width, canvas.Height)
      'Se obtiene el contexto gráfico de ese bitmap
      Dim Graph As Graphics = Graphics.FromImage(BMap)

      'Código para mejorar la imagen
      Graph.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
      Graph.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
      Graph.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

      'Se pasa el contexto gráfico (del BitMap con las dimensiones del canvas) a la función Paint_to_File
      Paint_to_File(Graph)

      If SaveFileDialog1.FilterIndex = 1 Then
        BMap.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp)
      ElseIf SaveFileDialog1.FilterIndex = 2 Then
        BMap.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff)
      End If

      'Clean up
      BMap.Dispose()
      Graph.Dispose()
    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "Swath Profiles")
    End Try
  End Sub

  Private Sub save_vector_file(ByVal FileName As String)
    'Esta función permite salvar el gráfico en formato vectorial
    'Es llamada desde el Save_button, y toma el estado del SaveFileDialog1 actual (una vez seleccionado el filter index en el saveFileDialog)
    Try
      'Se crea un contexto gráfico del canvas
      Dim gr As Graphics = canvas.CreateGraphics()

      'Se obtiene el Hdc del contexto gráfico creado
      Dim hdc As IntPtr = gr.GetHdc()

      ' Se crea un Metafile que pueda trabajar con el objeto gráfico creado
      Dim mf As New Metafile(FileName, hdc, New Rectangle(canvas.Location.X, canvas.Location.Y, canvas.Width, _
                                                          canvas.Height), MetafileFrameUnit.Pixel)

      ' Se crea un contexto gráfico que pueda trabajar con el metafile creado
      Dim mf_gr As Graphics = Graphics.FromImage(mf)

      'Se pasa el contexto gráfico creado la función Paint_to_File
      Paint_to_File(mf_gr)

      ''Liberan recursos
      mf_gr.Dispose()
      mf.Dispose()
      gr.ReleaseHdc(hdc)
      gr.Dispose()

    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "Swath Profiles")
    End Try
  End Sub

  Private Sub Paint_to_File(ByVal oGraph As Graphics)
    'Esta función emula al evento paint del canvas
    'Primero se limpia el contexto gráfico con el color de fondo del canvas
    oGraph.Clear(canvas.BackColor)

    'Todo este código es igual que para el Canvas.paint
    Dim ind As Integer
    If swath_profiles IsNot Nothing Then
      ind = combo_lines.SelectedIndex
      paint_swath_profile(oGraph, ind)
    Else
      draw_chart.draw(oGraph)
    End If

  End Sub

  Private Sub paint_swath_profile(ByVal oGraph As System.Drawing.Graphics, ByVal ind As Integer)
    'Función principal que dibuja los perfiles en el gráfico

    'Se escala el draw_chart según las opciones elegidas
    If check_scale.Checked Then
      'Si se ha seleccionado que se ajuste la escala, se ajusta el gráfico con el perfil seleccionado
      draw_chart.x_axis.range = round_range(swath_profiles(ind).x_range)
      draw_chart.y_axis.range = round_range(swath_profiles(ind).y_range)
      HI_chart.x_axis.range = round_range(swath_profiles(ind).x_range)
    End If

    If show_HI Then
      draw_chart.x_axis.show_title = False
      draw_chart.x_axis.show_labels = False
      draw_chart.x_axis.show_tics = False
      HI_chart.draw(oGraph)
      HI_chart.draw_HI_scale(oGraph, profile_HI_color, ETHI)
      'HI_chart.draw_line(oGraph, swath_profiles(ind).HI_profile(New Range(0, 100)), profile_HI_color, profile_HI_size)
      HI_chart.draw_line(oGraph, swath_profiles(ind).THi_profile(ETHI), profile_HI_color, profile_HI_size)
      HI_chart.draw_horizonal_line(oGraph, 50, draw_chart.chart_color, {4, 2})
      HI_chart.draw_horizonal_line(oGraph, 75, Color.LightGray, {1.0F, 2.0F})
      HI_chart.draw_horizonal_line(oGraph, 25, Color.LightGray, {1.0F, 2.0F})

    Else
      draw_chart.x_axis.show_title = True
      draw_chart.x_axis.show_labels = True
      draw_chart.x_axis.show_tics = True

    End If

    'If show_relief = True Then
    '  'Si se ha seleccionado que se muestre el relieve local (hmax-hmin), la coordenada mínima se hace 0
    '  'draw_chart.y_axis.range = New Range(0, draw_chart.y_axis.range.max)
    'End If

    'If show_HI = True Then
    '  'Si se ha seleccionado que se muestre la integral hipsométrica, se cambia la escala para que un 20% del gráfico esté por debajo de 0
    '  'Este código se utiliza dos veces... mejorarlo!!
    '  Dim new_bottom = draw_chart.y_axis.range.length * 0.2 * -1
    '  'new_bottom = draw_chart.y_axis.range.length * 0.2 * -1
    '  draw_chart.y_axis.range = New Range(new_bottom, draw_chart.y_axis.range.max)
    '  new_bottom = draw_chart.y_axis.range.length * 0.2 * -1
    '  draw_chart.y_axis.range = New Range(new_bottom, draw_chart.y_axis.range.max)
    '  'draw_chart.y_axis.bottom = chart_bottom
    '  'draw_chart.draw_horizonal_line(oGraph, draw_chart.y_axis.bottom)
    'Else
    '  'If show_relief Or draw_chart.y_axis.range.min < 0 Then
    '  '  draw_chart.y_axis.range = New Range(0, draw_chart.y_axis.range.max)
    '  'End If
    '  'draw_chart.y_axis.range = New Range(draw_chart.y_axis.bottom, draw_chart.y_axis.range.max)
    'End If

    draw_chart.draw(oGraph)

    'If show_HI = True Then
    '  'Si se ha seleccionado mostrar el HI, se dibuja la escala alternativa
    '  draw_chart.draw_horizonal_line(oGraph, 0)
    '  draw_chart.draw_horizonal_line(oGraph, new_bottom)
    '  draw_chart.draw_horizonal_line(oGraph, new_bottom / 2, New Single() {6.0F, 4.0F})
    '  draw_chart.draw_HI_scale(oGraph)
    'End If

    If show_data Then
      'Dibuja los perfiles 
      'draw_chart.draw_polygon(oGraph, swath_profiles(ind).profile_area, New SolidBrush(Color.LightGray))
      For Each profile In swath_profiles(ind).profile_data
        draw_chart.draw_line(oGraph, profile, profile_data_color, profile_data_size)
      Next
    End If

    If show_min_max Then
      'Dibuja el perfil de alturas máximas y mínimas
      draw_chart.draw_line(oGraph, swath_profiles(ind).max_profile, profile_max_color, profile_max_size)
      draw_chart.draw_line(oGraph, swath_profiles(ind).min_profile, profile_min_color, profile_min_size)
    End If

    If show_mean Then
      'Dibuja el perfil de alturas medias
      draw_chart.draw_line(oGraph, swath_profiles(ind).mean_profile, profile_mean_color, profile_mean_size)
    End If

    If show_q1_q3 Then
      'Dibuja los perfiles q1 y q3
      draw_chart.draw_line(oGraph, swath_profiles(ind).q1_profile, profile_q1_color, profile_q1_size)
      draw_chart.draw_line(oGraph, swath_profiles(ind).q3_profile, profile_q3_color, profile_q3_size)
    End If

    If show_relief Then
      'Se dibuja el perfil del relieve local desde el fondo del gráfico
      draw_chart.draw_line(oGraph, swath_profiles(ind).relief_profile(draw_chart.y_axis.range.min), profile_relief_color, profile_relief_size)
      'Se dibuja la leyenda alternativa
      draw_chart.draw_relief_scale(oGraph, profile_relief_color, swath_profiles(ind).get_max_relief, show_HI)
      'Dim my_range As Range = swath_profiles(ind).get_relief_range
      'draw_chart.draw_relief_scale(oGraph, chart_bottom, my_range, profile_relief_color)
    End If

    'If show_HI Then
    '  'Get the range for HI drawing
    '  draw_chart.draw_line(oGraph, swath_profiles(ind).HI_profile(New Range(0, 500)), profile_HI_color, profile_HI_size)
    'End If


  End Sub

End Class
