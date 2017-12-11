Imports WWTS.Business.Contracts
Imports System.ComponentModel.Composition
Imports WWTS.Data.Model
Imports Microsoft.Practices.Unity
Imports WWTS.Repository
Imports System.Data
Imports System.Data.SqlClient

<Export(GetType(ICustomerService))>
Public Class CustomerServiceManager
    Inherits ManagerBase
    Implements ICustomerService

    <Dependency> _
    Public Property CustomerServiceRepository() As ICustomerServiceRepository
        Get
            Return m_CustomerServiceRepository
        End Get
        Set(value As ICustomerServiceRepository)
            m_CustomerServiceRepository = value
        End Set
    End Property
    Private m_CustomerServiceRepository As ICustomerServiceRepository
    Public Function GetScalarValue(sql As String) As String Implements ICustomerService.GetScalarValue
        Return CustomerServiceRepository.ExecuteScalarValueCommand(Of String)(sql)
    End Function
    Public Function GetScalarValueCount(sql As String) As Integer Implements ICustomerService.GetScalarValueCount
        Return CustomerServiceRepository.ExecuteScalarValueCommand(Of Integer)(sql)
    End Function
    Public Function GetCallResult(sql As String) As IList(Of CallResult) Implements ICustomerService.GetCallResult
        Return CustomerServiceRepository.ExecuteSqlInlineQuery(Of CallResult)(sql)
    End Function
    Public Function GetWayBillResult(sql As String) As IList(Of WayBillResult) Implements ICustomerService.GetWayBillResult
        Return CustomerServiceRepository.ExecuteSqlInlineQuery(Of WayBillResult)(sql)
    End Function
    Public Function InsertDashLog(ByVal DashType As String, ByVal UserId As String, ByVal LogType As String) As Integer Implements ICustomerService.InsertDashLog
        Dim Sql As String = "insert into cat.dbo.Dash_Log (DashType, LogDTM, UserId, LogType) values (@DashType, getdate(), @UserId, @LogType)"
        Return CustomerServiceRepository.ExecuteNonQueryCommand(Sql, New SqlParameter("DashType", SqlDbType.VarChar) With { _
     .Value = DashType}, New SqlParameter("UserId", SqlDbType.VarChar) With { _
     .Value = UserId}, New SqlParameter("LogType", SqlDbType.VarChar) With { _
     .Value = LogType})
    End Function
End Class


