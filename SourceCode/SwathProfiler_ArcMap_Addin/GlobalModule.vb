Imports System.Drawing
Imports System.IO

Module GlobalModule
  'Global variables for analysis
  Public swath_profiles As List(Of SwathProfile)
  Public HI_chart As DrawChart
  Public draw_chart As DrawChart
  Public draw_rectangle As Rectangle
  Public rectangle_01 As Rectangle
  Public rectangle_02 As Rectangle
  Public rectangle_03 As Rectangle

  'Global variables for display
  Public show_min_max As Boolean
  Public show_q1_q3 As Boolean
  Public show_mean As Boolean
  Public show_data As Boolean
  Public show_relief As Boolean
  Public show_HI As Boolean
  Public ETHI As Boolean

  'Global variables for profile drawing
  Public profile_data_color As System.Drawing.Color
  Public profile_min_color As System.Drawing.Color
  Public profile_max_color As System.Drawing.Color
  Public profile_mean_color As System.Drawing.Color
  Public profile_relief_color As System.Drawing.Color
  Public profile_HI_color As System.Drawing.Color
  Public profile_q1_color As System.Drawing.Color
  Public profile_q3_color As System.Drawing.Color
  Public profile_data_size As Single
  Public profile_min_size As Single
  Public profile_max_size As Single
  Public profile_mean_size As Single
  Public profile_relief_size As Single
  Public profile_HI_size As Single
  Public profile_q1_size As Single
  Public profile_q3_size As Single


  'Helper functions
  Public Function get_x_range(ByVal in_points() As PointF) As Range
    'Esta función devuelve los valores de XMin y XMax de una matriz de puntos (PointF)
    'Los valores se devuelve como un objeto de tipo Range
    Dim xMax As Double = -99999
    Dim xMin As Double = 99999
    Dim i As Integer
    Dim out_range As Range
    For i = 0 To in_points.GetUpperBound(0)
      If in_points(i).X < xMin Then xMin = in_points(i).X
      If in_points(i).X > xMax Then xMax = in_points(i).X
    Next
    out_range = round_range(New Range(xMin, xMax))
    Return out_range
  End Function

  Public Function get_y_range(ByVal in_points() As PointF) As Range
    'Esta función devuelve los valores de XMin y XMax de una matriz de puntos (PointF)
    'Los valores se devuelve como un objeto de tipo Range
    Dim yMax As Double = -99999
    Dim yMin As Double = 99999
    Dim i As Integer
    Dim out_range As Range

    For i = 0 To in_points.GetUpperBound(0)
      If in_points(i).Y < yMin Then yMin = in_points(i).Y
      If in_points(i).Y > yMax Then yMax = in_points(i).Y
    Next
    out_range = round_range(New Range(yMin, yMax))
    Return out_range
  End Function

  Public Function round_range(ByVal in_range As Range) As Range
    'Devuelve valores redondeados max y min de un rango
    'El dato de entrada es un rango (valor maximo, valor mínimo) en la forma de un PointF (minimo = PointF.X , máximo = PointF.Y)
    'Estos valores serán las aproximaciones al numero entero un orden de magnitud menor que el rango de valores (valor_max - valor_min)
    'El valor devuelto es rango en la forma de un PointF (minimo = PointF.X , máximo = PointF.Y)
    Dim round_min As Integer
    Dim round_max As Integer
    Dim exp As Integer
    Dim outPoint As Range

    exp = Int(Math.Log10(in_range.max - in_range.min))
    If in_range.min >= 0 Then
      round_min = CInt(in_range.min - (in_range.min Mod 10 ^ (exp - 1)))
    Else
      round_min = CInt((in_range.min - (in_range.min Mod 10 ^ (exp - 1))) - 10 ^ (exp - 1))
    End If
    round_max = CInt(in_range.max + (10 ^ (exp - 1) - (in_range.max Mod 10 ^ (exp - 1))))
    outPoint = New Range(round_min, round_max)
    Return outPoint
  End Function

End Module
