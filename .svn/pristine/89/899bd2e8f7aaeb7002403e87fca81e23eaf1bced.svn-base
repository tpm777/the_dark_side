Imports System.Data.SqlClient

Namespace Utils
    Public Module Tools
        Public Function SetLeadingCap(ByVal sz As String) As String
            If sz.Length > 1 Then
                Return sz.Substring(0, 1).ToUpper & sz.Substring(1, sz.Length - 1)
            ElseIf sz.Length = 1 Then
                Return sz.ToUpper
            Else
                Return ""
            End If
        End Function
        Public Function ListItemCount(ByVal szList As String) As Integer
            Dim aWord() As String
            aWord = szList.Split(",")
            Return aWord.GetUpperBound(0) + 1
        End Function

        Public Sub WriteEventLog(ByVal sz As String)
            Try
                Dim appLog As New System.Diagnostics.EventLog
                System.Diagnostics.EventLog.WriteEntry("DashSumm", sz, System.Diagnostics.EventLogEntryType.Error)
            Catch ex As Exception
            End Try
        End Sub
        Public Function List2Set(ByVal szList As String) As String
            Dim aWord() As String, i As Integer, szSet As String = ""
            aWord = szList.Split(",")
            For i = 0 To aWord.GetUpperBound(0)
                If i <> 0 Then szSet &= ","
                szSet &= "'" & aWord(i).Trim & "'"
            Next
            Return "(" & szSet & ")"
        End Function
        Public Function Array2List(ByVal array() As Object) As String
            Dim szList As String = String.Empty
            For Each elem In array
                If (szList <> String.Empty) Then szList &= ","
                szList &= elem.Trim
            Next
            Return szList
        End Function
        Public Function RdrStrValue(ByRef value As String) As String
            If String.IsNullOrEmpty(value) Then Return ""
            Return value
        End Function
        
        Public Function RdrDecValue(ByRef rdr As SqlDataReader, ByVal idx As Integer) As Decimal
            If rdr.IsDBNull(idx) Then Return 0
            Return rdr.GetDecimal(idx)
        End Function
        Public Function RdrIntValue(ByRef rdr As SqlDataReader, ByVal idx As Integer) As Integer
            If rdr.IsDBNull(idx) Then Return 0
            Select Case rdr.GetDataTypeName(idx).ToLower
                Case "smallint"
                    Return rdr.GetInt16(idx)
                Case Else
                    Return rdr.GetInt32(idx)
            End Select
        End Function
        Public Function TZShift(ByVal dtm As DateTime?, ByVal TimeZone As Decimal) As DateTime?
            If dtm.HasValue Then
                Return DateAdd(DateInterval.Minute, TimeZone * 60, dtm.Value)
            Else
                Return Nothing
            End If
        End Function
        Public Function TruncDate(ByVal dtm As DateTime?) As DateTime?
            If dtm.HasValue Then
                Return CType(dtm.Value.ToString("MM/dd/yyyy"), System.DateTime?)
            Else
                Return Nothing
            End If
        End Function

        Public Sub Fatal(ByVal szErrMsg As String)
            Throw New Exception(szErrMsg)
        End Sub

        Public Function GetComparatorString(ByVal Comparator As String, ByVal ComparatorValue As String) As String
            Dim ComparatorString As String = ""

            Select Case Comparator.ToLower
                Case "equals"
                    ComparatorString = "='" & ComparatorValue & "'"
                Case "notequals"
                    ComparatorString = "<>'" & ComparatorValue & "'"
                Case "startswith"
                    ComparatorString = "like '" & ComparatorValue & "%'"
                Case "doesnotstartwith"
                    ComparatorString = "not like '" & ComparatorValue & "%'"
                Case "contains"
                    ComparatorString = "like '%" & ComparatorValue & "%'"
                Case "doesnotcontain"
                    ComparatorString = "not like '%" & ComparatorValue & "%'"
                Case "inlist"
                    ComparatorString = "in " & List2Set(String.Join(",", ComparatorValue))
                Case "notinlist"
                    ComparatorString = "not in " & List2Set(String.Join(",", ComparatorValue))
                Case "all"
                Case Else
                    ComparatorString = String.Empty
            End Select

            Return ComparatorString

        End Function

        Public Function GetCustomerComparatorString(ByVal Comparator As String, ByVal ComparatorValue As String) As String
            Dim ComparatorString As String = ""

            Select Case Comparator.ToLower
                Case "equals"
                    ComparatorString = "query_customer_number ='" & ComparatorValue & "'"
                Case "notequals"
                    ComparatorString = "query_customer_number <>'" & ComparatorValue & "'"
                Case "startswith"
                    ComparatorString = "query_customer_number like '" & ComparatorValue & "%'"
                Case "doesnotstartwith"
                    ComparatorString = "query_customer_number not like '" & ComparatorValue & "%'"
                Case "contains"
                    ComparatorString = "query_customer_number like '%" & ComparatorValue & "%'"
                Case "doesnotcontain"
                    ComparatorString = "query_customer_number not like '%" & ComparatorValue & "%'"
                Case "inlist"
                    Dim list As List(Of String) = ComparatorValue.Split(",").ToList
                    If (list.FindAll(Function(a) Not a.Contains("*")).Count > 0) Then
                        ComparatorString = "(query_customer_number in " & List2Set(String.Join(",", list.FindAll(Function(a) Not a.Contains("*")))) & ")"
                    End If

                    Dim Count As Integer = 1
                    For Each strvalue As String In list.FindAll(Function(a) a.Contains("*"))
                        If (list.FindAll(Function(a) Not a.Contains("*")).Count = 0 And Count = 1) Then
                            ComparatorString &= vbCrLf & " (query_customer_number like '" & strvalue.Replace("*", "") & "%')"
                        Else
                            ComparatorString &= vbCrLf & " or (query_customer_number like '" & strvalue.Replace("*", "") & "%')"
                        End If

                        Count = Count + 1
                    Next
                Case "notinlist"
                    Dim list As List(Of String) = ComparatorValue.Split(",").ToList
                    If (list.FindAll(Function(a) Not a.Contains("*")).Count > 0) Then
                        ComparatorString = "(query_customer_number not in " & List2Set(String.Join(",", list.FindAll(Function(a) Not a.Contains("*")))) & ")"
                    End If

                    Dim Count As Integer = 1
                    For Each strvalue As String In list.FindAll(Function(a) a.Contains("*"))
                        If (list.FindAll(Function(a) Not a.Contains("*")).Count = 0 And Count = 1) Then
                            ComparatorString &= vbCrLf & " (query_customer_number not like '" & strvalue.Replace("*", "") & "%')"
                        Else
                            ComparatorString &= vbCrLf & " and (query_customer_number not like '" & strvalue.Replace("*", "") & "%')"
                        End If

                        Count = Count + 1
                    Next

                Case "all"
                Case Else
                    ComparatorString = String.Empty
            End Select

            Return ComparatorString

        End Function

    End Module
End Namespace

