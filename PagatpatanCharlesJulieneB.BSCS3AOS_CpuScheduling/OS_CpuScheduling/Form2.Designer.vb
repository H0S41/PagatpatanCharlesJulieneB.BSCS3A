<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.InputTable2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.CmbAlg = New System.Windows.Forms.ComboBox()
        Me.Prioritylbl = New System.Windows.Forms.Label()
        Me.TxtBoxAdd2 = New System.Windows.Forms.TextBox()
        Me.LblAdd2 = New System.Windows.Forms.Label()
        Me.LblCalcu2 = New System.Windows.Forms.Label()
        Me.TxtTQ = New System.Windows.Forms.TextBox()
        Me.CMBprio = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'InputTable2
        '
        Me.InputTable2.AutoScroll = True
        Me.InputTable2.BackColor = System.Drawing.Color.Transparent
        Me.InputTable2.Location = New System.Drawing.Point(541, 194)
        Me.InputTable2.Name = "InputTable2"
        Me.InputTable2.Size = New System.Drawing.Size(844, 493)
        Me.InputTable2.TabIndex = 8
        '
        'CmbAlg
        '
        Me.CmbAlg.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbAlg.FormattingEnabled = True
        Me.CmbAlg.Items.AddRange(New Object() {"First Come First Serve", "Shortest Job First", "Shortest Remaining Time", "Round Robin", "Priority Scheduling", "Priority with Round Robin"})
        Me.CmbAlg.Location = New System.Drawing.Point(53, 221)
        Me.CmbAlg.Name = "CmbAlg"
        Me.CmbAlg.Size = New System.Drawing.Size(353, 45)
        Me.CmbAlg.TabIndex = 10
        '
        'Prioritylbl
        '
        Me.Prioritylbl.BackColor = System.Drawing.Color.Transparent
        Me.Prioritylbl.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Prioritylbl.ForeColor = System.Drawing.Color.White
        Me.Prioritylbl.Location = New System.Drawing.Point(50, 407)
        Me.Prioritylbl.Name = "Prioritylbl"
        Me.Prioritylbl.Size = New System.Drawing.Size(381, 139)
        Me.Prioritylbl.TabIndex = 11
        Me.Prioritylbl.Text = "Label7"
        '
        'TxtBoxAdd2
        '
        Me.TxtBoxAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBoxAdd2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBoxAdd2.Location = New System.Drawing.Point(1077, 127)
        Me.TxtBoxAdd2.Name = "TxtBoxAdd2"
        Me.TxtBoxAdd2.Size = New System.Drawing.Size(199, 28)
        Me.TxtBoxAdd2.TabIndex = 13
        '
        'LblAdd2
        '
        Me.LblAdd2.BackColor = System.Drawing.Color.Transparent
        Me.LblAdd2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblAdd2.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdd2.ForeColor = System.Drawing.SystemColors.Control
        Me.LblAdd2.Location = New System.Drawing.Point(1297, 123)
        Me.LblAdd2.Name = "LblAdd2"
        Me.LblAdd2.Size = New System.Drawing.Size(99, 37)
        Me.LblAdd2.TabIndex = 14
        Me.LblAdd2.Text = "Add"
        Me.LblAdd2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LblCalcu2
        '
        Me.LblCalcu2.BackColor = System.Drawing.Color.Transparent
        Me.LblCalcu2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblCalcu2.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCalcu2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LblCalcu2.Location = New System.Drawing.Point(1240, 707)
        Me.LblCalcu2.Name = "LblCalcu2"
        Me.LblCalcu2.Size = New System.Drawing.Size(145, 40)
        Me.LblCalcu2.TabIndex = 15
        Me.LblCalcu2.Text = "Calculate"
        Me.LblCalcu2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TxtTQ
        '
        Me.TxtTQ.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTQ.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTQ.Location = New System.Drawing.Point(43, 689)
        Me.TxtTQ.Name = "TxtTQ"
        Me.TxtTQ.Size = New System.Drawing.Size(226, 32)
        Me.TxtTQ.TabIndex = 16
        '
        'CMBprio
        '
        Me.CMBprio.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBprio.FormattingEnabled = True
        Me.CMBprio.Items.AddRange(New Object() {"Lower number = Higher priority", "Higher number = Higher priority"})
        Me.CMBprio.Location = New System.Drawing.Point(165, 348)
        Me.CMBprio.Name = "CMBprio"
        Me.CMBprio.Size = New System.Drawing.Size(277, 29)
        Me.CMBprio.TabIndex = 10
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.OS_CpuScheduling.My.Resources.Resources.Group_8__3_
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1440, 786)
        Me.Controls.Add(Me.TxtTQ)
        Me.Controls.Add(Me.LblCalcu2)
        Me.Controls.Add(Me.LblAdd2)
        Me.Controls.Add(Me.TxtBoxAdd2)
        Me.Controls.Add(Me.Prioritylbl)
        Me.Controls.Add(Me.CMBprio)
        Me.Controls.Add(Me.CmbAlg)
        Me.Controls.Add(Me.InputTable2)
        Me.DoubleBuffered = True
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents InputTable2 As FlowLayoutPanel
    Friend WithEvents CmbAlg As ComboBox
    Friend WithEvents Prioritylbl As Label
    Friend WithEvents TxtBoxAdd2 As TextBox
    Friend WithEvents LblAdd2 As Label
    Friend WithEvents LblCalcu2 As Label
    Friend WithEvents TxtTQ As TextBox
    Friend WithEvents CMBprio As ComboBox
End Class
