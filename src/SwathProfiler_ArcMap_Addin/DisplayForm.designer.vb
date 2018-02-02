<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DisplayForm
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.canvas = New System.Windows.Forms.PictureBox()
    Me.properties_button = New System.Windows.Forms.Button()
    Me.export_button = New System.Windows.Forms.Button()
    Me.save_button = New System.Windows.Forms.Button()
    Me.check_min_max = New System.Windows.Forms.CheckBox()
    Me.check_mean = New System.Windows.Forms.CheckBox()
    Me.check_q1_q3 = New System.Windows.Forms.CheckBox()
    Me.check_data = New System.Windows.Forms.CheckBox()
    Me.combo_lines = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.check_scale = New System.Windows.Forms.CheckBox()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    Me.check_relief = New System.Windows.Forms.CheckBox()
    Me.check_HI = New System.Windows.Forms.CheckBox()
    CType(Me.canvas, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'canvas
    '
    Me.canvas.BackColor = System.Drawing.SystemColors.ButtonHighlight
    Me.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.canvas.Location = New System.Drawing.Point(8, 10)
    Me.canvas.Name = "canvas"
    Me.canvas.Size = New System.Drawing.Size(880, 410)
    Me.canvas.TabIndex = 0
    Me.canvas.TabStop = False
    '
    'properties_button
    '
    Me.properties_button.Location = New System.Drawing.Point(903, 372)
    Me.properties_button.Name = "properties_button"
    Me.properties_button.Size = New System.Drawing.Size(99, 21)
    Me.properties_button.TabIndex = 1
    Me.properties_button.Text = "Properties"
    Me.properties_button.UseVisualStyleBackColor = True
    '
    'export_button
    '
    Me.export_button.Location = New System.Drawing.Point(903, 345)
    Me.export_button.Name = "export_button"
    Me.export_button.Size = New System.Drawing.Size(99, 21)
    Me.export_button.TabIndex = 2
    Me.export_button.Text = "Export data"
    Me.export_button.UseVisualStyleBackColor = True
    '
    'save_button
    '
    Me.save_button.Location = New System.Drawing.Point(903, 318)
    Me.save_button.Name = "save_button"
    Me.save_button.Size = New System.Drawing.Size(99, 21)
    Me.save_button.TabIndex = 3
    Me.save_button.Text = "Save image"
    Me.save_button.UseVisualStyleBackColor = True
    '
    'check_min_max
    '
    Me.check_min_max.AutoSize = True
    Me.check_min_max.Location = New System.Drawing.Point(903, 191)
    Me.check_min_max.Name = "check_min_max"
    Me.check_min_max.Size = New System.Drawing.Size(102, 17)
    Me.check_min_max.TabIndex = 4
    Me.check_min_max.Text = "Min-Max profiles"
    Me.check_min_max.UseVisualStyleBackColor = True
    '
    'check_mean
    '
    Me.check_mean.AutoSize = True
    Me.check_mean.Location = New System.Drawing.Point(903, 214)
    Me.check_mean.Name = "check_mean"
    Me.check_mean.Size = New System.Drawing.Size(84, 17)
    Me.check_mean.TabIndex = 5
    Me.check_mean.Text = "Mean profile"
    Me.check_mean.UseVisualStyleBackColor = True
    '
    'check_q1_q3
    '
    Me.check_q1_q3.AutoSize = True
    Me.check_q1_q3.Location = New System.Drawing.Point(903, 237)
    Me.check_q1_q3.Name = "check_q1_q3"
    Me.check_q1_q3.Size = New System.Drawing.Size(102, 17)
    Me.check_q1_q3.TabIndex = 6
    Me.check_q1_q3.Text = "Quartile (Q1-Q3)"
    Me.check_q1_q3.UseVisualStyleBackColor = True
    '
    'check_data
    '
    Me.check_data.AutoSize = True
    Me.check_data.Checked = True
    Me.check_data.CheckState = System.Windows.Forms.CheckState.Checked
    Me.check_data.Location = New System.Drawing.Point(903, 168)
    Me.check_data.Name = "check_data"
    Me.check_data.Size = New System.Drawing.Size(79, 17)
    Me.check_data.TabIndex = 7
    Me.check_data.Text = "Profile data"
    Me.check_data.UseVisualStyleBackColor = True
    '
    'combo_lines
    '
    Me.combo_lines.FormattingEnabled = True
    Me.combo_lines.Location = New System.Drawing.Point(903, 25)
    Me.combo_lines.Name = "combo_lines"
    Me.combo_lines.Size = New System.Drawing.Size(101, 21)
    Me.combo_lines.TabIndex = 8
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(900, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(89, 13)
    Me.Label1.TabIndex = 9
    Me.Label1.Text = "Select line feture:"
    '
    'check_scale
    '
    Me.check_scale.AutoSize = True
    Me.check_scale.Location = New System.Drawing.Point(903, 52)
    Me.check_scale.Name = "check_scale"
    Me.check_scale.Size = New System.Drawing.Size(94, 17)
    Me.check_scale.TabIndex = 10
    Me.check_scale.Text = "Autoscale axis"
    Me.check_scale.UseVisualStyleBackColor = True
    '
    'check_relief
    '
    Me.check_relief.AutoSize = True
    Me.check_relief.Location = New System.Drawing.Point(903, 260)
    Me.check_relief.Name = "check_relief"
    Me.check_relief.Size = New System.Drawing.Size(77, 17)
    Me.check_relief.TabIndex = 12
    Me.check_relief.Text = "Local relief"
    Me.check_relief.UseVisualStyleBackColor = True
    '
    'check_HI
    '
    Me.check_HI.AutoSize = True
    Me.check_HI.Location = New System.Drawing.Point(903, 283)
    Me.check_HI.Name = "check_HI"
    Me.check_HI.Size = New System.Drawing.Size(43, 17)
    Me.check_HI.TabIndex = 13
    Me.check_HI.Text = "THi"
    Me.check_HI.UseVisualStyleBackColor = True
    '
    'DisplayForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1017, 432)
    Me.Controls.Add(Me.check_HI)
    Me.Controls.Add(Me.check_relief)
    Me.Controls.Add(Me.combo_lines)
    Me.Controls.Add(Me.check_scale)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.check_data)
    Me.Controls.Add(Me.check_q1_q3)
    Me.Controls.Add(Me.check_mean)
    Me.Controls.Add(Me.check_min_max)
    Me.Controls.Add(Me.save_button)
    Me.Controls.Add(Me.export_button)
    Me.Controls.Add(Me.properties_button)
    Me.Controls.Add(Me.canvas)
    Me.MinimumSize = New System.Drawing.Size(500, 325)
    Me.Name = "DisplayForm"
    Me.Text = "Swath profile"
    CType(Me.canvas, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents canvas As System.Windows.Forms.PictureBox
  Friend WithEvents properties_button As System.Windows.Forms.Button
  Friend WithEvents export_button As System.Windows.Forms.Button
  Friend WithEvents save_button As System.Windows.Forms.Button
  Friend WithEvents check_min_max As System.Windows.Forms.CheckBox
  Friend WithEvents check_mean As System.Windows.Forms.CheckBox
  Friend WithEvents check_q1_q3 As System.Windows.Forms.CheckBox
  Friend WithEvents check_data As System.Windows.Forms.CheckBox
  Friend WithEvents combo_lines As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents check_scale As System.Windows.Forms.CheckBox
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents check_relief As System.Windows.Forms.CheckBox
  Friend WithEvents check_HI As System.Windows.Forms.CheckBox

End Class
