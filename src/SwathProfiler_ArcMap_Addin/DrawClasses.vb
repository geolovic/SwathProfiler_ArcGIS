Imports System.Drawing

Public Structure Range
  '''Estructura para encapsular rangos de valores (valor mínimo - valor máximo)

  Private _min_value As Double
  Private _max_value As Double

  Public Sub New(ByVal min_value As Double, ByVal max_value As Double)
    'Método que crea un nuevo objeto
    _min_value = min_value
    _max_value = max_value
  End Sub

  Public Property min As Double
    'Propiedad que devuelve / establece el valor mínimo del rango
    Get
      Return _min_value
    End Get
    Set(ByVal value As Double)
      _min_value = value
    End Set
  End Property

  Public Property max As Double
    'Propiedad que devuelve / establece el valor máximo del rango
    Get
      Return _max_value
    End Get
    Set(ByVal value As Double)
      _max_value = value
    End Set
  End Property

  Public ReadOnly Property length As Double
    'Propiedad de solo lectura que devuelve la longitud (max -  min) del rango
    Get
      Return _max_value - _min_value
    End Get
  End Property

End Structure

Public Class ChartAxis
  'Clase que encapsula las principales propiedades de un eje
  Dim _min_value As Double
  Dim _max_value As Double
  Dim _division As Single
  Dim _grid As Boolean
  Dim _tics As Boolean
  Dim _tic_size As Single
  Dim _labels As Boolean
  Dim _label_gap As Single
  Dim _label_font As System.Drawing.Font
  Dim _show_title As Boolean
  Dim _title As String
  Dim _title_font As System.Drawing.Font

  Public Sub New(ByVal value_range As Range)
    _min_value = value_range.min
    _max_value = value_range.max
    _division = CInt(value_range.length / 5)
    _labels = True
    _tics = True
    _grid = True
    _show_title = True
    _tic_size = 4
    _label_gap = 3
    _label_font = New System.Drawing.Font("Arial", 9)
    _title_font = New System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold)
    _title = ""
  End Sub

  Public Sub New(ByVal value_range As Range, ByVal title As String)
    _min_value = value_range.min
    _max_value = value_range.max
    _division = CInt(value_range.length / 5)
    _labels = True
    _tics = True
    _grid = True
    _show_title = True
    _tic_size = 4
    _label_gap = 3
    _label_font = New System.Drawing.Font("Arial", 9)
    _title_font = New System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold)
    _title = title
  End Sub

  'PROPIEDADES DEL EJE
  Public Property range As Range
    'Propiedad que establece los valores mínimos y máximos del eje encapsulados en objeto Range
    Get
      Dim out_range As Range = New Range(_min_value, _max_value)
      Return out_range
    End Get
    Set(ByVal value As Range)
      _min_value = value.min
      _max_value = value.max
    End Set
  End Property

  Public Property division As Single
    'Propiedad que establece las divisiones del eje
    Get
      Return _division
    End Get
    Set(ByVal value As Single)
      _division = value
    End Set
  End Property

  Public Property show_labels As Boolean
    'Propiedad que establece si las etiquetas se mostrarán o no
    Get
      Return _labels
    End Get
    Set(ByVal value As Boolean)
      _labels = value
    End Set
  End Property

  Public Property show_tics As Boolean
    'Propiedad que establece si los tics se mostrarán o no
    Get
      Return _tics
    End Get
    Set(ByVal value As Boolean)
      _tics = value
    End Set
  End Property

  Public Property show_grid As Boolean
    'Propiedad que establece si el grid se mostrará o no
    Get
      Return _grid
    End Get
    Set(ByVal value As Boolean)
      _grid = value
    End Set
  End Property


  Public Property show_title As Boolean
    'Propiedad que establece si el grid se mostrará o no
    Get
      Return _show_title
    End Get
    Set(ByVal value As Boolean)
      _show_title = value
    End Set
  End Property

  Public Property label_font As System.Drawing.Font
    'Propiedad que establece la fuente para las etiquetas del eje
    Get
      Return _label_font
    End Get
    Set(ByVal value As System.Drawing.Font)
      _label_font = value
    End Set
  End Property

  Public Property tic_size As Single
    'Propiedad que establece el tamaño de los tics
    Get
      Return _tic_size
    End Get
    Set(ByVal value As Single)
      _tic_size = value
    End Set
  End Property

  Public Property label_gap As Single
    'Establece la separación de las etiquetas del eje de los tics
    'A esta separación se le suma automaticamente el tic_size
    Get
      Return _label_gap
    End Get
    Set(ByVal value As Single)
      _label_gap = value
    End Set
  End Property

  Public Property title_font As System.Drawing.Font
    'Propiedad que establece la fuente para el título
    Get
      Return _title_font
    End Get
    Set(ByVal value As System.Drawing.Font)
      _title_font = value
    End Set
  End Property

  Public Property title As String
    'Propiedad que establece el título del eje
    Get
      Return _title
    End Get
    Set(ByVal value As String)
      _title = value
    End Set
  End Property

End Class

Public Class DrawChart
  Dim _x_axis As ChartAxis
  Dim _y_axis As ChartAxis
  Dim _draw_area As System.Drawing.Rectangle
  Dim _profile_title As String
  Dim _show_profile_title As Boolean
  Dim _profile_title_font As System.Drawing.Font
  Dim _chart_color As System.Drawing.Color

  'CONSTRUCTOR
  Public Sub New(ByVal x_range As Range, ByVal y_range As Range, ByVal draw_area As Rectangle)
    'Inicializador de objeto. Se crean los dos ejes y el área gráfica (de tipo Rectangle)
    'Draw_area es un rectangulo que define el área de dibujo del gráfico
    _x_axis = New ChartAxis(x_range)
    _y_axis = New ChartAxis(y_range)
    _draw_area = draw_area

    'Se inicializan las demás variables con valores por defecto
    _chart_color = System.Drawing.Color.Navy
    title_font = New Font("Arial", 12, FontStyle.Bold)
    _profile_title = "Profile title"
    _show_profile_title = True
  End Sub

  'PROPIEDADES
  Public Property x_axis As ChartAxis
    'Propiedad que establece el eje X. Es un objeto de tipo ChartAxis
    Get
      Return _x_axis
    End Get
    Set(ByVal value As ChartAxis)
      _x_axis = value
    End Set
  End Property

  Public Property y_axis As ChartAxis
    'Propiedad que establece el eje Y. Es un objeto de tipo ChartAxis
    Get
      Return _y_axis
    End Get
    Set(ByVal value As ChartAxis)
      _y_axis = value
    End Set
  End Property

  Public Property profile_title As String
    'Establece el título del gráfico
    Get
      Return _profile_title
    End Get
    Set(ByVal value As String)
      If value.Length > 50 Then
        value = value.Substring(0, 50)
      End If
      _profile_title = value
    End Set
  End Property

  Public Property title_font As System.Drawing.Font
    'Establece la fuente para el título del gráfico
    Get
      Return _profile_title_font
    End Get
    Set(ByVal value As System.Drawing.Font)
      _profile_title_font = value
    End Set
  End Property

  Public Property chart_color As System.Drawing.Color
    'Establece el color para el gráfico
    Get
      Return _chart_color
    End Get
    Set(ByVal value As System.Drawing.Color)
      _chart_color = value
    End Set
  End Property

  Public ReadOnly Property get_area As System.Drawing.Rectangle
    'Esta propiedad devuelve una COPIA del rectangulo que define el área de dibujo
    Get
      Return New System.Drawing.Rectangle(_draw_area.Location, _draw_area.Size)
    End Get
  End Property

  Public Property show_title As Boolean
    Get
      Return _show_profile_title
    End Get
    Set(ByVal value As Boolean)
      _show_profile_title = value
    End Set
  End Property


  'METODOS DE DIBUJO EN EL CHART
  Public Sub draw(ByVal graph As System.Drawing.Graphics)
    'Método que dibuja el gráfico (ejes con divisiones y el rectángulo de dibujo)
    If _show_profile_title Then
      Dim title_size As SizeF = graph.MeasureString(profile_title, title_font)
      Dim punto As PointF = New PointF(_draw_area.X + _draw_area.Width / 2 - title_size.Width / 2, _draw_area.Y - title_size.Height - 10)
      graph.DrawString(_profile_title, _profile_title_font, New SolidBrush(_chart_color), punto)
    End If
    draw_x_axis(graph)
    draw_y_axis(graph)
    graph.DrawRectangle(New Pen(_chart_color, 1), _draw_area)
  End Sub

  Public Sub draw_point(ByVal graph As System.Drawing.Graphics, ByVal in_point As System.Drawing.PointF, _
                        ByVal point_size As Single, ByVal draw_color As System.Drawing.Color)
    'Función que dibuja un punto en el área gráfica
    'Se comprueba que punto a dibujarse está dentro de los límites del área gráfica
    If is_inside(in_point) Then
      'Se obtienen las coordenadas del punto en el chart
      Dim draw_point As PointF
      draw_point = get_chart_point(in_point.X, in_point.Y)
      draw_point.X = draw_point.X - (point_size / 2)
      draw_point.Y = draw_point.Y - (point_size / 2)
      graph.FillEllipse(New System.Drawing.SolidBrush(draw_color), New RectangleF _
                        (draw_point, New Size(point_size, point_size)))
    End If
  End Sub

  Public Sub draw_line(ByVal graph As System.Drawing.Graphics, ByVal line_points() As PointF, ByVal line_color As  _
                       System.Drawing.Color, ByVal line_size As Single)
    'Dibuja una línea en el gráfico, la línea se construye uniendo pares de puntos (para que se pueda manejar un emf)
    'El color y el ancho se establecen al llamar al método
    Dim i As Integer
    Dim draw_pen As System.Drawing.Pen = New System.Drawing.Pen(line_color, line_size)
    Dim graphs_points As List(Of PointF) = New List(Of PointF)
    Dim pto1 As PointF
    Dim pto2 As PointF
    For i = 0 To line_points.GetUpperBound(0) - 1
      If is_inside(line_points(i)) Then
        pto1 = get_chart_point(line_points(i).X, line_points(i).Y)
        pto2 = get_chart_point(line_points(i + 1).X, line_points(i + 1).Y)
        graph.DrawLine(draw_pen, pto1, pto2)
        'graphs_points.Add(get_chart_point(line_points(i).X, line_points(i).Y))
      End If
    Next
    'graph.DrawCurve(draw_pen, graphs_points.ToArray)
  End Sub

  Public Sub draw_line2(ByVal graph As System.Drawing.Graphics, ByVal line_points() As PointF, ByVal line_color As  _
                       System.Drawing.Color, ByVal line_size As Single)
    'Dibuja una línea en el gráfico, la línea se establece como una matriz de tipo PointF.
    'El color y el ancho se establecen al llamar al método
    Dim i As Integer
    Dim draw_pen As System.Drawing.Pen = New System.Drawing.Pen(line_color, line_size)
    Dim graphs_points As List(Of PointF) = New List(Of PointF)
    'Dim pto1 As PointF
    'Dim pto2 As PointF
    For i = 0 To line_points.GetUpperBound(0) - 1
      If is_inside(line_points(i)) Then
        'pto1 = get_chart_point(line_points(i).X, line_points(i).Y)
        'pto2 = get_chart_point(line_points(i + 1).X, line_points(i + 1).Y)
        'graph.DrawLine(draw_pen, pto1, pto2)
        graphs_points.Add(get_chart_point(line_points(i).X, line_points(i).Y))
      End If
    Next
    graph.DrawCurve(draw_pen, graphs_points.ToArray)
  End Sub

  Public Sub draw_horizonal_line(ByVal graph As System.Drawing.Graphics, ByVal altitude As Single, ByVal lcolor As System.Drawing.Color, ByVal dash_pattern As Single())
    'Dibuja una línea horizontal en el gráfico con un patrón (linea punto, etc)
    'El patron es una matriz de tipo single que establece "linea - espacio - linea ... "
    Dim line_size As Single = 1
    Dim pen_grid As System.Drawing.Pen = New System.Drawing.Pen(lcolor, line_size)
    pen_grid.DashPattern = dash_pattern
    graph.DrawLine(pen_grid, get_chart_point(_x_axis.range.min, altitude), get_chart_point(_x_axis.range.max, altitude))
  End Sub

  Public Sub draw_horizonal_line(ByVal graph As System.Drawing.Graphics, ByVal altitude As Single, ByVal lcolor As System.Drawing.Color)
    'Dibuja una línea horizontal en el gráfico
    Dim line_size As Single = 1
    Dim pen_grid As System.Drawing.Pen = New System.Drawing.Pen(lcolor, line_size)
    graph.DrawLine(pen_grid, get_chart_point(_x_axis.range.min, altitude), get_chart_point(_x_axis.range.max, altitude))
  End Sub

  'METODO PARA ESTABLECER UNA NUEVA ÁREA DE DIBUJO
  Public Sub set_draw_area(ByVal draw_area As System.Drawing.Rectangle)
    'Metodo que establece una nueva área de dibujo como un objeto de tipo Rectangle
    _draw_area = draw_area
  End Sub

  'FUNCIONES DE AMBITO PRIVADO
  Private Sub draw_x_axis(ByVal graph As System.Drawing.Graphics)
    'Función que dibuja los Tics, Etiquetas y el grid del eje X
    'Se declaran las variables necesarias para dibujar el eje x
    Dim tic_point As PointF
    Dim tic_x As Double, tic_y As Double
    Dim point_1 As PointF
    Dim point_2 As PointF
    Dim label_size As SizeF
    Dim pen_grid As System.Drawing.Pen = New Pen(Color.LightGray, 0.75)
    pen_grid.DashPattern = New Single() {1.0F, 2.0F}
    Dim last_label_Xposition As Single
    Dim drawing_tic As Boolean = False

    tic_x = _x_axis.range.min
    tic_y = _y_axis.range.min
    tic_point = New PointF(tic_x, tic_y)
    Do While tic_x <= _x_axis.range.max
      'Se establecen los puntos para pintar las líneas y situar las etiquetas
      point_1 = get_chart_point(tic_x, tic_y)
      point_2 = get_chart_point(tic_x, tic_y)
      point_2.Y = point_2.Y + _x_axis.tic_size

      'Puntos auxiliares para pintar los ticks
      Dim aux_pt1 As PointF = New PointF(point_1.X, point_1.Y)
      Dim aux_pt2 As PointF = New PointF(point_2.X, point_2.Y)

      'Se pinta el grid si está establecido
      If _x_axis.show_grid = True Then
        point_2 = get_chart_point(tic_x, _y_axis.range.max)
        graph.DrawLine(pen_grid, point_1, point_2)
      End If

      'Se dibujan las etiquetas si está establecido
      If _x_axis.show_labels Then
        label_size = graph.MeasureString(CStr(Int(tic_x)), _x_axis.label_font)
        point_1.X = point_1.X - (label_size.Width / 2)
        point_1.Y = point_1.Y + _x_axis.tic_size + _x_axis.label_gap
        drawing_tic = False
        If point_1.X > last_label_Xposition Then
          graph.DrawString(CStr(Int(tic_x)), _x_axis.label_font, New System.Drawing.SolidBrush(_chart_color), point_1)
          last_label_Xposition = point_1.X + label_size.Width
          drawing_tic = True
        End If
      End If

      'Si se ha dibujado el label, se dibuja el tick
      If _x_axis.show_tics And drawing_tic Then
        graph.DrawLine(New Pen(_chart_color, 1), aux_pt1, aux_pt2)
      End If

      tic_x += _x_axis.division
    Loop

    'Se dibuja el título si este se ha establecido
    If _x_axis.title.Length > 0 And _x_axis.show_title Then
      Dim x_title_size As SizeF = graph.MeasureString(_x_axis.title, _x_axis.title_font)
      Dim aux_label As String = CStr(_x_axis.range.max)
      Dim aux_label_size As SizeF = graph.MeasureString(aux_label, _x_axis.label_font)
      Dim punto As PointF = New PointF(_draw_area.X + _draw_area.Width / 2 - x_title_size.Width / 2, _
                                       _draw_area.Y + _draw_area.Height + _x_axis.tic_size + _x_axis.label_gap _
                                       + aux_label_size.Height + 3)
      graph.DrawString(_x_axis.title, _x_axis.title_font, New SolidBrush(_chart_color), punto)
    End If
  End Sub

  Private Sub draw_y_axis(ByVal graph As Graphics)
    'Función que dibuja los Tics, Etiquetas y el grid del eje Y
    'Se declaran las variables necesarias para dibujar el eje Y
    Dim tic_point As PointF
    Dim tic_x As Double, tic_y As Double
    Dim point_1 As PointF
    Dim point_2 As PointF
    Dim label_size As SizeF
    Dim pen_grid As System.Drawing.Pen = New Pen(Color.LightGray, 0.75)
    pen_grid.DashPattern = New Single() {1.0F, 2.0F}
    tic_x = _x_axis.range.min
    tic_y = _y_axis.range.min
    tic_point = New PointF(tic_x, tic_y)

    Do While tic_y <= _y_axis.range.max
      'Se establecen los puntos para pintar las líneas y situar las etiquetas
      point_1 = get_chart_point(tic_x, tic_y)
      point_2 = get_chart_point(tic_x, tic_y)
      point_1.X = point_1.X - _y_axis.tic_size

      'Se pintan los tics si está establecido
      If _y_axis.show_tics Then
        graph.DrawLine(New Pen(_chart_color, 1), point_1, point_2)
      End If

      'Se pinta el grid si está establecido
      If _y_axis.show_grid Then
        graph.DrawLine(pen_grid, point_2, get_chart_point(_x_axis.range.max, tic_y))
      End If

      'Se dibujan las etiquetas si está establecido
      If _y_axis.show_labels Then
        label_size = graph.MeasureString(CStr(Int(tic_y)), _y_axis.label_font)
        point_1.X = point_1.X - (label_size.Width) - _y_axis.label_gap
        point_1.Y = point_1.Y - (label_size.Height / 2)
        graph.DrawString(CStr(Int(tic_y)), _y_axis.label_font, New System.Drawing.SolidBrush(_chart_color), point_1)
      End If
      tic_y += _y_axis.division
    Loop

    'Se dibuja el título si este se ha establecido
    If _y_axis.title.Length > 0 And _y_axis.show_title Then
      Dim y_title_size As SizeF = graph.MeasureString(_y_axis.title, _y_axis.title_font)
      Dim aux_label As String = CStr(_y_axis.range.max)
      Dim aux_label_size As SizeF = graph.MeasureString(aux_label, _y_axis.label_font)
      Dim punto As PointF = New PointF(_draw_area.X - 6 - aux_label_size.Width - _y_axis.tic_size - y_axis.label_gap - y_title_size.Height _
                                       , _draw_area.Y + _draw_area.Height / 2 + y_title_size.Width / 2)
      graph.TranslateTransform(punto.X, punto.Y)
      graph.RotateTransform(-90)
      graph.DrawString(_y_axis.title, _y_axis.title_font, New SolidBrush(_chart_color), New Point(0, 0))
      graph.ResetTransform()
    End If
  End Sub

  Private Function get_point(ByVal chart_x As Double, ByVal chart_y As Double) As PointF
    'Esta función devuelve las coordenadas reales de un punto según la escala X e Y tomando como entrada las coordenadas X e Y del objeto gráfico
    'donde se dibuja el Chart (pictureBox)
    Dim out_point As PointF
    Dim x_val As Double, y_val As Double
    x_val = (((chart_x - _draw_area.X) * (_x_axis.range.max - _x_axis.range.min)) / _draw_area.Width) + _x_axis.range.min
    y_val = ((_draw_area.Height - chart_y + _draw_area.Y) / _draw_area.Height) * (_y_axis.range.max - _y_axis.range.min) + _y_axis.range.min
    'y_val = (((chart_y - _draw_area.Y) * (_y_axis.range.max - _y_axis.range.min)) / _draw_area.Height) + _y_axis.range.min
    'y_val = y_val - _draw_area.Y - _draw_area.Height
    out_point = New PointF(x_val, y_val)
    Return out_point
  End Function

  Private Function get_chart_point(ByVal x_coord As Double, ByVal y_coord As Double) As PointF
    'Devuelve un punto con las coordenadas del objeto gráfico a partir dos coordenadas reales
    'Se utiliza para dibujar en el gráfico
    Dim chart_point As PointF
    Dim chart_x As Double
    Dim chart_y As Double
    chart_x = (((x_coord - _x_axis.range.min) / (_x_axis.range.max - _x_axis.range.min)) * _draw_area.Width) + _draw_area.X
    chart_y = (((y_coord - _y_axis.range.min) / (_y_axis.range.max - _y_axis.range.min)) * _draw_area.Height)
    chart_y = _draw_area.Height + _draw_area.Y - chart_y
    chart_point = New PointF(chart_x, chart_y)
    Return chart_point
  End Function

  Private Function is_inside(ByVal in_point As PointF) As Boolean
    'Comprueba si un punto (de coordenadas reales) está o no dentro de aréa gráfica
    If (in_point.X >= _x_axis.range.min And in_point.X <= _x_axis.range.max) And (in_point.Y >= _y_axis.range.min And in_point.Y <= _y_axis.range.max) Then
      Return True
    Else
      Return False
    End If
  End Function

  'OTRAS FUNCIONES (sin mucha utilidad, para debugging o antíguas)
  Public Sub draw_HI_scale(ByVal graph As System.Drawing.Graphics, ByVal hi_color As Color, ByVal ETHI As Boolean)
    'Función que dibuja la escala para la Transverse Hipsometry a la izquierda del gráfico
    'Se declaran las variables necesarias para dibujar el eje Y
    Dim tic_point As PointF
    Dim tic_x As Double, tic_y As Double
    Dim point_1 As PointF
    Dim point_2 As PointF
    Dim label_size As SizeF
    tic_x = _x_axis.range.max
    tic_y = _y_axis.range.min
    tic_point = New PointF(tic_x, tic_y)

    Dim labels As String() = {"1.0", "0.5", "0.0"}
    'Dim labels As String() = {"0.8", "0.5", "0.2"}
    For i = 0 To 2
      point_1 = New PointF(_draw_area.Location.X + _draw_area.Size.Width, _draw_area.Location.Y + (_draw_area.Size.Height / 2) * i)
      point_2 = New PointF(_draw_area.Location.X + _draw_area.Size.Width, _draw_area.Location.Y + (_draw_area.Size.Height / 2) * i)
      point_1.X = point_1.X + _y_axis.tic_size
      graph.DrawLine(New Pen(hi_color, 1), point_1, point_2)

      label_size = graph.MeasureString(labels(i), _y_axis.label_font)
      point_1.X += _y_axis.label_gap
      point_1.Y -= label_size.Height / 2
      graph.DrawString(labels(i), _y_axis.label_font, New System.Drawing.SolidBrush(hi_color), point_1)
    Next

    point_1 = New PointF(_draw_area.X + _draw_area.Size.Width + 40, _draw_area.Location.Y + (_draw_area.Size.Height / 2))
    point_1.Y -= label_size.Height / 2
    Dim label As String = "THi"

    If ETHI Then
      label = "THi*"
    End If

    graph.DrawString(label, _y_axis.title_font, New System.Drawing.SolidBrush(hi_color), point_1)
  End Sub

  Public Sub draw_relief_scale(ByVal graph As System.Drawing.Graphics, ByVal r_color As Color, ByVal max_relief As Single, ByVal flag As Boolean)
    'Función que dibuja la escala para el local relief a la izquierda del gráfico
    'Se declaran las variables necesarias para dibujar el eje Y
    Dim tic_point As PointF
    Dim tic_x As Double, tic_y As Double
    Dim point_1 As PointF
    Dim point_2 As PointF
    Dim label_size As SizeF
    tic_x = _x_axis.range.max
    tic_y = _y_axis.range.min
    tic_point = New PointF(tic_x, tic_y)

    Dim label As Integer = 0

    Do While label <= max_relief
      'Se establecen los puntos para pintar las líneas y situar las etiquetas
      point_1 = get_chart_point(tic_x, tic_y)
      point_2 = get_chart_point(tic_x, tic_y)
      point_1.X = point_1.X + _y_axis.tic_size

      'Se pintan los tics si está establecido
      If _y_axis.show_tics Then
        graph.DrawLine(New Pen(r_color, 1), point_1, point_2)
      End If

      'Se dibujan las etiquetas

      label_size = graph.MeasureString(CStr(Int(tic_y)), _y_axis.label_font)
      point_1.X = point_1.X + _y_axis.label_gap + _y_axis.label_gap
      point_1.Y = point_1.Y - (label_size.Height / 2)
      If label = 0 And flag = True Then

      Else
        graph.DrawString(CStr(label), _y_axis.label_font, New System.Drawing.SolidBrush(r_color), point_1)
      End If


      tic_y += _y_axis.division
      label += _y_axis.division
    Loop

    'Se dibuja un tic por encima de máximo (TODO EL CÓDIGO ES IGUAL QUE EN EL LOOP)
    'Se establecen los puntos para pintar las líneas y situar las etiquetas
    point_1 = get_chart_point(tic_x, tic_y)
    point_2 = get_chart_point(tic_x, tic_y)
    point_1.X = point_1.X + _y_axis.tic_size

    'Se pintan los tics si está establecido
    If _y_axis.show_tics Then
      graph.DrawLine(New Pen(r_color, 1), point_1, point_2)
    End If

    graph.DrawLine(New Pen(r_color, 1), point_2, get_chart_point(_x_axis.range.max, _y_axis.range.min))

    'Se dibujan las etiquetas si está establecido
    If _y_axis.show_labels Then
      label_size = graph.MeasureString(CStr(Int(tic_y)), _y_axis.label_font)
      point_1.X = point_1.X + _y_axis.label_gap
      point_1.Y = point_1.Y - (label_size.Height / 2)
      graph.DrawString(CStr(label), _y_axis.label_font, New System.Drawing.SolidBrush(r_color), point_1)
    End If


    ''Se dibuja el título para el local relief
    'If _y_axis.title.Length > 0 Then
    Dim y_title_size As SizeF = graph.MeasureString(_y_axis.title, _y_axis.title_font)
    Dim aux_label As String = CStr(_y_axis.range.max)
    Dim aux_label_size As SizeF = graph.MeasureString("Local relief (m)", _y_axis.label_font)
    Dim labelY As Single = point_2.Y + ((_draw_area.Size.Height - point_2.Y) / 2)

    Dim punto As PointF = New PointF(_draw_area.X + _draw_area.Size.Width + 40, _draw_area.Y + _draw_area.Height)
    graph.TranslateTransform(punto.X, punto.Y)
    graph.RotateTransform(-90)
    graph.DrawString("Local relief (m)", _y_axis.title_font, New SolidBrush(r_color), New Point(0, 0))
    graph.ResetTransform()
    'End If

    'graph As System.Drawing.Graphics, offset As Single, rango As Range, LColor As Color
  End Sub

End Class

Public Class SwathProfile
  ''' <summary>
  ''' La clase SwathProfile encapsula los datos de todos los perfiles realizados en la banda y algunas propiedades necesarias para el dibujo.
  ''' Los datos de los perfiles se guardan como una lista de "listas de puntos", de manera que cada lista de puntos es uno de los perfiles.
  ''' Los perfiles _min, _max, _mean, _relief y _HI se guardan como listas independientes de puntos.
  ''' Para cada perfil tambien se guardan propiedades como el nombre y los valores máximos-mínimos (como rangos) de los ejes X e Y
  ''' El campo _n_data guarda el número de perfiles en el SWATH
  ''' </summary>
  ''' <remarks></remarks>
  Private _profile_name As String
  Private _n_data As Integer
  Private _x_range As Range
  Private _y_range As Range
  Private _min_profile() As PointF
  Private _max_profile() As PointF
  Private _mean_profile() As PointF
  Private _relief_profile() As PointF
  Private _HI_profile() As PointF
  Private _THi_profile() As PointF
  Private _st_dev_profile() As PointF
  Private _q1_profile() As PointF
  Private _q3_profile() As PointF
  Private _profile_data As List(Of PointF())

  Private _max_relief As Single

  'Constructores. Para crear un swath profile se introduce una lista de "listas" de puntos (datos de cada perfil)
  'Exite una sobrecarga para meterle el nombre del perfil
  Public Sub New(ByVal profile_data As List(Of PointF()))
    'Se inicializan los campos principales
    'Para inicializar el objeto se toma como argumento una lista de "perfiles" (listas de puntos)
    _profile_data = profile_data
    _n_data = profile_data.Count
    _profile_name = "Swath profile"

    'Se crean los perfiles de valores min, max, mean, local relief y HI
    create_max_min(profile_data)
  End Sub

  Public Sub New(ByVal profile_data As List(Of PointF()), ByVal name As String)
    'Se inicializan los campos principales
    'Para inicializar el objeto se toma como argumento una lista de "perfiles" (listas de puntos)
    _profile_data = profile_data
    _n_data = profile_data.Count
    _profile_name = name

    'Se crean los perfiles de valores min, max, mean, local relief y HI
    create_max_min(profile_data)
  End Sub

  'Propiedades del perfil como nombre y número de perfiles que forman el perfil swath
  Public Property name As String
    'Propiedad que establece el nombre del perfil
    Get
      Return _profile_name
    End Get
    Set(ByVal value As String)
      _profile_name = value
    End Set
  End Property

  Public Property count As Integer
    'Propiedad que establece el número de perfiles
    Get
      Return _n_data
    End Get
    Set(ByVal value As Integer)
      _n_data = value
    End Set
  End Property

  'Propiedades de los ejes del Chart ( X e Y)
  Public ReadOnly Property x_range As Range
    'Propiedad que devuelve el rango máximo de valores para el eje x (de todos los perfiles)
    Get
      Return _x_range
    End Get
  End Property

  Public ReadOnly Property y_range As Range
    'Propiedad que devuelve el rango máximo de valores para el eje y (de todos los perfiles)
    Get
      Return _y_range
    End Get
  End Property

  'Propiedades del chart para devolver los datos o los perfiles máximos, mimino, media, relieve y HI
  Public ReadOnly Property min_profile As PointF()
    'Propiedad que devuelve el perfil con las elevaciones mínimas
    Get
      Return _min_profile
    End Get
  End Property

  Public ReadOnly Property max_profile As PointF()
    'Propiedad que devuelve el perfil con las elevaciones máximas
    Get
      Return _max_profile
    End Get
  End Property

  Public ReadOnly Property mean_profile As PointF()
    'Propiedad que devuelve el perfil con las elevaciones medias
    Get
      Return _mean_profile
    End Get
  End Property

  Public ReadOnly Property q1_profile As PointF()
    'Propiedad que devuelve el perfil con el primer cuartil
    Get
      Return _q1_profile
    End Get
  End Property

  Public ReadOnly Property q3_profile As PointF()
    'Propiedad que devuelve el perfil con el tercer cuartil
    Get
      Return _q3_profile
    End Get
  End Property

  Public ReadOnly Property relief_profile As PointF()
    'Propiedad que devuelve el perfil con el relieve local (Hmax - Hmin)
    Get
      Return _relief_profile
    End Get
  End Property

  Public ReadOnly Property HI_profile As PointF()
    'Esta propiedad devuelve el perfile con la integral hipsométrica sin escalar
    'Se utiliza para exportar los datos a texto
    Get
      Return _HI_profile
    End Get
  End Property

  Public ReadOnly Property THi_profile(ByVal enhanced As Boolean) As PointF()
    'Esta propiedad devuelve el perfile con la integral hipsométrica sin escalar
    'Se utiliza para exportar los datos a texto
    Get
      calculate_THi_profile(enhanced)
      Return _THi_profile
    End Get
  End Property

  Public ReadOnly Property profile_data As List(Of PointF())
    'Propiedad que devuelve los datos de los perfiles (una lista con todos los perfiles - matrices de puntos)
    Get
      Return _profile_data
    End Get
  End Property

  Public ReadOnly Property get_max_relief As Single
    'Propiedad que devuelve el relieve máximo
    Get
      Return _max_relief
    End Get
  End Property

  'Propiedades sobrecargadas para devolver los perfiles de relieve local y HI escalados (para su dibujo)
  'Y un método (no utilizado) para devolver el área entre el perfil máximo y mínimo
  Public ReadOnly Property relief_profile(ByVal offset As Single) As PointF()
    'Propiedad que devuelve el perfil con el relieve local (Hmax - Hmin)
    Get
      Dim offset_relief As List(Of PointF) = New List(Of PointF)
      For i = 0 To _relief_profile.Length - 1
        offset_relief.Add(New PointF(_HI_profile(i).X, _relief_profile(i).Y + offset))
      Next
      Return offset_relief.ToArray
    End Get
  End Property

  Public ReadOnly Property HI_profile(ByVal rango As Range) As PointF()
    'Propiedad que devuelve el perfil con la integral hipsométrica (mean- min / max - min)
    'La propiedad toma como parámetro un rango (range) para dibujar la hipsometría (este rango siempre estára por debajo de cero)
    'Los valores devueltos están escalados de 0.2 (0)  a 0.8 (1) para realzar diferencias
    Get
      Dim scaled_HI As List(Of PointF) = New List(Of PointF)
      Dim HI_value As Single
      For i = 0 To _HI_profile.Length - 1
        HI_value = _HI_profile(i).Y
        HI_value = (HI_value - 0.2) / 0.6
        HI_value = HI_value * rango.length
        scaled_HI.Add(New PointF(_HI_profile(i).X, HI_value))
      Next
      Return scaled_HI.ToArray
    End Get
  End Property

  Private Sub calculate_THi_profile(ByVal enhanced As Boolean)
    'Método para calcular la integral hipsométrica
    'La propiedad toma como parámetro un rango (range) para dibujar la hipsometría (este rango siempre estára por debajo de cero)
    'Los valores devueltos están escalados de 0.2 (0)  a 0.8 (1) para realzar diferencias
    Dim THI As List(Of PointF) = New List(Of PointF)
    Dim HI_value As Single
    Dim factor As Single
    Dim HI As Single
    For i = 0 To _HI_profile.Length - 1
      If enhanced Then
        HI = (_HI_profile(i).Y - 0.2) / 0.6
      Else
        HI = _HI_profile(i).Y
      End If
      factor = 0.2 * Math.Log(_relief_profile(i).Y / _max_relief) + 1
      HI_value = ((HI - 0.5) * factor + 0.5) * 100
      THI.Add(New PointF(_HI_profile(i).X, HI_value))
    Next
    _THi_profile = THI.ToArray()
  End Sub

  Public ReadOnly Property profile_area As PointF()
    'Esta función devuelve un polígono que tiene los perfiles máximos y mínimos 
    'La matriz devuelta es el perfil máximo más el mínimo
    Get
      Dim profile_points As List(Of PointF) = New List(Of PointF)
      'Se "llena" la nueva lista de puntos con los puntos del perfil máximo.
      For i = 0 To _max_profile.Length - 1
        profile_points.Add(New PointF(_max_profile(i).X, _max_profile(i).Y))
      Next
      For i = _max_profile.Length - 1 To 0 Step -1
        profile_points.Add(New PointF(_min_profile(i).X, _min_profile(i).Y))
      Next

      Return profile_points.ToArray
    End Get
  End Property

  'Metodo privado para crear los datos
  Private Sub create_max_min(ByVal profile_data As List(Of PointF()))
    'Loop for max and minimun values
    Dim min_value As Double, max_value As Double, mean_value As Double, q1_value As Double, q3_value As Double ', st_dev_value As double
    Dim x_value As Double
    Dim abs_y_min As Double = 9999999999
    Dim abs_y_max As Double = -9999999999
    Dim acc As Double, aux_counter As Integer
    Dim n_points As Integer
    Dim min_values As List(Of PointF) = New List(Of PointF)
    Dim max_values As List(Of PointF) = New List(Of PointF)
    Dim mean_values As List(Of PointF) = New List(Of PointF)
    Dim relief_values As List(Of PointF) = New List(Of PointF)
    Dim HI_values As List(Of PointF) = New List(Of PointF)
    'Dim st_dev_values As List(Of PointF) = New List(Of PointF)
    Dim q1_values As List(Of PointF) = New List(Of PointF)
    Dim q3_values As List(Of PointF) = New List(Of PointF)

    Dim max_relief As Single = -9999999999
    'Se modifican todos los perfiles para que tengan las mismas X y el mismo número de puntos
    Dim baseline As PointF() = profile_data(0)
    n_points = baseline.Length
    For Each profile In profile_data
      ReDim Preserve profile(n_points)
      For i = 0 To n_points - 1
        profile(i).X = baseline(i).X
      Next
    Next

    For i = 0 To n_points - 1
      min_value = 9999999999
      max_value = -9999999999
      acc = 0
      aux_counter = 0
      Dim aux_values As List(Of Double) = New List(Of Double)

      For Each perfil In profile_data
        aux_values.Add(perfil(i).Y)
        If perfil(i).Y > max_value Then max_value = perfil(i).Y
        If perfil(i).Y < min_value Then min_value = perfil(i).Y
        acc += perfil(i).Y
        aux_counter += 1
      Next

      x_value = baseline(i).X

      If min_value < abs_y_min Then abs_y_min = min_value
      If max_value > abs_y_max Then abs_y_max = max_value
      mean_value = acc / aux_counter

      aux_values.Sort()
      Dim q1 As Single = (aux_values.Count + 1) / 4
      Dim q3 As Single = 3 * (aux_values.Count + 1) / 4

      If (q1 Mod 1) = 0 Then
        q1_value = aux_values(Int(q1))
      Else
        q1_value = (aux_values(Int(q1)) + aux_values(Int(q1) + 1)) / 2
      End If

      If (q3 Mod 1) = 0 Then
        q3_value = aux_values(Int(q3))
      Else
        q3_value = (aux_values(Int(q3)) + aux_values(Int(q3) + 1)) / 2
      End If

      'acc = 0
      'For Each value In aux_values
      '  acc += (value - mean_value) ^ 2
      'Next
      'st_dev_value = Math.Sqrt(acc / aux_values.Count)


      If max_value - min_value > max_relief Then
        max_relief = max_value - min_value
      End If
      If min_value = max_value Then
        max_value += 0.00001
      End If
      min_values.Add(New PointF(x_value, min_value))
      max_values.Add(New PointF(x_value, max_value))
      mean_values.Add(New PointF(x_value, mean_value))
      'st_dev_values.Add(New PointF(x_value, st_dev_value))
      q1_values.Add(New PointF(x_value, q1_value))
      q3_values.Add(New PointF(x_value, q3_value))
      relief_values.Add(New PointF(x_value, max_value - min_value))
      HI_values.Add(New PointF(x_value, (mean_value - min_value) / (max_value - min_value)))
    Next

    'Store results in profile arrays
    _max_relief = max_relief
    _min_profile = min_values.ToArray()
    _max_profile = max_values.ToArray()
    _mean_profile = mean_values.ToArray()
    _relief_profile = relief_values.ToArray()
    _q1_profile = q1_values.ToArray()
    _q3_profile = q3_values.ToArray()
    _HI_profile = HI_values.ToArray()
    '_st_dev_profile = st_dev_values.ToArray()
    _x_range = New Range(_max_profile(0).X, _max_profile(_max_profile.GetUpperBound(0)).X)
    _y_range = New Range(abs_y_min, abs_y_max)
  End Sub


End Class

