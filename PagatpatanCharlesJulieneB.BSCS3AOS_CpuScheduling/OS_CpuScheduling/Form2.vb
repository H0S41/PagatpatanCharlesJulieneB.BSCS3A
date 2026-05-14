Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CmbAlg.SelectedIndex = 0
        UpdateFormFields()
        TxtBoxAdd2.Text = "3"
        DrawHeaders()
        For i As Integer = 1 To 3
            AddProcessRow(i)
        Next
    End Sub

    Private Sub CmbAlg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAlg.SelectedIndexChanged, CMBprio.SelectedIndexChanged
        UpdateFormFields()
    End Sub

    ' to show the priorty column only on the relevant algorithms, and the quantum field only for RR algorithms
    Private Sub UpdateFormFields()
        If CmbAlg.SelectedItem Is Nothing Then Return

        Dim algo As String = CmbAlg.SelectedItem.ToString()

        Dim needsQuantum As Boolean = (algo = "Round Robin" OrElse
                                   algo = "Priority with Round Robin")
        ShowQuantumField(needsQuantum)

        Dim needsPriority As Boolean = (algo = "Priority Scheduling" OrElse
                                    algo = "Priority with Round Robin")
        ShowPriorityOptions(needsPriority)

        ' to display the algo description
        Select Case algo
            Case "First Come First Serve"
                Prioritylbl.Text = "Non-Preemptive. Processes are executed in the order they arrive. Simple but can cause long waiting times."

            Case "Shortest Job First"
                Prioritylbl.Text = "Non-Preemptive. The process with the shortest burst time is selected next. Minimizes average waiting time."

            Case "Shortest Remaining Time"
                Prioritylbl.Text = "Preemptive. If a new process arrives with a shorter remaining time, the current process is interrupted."

            Case "Round Robin"
                Prioritylbl.Text = "Preemptive. Each process gets a fixed time quantum. Fair but can have high turnaround time."

            Case "Priority Scheduling"
                Prioritylbl.Text = "Non-Preemptive. Process with the highest priority (lowest number or highest number) runs first. Can cause starvation."

            Case "Priority with Round Robin"
                Prioritylbl.Text = "Preemptive. Higher priority processes run first. Tied priorities share CPU using Round Robin."
        End Select
    End Sub

    Private Sub ShowQuantumField(show As Boolean)

        TxtTQ.Visible = show
    End Sub

    Private Sub ShowPriorityOptions(show As Boolean)
        CMBprio.Visible = show

        ' Show/hide priority header 
        For Each ctrl As Control In InputTable2.Controls
            If ctrl.Name = "headerPanel" Then
                For Each inner As Control In ctrl.Controls
                    If inner.Name = "lblPriorityHeader" Then
                        inner.Visible = show
                    End If
                Next
            End If
        Next

        ' Show/hide priority textboxes in each row
        For Each ctrl As Control In InputTable2.Controls
            If TypeOf ctrl Is Panel AndAlso ctrl.Name <> "headerPanel" Then
                For Each inner As Control In ctrl.Controls
                    If inner.Name.StartsWith("txtPriority_") Then
                        inner.Visible = show
                    End If
                Next
            End If
        Next
    End Sub


    ' to dynamically add rows of processes based on user input, with a minimum of 3 processes required
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles LblAdd2.Click

        InputTable2.Controls.Clear()
        Dim numProcesses As Integer
        If Not Integer.TryParse(TxtBoxAdd2.Text.Trim(), numProcesses) Then
            MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' to set 3 minimum processses
        If numProcesses < 3 Then
            MessageBox.Show("Minimum number of processes is 3.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        DrawHeaders()
        For i As Integer = 1 To numProcesses
            AddProcessRow(i)
        Next

        UpdateFormFields()
    End Sub
    Private Sub DrawHeaders()
        Dim existing = InputTable2.Controls.OfType(Of Panel)().FirstOrDefault(Function(p) p.Name = "headerPanel")
        If existing IsNot Nothing Then
            InputTable2.Controls.Remove(existing)
            existing.Dispose()
        End If

        Dim headerPanel As New Panel()
        headerPanel.Name = "headerPanel"
        headerPanel.Width = InputTable2.Width - 10

        'to create the column headers for the process input table
        Dim lblName As New Label()
        lblName.Text = "Process"
        lblName.Font = New Font("Segoe UI", 32, FontStyle.Bold)
        lblName.ForeColor = Color.White
        lblName.AutoSize = True
        lblName.Location = New Point(0, 5)

        Dim lblArrival As New Label()
        lblArrival.Text = "Arrival"
        lblArrival.Font = New Font("Segoe UI", 32, FontStyle.Bold)
        lblArrival.ForeColor = Color.White
        lblArrival.AutoSize = True
        lblArrival.Location = New Point(200, 5)

        Dim lblBurst As New Label()
        lblBurst.Text = "Burst"
        lblBurst.Font = New Font("Segoe UI", 32, FontStyle.Bold)
        lblBurst.ForeColor = Color.White
        lblBurst.AutoSize = True
        lblBurst.Location = New Point(415, 5)

        Dim lblPriority As New Label()
        lblPriority.Name = "lblPriorityHeader"
        lblPriority.Text = "Priority"
        lblPriority.Font = New Font("Segoe UI", 32, FontStyle.Bold)
        lblPriority.ForeColor = Color.White
        lblPriority.AutoSize = True
        lblPriority.Location = New Point(588, 5)
        lblPriority.Visible = False

        headerPanel.Controls.Add(lblName)
        headerPanel.Controls.Add(lblArrival)
        headerPanel.Controls.Add(lblBurst)
        headerPanel.Controls.Add(lblPriority)

        headerPanel.Height = lblName.PreferredHeight + 10

        InputTable2.Controls.Add(headerPanel)
        InputTable2.Controls.SetChildIndex(headerPanel, 0)
    End Sub
    Private Sub AddProcessRow(rowNumber As Integer)
        Dim rowPanel As New Panel()
        rowPanel.Name = "rowPanel_" & rowNumber
        rowPanel.Tag = "processRow"
        rowPanel.Width = InputTable2.Width - 10

        Dim txtName As New TextBox()
        txtName.Name = "txtName_" & rowNumber
        txtName.Text = "P" & rowNumber
        txtName.Width = 100
        txtName.Font = New Font("Segoe UI", 12)
        txtName.Location = New Point(35, 5)

        rowPanel.Height = txtName.PreferredHeight + 10

        Dim txtArrival As New TextBox()
        txtArrival.Name = "txtArrival_" & rowNumber
        txtArrival.Width = 100
        txtArrival.Font = New Font("Segoe UI", 12)
        txtArrival.Location = New Point(225, 5)

        Dim txtBurst As New TextBox()
        txtBurst.Name = "txtBurst_" & rowNumber
        txtBurst.Width = 100
        txtBurst.Font = New Font("Segoe UI", 12)
        txtBurst.Location = New Point(430, 5)

        Dim txtPriority As New TextBox()
        txtPriority.Name = "txtPriority_" & rowNumber
        txtPriority.Width = 100
        txtPriority.Font = New Font("Segoe UI", 12)
        txtPriority.Location = New Point(620, 5)
        txtPriority.Visible = False

        Dim btnDelete As New Button()
        btnDelete.Text = "X"
        btnDelete.Width = 35
        btnDelete.Height = 25
        btnDelete.BackColor = Color.Red
        btnDelete.ForeColor = Color.White
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.Location = New Point(770, 5)
        btnDelete.Tag = rowPanel
        AddHandler btnDelete.Click, AddressOf DeleteRow_Click

        rowPanel.Controls.Add(txtName)
        rowPanel.Controls.Add(txtArrival)
        rowPanel.Controls.Add(txtBurst)
        rowPanel.Controls.Add(txtPriority)
        rowPanel.Controls.Add(btnDelete)

        InputTable2.Controls.Add(rowPanel)
    End Sub

    Private Sub DeleteRow_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim rowPanel As Panel = CType(btn.Tag, Panel)
        InputTable2.Controls.Remove(rowPanel)
        rowPanel.Dispose()
    End Sub

    ' to get the inpus from the user and validate them before running the algorithms, with error messages for invalid inputs
    Private Function GetProcessInputs() As List(Of Process)
        Dim processList As New List(Of Process)()

        For Each ctrl As Control In InputTable2.Controls
            If TypeOf ctrl Is Panel AndAlso ctrl.Name <> "headerPanel" Then
                Dim rowPanel As Panel = CType(ctrl, Panel)
                Dim proc As New Process()
                Dim isValid As Boolean = True

                ' === READ NAME FIRST BEFORE THE LOOP ===
                Dim nameBox = rowPanel.Controls.OfType(Of TextBox)().
                          FirstOrDefault(Function(t) t.Name.StartsWith("txtName_"))
                If nameBox IsNot Nothing Then
                    proc.Name = If(nameBox.Text.Trim() = "", "P?", nameBox.Text.Trim())
                End If

                For Each innerCtrl As Control In rowPanel.Controls
                    If TypeOf innerCtrl Is TextBox Then
                        Dim txt As TextBox = CType(innerCtrl, TextBox)

                        If txt.Name.StartsWith("txtName_") Then
                            ' Already handled above

                        ElseIf txt.Name.StartsWith("txtArrival_") Then
                            If txt.Text.Trim() = "" Then
                                proc.ArrivalTime = 0
                            ElseIf Not Integer.TryParse(txt.Text.Trim(), proc.ArrivalTime) Then
                                MessageBox.Show($"Invalid Arrival Time for {proc.Name}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                isValid = False
                                Exit For
                            End If

                        ElseIf txt.Name.StartsWith("txtBurst_") Then
                            If txt.Text.Trim() = "" Then
                                MessageBox.Show($"Burst Time for {proc.Name} cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                isValid = False
                                Exit For
                            ElseIf Not Integer.TryParse(txt.Text.Trim(), proc.BurstTime) OrElse proc.BurstTime <= 0 Then
                                MessageBox.Show($"Invalid Burst Time for {proc.Name}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                isValid = False
                                Exit For
                            End If

                        ElseIf txt.Name.StartsWith("txtPriority_") Then
                            If txt.Text.Trim() = "" Then
                                proc.Priority = 0
                            ElseIf Not Integer.TryParse(txt.Text.Trim(), proc.Priority) Then
                                MessageBox.Show($"Invalid Priority for {proc.Name}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                isValid = False
                                Exit For
                            End If
                        End If
                    End If
                Next

                If isValid Then
                    proc.RemainingTime = proc.BurstTime
                    processList.Add(proc)
                Else
                    Return Nothing
                End If
            End If
        Next

        Return processList
    End Function

    Private Function GetSelectedAlgorithm() As String
        Try
            If CmbAlg.SelectedItem Is Nothing Then Return ""

            Select Case CmbAlg.SelectedItem.ToString()
                Case "First Come First Serve" : Return "FCFS"
                Case "Shortest Job First" : Return "SJF"
                Case "Shortest Remaining Time" : Return "SRT"
                Case "Round Robin" : Return "RR"
                Case "Priority Scheduling" : Return "Priority"
                Case "Priority with Round Robin" : Return "PriorityRR"
                Case Else : Return ""
            End Select

        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' to run the selected algorithm and pass the results to Form3 for display, with error handling for missing algorithm selection or invalid inputs
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles LblCalcu2.Click

        Dim selectedAlgorithm As String = GetSelectedAlgorithm()

        If selectedAlgorithm = "" Then
            MessageBox.Show("Please select an algorithm.", "No Algorithm", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim processes As List(Of Process) = GetProcessInputs()

        If processes Is Nothing Then Return

        If processes.Count = 0 Then
            MessageBox.Show("Please add at least one process.", "No Processes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validate quantum for RR algorithms
        Dim quantum As Integer = 0
        If selectedAlgorithm = "RR" OrElse selectedAlgorithm = "PriorityRR" Then
            If Not Integer.TryParse(TxtTQ.Text.Trim(), quantum) OrElse quantum <= 0 Then
                MessageBox.Show("Please enter a valid Time Quantum.", "Invalid Quantum", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        Dim results As List(Of ProcessResult)
        Dim gantt As List(Of GanttBlock)
        'select the algorithm to run based on user selection and get the results and gantt chart data
        Select Case selectedAlgorithm
            Case "FCFS"
                Dim output = RunFCFS(processes)
                results = output.results
                gantt = output.gantt

            Case "SJF"
                Dim output = RunSJF(processes)
                results = output.results
                gantt = output.gantt

            Case "SRT"
                Dim output = RunSRT(processes)
                results = output.results
                gantt = output.gantt

            Case "RR"
                Dim output = RunRoundRobin(processes, quantum)
                results = output.results
                gantt = output.gantt

            Case "Priority"
                Dim output = RunPriority(processes)
                results = output.results
                gantt = output.gantt

            Case "PriorityRR"
                Dim output = RunPriorityRR(processes, quantum)
                results = output.results
                gantt = output.gantt

            Case Else
                Return
        End Select

        Dim form3 As New Form3(results, gantt, selectedAlgorithm)
        Me.Hide()
        form3.Show()
    End Sub

End Class