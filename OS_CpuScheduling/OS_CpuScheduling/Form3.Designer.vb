<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Me.TlpSummary = New System.Windows.Forms.TableLayoutPanel()
        Me.PnlGanntChart = New System.Windows.Forms.Panel()
        Me.LblAlgo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblAdd2 = New System.Windows.Forms.Label()
        Me.AWTsolution = New System.Windows.Forms.FlowLayoutPanel()
        Me.ATTsolution = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'TlpSummary
        '
        Me.TlpSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TlpSummary.AutoScroll = True
        Me.TlpSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TlpSummary.ColumnCount = 6
        Me.TlpSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TlpSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TlpSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TlpSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TlpSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TlpSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TlpSummary.Location = New System.Drawing.Point(46, 186)
        Me.TlpSummary.Name = "TlpSummary"
        Me.TlpSummary.RowCount = 2
        Me.TlpSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TlpSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TlpSummary.Size = New System.Drawing.Size(829, 423)
        Me.TlpSummary.TabIndex = 0
        '
        'PnlGanntChart
        '
        Me.PnlGanntChart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlGanntChart.AutoScroll = True
        Me.PnlGanntChart.BackColor = System.Drawing.Color.Transparent
        Me.PnlGanntChart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PnlGanntChart.Location = New System.Drawing.Point(46, 695)
        Me.PnlGanntChart.Name = "PnlGanntChart"
        Me.PnlGanntChart.Size = New System.Drawing.Size(1359, 100)
        Me.PnlGanntChart.TabIndex = 1
        '
        'LblAlgo
        '
        Me.LblAlgo.AutoSize = True
        Me.LblAlgo.BackColor = System.Drawing.Color.Transparent
        Me.LblAlgo.Font = New System.Drawing.Font("Segoe UI", 33.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAlgo.Location = New System.Drawing.Point(610, 15)
        Me.LblAlgo.Name = "LblAlgo"
        Me.LblAlgo.Size = New System.Drawing.Size(165, 61)
        Me.LblAlgo.TabIndex = 2
        Me.LblAlgo.Text = "Label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(922, 174)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(321, 40)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Average Wating Time:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(922, 399)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(377, 40)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "AverageTurnaround Time:"
        '
        'LblAdd2
        '
        Me.LblAdd2.BackColor = System.Drawing.Color.Transparent
        Me.LblAdd2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblAdd2.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdd2.ForeColor = System.Drawing.SystemColors.Control
        Me.LblAdd2.Location = New System.Drawing.Point(1263, 27)
        Me.LblAdd2.Name = "LblAdd2"
        Me.LblAdd2.Size = New System.Drawing.Size(131, 37)
        Me.LblAdd2.TabIndex = 15
        Me.LblAdd2.Text = "Back"
        Me.LblAdd2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'AWTsolution
        '
        Me.AWTsolution.AutoScroll = True
        Me.AWTsolution.BackColor = System.Drawing.Color.Transparent
        Me.AWTsolution.Location = New System.Drawing.Point(929, 217)
        Me.AWTsolution.Name = "AWTsolution"
        Me.AWTsolution.Size = New System.Drawing.Size(445, 179)
        Me.AWTsolution.TabIndex = 16
        '
        'ATTsolution
        '
        Me.ATTsolution.AutoScroll = True
        Me.ATTsolution.BackColor = System.Drawing.Color.Transparent
        Me.ATTsolution.Location = New System.Drawing.Point(929, 442)
        Me.ATTsolution.Name = "ATTsolution"
        Me.ATTsolution.Size = New System.Drawing.Size(445, 167)
        Me.ATTsolution.TabIndex = 16
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.OS_CpuScheduling.My.Resources.Resources.Group_9__2_
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1436, 807)
        Me.Controls.Add(Me.ATTsolution)
        Me.Controls.Add(Me.AWTsolution)
        Me.Controls.Add(Me.LblAdd2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblAlgo)
        Me.Controls.Add(Me.PnlGanntChart)
        Me.Controls.Add(Me.TlpSummary)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Name = "Form3"
        Me.Text = "Form3"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TlpSummary As TableLayoutPanel
    Friend WithEvents PnlGanntChart As Panel
    Friend WithEvents LblAlgo As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LblAdd2 As Label
    Friend WithEvents AWTsolution As FlowLayoutPanel
    Friend WithEvents ATTsolution As FlowLayoutPanel
End Class
