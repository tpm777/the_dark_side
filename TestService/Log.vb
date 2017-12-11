
Imports System.IO

Module Log

    Public LogByteCount As Long
    Public logFileName As String

    Private LogFile As StreamWriter

    Public Sub Open()
        LogFile = New StreamWriter(logFileName, True)

        Dim fi As FileInfo = New FileInfo(logFilename)
        LogByteCount = fi.Length
    End Sub

    Public Sub Close()
        LogFile.Close()
    End Sub

    Public Sub WriteLog(ByVal subject As String, ByVal msg As String)
        Dim curtm As DateTime = Now()
        LogFile.WriteLine(curtm.ToString("MM/dd/yyyy HH:mm:ss") & " LogEntry:" & subject & " " & msg.Trim)
        LogByteCount += subject.Length + 2
        'msg = msg.Trim
        'If msg <> "" Then
        '    LogFile.WriteLine(msg & vbCrLf)
        '    LogByteCount += msg.Length + 2
        'End If
        Flush()

    End Sub
    Public Sub WriteLog2(ByVal subject As String, ByRef msg As Object)
        Dim curtm As DateTime = Now()
        LogFile.WriteLine(curtm.ToString("MM/dd/yyyy HH:mm:ss") & " LogEntry:" & subject)
        LogByteCount += subject.Length + 2
        msg = msg.Trim
        If msg <> "" Then
            LogFile.WriteLine(msg & vbCrLf)
            LogByteCount += msg.Length + 2
        End If

    End Sub

    Public Sub Flush()
        LogFile.Flush()
    End Sub

End Module
