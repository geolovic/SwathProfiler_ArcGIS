Public Class PropertiesForm

  Private Sub properties_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'Get draw chart properties to fields in formular
    profile_title_textbox.Text = draw_chart.profile_title
    x_min_textbox.Text = CStr(draw_chart.x_axis.range.min)
    x_max_textbox.Text = CStr(draw_chart.x_axis.range.max)
    y_min_textbox.Text = CStr(draw_chart.y_axis.range.min)
    y_max_textbox.Text = CStr(draw_chart.y_axis.range.max)

    x_tic_division_textbox.Text = CStr(draw_chart.x_axis.division)
    y_tic_division_textbox.Text = CStr(draw_chart.y_axis.division)

    check_show_x_tics.Checked = draw_chart.x_axis.show_tics
    check_show_x_grid.Checked = draw_chart.x_axis.show_grid
    check_show_x_labels.Checked = draw_chart.x_axis.show_labels
    check_show_y_tics.Checked = draw_chart.y_axis.show_tics
    check_show_y_grid.Checked = draw_chart.y_axis.show_grid
    check_show_y_labels.Checked = draw_chart.y_axis.show_labels
    check_THI.Checked = ETHI


    data_color.BackColor = profile_data_color
    thi_color.BackColor = profile_HI_color
    max_color.BackColor = profile_max_color
    min_color.BackColor = profile_min_color
    mean_color.BackColor = profile_mean_color
    relief_color.BackColor = profile_relief_color
    q1_color.BackColor = profile_q1_color
    q3_color.BackColor = profile_q3_color

    data_size.Text = CStr(profile_data_size)
    thi_size.Text = CStr(profile_HI_size)
    max_size.Text = CStr(profile_max_size)
    min_size.Text = CStr(profile_min_size)
    mean_size.Text = CStr(profile_mean_size)
    relief_size.Text = CStr(profile_relief_size)
    q1_size.Text = CStr(profile_q1_size)
    q3_size.Text = CStr(profile_q3_size)
  End Sub

  Private Sub accept_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles accept_button.Click
    'Apply properties to chart
    draw_chart.profile_title = profile_title_textbox.Text
    draw_chart.x_axis.range = New Range(CSng(x_min_textbox.Text), CSng(x_max_textbox.Text))
    draw_chart.y_axis.range = New Range(CSng(y_min_textbox.Text), CSng(y_max_textbox.Text))
    draw_chart.x_axis.division = CSng(x_tic_division_textbox.Text)
    draw_chart.y_axis.division = CSng(y_tic_division_textbox.Text)
    HI_chart.x_axis.range = New Range(CSng(x_min_textbox.Text), CSng(x_max_textbox.Text))
    HI_chart.x_axis.division = CSng(x_tic_division_textbox.Text)

    draw_chart.x_axis.show_tics = check_show_x_tics.Checked
    draw_chart.x_axis.show_grid = check_show_x_grid.Checked
    draw_chart.x_axis.show_labels = check_show_x_labels.Checked
    draw_chart.y_axis.show_tics = check_show_y_tics.Checked
    draw_chart.y_axis.show_grid = check_show_y_grid.Checked
    draw_chart.y_axis.show_labels = check_show_y_labels.Checked
    ETHI = check_THI.Checked

    profile_data_color = data_color.BackColor
    profile_max_color = max_color.BackColor
    profile_min_color = min_color.BackColor
    profile_mean_color = mean_color.BackColor
    profile_HI_color = thi_color.BackColor
    profile_relief_color = relief_color.BackColor
    profile_q1_color = q1_color.BackColor
    profile_q3_color = q3_color.BackColor

    profile_data_size = CSng(data_size.Text)
    profile_max_size = CSng(max_size.Text)
    profile_min_size = CSng(min_size.Text)
    profile_mean_size = CSng(mean_size.Text)
    profile_HI_size = CSng(thi_size.Text)
    profile_relief_size = CSng(relief_size.Text)
    profile_q1_size = CSng(q1_size.Text)
    profile_q3_size = CSng(q3_size.Text)

    Me.Close()
  End Sub

  Private Sub cancel_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancel_button.Click
    Me.Close()
  End Sub

  Private Sub data_color_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles data_color.Click, min_color.Click, _
    max_color.Click, mean_color.Click, thi_color.Click, relief_color.Click, q1_color.Click, q3_color.Click

    ColorDialog1.ShowDialog()
    sender.BackColor = ColorDialog1.Color
  End Sub

End Class