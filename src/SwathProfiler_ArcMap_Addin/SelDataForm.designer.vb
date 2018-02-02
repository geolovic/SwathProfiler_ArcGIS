<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelDataForm
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
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.width_textbox = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.step_size_textbox = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.CmbR = New System.Windows.Forms.ComboBox()
    Me.CmbL = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.LoadData = New System.Windows.Forms.Button()
    Me.CheckSelected = New System.Windows.Forms.CheckBox()
    Me.n_profiles_textbox = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.check_step = New System.Windows.Forms.CheckBox()
    Me.check_n_profiles = New System.Windows.Forms.CheckBox()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.check_n_profiles)
    Me.GroupBox1.Controls.Add(Me.check_step)
    Me.GroupBox1.Controls.Add(Me.n_profiles_textbox)
    Me.GroupBox1.Controls.Add(Me.Label5)
    Me.GroupBox1.Controls.Add(Me.width_textbox)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.step_size_textbox)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.CmbR)
    Me.GroupBox1.Controls.Add(Me.CmbL)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 9)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(272, 162)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Input Data"
    '
    'width_textbox
    '
    Me.width_textbox.Location = New System.Drawing.Point(156, 76)
    Me.width_textbox.Name = "width_textbox"
    Me.width_textbox.Size = New System.Drawing.Size(55, 20)
    Me.width_textbox.TabIndex = 11
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(91, 79)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(59, 13)
    Me.Label4.TabIndex = 10
    Me.Label4.Text = "Strip width:"
    '
    'step_size_textbox
    '
    Me.step_size_textbox.Enabled = False
    Me.step_size_textbox.Location = New System.Drawing.Point(156, 102)
    Me.step_size_textbox.Name = "step_size_textbox"
    Me.step_size_textbox.Size = New System.Drawing.Size(55, 20)
    Me.step_size_textbox.TabIndex = 9
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(51, 105)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(99, 13)
    Me.Label3.TabIndex = 8
    Me.Label3.Text = "Step size (optional):"
    '
    'CmbR
    '
    Me.CmbR.FormattingEnabled = True
    Me.CmbR.Location = New System.Drawing.Point(156, 49)
    Me.CmbR.Name = "CmbR"
    Me.CmbR.Size = New System.Drawing.Size(102, 21)
    Me.CmbR.TabIndex = 3
    '
    'CmbL
    '
    Me.CmbL.FormattingEnabled = True
    Me.CmbL.Location = New System.Drawing.Point(156, 19)
    Me.CmbL.Name = "CmbL"
    Me.CmbL.Size = New System.Drawing.Size(102, 21)
    Me.CmbL.TabIndex = 2
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(32, 52)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(118, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Digital Elevation Model:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(57, 22)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(93, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Line feature class:"
    '
    'LoadData
    '
    Me.LoadData.Location = New System.Drawing.Point(182, 177)
    Me.LoadData.Name = "LoadData"
    Me.LoadData.Size = New System.Drawing.Size(102, 26)
    Me.LoadData.TabIndex = 2
    Me.LoadData.Text = "Procces profiles"
    Me.LoadData.UseVisualStyleBackColor = True
    '
    'CheckSelected
    '
    Me.CheckSelected.AutoSize = True
    Me.CheckSelected.Location = New System.Drawing.Point(12, 183)
    Me.CheckSelected.Name = "CheckSelected"
    Me.CheckSelected.Size = New System.Drawing.Size(90, 17)
    Me.CheckSelected.TabIndex = 1
    Me.CheckSelected.Text = "Only selected"
    Me.CheckSelected.UseVisualStyleBackColor = True
    '
    'n_profiles_textbox
    '
    Me.n_profiles_textbox.Enabled = False
    Me.n_profiles_textbox.Location = New System.Drawing.Point(156, 128)
    Me.n_profiles_textbox.Name = "n_profiles_textbox"
    Me.n_profiles_textbox.Size = New System.Drawing.Size(55, 20)
    Me.n_profiles_textbox.TabIndex = 13
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(9, 131)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(141, 13)
    Me.Label5.TabIndex = 12
    Me.Label5.Text = "Number of profiles (optional):"
    '
    'check_step
    '
    Me.check_step.AutoSize = True
    Me.check_step.Location = New System.Drawing.Point(221, 105)
    Me.check_step.Name = "check_step"
    Me.check_step.Size = New System.Drawing.Size(15, 14)
    Me.check_step.TabIndex = 14
    Me.check_step.UseVisualStyleBackColor = True
    '
    'check_n_profiles
    '
    Me.check_n_profiles.AutoSize = True
    Me.check_n_profiles.Location = New System.Drawing.Point(221, 130)
    Me.check_n_profiles.Name = "check_n_profiles"
    Me.check_n_profiles.Size = New System.Drawing.Size(15, 14)
    Me.check_n_profiles.TabIndex = 15
    Me.check_n_profiles.UseVisualStyleBackColor = True
    '
    'SelDataForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(294, 211)
    Me.Controls.Add(Me.LoadData)
    Me.Controls.Add(Me.CheckSelected)
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "SelDataForm"
    Me.Text = "Select data"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents CmbR As System.Windows.Forms.ComboBox
  Friend WithEvents CmbL As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents LoadData As System.Windows.Forms.Button
  Friend WithEvents width_textbox As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents step_size_textbox As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents CheckSelected As System.Windows.Forms.CheckBox
  Friend WithEvents n_profiles_textbox As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents check_n_profiles As System.Windows.Forms.CheckBox
  Friend WithEvents check_step As System.Windows.Forms.CheckBox
End Class
