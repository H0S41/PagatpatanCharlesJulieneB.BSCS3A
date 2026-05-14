Public Class Form3

    Private _results As List(Of ProcessResult)
    Private _gantt As List(Of GanttBlock)
    Private _algorithm As String

    Public Sub New(results As List(Of ProcessResult), gantt As List(Of GanttBlock), algorithm As String)
        InitializeComponent()
        _results = results
        _gantt = gantt
        _algorithm = algorithm
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblAlgo.Text = _algorithm
        PopulateResultsTable()
        DrawGanttChart()
        ShowTATSolution()
        ShowWTSolution()
    End Sub

    ' result display table
    Private Sub PopulateResultsTable()
        If _results Is Nothing OrElse _results.Count = 0 Then
            MessageBox.Show("No results to display.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        TlpSummary.Controls.Clear()
        TlpSummary.RowStyles.Clear()
        TlpSummary.ColumnStyles.Clear()
        TlpSummary.RowCount = 0
        TlpSummary.ColumnCount = 0

        Dim sortedResults = _results.
        Where(Function(r) r.Name IsNot Nothing AndAlso r.Name <> "").
        OrderBy(Function(r)
                    Dim num As Integer
                    Dim nameNum As String = r.Name.Replace("P", "").Trim()
                    If Integer.TryParse(nameNum, num) Then
                        Return num
                    Else
                        Return 0
                    End If
                End Function).ToList()

        ' adds priority column if the algorithm is priority based
        Dim showPriority As Boolean = (_algorithm = "Priority" OrElse _algorithm = "PriorityRR")

        Dim headers As New List(Of String)
        headers.Add("Process Name")
        headers.Add("Arrival")
        headers.Add("Burst Time")
        If showPriority Then headers.Add("Priority")
        headers.Add("Completion")
        headers.Add("Turnaround")
        headers.Add("Waiting")

        TlpSummary.ColumnCount = headers.Count
        For i As Integer = 0 To headers.Count - 1
            TlpSummary.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F / headers.Count))
        Next

        Dim totalRows As Integer = sortedResults.Count + 2
        TlpSummary.RowCount = totalRows

        TlpSummary.RowStyles.Add(New RowStyle(SizeType.Absolute, 32))
        For i As Integer = 0 To sortedResults.Count - 1
            TlpSummary.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        Next
        TlpSummary.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        ' Header row
        For i As Integer = 0 To headers.Count - 1
            Dim headerLabel As New Label()
            headerLabel.Text = headers(i)
            headerLabel.Dock = DockStyle.Fill
            headerLabel.TextAlign = ContentAlignment.MiddleLeft
            headerLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            headerLabel.ForeColor = Color.White
            headerLabel.BackColor = Color.FromArgb(30, 40, 70)
            headerLabel.Padding = New Padding(5, 0, 0, 0)
            headerLabel.Margin = New Padding(0)
            TlpSummary.Controls.Add(headerLabel, i, 0)
        Next

        ' Data rows
        For rowIndex As Integer = 0 To sortedResults.Count - 1
            Dim r As ProcessResult = sortedResults(rowIndex)

            Dim rowColor As Color = If(rowIndex Mod 2 = 0,
            Color.FromArgb(15, 20, 40),
            Color.FromArgb(20, 28, 55))

            Dim values As New List(Of String)
            values.Add(r.Name)
            values.Add(r.ArrivalTime.ToString())
            values.Add(r.BurstTime.ToString())
            If showPriority Then values.Add(r.Priority.ToString())
            values.Add(r.CompletionTime.ToString())
            values.Add(r.TurnaroundTime.ToString())
            values.Add(r.WaitingTime.ToString())

            For col As Integer = 0 To values.Count - 1
                Dim cellLabel As New Label()
                cellLabel.Text = values(col)
                cellLabel.Dock = DockStyle.Fill
                cellLabel.TextAlign = ContentAlignment.MiddleLeft
                cellLabel.Font = New Font("Segoe UI", 9)
                cellLabel.ForeColor = Color.White
                cellLabel.BackColor = rowColor
                cellLabel.Padding = New Padding(5, 0, 0, 0)
                cellLabel.Margin = New Padding(0)
                TlpSummary.Controls.Add(cellLabel, col, rowIndex + 1)
            Next
        Next

        ' Filler row
        For col As Integer = 0 To headers.Count - 1
            Dim fillerLabel As New Label()
            fillerLabel.Dock = DockStyle.Fill
            fillerLabel.BackColor = Color.FromArgb(15, 20, 40)
            fillerLabel.Margin = New Padding(0)
            TlpSummary.Controls.Add(fillerLabel, col, sortedResults.Count + 1)
        Next
    End Sub


    ' gantt chart display
    Private Sub DrawGanttChart()
        PnlGanntChart.Controls.Clear()
        PnlGanntChart.AutoScroll = True

        Dim blockWidth As Integer = 60
        Dim blockHeight As Integer = 40
        Dim startX As Integer = 10
        Dim blockY As Integer = 10
        Dim labelY As Integer = blockY + blockHeight + 4

        Dim palette As Color() = {
        Color.Cyan, Color.LimeGreen, Color.Orange, Color.HotPink,
        Color.Yellow, Color.MediumPurple, Color.Aquamarine, Color.Coral
    }

        Dim colorMap As New Dictionary(Of String, Color)()
        Dim colorIndex As Integer = 0

        ' To add idle block
        Dim fullGantt As New List(Of GanttBlock)()

        For i As Integer = 0 To _gantt.Count - 1
            Dim current As GanttBlock = _gantt(i)

            ' to check if there is a idle  time
            If i = 0 AndAlso current.StartTime > 0 Then
                ' add idle block in the first if the first block does not start at time 0
                fullGantt.Add(New GanttBlock() With {
                .ProcessName = "Idle",
                .StartTime = 0,
                .EndTime = current.StartTime
            })
            ElseIf i > 0 Then
                Dim previous As GanttBlock = _gantt(i - 1)
                If previous.EndTime < current.StartTime Then
                    ' to bel able to add idle block if the idle block is not on the start
                    fullGantt.Add(New GanttBlock() With {
                    .ProcessName = "Idle",
                    .StartTime = previous.EndTime,
                    .EndTime = current.StartTime
                })
                End If
            End If

            fullGantt.Add(current)
        Next

        ' to draw the indiviadual blocks of the gantt chart, including the idle blocks
        For Each block As GanttBlock In fullGantt
            Dim duration As Integer = block.EndTime - block.StartTime
            Dim width As Integer = duration * blockWidth

            Dim lbl As New Label()
            lbl.Text = block.ProcessName
            lbl.Width = width
            lbl.Height = blockHeight
            lbl.Location = New Point(startX, blockY)
            lbl.ForeColor = Color.Black
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            lbl.BorderStyle = BorderStyle.FixedSingle

            ' Idle block gets gray color, process blocks get palette colors
            If block.ProcessName = "Idle" Then
                lbl.BackColor = Color.DimGray
                lbl.ForeColor = Color.White
            Else
                If Not colorMap.ContainsKey(block.ProcessName) Then
                    colorMap(block.ProcessName) = palette(colorIndex Mod palette.Length)
                    colorIndex += 1
                End If
                lbl.BackColor = colorMap(block.ProcessName)
            End If

            PnlGanntChart.Controls.Add(lbl)
            'to add labels below each block to indicate the start time of each block, including the idle blocks
            ' Start time label
            Dim startTimeLabel As New Label()
            startTimeLabel.Text = block.StartTime.ToString()
            startTimeLabel.Width = 40
            startTimeLabel.Height = 18
            startTimeLabel.Location = New Point(startX, labelY)
            startTimeLabel.ForeColor = Color.White
            startTimeLabel.Font = New Font("Segoe UI", 8)
            startTimeLabel.TextAlign = ContentAlignment.TopLeft
            PnlGanntChart.Controls.Add(startTimeLabel)

            startX += width
        Next

        ' Last end time label
        If fullGantt.Count > 0 Then
            Dim endLabel As New Label()
            endLabel.Text = fullGantt.Last().EndTime.ToString()
            endLabel.Width = 40
            endLabel.Height = 18
            endLabel.Location = New Point(startX, labelY)
            endLabel.ForeColor = Color.White
            endLabel.Font = New Font("Segoe UI", 8)
            endLabel.TextAlign = ContentAlignment.TopLeft
            PnlGanntChart.Controls.Add(endLabel)
        End If

        PnlGanntChart.Height = labelY + 25
    End Sub


    ' TAT computation display
    Private Sub ShowTATSolution()
        ATTsolution.Controls.Clear()

        ' Title
        Dim lblTitle As New Label()
        lblTitle.Text = "Turnaround Time (TAT = CT - AT)"
        lblTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblTitle.ForeColor = Color.Cyan
        lblTitle.AutoSize = False
        lblTitle.Width = ATTsolution.Width - 10
        lblTitle.Height = 25
        lblTitle.Padding = New Padding(0, 5, 0, 5)
        ATTsolution.Controls.Add(lblTitle)

        ' to display new line for each process with the TAT calculation
        For Each r As ProcessResult In _results
            Dim lbl As New Label()
            lbl.Text = $"TAT({r.Name}) = {r.CompletionTime} - {r.ArrivalTime} = {r.TurnaroundTime}"
            lbl.Font = New Font("Segoe UI", 9)
            lbl.ForeColor = Color.White
            lbl.AutoSize = False
            lbl.Width = ATTsolution.Width - 10
            lbl.Height = 22
            lbl.Padding = New Padding(10, 0, 0, 0)
            ATTsolution.Controls.Add(lbl)
        Next

        ' Average
        Dim totalTAT As Integer = _results.Sum(Function(r) r.TurnaroundTime)
        Dim avgTAT As Double = totalTAT / _results.Count
        Dim tatValues As String = String.Join(" + ", _results.Select(Function(r) r.TurnaroundTime.ToString()))

        Dim lblAvg As New Label()
        lblAvg.Text = $"Average TAT = ({tatValues}) / {_results.Count} = {avgTAT:F2}"
        lblAvg.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblAvg.ForeColor = Color.LimeGreen
        lblAvg.AutoSize = False
        lblAvg.Width = ATTsolution.Width - 10
        lblAvg.Height = 22
        lblAvg.Padding = New Padding(10, 0, 0, 0)
        ATTsolution.Controls.Add(lblAvg)
    End Sub

    ' WT computation display
    Private Sub ShowWTSolution()
        AWTsolution.Controls.Clear()

        ' Title
        Dim lblTitle As New Label()
        lblTitle.Text = "Waiting Time (WT = TAT - BT)"
        lblTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblTitle.ForeColor = Color.Cyan
        lblTitle.AutoSize = False
        lblTitle.Width = AWTsolution.Width - 10
        lblTitle.Height = 25
        lblTitle.Padding = New Padding(0, 5, 0, 5)
        AWTsolution.Controls.Add(lblTitle)

        ' to display new line for each process with the WT calculation
        For Each r As ProcessResult In _results
            Dim lbl As New Label()
            lbl.Text = $"WT({r.Name}) = {r.TurnaroundTime} - {r.BurstTime} = {r.WaitingTime}"
            lbl.Font = New Font("Segoe UI", 9)
            lbl.ForeColor = Color.White
            lbl.AutoSize = False
            lbl.Width = AWTsolution.Width - 10
            lbl.Height = 22
            lbl.Padding = New Padding(10, 0, 0, 0)
            AWTsolution.Controls.Add(lbl)
        Next

        ' Average
        Dim totalWT As Integer = _results.Sum(Function(r) r.WaitingTime)
        Dim avgWT As Double = totalWT / _results.Count
        Dim wtValues As String = String.Join(" + ", _results.Select(Function(r) r.WaitingTime.ToString()))

        Dim lblAvg As New Label()
        lblAvg.Text = $"Average WT = ({wtValues}) / {_results.Count} = {avgWT:F2}"
        lblAvg.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblAvg.ForeColor = Color.LimeGreen
        lblAvg.AutoSize = False
        lblAvg.Width = AWTsolution.Width - 10
        lblAvg.Height = 22
        lblAvg.Padding = New Padding(10, 0, 0, 0)
        AWTsolution.Controls.Add(lblAvg)
    End Sub
    Private Sub LblAdd2_Click(sender As Object, e As EventArgs) Handles LblAdd2.Click
        Me.Hide()
        Form2.Show()
    End Sub
End Class