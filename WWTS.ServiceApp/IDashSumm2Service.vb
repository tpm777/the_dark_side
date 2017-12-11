Imports System.IO
Imports WWTS.Data
Imports WWTS.Data.Model
Imports System.Runtime.Serialization

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
Public Interface IDashSumm2Service

    <OperationContract> _
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Bare, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json, UriTemplate:="/GetSummaryDataSample")> _
    Function GetSummaryDataSample(ByVal filterData As FilterData) As JsonResponse(Of ColsData)

    <OperationContract>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Bare, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json, UriTemplate:="/GetSummaryData")>
    Function GetSummaryData(ByVal filterData As FilterData) As JsonResponse(Of ColsData)

    <OperationContract>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Bare, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json, UriTemplate:="/GetSummaryDetailData")>
    Function GetSummaryDetailData(ByVal filterData As FilterDetailData) As JsonResponse(Of ColsData)


End Interface

<DataContract()>
Public Class FilterData

    <DataMember()>
    Public Property UserId() As String

    <DataMember()>
    Public Property OrgType() As String

    <DataMember()>
    Public Property OrgFilterType() As String

    <DataMember()>
    Public Property OrgFilterList() As String

    <DataMember()>
    Public Property OrgBranchList() As String

    <DataMember()>
    Public Property CustAssoc() As String

    <DataMember()>
    Public Property VendorCode() As String

    <DataMember()>
    Public Property Col() As Col

    <DataMember()>
    Public Property Rows() As List(Of Integer)

    <DataMember()>
    Public Property IsCallDetail() As Boolean

    <DataMember()>
    Public Property UserPermission() As UserPermission

End Class
<DataContract()>
Public Class FilterDetailData

    <DataMember()>
    Public Property UserId() As String

    <DataMember()>
    Public Property OrgType() As String

    <DataMember()>
    Public Property OrgFilterType() As String

    <DataMember()>
    Public Property OrgFilterList() As String

    <DataMember()>
    Public Property OrgBranchList() As String

    <DataMember()>
    Public Property CustAssoc() As String

    <DataMember()>
    Public Property VendorCode() As String

    <DataMember()>
    Public Property ColDatas() As List(Of Col)




    <DataMember()>
    Public Property Rows() As List(Of Integer)

    <DataMember()>
    Public Property IsCallDetail() As Boolean

    <DataMember()>
    Public Property UserPermission() As UserPermission

End Class

<DataContract()>
Public Class Col

    <DataMember()>
    Public Property CustomerCamparatorName() As String
    <DataMember()>
    Public Property CustomerCamparatorValue() As String
    <DataMember()>
    Public Property SubDivisionCamparatorName() As String
    <DataMember()>
    Public Property SubDivisionCamparatorValue() As String
    <DataMember()>
    Public Property WorkOrderType() As String
End Class

<DataContract()>
Public Class UserPermission

    <DataMember()>
    Public Property DashVendor() As Boolean
    <DataMember()>
    Public Property DashVendorFilterOff() As Boolean
    <DataMember()>
    Public Property DashCustomerFilterOff() As Boolean

End Class

'<DataContract()>
'Public Class ColsDataResponse
'    <DataMember()>
'    Public Property Results() As ColsData()
'    <DataMember()>
'    Public Property SingleResult() As ColsData
'    <DataMember()>
'    Public Property TotalRecordCount() As Integer

'End Class