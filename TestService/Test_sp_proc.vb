Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class TestAsync

    Dim oSQLHelper As New SQLHelper(ConfigurationManager.ConnectionStrings("Cat_DB_Connection").ConnectionString)
    Public Sub ImportCSVFile()
        Dim strBuffer As String = ""
        Dim strTechID As String()
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Filter = "Cursor Files|*.csv"
        openFileDialog1.Title = "Select a Comma Delimited File"

        oSQLHelper.m_dtAsyncResults = New DataTable



        FileOpen(1, "c:\temp\query1.csv", OpenMode.Input, OpenAccess.Read)
        While Not EOF(1)
            strBuffer = LineInput(1)
        End While



        FileClose(1)
        strTechID = Split(strBuffer, ",")

        Dim dtTech As New DataTable
        dtTech.Columns.Add("TechID", GetType(String))
        For Each strBuffer In strTechID
            dtTech.Rows.Add(strBuffer)
        Next


        oSQLHelper.ExecuteAsyncDataTableSP("sp_DashDistanceTech2Call_Test_DJP", "92806", dtTech)
        dtTech.Rows.Clear()

        'Dim sqlparams(1) As SqlClient.SqlParameter
        'sqlparams(0) = New SqlClient.SqlParameter("@callZip", SqlDbType.VarChar)
        'sqlparams(0).Value = "55425"
        'sqlparams(0).Direction = ParameterDirection.Input

        '' sqlparams(0).SqlDbType = SqlDbType.VarChar 
        'sqlparams(1) = New SqlClient.SqlParameter("@techList", SqlDbType.VarChar)
        'sqlparams(1).Value = "64368,328550,60199"
        'sqlparams(1).Direction = ParameterDirection.Input

        ' dtTech = oSQLHelper.ExecuteDataTableSP("sp_DashDistanceTech2Call", sqlparams)



        FileOpen(1, "c:\temp\query2.csv", OpenMode.Input, OpenAccess.Read)
        While Not EOF(1)
            strBuffer = LineInput(1)
        End While
        FileClose(1)
        strTechID = Split(strBuffer, ",")
        For Each strBuffer In strTechID
            dtTech.Rows.Add(strBuffer)
        Next
        oSQLHelper.ExecuteAsyncDataTableSP("sp_DashDistanceTech2Call_Test_DJP", "92806", dtTech)
        dtTech.Rows.Clear()


        FileOpen(1, "c:\temp\query3.csv", OpenMode.Input, OpenAccess.Read)
        While Not EOF(1)
            strBuffer = LineInput(1)
        End While
        FileClose(1)
        strTechID = Split(strBuffer, ",")
        For Each strBuffer In strTechID
            dtTech.Rows.Add(strBuffer)
        Next
        oSQLHelper.ExecuteAsyncDataTableSP("sp_DashDistanceTech2Call_Test_DJP", "92806", dtTech)
        dtTech.Rows.Clear()


        FileOpen(1, "c:\temp\query4.csv", OpenMode.Input, OpenAccess.Read)
        While Not EOF(1)
            strBuffer = LineInput(1)
        End While
        FileClose(1)
        strTechID = Split(strBuffer, ",")
        For Each strBuffer In strTechID
            dtTech.Rows.Add(strBuffer)
        Next
        oSQLHelper.ExecuteAsyncDataTableSP("sp_DashDistanceTech2Call_Test_DJP", "92806", dtTech)
        dtTech.Rows.Clear()


    End Sub


    Public Function GetRecordRecounts() As Integer
        Return oSQLHelper.m_dtAsyncResults.Rows.Count
    End Function




End Class
