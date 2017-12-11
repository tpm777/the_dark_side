Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Public Class SQLHelper
    Public Shared strConnect As String
    Public m_dtAsyncResults As DataTable
    Public Sub New(strConn As String)
        strConnect = strConn
    End Sub

    'Shared Sub New()
    '    strConnect = SiteConfigs.J4ProposalDataCS
    'End Sub

    Public Function GetConnection() As SqlConnection
        Dim oconn As SqlConnection = New SqlConnection(strConnect)
        Return oconn
    End Function

    Public Function ExecuteNonQuery(ByVal strSQL As String) As Integer
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQL, oconn)
        command.CommandType = CommandType.Text
        oconn.Open()
        Dim retval As Integer = command.ExecuteNonQuery()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteNonQuery(ByVal strSQL As String, ByVal params() As SqlParameter) As Integer
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQL, oconn)
        command.CommandType = CommandType.Text
        For i As Integer = 0 To params.Length - 1
            command.Parameters.Add(params(i))
        Next
        oconn.Open()
        Dim retval As Integer = command.ExecuteNonQuery()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteNonQuerySP(ByVal strSQLSP As String) As Integer
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        oconn.Open()
        Dim retval As Integer = command.ExecuteNonQuery()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteNonQuerySP(ByVal strSQLSP As String, ByVal params() As SqlParameter) As Integer
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        For i As Integer = 0 To params.Length - 1
            command.Parameters.Add(params(i))
        Next
        oconn.Open()
        Dim retval As Integer = command.ExecuteNonQuery()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteNonQueryCommand(ByVal strSQL As String, ByVal params() As SqlParameter) As SqlCommand
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQL, oconn)
        command.CommandType = CommandType.Text
        For i As Integer = 0 To params.Length - 1
            command.Parameters.Add(params(i))
        Next
        oconn.Open()
        Dim retval As Integer = command.ExecuteNonQuery()
        oconn.Close()
        oconn.Dispose()
        Return command
    End Function

    Public Function ExecuteNonQueryCommandSP(ByVal strSQLSP As String, ByVal params() As SqlParameter) As SqlCommand
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        For i As Integer = 0 To params.Length - 1
            command.Parameters.Add(params(i))
        Next
        oconn.Open()
        Dim retval As Integer = command.ExecuteNonQuery()
        oconn.Close()
        oconn.Dispose()
        Return command
    End Function

    Public Function ExecuteScalar(ByVal strSQL As String) As Object
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQL, oconn)
        command.CommandType = CommandType.Text
        oconn.Open()
        Dim retval As Object = command.ExecuteScalar()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval

    End Function

    Public Function ExecuteScalar(ByVal strSQL As String, ByVal params() As SqlParameter) As Object
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQL, oconn)
        command.CommandType = CommandType.Text
        For i As Integer = 0 To params.Length - 1
            command.Parameters.Add(params(i))
        Next
        oconn.Open()
        Dim retval As Object = command.ExecuteScalar()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteScalarSP(ByVal strSQLSP As String) As Object
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        oconn.Open()
        Dim retval As Object = command.ExecuteScalar()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteScalarSP(ByVal strSQLSP As String, ByVal params() As SqlParameter) As Object
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        For i As Integer = 0 To params.Length - 1
            command.Parameters.Add(params(i))
        Next
        oconn.Open()
        Dim retval As Object = command.ExecuteScalar()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return retval
    End Function

    Public Function ExecuteDataSet(ByVal strSQL As String) As DataSet
        Dim oconn As SqlConnection = GetConnection()
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(strSQL, oconn)
        da.Fill(ds)
        oconn.Close()
        oconn.Dispose()
        da.Dispose()
        Return ds
    End Function

    Public Function ExecuteDataSet(ByVal strSQL As String, ByVal params() As SqlParameter) As DataSet
        Dim oconn As SqlConnection = GetConnection()
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(strSQL, oconn)
        For i As Integer = 0 To params.Length - 1
            da.SelectCommand.Parameters.Add(params(i))
        Next
        da.Fill(ds)
        oconn.Close()
        oconn.Dispose()
        da.Dispose()
        Return ds
    End Function

    Public Function ExecuteDataSetSP(ByVal strSQLSP As String) As DataSet
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(command)
        da.Fill(ds)
        da.Dispose()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return ds
    End Function

    Public Function ExecuteAsyncDataTableSP(ByVal strSQLSP As String, ByVal strZipCode As String, ByRef dtTable As DataTable)
        Dim oconn As SqlConnection = GetConnection()

        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.AddWithValue("@callZip", strZipCode)
        command.Parameters.AddWithValue("@tblTech", dtTable)
        command.CommandTimeout = 120

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(command)

        oconn.Open()
        Dim callback As New AsyncCallback(AddressOf HandleCallback)
        'command.BeginExecuteNonQuery(callback, command)
        command.BeginExecuteReader(callback, command, CommandBehavior.CloseConnection)

    End Function


    Private Sub HandleCallback(ByVal result As IAsyncResult)
        Dim dtTable As New DataTable
        Try
            ' Retrieve the original command object, passed
            ' to this procedure in the AsyncState property
            ' of the IAsyncResult parameter.
            Dim command As SqlCommand = CType(result.AsyncState, SqlCommand)
            Dim sqlReader As SqlDataReader = command.EndExecuteReader(result)




            dtTable.Load(sqlReader)

            m_dtAsyncResults.Merge(dtTable)


            ' You may not interact with the form and its contents
            ' from a different thread, and this callback procedure
            ' is all but guaranteed to be running from a different thread
            ' than the form. Therefore you cannot simply call code that 
            ' displays the results, like this:
            ' DisplayResults(rowText)

            ' Instead, you must call the procedure from the form's thread.
            ' One simple way to accomplish this is to call the Invoke
            ' method of the form, which calls the delegate you supply
            ' from the form's thread. 
            'Dim del As New DisplayInfoDelegate(AddressOf DisplayResults)
            'Me.Invoke(del, rowText)

        Catch ex As Exception
            ' Because you are now running code in a separate thread, 
            ' if you do not handle the exception here, none of your other
            ' code catches the exception. Because none of your code
            ' is on the call stack in this thread, there is nothing
            ' higher up the stack to catch the exception if you do not 
            ' handle it here. You can either log the exception or 
            ' invoke a delegate (as in the non-error case in this 
            ' example) to display the error on the form. In no case
            ' can you simply display the error without executing a delegate
            ' as in the Try block here. 

            ' You can create the delegate instance as you 
            '' invoke it, like this:
            'Me.Invoke(New DisplayInfoDelegate(AddressOf DisplayStatus),
            '    String.Format("Ready(last error: {0}", ex.Message))
        Finally
            'isExecuting = False
            'If connection IsNot Nothing Then
            '    connection.Close()
            ' End If
        End Try
    End Sub
    Public Function ExecuteDataSetSP(ByVal strSQLSP As String, ByVal params() As SqlParameter) As DataSet
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(command)
        For i As Integer = 0 To params.Length - 1
            da.SelectCommand.Parameters.Add(params(i))
        Next
        da.Fill(ds)
        da.Dispose()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return ds
    End Function


    Public Function ExecuteDataTable(ByVal strSQL As String) As DataTable
        Dim oconn As SqlConnection = GetConnection()
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(strSQL, oconn)
        da.Fill(dt)
        oconn.Close()
        oconn.Dispose()
        da.Dispose()
        Return dt
    End Function

    Public Function ExecuteDataTable(ByVal strSQL As String, ByVal params() As SqlParameter) As DataTable
        Dim oconn As SqlConnection = GetConnection()
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(strSQL, oconn)
        For i As Integer = 0 To params.Length - 1
            da.SelectCommand.Parameters.Add(params(i))
        Next
        da.Fill(dt)
        oconn.Close()
        oconn.Dispose()
        da.Dispose()
        Return dt
    End Function

    Public Function ExecuteDataTableSP(ByVal strSQLSP As String) As DataTable
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(command)
        da.Fill(dt)
        da.Dispose()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return dt
    End Function

    Public Function ExecuteDataTableSP(ByVal strSQLSP As String, ByVal params() As SqlParameter) As DataTable
        Dim oconn As SqlConnection = GetConnection()
        Dim command As New SqlCommand(strSQLSP, oconn)
        command.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(command)
        For i As Integer = 0 To params.Length - 1
            da.SelectCommand.Parameters.Add(params(i))
        Next
        da.Fill(dt)
        da.Dispose()
        oconn.Close()
        oconn.Dispose()
        command.Dispose()
        Return dt
    End Function

    ''' <summary>
    ''' The Names Of The Columns Must Match The Names Of The Properties
    ''' </summary>
    ''' <param name="myObject"></param>
    ''' <param name="myData"></param>
    ''' <remarks></remarks>
    Public Sub PopulateObject(ByVal myObject As Object, ByVal myData As DataTable)
        For Each row As DataRow In myData.Rows
            With myObject
                For Each p As PropertyInfo In myObject.GetType().GetProperties()
                    Try
                        p.SetValue(myObject, row(p.Name), Nothing)
                    Catch ex As Exception

                    End Try
                Next
            End With
            Exit For
        Next
    End Sub

    ''' <summary>
    ''' The Names Of The Columns Must Match The Names Of The Properties
    ''' </summary>
    ''' <param name="myObject"></param>
    ''' <param name="myData"></param>
    ''' <remarks></remarks>
    Public Sub PopulateObject(ByVal myObject As Object, ByVal myData As DataRow)
        With myObject
            For Each p As PropertyInfo In myObject.GetType().GetProperties()
                Try
                    p.SetValue(myObject, myData(p.Name), Nothing)
                Catch ex As Exception

                End Try
            Next
        End With
    End Sub
End Class

