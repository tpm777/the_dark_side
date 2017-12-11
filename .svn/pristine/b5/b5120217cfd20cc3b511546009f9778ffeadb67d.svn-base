Imports WWTS.Data.Model

Namespace Contracts
    Public Interface ICustomerService
        Inherits IManagerBase
        Function GetScalarValue(ByVal sql As String) As String
        Function GetScalarValueCount(ByVal sql As String) As Integer
        Function GetCallResult(ByVal sql As String) As IList(Of CallResult)
        Function GetWayBillResult(ByVal sql As String) As IList(Of WayBillResult)
        Function InsertDashLog(ByVal DashType As String, ByVal UserId As String, ByVal LogType As String) As Integer
    End Interface
End Namespace

