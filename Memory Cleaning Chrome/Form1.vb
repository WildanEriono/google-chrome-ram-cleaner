Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic.CompilerServices

Public Class Form1
    <DllImport("kernel32.dll", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Public Shared Function SetProcessWorkingSetSize(ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
                Form1.SetProcessWorkingSetSize(Process.GetCurrentProcess.Handle, -1, -1)
                Dim processesByName As Process() = Process.GetProcessesByName("Chrome")
                Dim index As Integer = 0
                Do While True
                    If (index >= processesByName.Length) Then
                        Exit Do
                    End If
                    Dim process As Process = processesByName(index)
                    Form1.SetProcessWorkingSetSize(process.Handle, -1, -1)
                    index += 1
                Loop
            End If
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Dim exception As Exception = ex
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
        MsgBox("Reduce RAM is Actived.",, "Active")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
        MsgBox("Restoring data's content. ",, "Deactive")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
