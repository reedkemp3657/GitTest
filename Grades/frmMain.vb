'Reed Kemp'
'10/20/2015'
'CRN: 1588'
Option Explicit On
Option Strict On
Imports System.IO
Imports System.Math

Public Class frmMain

    Dim FirstName(24) As String
    Dim LastName(24) As String
    Dim grade(24) As String

    Dim Hw1(24) As Integer
    Dim Hw2(24) As Integer
    Dim Project(24) As Integer
    Dim Midterm(24) As Integer
    Dim Final(24) As Integer
    Dim average(24) As Integer
    Dim ndx As Integer

    Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        OpenFileDialog.ShowDialog()
    End Sub

    Private Sub OpenFileDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog.FileOk
        Dim inFile As StreamReader
        OpenFileDialog.InitialDirectory = "C:\"
        OpenFileDialog.Filter = "files (*.cs)|*.csv"

        '   If (OpenFileDialog.ShowDialog() = DialogResult.Cancel) Then
        'Exit Sub
        '     Else

        Dim record As String
        Dim fields() As String
        Dim avg As Double

        inFile = File.OpenText(OpenFileDialog.FileName)
        ndx = 0

        Do While Not (inFile.EndOfStream)

            record = (inFile.ReadLine)
            fields = Split(record, ",")

            FirstName(ndx) = fields(0)
            LastName(ndx) = fields(1)
            Hw1(ndx) = CInt(fields(2))
            Hw2(ndx) = CInt(fields(3))
            Project(ndx) = CInt(fields(4))
            Midterm(ndx) = CInt(fields(5))
            Final(ndx) = CInt(fields(6))

            lstNames.Items.Add(LastName(ndx) & "," & FirstName(ndx))

            avg = ((Hw1(ndx) + Hw2(ndx)) * 3) + (Project(ndx) * 4) + ((Midterm(ndx) + Final(ndx)) * 5)
            avg = (avg / 20) + 0.5
            average(ndx) = CInt(Truncate(avg))

            grade(ndx) = GetFinalGrade(average(ndx))

            ndx = ndx + 1
        Loop

        inFile.Close()
        lstNames.SelectedIndex = 0

        '   End If
    End Sub
    Private Sub SymmaryReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SymmaryReportToolStripMenuItem.Click
        SaveFileDialog.ShowDialog()
    End Sub

    Private Sub SaveFileDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog.FileOk
        Dim outFile As StreamWriter
        Dim savedfile As String = SaveFileDialog.FileName
        SaveFileDialog.InitialDirectory = "C:"
        SaveFileDialog.Filter = "txt files (*.txt)|*.txt|csv     files (*.cs)|*.csv"

        '     If (SaveFileDialog.ShowDialog() = DialogResult.Cancel) Then
        'Exit Sub
        '   Else
        Dim output As String = SaveFileDialog.FileName
        Dim line As String
        Dim Avg As Double

        Avg = average.Sum / ndx
        Avg = Avg + 0.5
        outFile = File.CreateText(output)

        outFile.WriteLine("Summary Report Created: " & Format(Now, "dddd, d MMM yyyy"))
        outFile.WriteLine(" ")
        outFile.WriteLine("Class Average:  " & Truncate(Avg))
        outFile.WriteLine(" ")

        For i = 0 To ndx - 1
            line = FirstName(i) & " " & LastName(i) & " " & Hw1(i) & " " & Hw2(i) & " " & Project(i) & " " & Midterm(i) & " " & Final(i) & " ----> " & average(i) & " " & grade(i)

            outFile.WriteLine(line)
        Next

        outFile.Close()
        MsgBox("Summary Report has been created")
        '   End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Dim response As Integer
        response = MsgBox("Are you sure you want to exit", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, MsgBoxStyle), "Leaving?")
        If (response = vbYes) Then Close()
    End Sub

    Private Sub lstNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNames.SelectedIndexChanged

        lblHw1.Text = Str(Hw1(lstNames.SelectedIndex))
        lblHw2.Text = Str(Hw2(lstNames.SelectedIndex))
        lblProject.Text = Str(Project(lstNames.SelectedIndex))
        lblMidterm.Text = Str(Midterm(lstNames.SelectedIndex))
        lblFinal.Text = Str(Final(lstNames.SelectedIndex))
        lblAverage.Text = Str(average(lstNames.SelectedIndex))
        lblLetterGrade.Text = grade(lstNames.SelectedIndex)
    End Sub

    Function GetFinalGrade(Average As Integer) As String

        Select Case Average
            Case 93 To 100
                Return "A"
            Case 90 To 92
                Return "A-"
            Case 87 To 89
                Return "B+"
            Case 83 To 86
                Return "B"
            Case 80 To 82
                Return "B-"
            Case 77 To 79
                Return "C+"
            Case 73 To 76
                Return "C"
            Case 70 To 72
                Return "C-"
            Case 67 To 69
                Return "D+"
            Case 60 To 66
                Return "D"
            Case Else
                Return "E"
        End Select
    End Function
End Class
