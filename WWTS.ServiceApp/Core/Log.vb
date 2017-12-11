
Imports System.IO
Imports System.Threading



Module Log

    Public LogByteCount As Long
    Public logFileName As String
    Public m_strCheckSum As String
    Private LogFile As StreamWriter
    Private lock As New Object()
    Private LogList As New Queue(Of String)
    Private WriterBusy As Boolean = False
    Public _fileMutex As New Mutex(False, "DashSumm2API")

    Public Sub WriteToLog(ByVal subject As String, ByVal msg As String)
        Dim curtm As DateTime = Now()

        _fileMutex.WaitOne()
        Dim thread As Thread = Thread.CurrentThread

        SyncLock (lock)

            Try


                curtm = Now()

                '      Dim fs = New FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                Dim strMsg As String = "[" & IIf(m_strCheckSum <> "", m_strCheckSum, thread.ManagedThreadId.ToString()) & "] " & curtm.ToString("MM/dd/yyyy HH:mm:ss.fff tt") & " LogEntry: " & subject & " " & msg.Trim & vbCrLf
                Dim info As [Byte]() = New UTF8Encoding(True).GetBytes("[" & IIf(m_strCheckSum <> "", m_strCheckSum, thread.ManagedThreadId.ToString()) & "] " & curtm.ToString("MM/dd/yyyy HH:mm:ss.fff tt") & " LogEntry: " & subject & " " & msg.Trim & vbCrLf)

                Using fs As New FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.None, 10000)

                    Debug.WriteLine("[" + m_strCheckSum + "] " + "thread id =[" + thread.ManagedThreadId.ToString() + "]" + "msg = " + strMsg)
                    fs.Write(info, 0, info.Length)
                    fs.Flush(True)
                    fs.Close()

                End Using



            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try

        End SyncLock
        _fileMutex.ReleaseMutex()

    End Sub

    Public Sub WriteLog(ByVal subject As String, ByVal msg As String)
        Dim curtm As DateTime = Now()
        Dim thread As Thread = Thread.CurrentThread

        Dim strMsg As String = "[" & IIf(m_strCheckSum <> "", m_strCheckSum, thread.ManagedThreadId.ToString()) & "] " & curtm.ToString("MM/dd/yyyy HH:mm:ss.fff tt") & " LogEntry: " & subject & " " & msg.Trim  ' & vbCrLf


        LogList.Enqueue(strMsg)

        If WriterBusy = True Then Exit Sub
        WriterBusy = True




        Using outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(logFileName, True)

            While LogList.Count > 0
                outFile.WriteLine(LogList.Dequeue)
            End While
            outFile.Close()

        End Using

        WriterBusy = False

    End Sub

    Public Function GetCheckSum(ByVal strFilterData As String) As String
        '' The value to be returned to the caller                                                        

        Dim hexData() As Byte = System.Text.Encoding.ASCII.GetBytes(strFilterData)

        '        Dim checkSum As Byte = 0
        Dim checksum As Int32 = 0
        '' This is here so that we wait til the second value is being processed
        Dim isFirst As Boolean = True

        '' Calcuate the check sum. Do not process the last three characters and 
        '' the reason for -4 on the first line                                                        
        For i As Integer = 0 To hexData.Length - 4
            '' If checkSum does not already have a value then skip this iteration
            If isFirst Then
                isFirst = False
                checksum = Convert.ToInt32(hexData(i))
                Continue For
            End If
            '' Calculating the checkSum
            'checkSum = checkSum Xor hexData(i)
            checksum = checksum + Convert.ToInt32(hexData(i))
        Next
        m_strCheckSum = checksum.ToString()
        Return m_strCheckSum

    End Function

    Public Function FileInUse(ByVal sFile As String) As Boolean
        Dim thisFileInUse As Boolean = False
        Debug.WriteLine("Filename = " + sFile)
        If System.IO.File.Exists(sFile) Then
            Try
                Using f As New IO.FileStream(sFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                    ' thisFileInUse = False
                End Using
            Catch
                thisFileInUse = True
            End Try
        End If
        Return thisFileInUse
    End Function
End Module
