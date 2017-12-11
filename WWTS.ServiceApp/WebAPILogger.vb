Imports log4net
Public Class WebAPILogger
    Private Shared logger As ILog = LogManager.GetLogger("DashBoard2API")
    Private Shared m_strCheckSum As String

    Public Shared Sub LogInfo(ByVal str As String)

        logger.Info(str)
        'If IsNothing(m_strCheckSum) Then
        '    logger.Info(str)
        'Else
        '    logger.Info(m_strCheckSum + " " + str)
        'End If

    End Sub

    Public Shared Sub LogError(ByVal str As String)
        If IsNothing(m_strCheckSum) Then
            logger.Info(str)
        Else
            logger.Info(m_strCheckSum + " " + str)
        End If

    End Sub

    Public Shared Sub LogFatal(ByVal str As String)
        If IsNothing(m_strCheckSum) Then
            logger.Info(str)
        Else
            logger.Info(m_strCheckSum + " " + str)
        End If

    End Sub
    Public Shared Function SetCheckSum(ByVal strSeedValue As String)
        '' The value to be returned to the caller                                                        

        Dim hexData() As Byte = System.Text.Encoding.ASCII.GetBytes(strSeedValue)

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
        ThreadContext.Properties("CheckSum") = m_strCheckSum
        Return m_strCheckSum

    End Function

    Public Shared Function GetCheckSum() As String
        Return m_strCheckSum
    End Function

End Class
