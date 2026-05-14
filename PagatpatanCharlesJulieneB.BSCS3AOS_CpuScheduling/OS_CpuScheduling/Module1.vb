' proccesses input data
Public Class Process
    Public Property Name As String
    Public Property ArrivalTime As Integer
    Public Property BurstTime As Integer
    Public Property Priority As Integer
    Public Property RemainingTime As Integer
End Class


' returns results after scheduling algorithms are applied

Public Class ProcessResult
    Public Property Name As String
    Public Property ArrivalTime As Integer
    Public Property BurstTime As Integer
    Public Property Priority As Integer
    Public Property CompletionTime As Integer
    Public Property TurnaroundTime As Integer
    Public Property WaitingTime As Integer
End Class



' gannt chart display

Public Class GanttBlock
        Public Property ProcessName As String
        Public Property StartTime As Integer
        Public Property EndTime As Integer
    End Class
Module module1

    ' FCFS scheduling algorithm

    Public Function RunFCFS(processes As List(Of Process)) As (results As List(Of ProcessResult), gantt As List(Of GanttBlock))
        Dim sortedList = processes.OrderBy(Function(p) p.ArrivalTime).ToList()
        Dim results As New List(Of ProcessResult)
        Dim gantt As New List(Of GanttBlock)
        Dim currentTime As Integer = 0

        For Each p As Process In sortedList
            If currentTime < p.ArrivalTime Then
                currentTime = p.ArrivalTime
            End If

            Dim start As Integer = currentTime
            currentTime += p.BurstTime

            gantt.Add(New GanttBlock() With {
                .ProcessName = p.Name,
                .StartTime = start,
                .EndTime = currentTime
            })

            Dim tat As Integer = currentTime - p.ArrivalTime
            Dim wt As Integer = tat - p.BurstTime

            results.Add(New ProcessResult() With {
                .Name = p.Name,
                .ArrivalTime = p.ArrivalTime,
                .BurstTime = p.BurstTime,
                .CompletionTime = currentTime,
                .TurnaroundTime = tat,
                .WaitingTime = wt
            })
        Next

        Return (results, gantt)
    End Function


    ' SJF scheduling algorithm

    Public Function RunSJF(processes As List(Of Process)) As (results As List(Of ProcessResult), gantt As List(Of GanttBlock))
        Dim remaining = processes.Select(Function(p) New Process() With {
            .Name = p.Name,
            .ArrivalTime = p.ArrivalTime,
            .BurstTime = p.BurstTime,
            .RemainingTime = p.BurstTime,
            .Priority = p.Priority
        }).ToList()

        Dim results As New List(Of ProcessResult)
        Dim gantt As New List(Of GanttBlock)
        Dim currentTime As Integer = 0
        Dim completed As Integer = 0
        Dim n As Integer = remaining.Count

        While completed < n
            Dim available = remaining.Where(Function(p) p.ArrivalTime <= currentTime AndAlso p.RemainingTime > 0).ToList()

            If available.Count = 0 Then
                currentTime += 1
                Continue While
            End If

            Dim shortest = available.OrderBy(Function(p) p.BurstTime).First()
            Dim start As Integer = currentTime
            currentTime += shortest.BurstTime
            shortest.RemainingTime = 0

            gantt.Add(New GanttBlock() With {
                .ProcessName = shortest.Name,
                .StartTime = start,
                .EndTime = currentTime
            })

            Dim tat As Integer = currentTime - shortest.ArrivalTime
            Dim wt As Integer = tat - shortest.BurstTime

            results.Add(New ProcessResult() With {
                .Name = shortest.Name,
                .ArrivalTime = shortest.ArrivalTime,
                .BurstTime = shortest.BurstTime,
                .CompletionTime = currentTime,
                .TurnaroundTime = tat,
                .WaitingTime = wt
            })

            completed += 1
        End While

        Return (results, gantt)
    End Function

    ' SRT scheduling

    Public Function RunSRT(processes As List(Of Process)) As (results As List(Of ProcessResult), gantt As List(Of GanttBlock))
        Dim remaining = processes.Select(Function(p) New Process() With {
            .Name = p.Name,
            .ArrivalTime = p.ArrivalTime,
            .BurstTime = p.BurstTime,
            .RemainingTime = p.BurstTime,
            .Priority = p.Priority
        }).ToList()

        Dim results As New List(Of ProcessResult)
        Dim gantt As New List(Of GanttBlock)
        Dim currentTime As Integer = 0
        Dim completed As Integer = 0
        Dim n As Integer = remaining.Count

        While completed < n
            Dim available = remaining.Where(Function(p) p.ArrivalTime <= currentTime AndAlso p.RemainingTime > 0).ToList()

            If available.Count = 0 Then
                currentTime += 1
                Continue While
            End If

            Dim shortest = available.OrderBy(Function(p) p.RemainingTime).First()

            If gantt.Count > 0 AndAlso gantt.Last().ProcessName = shortest.Name Then
                gantt.Last().EndTime += 1
            Else
                gantt.Add(New GanttBlock() With {
                    .ProcessName = shortest.Name,
                    .StartTime = currentTime,
                    .EndTime = currentTime + 1
                })
            End If

            shortest.RemainingTime -= 1
            currentTime += 1

            If shortest.RemainingTime = 0 Then
                Dim tat As Integer = currentTime - shortest.ArrivalTime
                Dim wt As Integer = tat - shortest.BurstTime

                results.Add(New ProcessResult() With {
                    .Name = shortest.Name,
                    .ArrivalTime = shortest.ArrivalTime,
                    .BurstTime = shortest.BurstTime,
                    .CompletionTime = currentTime,
                    .TurnaroundTime = tat,
                    .WaitingTime = wt
                })

                completed += 1
            End If
        End While

        Return (results, gantt)
    End Function


    ' ROUND ROBIN scheduling

    Public Function RunRoundRobin(processes As List(Of Process), quantum As Integer) As (results As List(Of ProcessResult), gantt As List(Of GanttBlock))
        Dim remaining = processes.OrderBy(Function(p) p.ArrivalTime).Select(Function(p) New Process() With {
            .Name = p.Name,
            .ArrivalTime = p.ArrivalTime,
            .BurstTime = p.BurstTime,
            .RemainingTime = p.BurstTime,
            .Priority = p.Priority
        }).ToList()

        Dim results As New List(Of ProcessResult)
        Dim gantt As New List(Of GanttBlock)
        Dim queue As New Queue(Of Process)
        Dim currentTime As Integer = 0
        Dim completed As Integer = 0
        Dim n As Integer = remaining.Count
        Dim index As Integer = 0

        While index < n AndAlso remaining(index).ArrivalTime <= currentTime
            queue.Enqueue(remaining(index))
            index += 1
        End While

        While completed < n
            If queue.Count = 0 Then
                currentTime += 1
                While index < n AndAlso remaining(index).ArrivalTime <= currentTime
                    queue.Enqueue(remaining(index))
                    index += 1
                End While
                Continue While
            End If

            Dim current As Process = queue.Dequeue()
            Dim execTime As Integer = Math.Min(quantum, current.RemainingTime)
            Dim start As Integer = currentTime
            currentTime += execTime
            current.RemainingTime -= execTime

            gantt.Add(New GanttBlock() With {
                .ProcessName = current.Name,
                .StartTime = start,
                .EndTime = currentTime
            })

            While index < n AndAlso remaining(index).ArrivalTime <= currentTime
                queue.Enqueue(remaining(index))
                index += 1
            End While

            If current.RemainingTime = 0 Then
                Dim tat As Integer = currentTime - current.ArrivalTime
                Dim wt As Integer = tat - current.BurstTime

                results.Add(New ProcessResult() With {
                    .Name = current.Name,
                    .ArrivalTime = current.ArrivalTime,
                    .BurstTime = current.BurstTime,
                    .CompletionTime = currentTime,
                    .TurnaroundTime = tat,
                    .WaitingTime = wt
                })

                completed += 1
            Else
                queue.Enqueue(current)
            End If
        End While

        Return (results, gantt)
    End Function


    ' PRIORITY scheduling

    Public Function RunPriority(processes As List(Of Process)) As (results As List(Of ProcessResult), gantt As List(Of GanttBlock))
        Dim remaining = processes.Select(Function(p) New Process() With {
            .Name = p.Name,
            .ArrivalTime = p.ArrivalTime,
            .BurstTime = p.BurstTime,
            .RemainingTime = p.BurstTime,
            .Priority = p.Priority
        }).ToList()

        Dim results As New List(Of ProcessResult)
        Dim gantt As New List(Of GanttBlock)
        Dim currentTime As Integer = 0
        Dim completed As Integer = 0
        Dim n As Integer = remaining.Count

        While completed < n
            Dim available = remaining.Where(Function(p) p.ArrivalTime <= currentTime AndAlso p.RemainingTime > 0).ToList()

            If available.Count = 0 Then
                currentTime += 1
                Continue While
            End If

            Dim highest = available.OrderBy(Function(p) p.Priority).ThenBy(Function(p) p.ArrivalTime).First()
            Dim start As Integer = currentTime
            currentTime += highest.BurstTime
            highest.RemainingTime = 0

            gantt.Add(New GanttBlock() With {
                .ProcessName = highest.Name,
                .StartTime = start,
                .EndTime = currentTime
            })

            Dim tat As Integer = currentTime - highest.ArrivalTime
            Dim wt As Integer = tat - highest.BurstTime

            results.Add(New ProcessResult() With {
                .Name = highest.Name,
                .ArrivalTime = highest.ArrivalTime,
                .BurstTime = highest.BurstTime,
                .Priority = highest.Priority,
                .CompletionTime = currentTime,
                .TurnaroundTime = tat,
                .WaitingTime = wt
            })

            completed += 1
        End While

        Return (results, gantt)
    End Function


    ' PRIORITY WITH ROUND ROBIN scheduling 

    Public Function RunPriorityRR(processes As List(Of Process), quantum As Integer) As (results As List(Of ProcessResult), gantt As List(Of GanttBlock))
        ' Make a deep copy with remaining time
        Dim remaining = processes.OrderBy(Function(p) p.ArrivalTime).Select(Function(p) New Process() With {
        .Name = p.Name,
        .ArrivalTime = p.ArrivalTime,
        .BurstTime = p.BurstTime,
        .RemainingTime = p.BurstTime,
        .Priority = p.Priority
    }).ToList()

        Dim results As New List(Of ProcessResult)
        Dim gantt As New List(Of GanttBlock)
        Dim currentTime As Integer = 0
        Dim completed As Integer = 0
        Dim n As Integer = remaining.Count

        ' Queue holds processes of the same priority using RR
        Dim readyQueue As New Queue(Of Process)()
        Dim lastPriority As Integer = -1
        Dim index As Integer = 0

        ' Enqueue first arrived processes
        While index < n AndAlso remaining(index).ArrivalTime <= currentTime
            readyQueue.Enqueue(remaining(index))
            index += 1
        End While

        While completed < n
            ' If queue is empty jump to next arrival
            If readyQueue.Count = 0 Then
                ' Find next arriving process
                If index < n Then
                    currentTime = remaining(index).ArrivalTime
                    While index < n AndAlso remaining(index).ArrivalTime <= currentTime
                        readyQueue.Enqueue(remaining(index))
                        index += 1
                    End While
                End If
                Continue While
            End If


            ' Check if a higher priority process has arrived
            Dim highestPriorityInQueue As Integer = readyQueue.Min(Function(p) p.Priority)
            Dim newArrival = remaining.Where(Function(p) p.ArrivalTime <= currentTime AndAlso
                                                      p.RemainingTime > 0 AndAlso
                                                      Not readyQueue.Contains(p)).ToList()

            ' Add newly arrived processes to queue
            For Each p As Process In newArrival
                If Not readyQueue.Contains(p) Then
                    readyQueue.Enqueue(p)
                End If
            Next

            ' Re-sort queue by priority — higher priority (lower number) goes first
            ' Among same priority, maintain RR order
            Dim sortedQueue = readyQueue.OrderBy(Function(p) p.Priority).ToList()
            readyQueue.Clear()
            For Each p As Process In sortedQueue
                readyQueue.Enqueue(p)
            Next

            ' Get the top priority process
            Dim current As Process = readyQueue.Peek()
            Dim topPriority As Integer = current.Priority

            ' If priority changed, rebuild queue with only top priority first
            Dim execTime As Integer = Math.Min(quantum, current.RemainingTime)
            Dim start As Integer = currentTime

            Dim timeSlice As Integer = 0
            While timeSlice < execTime
                ' Check if a higher priority process arrives mid-quantum
                Dim preempt = remaining.Where(Function(p) p.ArrivalTime = currentTime + timeSlice + 1 AndAlso
                                                       p.RemainingTime > 0 AndAlso
                                                       p.Priority < topPriority).FirstOrDefault()
                If preempt IsNot Nothing Then
                    'stop if higher priority arrived
                    timeSlice += 1
                    Exit While
                End If
                timeSlice += 1
            End While

            ' Dequeue and execute
            readyQueue.Dequeue()
            current.RemainingTime -= timeSlice
            currentTime += timeSlice

            ' Add to Gantt
            If gantt.Count > 0 AndAlso gantt.Last().ProcessName = current.Name Then
                gantt.Last().EndTime = currentTime
            Else
                gantt.Add(New GanttBlock() With {
                .ProcessName = current.Name,
                .StartTime = start,
                .EndTime = currentTime
            })
            End If

            ' Enqueue newly arrived processes
            While index < n AndAlso remaining(index).ArrivalTime <= currentTime
                readyQueue.Enqueue(remaining(index))
                index += 1
            End While

            ' If not finished put back in queue (RR rotation)
            If current.RemainingTime > 0 Then
                readyQueue.Enqueue(current)
            Else
                ' Process finished
                Dim tat As Integer = currentTime - current.ArrivalTime
                Dim wt As Integer = tat - current.BurstTime

                results.Add(New ProcessResult() With {
                .Name = current.Name,
                .ArrivalTime = current.ArrivalTime,
                .BurstTime = current.BurstTime,
                .Priority = current.Priority,
                .CompletionTime = currentTime,
                .TurnaroundTime = tat,
                .WaitingTime = wt
            })
                completed += 1
            End If
        End While

        Return (results, gantt)
    End Function


    'Compute Averages

    Public Function GetAverageWT(results As List(Of ProcessResult)) As Double
        Return results.Average(Function(r) r.WaitingTime)
    End Function

    Public Function GetAverageTAT(results As List(Of ProcessResult)) As Double
        Return results.Average(Function(r) r.TurnaroundTime)
    End Function

End Module