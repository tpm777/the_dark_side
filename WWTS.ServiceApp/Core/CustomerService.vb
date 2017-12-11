Imports System.ServiceModel.Activation
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF
Imports WWTS.Data
Imports WWTS.Data.Model
Imports WWTS.Aspects.Enums
Imports WWTS.Aspects.Utils
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Threading


<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
<ExceptionShielding("ServiceExceptionPolicy")>
Partial Public Class CoreService
    Inherits ServiceBase
    Implements IDashSumm2Service
    Dim CallResults As List(Of CallResult)
    Dim CallResultsV2 As List(Of CallResult)

    Dim ResponseData As New ColsData()
    Private mOrgType As OrgType = OrgType.OrgTypeNone
    Private mDashType As String = "Ds"
    Private mOrgFilterType As OrgFilterType = OrgFilterType.OrgFilterTypeNone
    Private mOrgFilterList As String = String.Empty
    Private mCustAssoc As String = String.Empty
    Private mVendorCode As String = String.Empty

    Private CustomerCamparatorName As String = String.Empty
    Private CustomerCamparatorValue As String = String.Empty
    Private SubDivisionCamparatorName As String = String.Empty
    Private SubDivisionCamparatorValue As String = String.Empty
    Private WorkOrderType As String = String.Empty

    Private mSrchCustAssoc As String = String.Empty
    Private mTs1 As TimeSpan
    Private mTs2 As TimeSpan
    Public RC As Integer
    Public ErrorMsg As String
    Private mNoCcTime As String = "10:00"
    Public g_astrCallTypesDPL() As String
    Public m_blnLoggingEnable As Boolean
    Private m_dtStartAPITime As DateTime
    Private m_dtEndAPITime As DateTime
    Private m_dtDBStartIme As DateTime
    Private elapsed_time As TimeSpan


    Dim Form1 As Object



    Public Sub New()
    End Sub

    Private Sub LogData(strMessage As String)

        WebAPILogger.LogInfo(strMessage)
    End Sub

    Private Sub LogFilterData(ByVal filterData As FilterData)
        Try
            If (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("TraceRESTCalls"))) Then


                LogData(" ")
                LogData(" ************** Data Sent To API ****************")
                LogData(" ")
                LogData(" Comparator Values")
                LogData(" --------------------")

                LogData(vbTab + "CustomerCamparatorName:" + vbTab + vbTab + filterData.Col.CustomerCamparatorName.ToString)
                LogData(vbTab + "CustomerCamparatorValue:" + vbTab + vbTab + filterData.Col.CustomerCamparatorValue.ToString)
                LogData(vbTab + "SubDivisionCamparatorName:" + vbTab + vbTab + filterData.Col.SubDivisionCamparatorName.ToString)
                LogData(vbTab + "SubDivisionCamparatorValue:" + vbTab + vbTab + filterData.Col.SubDivisionCamparatorValue.ToString)
                LogData(vbTab + "WorkOrderType:" + vbTab + vbTab + filterData.Col.WorkOrderType.ToString)

                Dim strArrrayRows As String = String.Join(",", filterData.Rows.ToArray)


                LogData(vbTab + "Rows:" + vbTab + vbTab + strArrrayRows)
                LogData(" ")
                LogData(" User Permissions")
                LogData(" ----------------")

                LogData(vbTab + "DashVendor:" + vbTab + vbTab + filterData.UserPermission.DashVendor.ToString)
                LogData(vbTab + "DashVendorFilterOff:" + vbTab + vbTab + filterData.UserPermission.DashVendorFilterOff.ToString)
                LogData(vbTab + "DashCustomerFilterOff:" + vbTab + vbTab + filterData.UserPermission.DashCustomerFilterOff.ToString)


                LogData(" ")
                LogData(" Filtered Data Settings")
                LogData(" ----------------------")

                LogData(vbTab + "CustAssoc:" + vbTab + vbTab + filterData.CustAssoc)
                LogData(vbTab + "CallDetail:" + vbTab + vbTab + filterData.IsCallDetail.ToString)
                LogData(vbTab + "OrgBranchList:" + vbTab + vbTab + filterData.OrgBranchList)
                LogData(vbTab + "OrgFilterList:" + vbTab + vbTab + filterData.OrgFilterList)
                LogData(vbTab + "OrgFilterType:" + vbTab + vbTab + filterData.OrgFilterType)
                LogData(vbTab + "OrgType:" + vbTab + vbTab + filterData.OrgType)
                LogData(vbTab + "UserID:" + vbTab + vbTab + filterData.UserId)
                LogData(vbTab + "VendorCode:" + vbTab + vbTab + filterData.VendorCode)
                LogData(" ")
                LogData(" **************  End Data Sent To API ****************")

                LogData(" ")


            End If
        Catch ex As Exception
            m_blnLoggingEnable = False
        End Try



    End Sub
    Private Sub LogFilterData(ByVal filterData As FilterDetailData)
        Try
            If (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("TraceRESTCalls"))) Then






                LogData(" ")
                LogData(" ************** Data Sent To API ****************")

                LogData(" ")
                LogData(" Filtered Data Settings")
                LogData(" ----------------------")

                LogData(vbTab + "CustAssoc:" + vbTab + vbTab + filterData.CustAssoc)
                LogData(vbTab + "CallDetail:" + vbTab + vbTab + filterData.IsCallDetail.ToString)
                LogData(vbTab + "OrgBranchList:" + vbTab + vbTab + filterData.OrgBranchList)
                LogData(vbTab + "OrgFilterList:" + vbTab + vbTab + filterData.OrgFilterList)
                LogData(vbTab + "OrgFilterType:" + vbTab + vbTab + filterData.OrgFilterType)
                LogData(vbTab + "OrgType:" + vbTab + vbTab + filterData.OrgType)
                LogData(vbTab + "UserID:" + vbTab + vbTab + filterData.UserId)
                LogData(vbTab + "VendorCode:" + vbTab + vbTab + filterData.VendorCode)
                LogData(" ")


                LogData(" Comparator Values")
                LogData(" --------------------")
                Dim nColLoopCount As Int16 = 1

                For Each oDetailCol As Col In filterData.ColDatas


                    LogData("Col[" + Convert.ToString(nColLoopCount) + "]")

                    LogData(vbTab + "CustomerCamparatorName:" + vbTab + vbTab + oDetailCol.CustomerCamparatorName.ToString)
                    LogData(vbTab + "CustomerCamparatorValue:" + vbTab + vbTab + oDetailCol.CustomerCamparatorValue.ToString)
                    LogData(vbTab + "SubDivisionCamparatorName:" + vbTab + vbTab + oDetailCol.SubDivisionCamparatorName.ToString)
                    LogData(vbTab + "SubDivisionCamparatorValue:" + vbTab + vbTab + oDetailCol.SubDivisionCamparatorValue.ToString)
                    LogData(vbTab + "WorkOrderType:" + vbTab + vbTab + oDetailCol.WorkOrderType.ToString)

                    nColLoopCount = nColLoopCount + 1

                Next
                Dim strArrrayRows As String = String.Join(",", filterData.Rows.ToArray)
                LogData(vbTab + "Rows:" + vbTab + vbTab + strArrrayRows)
                LogData(" ")
                LogData(" User Permissions")
                LogData(" ----------------")

                LogData(vbTab + "DashVendor:" + vbTab + vbTab + filterData.UserPermission.DashVendor.ToString)
                LogData(vbTab + "DashVendorFilterOff:" + vbTab + vbTab + filterData.UserPermission.DashVendorFilterOff.ToString)
                LogData(vbTab + "DashCustomerFilterOff:" + vbTab + vbTab + filterData.UserPermission.DashCustomerFilterOff.ToString)
                LogData(" ")



                LogData(" **************  End Data Sent To API ****************")

                LogData(" ")


            End If
        Catch ex As Exception
            m_blnLoggingEnable = False
        End Try



    End Sub

    Public Function GetSummaryDataSample(ByVal filterData As FilterData) As JsonResponse(Of ColsData) Implements IDashSumm2Service.GetSummaryDataSample
        Dim response As New JsonResponse(Of ColsData)()
        Dim customerData As New ColsData()
        customerData.CustomerPercentage = 10.0
        customerData.Rows = filterData.Rows.[Select](Function(E) New RowData() With {
           .Id = E,
           .CallCount = 0
        }).ToList()
        response.SingleResult = customerData
        response.IsSuccess = True
        Return response
    End Function


    Public Function GetSummaryData(ByVal filterData As FilterData) As JsonResponse(Of ColsData) Implements IDashSumm2Service.GetSummaryData


        Dim curtm As DateTime = Now()

        WebAPILogger.SetCheckSum(filterData.UserId + filterData.Col.CustomerCamparatorName + filterData.Col.CustomerCamparatorValue + Now.Ticks.ToString())

        '   Log.GetCheckSum(filterData.UserId + filterData.CustAssoc + filterData.OrgBranchList + Now.Ticks.ToString())
        m_dtStartAPITime = Now()
        LogData(" ")
        LogData(" *********************** Begin API Call **************************")
        LogData(" Begin API Call Start Time " + vbTab + vbTab + "[" + m_dtStartAPITime.ToString() + "]")
        LogFilterData(filterData)
        LogData(" ")
        LogData(" Begin Json Formatted Data Input - Can be used to test with Postman or WCFClient")
        LogData(" ")
        LogData(JsonConvert.SerializeObject(filterData))
        LogData(" ")
        LogData(" End Json Formatted Data Input")
        LogData(" ")
        LogData(" ******* Begin Database Calls - GetSummaryData ************************************")
        LogData("")
        Dim response As New JsonResponse(Of ColsData)()
        Dim sz As String = String.Empty

        Try

            If filterData Is Nothing Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Data:"
                Return response
            End If

            Select Case filterData.OrgType.ToLower
                Case "main"
                    mOrgType = OrgType.OrgTypeMain
                Case Else
                    mOrgType = OrgType.OrgTypeNone
            End Select

            If (mOrgType = OrgType.OrgTypeNone) Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgType - " & filterData.OrgType
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)
                Return response
            End If

            mDashType &= SetLeadingCap(filterData.OrgType.ToLower)

            Select Case filterData.OrgFilterType.ToLower
                Case "b"
                    mOrgFilterType = OrgFilterType.OrgFilterTypeBranch
                    mOrgFilterList = filterData.OrgBranchList
                    mDashType &= "Brch"
                Case "t"
                    mOrgFilterType = OrgFilterType.OrgFilterTypeTerritory
                    mOrgFilterList = filterData.OrgFilterList
                    mDashType &= "Terr"
                Case Else
                    mOrgFilterType = OrgFilterType.OrgFilterTypeNone
            End Select

            If mOrgFilterType = OrgFilterType.OrgFilterTypeNone Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgFilterType"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If mOrgFilterList = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgFilterList"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If filterData.Col Is Nothing Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Customers"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If filterData.Col.CustomerCamparatorName = String.Empty Or filterData.Col.CustomerCamparatorValue = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Customers"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If filterData.Col.SubDivisionCamparatorName = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Sub Division"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If filterData.Rows.Count = 0 Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Rows"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If


            CustomerCamparatorName = filterData.Col.CustomerCamparatorName
            CustomerCamparatorValue = filterData.Col.CustomerCamparatorValue
            SubDivisionCamparatorName = filterData.Col.SubDivisionCamparatorName
            SubDivisionCamparatorValue = filterData.Col.SubDivisionCamparatorValue
            WorkOrderType = filterData.Col.WorkOrderType

            mCustAssoc = filterData.CustAssoc.ToLower
            mVendorCode = filterData.VendorCode.ToLower


            If mCustAssoc <> String.Empty Then mSrchCustAssoc = "," & mCustAssoc & ","

            mTs1 = New TimeSpan(Now.Day, Now.Hour, Now.Minute, Now.Second, Now.Millisecond)
            RC = 0
            ErrorMsg = String.Empty

            'Open database
            LogData("Before GetSummaryData -->doGetDbCfgNoCC ")
            LogData(FnGetStartTime())

            LogData(" ")
            doGetDbCfgNoCC(RC, ErrorMsg)
            LogData("After GetSummaryData -->doGetDbCfgNoCC ")
            LogData(FnGetElpasedStartTime())
            LogData(ElapsedCummulativeTime())
            LogData(" ")

            If RC <> 0 Then
                response.IsSuccess = False
                response.RC = RC
                response.Message = ErrorMsg
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            'Get summary counts
            doSql(RC, ErrorMsg)


            If RC <> 0 Then
                response.IsSuccess = False
                response.RC = RC
                response.Message = ErrorMsg
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            Select Case mOrgType
                Case OrgType.OrgTypeMain
                    LogData("Before GetSummaryData -->doSumMain ")
                    LogData(FnGetStartTime())
                    LogData("")
                    m_dtDBStartIme = Now()
                    ResponseData = doSumMain(filterData)
                    LogData("End GetSummaryData -->doSumMain ")
                    LogData(FnGetElpasedStartTime())
                    LogData(ElapsedCummulativeTime())
                    LogData("")
                    response.IsSuccess = True
                    response.RC = 0
                    response.SingleResult = ResponseData
                    '  If Not String.IsNullOrEmpty(doDateChk(filterData)) Then
                    ' response.Message = GetWOsWithIssues()
                    ' End If

                Case Else
            End Select

            If RC <> 0 Then
                response.IsSuccess = False
                response.RC = RC
                response.Message = ErrorMsg
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            'Log usage
            Try
                mTs2 = New TimeSpan(Now.Day, Now.Hour, Now.Minute, Now.Second, Now.Millisecond)
                sz = "OrgCnt=" & ListItemCount(filterData.OrgFilterList) & ",Time=" & mTs2.Subtract(mTs1).ToString()
                m_dtDBStartIme = Now()
                LogData("Before GetSummaryData --> CustomerServiceBusinessInstance.InsertDashLog ")
                LogData(FnGetStartTime())
                CustomerServiceBusinessInstance.InsertDashLog(mDashType, filterData.UserId, sz)
                LogData("After GetSummaryData --> CustomerServiceBusinessInstance.InsertDashLog ")
                LogData(FnGetElpasedStartTime())
                LogData(ElapsedCummulativeTime())

                LogData("")
            Catch ex As Exception
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & ex.Message)

            End Try
        Catch ex As Exception
            response.IsSuccess = False
            response.RC = 5
            response.Message = "GetSummaryData Exception:" & ex.Message
            If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

            WriteEventLog(ErrorMsg)
            Return response
        End Try

        Dim strReturnedWOIDs As String = ""

        If (filterData.IsCallDetail) Then
            LogSummaryData(filterData)
        End If

        LogData(" ")
        LogData(" ******* End Database Calls GetSummaryData ************************************")
        LogData(" ")



        LogData(" *********************** End API Call ****************************")
        LogData(" End API Call Time = " + vbTab + vbTab + "[" + Now.ToString() + "]")
        LogData(" Total API Elasped Time (ms) = " + vbTab + vbTab + "[" + Now.Subtract(m_dtStartAPITime).TotalMilliseconds.ToString() + "]")
        LogData(" ")

        response.Message = response.Message + GetWOsWithIssues()

        Return response
    End Function

    Private Sub LogSummaryData(ByVal filterData As FilterData)
        Dim strReturnedWOIDs As String = ""

        If (filterData.IsCallDetail) Then
            LogData(" ")
            LogData(" *** Returned WO's *****")
            For Each oRowData As RowData In ResponseData.Rows


                strReturnedWOIDs = ""
                If oRowData.CallCount > 0 Then

                    For Each strCallId As String In oRowData.CallIds
                        If String.IsNullOrEmpty(strReturnedWOIDs) Then
                            strReturnedWOIDs += strCallId
                        Else
                            strReturnedWOIDs += "," + strCallId
                        End If

                    Next
                    LogData(" Returned WOs: Row[" + oRowData.Id.ToString() + "] Count = [" + oRowData.CallCount.ToString() + "]  [" + strReturnedWOIDs + "]")
                End If

            Next
        End If
    End Sub


    Public Function GetSummaryDetailData(ByVal filterData As FilterDetailData) As JsonResponse(Of ColsData) Implements IDashSumm2Service.GetSummaryDetailData


        Dim curtm As DateTime = Now()
        Dim filterDataPerCol = New FilterData

        filterDataPerCol.UserId = filterData.UserId
        filterDataPerCol.CustAssoc = filterData.CustAssoc
        filterDataPerCol.IsCallDetail = filterData.IsCallDetail
        filterDataPerCol.UserPermission = New UserPermission

        filterDataPerCol.UserPermission.DashCustomerFilterOff = filterData.UserPermission.DashCustomerFilterOff
        filterDataPerCol.UserPermission.DashVendorFilterOff = filterData.UserPermission.DashVendorFilterOff
        filterDataPerCol.UserPermission.DashVendor = filterData.UserPermission.DashVendor
        filterDataPerCol.OrgBranchList = filterData.OrgBranchList
        filterDataPerCol.OrgFilterList = filterData.OrgFilterList
        filterDataPerCol.OrgFilterType = filterData.OrgFilterType
        filterDataPerCol.OrgType = filterData.OrgType

        filterDataPerCol.Rows = New List(Of Integer)

        filterDataPerCol.Rows = filterData.Rows


        WebAPILogger.SetCheckSum(filterData.UserId + filterData.CustAssoc + filterData.OrgBranchList + Now.Ticks.ToString())

        '   Log.GetCheckSum(filterData.UserId + filterData.CustAssoc + filterData.OrgBranchList + Now.Ticks.ToString())
        m_dtStartAPITime = Now()
        LogData(" ")
        LogData(" *********************** Begin API Call **************************")
        LogData(" Begin API Call Start Time " + vbTab + vbTab + "[" + m_dtStartAPITime.ToString() + "]")
        LogFilterData(filterData)
        LogData(" ")
        LogData(" Begin Json Formatted Data Input - Can be used to test with Postman or WCFClient")
        LogData(" ")
        LogData(JsonConvert.SerializeObject(filterData))
        LogData(" ")
        LogData(" End Json Formatted Data Input")
        LogData(" ")

        LogData(" ")
        LogData(" ******* Begin Database Calls - GetSummaryDetailData ************************************")
        LogData("")
        Dim response As New JsonResponse(Of ColsData)()
        Dim sz As String = String.Empty
        Dim lstCallIds = New List(Of String)
        Dim strSQL As String = ""
        Dim nColCount As Integer = 1
        Dim nRowID As Integer = 0

        Try

            If filterData Is Nothing Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Data:"
                Return response
            End If

            Select Case filterData.OrgType.ToLower
                Case "main"
                    mOrgType = OrgType.OrgTypeMain
                Case Else
                    mOrgType = OrgType.OrgTypeNone
            End Select

            If (mOrgType = OrgType.OrgTypeNone) Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgType - " & filterData.OrgType
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)
                Return response
            End If

            mDashType &= SetLeadingCap(filterData.OrgType.ToLower)

            Select Case filterData.OrgFilterType.ToLower
                Case "b"
                    mOrgFilterType = OrgFilterType.OrgFilterTypeBranch
                    mOrgFilterList = filterData.OrgBranchList
                    mDashType &= "Brch"
                Case "t"
                    mOrgFilterType = OrgFilterType.OrgFilterTypeTerritory
                    mOrgFilterList = filterData.OrgFilterList
                    mDashType &= "Terr"
                Case Else
                    mOrgFilterType = OrgFilterType.OrgFilterTypeNone
            End Select

            If mOrgFilterType = OrgFilterType.OrgFilterTypeNone Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgFilterType"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If mOrgFilterList = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgFilterList"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            If filterData.ColDatas.Count = 0 Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Cols"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If


            If filterData.Rows.Count = 0 Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Rows"
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                Return response
            End If

            LogData("Before GetSummaryData -->doGetDbCfgNoCC ")
            LogData(FnGetStartTime())


            doGetDbCfgNoCC(RC, ErrorMsg) ' needed NBD and deployment.


            '           strSQL = GetBaseSql()
            LogData("Before Building SQL Cmd ")
            LogData(ElapsedCummulativeTime())

            If filterData.Rows.Count > 1 Then
                response.IsSuccess = False
                response.RC = 5
                response.Message = "GetSummaryDetailData "
                If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & "NumberOf Rows <> 1 ")

                WriteEventLog(ErrorMsg)
                Return response


            End If
            For Each oRowID As Integer In filterData.Rows
                nRowID = oRowID
            Next




            For Each oDetailCol As Col In filterData.ColDatas

                If oDetailCol Is Nothing Then
                    response.IsSuccess = False
                    response.RC = 16
                    response.Message = "Invalid Customers"
                    If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                    Return response
                End If

                If oDetailCol.CustomerCamparatorName = String.Empty Or oDetailCol.CustomerCamparatorValue = String.Empty Then
                    response.IsSuccess = False
                    response.RC = 16
                    response.Message = "Invalid Customers"
                    If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                    Return response
                End If

                If oDetailCol.SubDivisionCamparatorName = String.Empty Then
                    response.IsSuccess = False
                    response.RC = 16
                    response.Message = "Invalid Sub Division"
                    If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                    Return response
                End If

                filterDataPerCol.Col = New Col
                filterDataPerCol.Col.CustomerCamparatorName = oDetailCol.CustomerCamparatorName
                filterDataPerCol.Col.CustomerCamparatorValue = oDetailCol.CustomerCamparatorValue
                filterDataPerCol.Col.SubDivisionCamparatorName = oDetailCol.SubDivisionCamparatorName
                filterDataPerCol.Col.SubDivisionCamparatorValue = oDetailCol.SubDivisionCamparatorValue
                filterDataPerCol.Col.WorkOrderType = oDetailCol.WorkOrderType


                CustomerCamparatorName = oDetailCol.CustomerCamparatorName
                CustomerCamparatorValue = oDetailCol.CustomerCamparatorValue
                SubDivisionCamparatorName = oDetailCol.SubDivisionCamparatorName
                SubDivisionCamparatorValue = oDetailCol.SubDivisionCamparatorValue
                WorkOrderType = oDetailCol.WorkOrderType

                mCustAssoc = filterData.CustAssoc.ToLower
                mVendorCode = filterData.VendorCode.ToLower


                If mCustAssoc <> String.Empty Then mSrchCustAssoc = "," & mCustAssoc & ","

                mTs1 = New TimeSpan(Now.Day, Now.Hour, Now.Minute, Now.Second, Now.Millisecond)
                RC = 0
                ErrorMsg = String.Empty

                If (nRowID <> 33 And nRowID <> 34) Then


                    If (nColCount = 1) Then

                        strSQL = GetBaseSql(nRowID)

                    Else
                        strSQL = strSQL + AddColInfoToBaseSQL(nRowID)

                    End If
                End If

                nColCount = nColCount + 1

                If RC <> 0 Then
                    response.IsSuccess = False
                    response.RC = RC
                    response.Message = ErrorMsg
                    If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

                    Return response
                End If

            Next
        Catch ex As Exception
            response.IsSuccess = False
            response.RC = 5
            response.Message = "GetSummaryDetailData Exception:" & ex.Message
            If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & response.Message)

            WriteEventLog(ErrorMsg)
            Return response
        End Try

        LogData("After Building SQL Cmd ")
        LogData(ElapsedCummulativeTime())


        'Execute SELECT
        LogData("Before-->doSql-->CustomerServiceBusinessInstance.GetCallResult ")
        Try
            CallResults = CustomerServiceBusinessInstance.GetCallResult(strSQL)
        Catch ex As IndexOutOfRangeException
            Debug.WriteLine(ex.Message)
        End Try

        LogData("After-->doSql-->CustomerServiceBusinessInstance.GetCallResult ")
        LogData(ElapsedCummulativeTime())
        LogData("")
        '   Dim ResponseData As New ColsData()
        Dim nPodCount As Integer = 0
        Dim strACallIds As New List(Of String)

        Dim RowData As New RowData()
        If (nRowID = 33 Or nRowID = 34) Then

            RowData.CallIds = New List(Of String)


            For Each oDetailCol As Col In filterData.ColDatas


                filterDataPerCol.Col = New Col
                filterDataPerCol.Col.CustomerCamparatorName = oDetailCol.CustomerCamparatorName
                filterDataPerCol.Col.CustomerCamparatorValue = oDetailCol.CustomerCamparatorValue
                filterDataPerCol.Col.SubDivisionCamparatorName = oDetailCol.SubDivisionCamparatorName
                filterDataPerCol.Col.SubDivisionCamparatorValue = oDetailCol.SubDivisionCamparatorValue
                filterDataPerCol.Col.WorkOrderType = oDetailCol.WorkOrderType


                CustomerCamparatorName = oDetailCol.CustomerCamparatorName
                CustomerCamparatorValue = oDetailCol.CustomerCamparatorValue
                SubDivisionCamparatorName = oDetailCol.SubDivisionCamparatorName
                SubDivisionCamparatorValue = oDetailCol.SubDivisionCamparatorValue
                WorkOrderType = oDetailCol.WorkOrderType

                mCustAssoc = filterData.CustAssoc.ToLower
                mVendorCode = filterData.VendorCode.ToLower

                '                tempResponseData = doSumMain(filterDataPerCol)
                Dim nPodCountType As Integer = 0
                RowData.Id = 33
                If (nRowID = 34) Then
                    nPodCountType = 4
                    RowData.Id = 34
                End If

                Dim pod = Count_POD(filterDataPerCol, nPodCountType)
                Try

                    RowData.CallCount += pod.Count
                    If (filterData.IsCallDetail And pod.Count > 0) Then
                        'For Each strBuffer As String In pod.[Select](Function(s) s.RowId.ToString).ToList()
                        '    strACallIds.AddRange()
                        'Next
                        RowData.CallIds.AddRange(pod.[Select](Function(s) s.RowId.ToString).ToList())
                    End If
                Catch ex As Exception
                    If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR (Count_POD_Total) !!!!!!!!!: " & ex.Message)
                End Try

            Next
            Dim Rows As New List(Of RowData)
            Rows.Add(RowData)
            ResponseData.Rows = Rows
            ResponseData.CustomerPercentage = 0

        Else
            ResponseData = doSumMain(filterDataPerCol)
        End If

        response.IsSuccess = True
        response.RC = 0
        response.SingleResult = ResponseData

        Dim nReturnCount As Integer = response.SingleResult.Rows(0).CallCount
        LogData("WO Count = " + nReturnCount.ToString())
        LogData(" ")
        LogData(" ******* End Database Calls GetSummaryDetailData ************************************")
        LogData(" ")

        If (filterDataPerCol.IsCallDetail) Then
            LogSummaryData(filterDataPerCol)
        End If

        LogData(" *********************** End API Call ****************************")
        LogData(" End API Call Time = " + vbTab + vbTab + "[" + Now.ToString() + "]")
        LogData(" Total API Elasped Time (ms) = " + vbTab + vbTab + "[" + Now.Subtract(m_dtStartAPITime).TotalMilliseconds.ToString() + "]")
        LogData(" ")

        response.Message = response.Message + GetWOsWithIssues()
        Return response
    End Function



    Private Function FnGetStartTime() As String
        m_dtDBStartIme = Now()
        FnGetStartTime = vbTab + " ::: Start Time =  [" + m_dtDBStartIme.ToString() + "]  :::"
    End Function
    Private Function FnGetStartTime(ByVal strTitle As String) As String
        m_dtDBStartIme = Now()
        If strTitle = "" Then
            FnGetStartTime = vbTab + " ::: Start Time = [" + m_dtDBStartIme.ToString() + "] :::"
        Else
            FnGetStartTime = strTitle + " Start Time =  [" + m_dtDBStartIme.ToString() + "] "
        End If

    End Function
    Private Function ElapsedCummulativeTime() As String

        ElapsedCummulativeTime = vbTab + " ::: Total Elapsed Time (ms) = " + vbTab + vbTab + " [" + Now.Subtract(m_dtStartAPITime).TotalMilliseconds.ToString() + "] "
    End Function
    Private Function FnGetElpasedStartTime() As String

        FnGetElpasedStartTime = vbTab + " ::: Elapsed Time(ms) = " + vbTab + vbTab + " [" + Now.Subtract(m_dtDBStartIme).TotalMilliseconds.ToString() + "]"
    End Function
    Private Function FnGetElpasedStartTime(ByVal strTitle As String) As String
        If strTitle = "" Then
            FnGetElpasedStartTime = vbTab + " ::: Elapsed Time(ms) = " + vbTab + vbTab + " [" + Now.Subtract(m_dtDBStartIme).TotalMilliseconds.ToString() + "]  :::"
        Else
            FnGetElpasedStartTime = strTitle + " Elapsed Time(ms) =" + vbTab + vbTab + " [" + Now.Subtract(m_dtDBStartIme).TotalMilliseconds.ToString() + "] "
        End If
    End Function

    Private Sub doGetDbCfgNoCC(ByRef RC As Integer, ByRef ErrorMsg As String)

        Dim strKey1Value As String = "MainCnt."
        Dim strKey2Value As String = ""
        Dim DfltTypes As String


        Try

            Dim sql As String
            Dim sz As String

            ' Get the DPLY Call Types
            strKey2Value = "WOTypes.DPLY"
            DfltTypes = "dply, 2Dpl, 3Dpl, 4Dpl, 5Dpl, ndpl, sdpl, ndpb, fdpl, cdpl, pshe"
            sql = String.Empty
            sql &= "Declare @szValue1 varchar(255), @szValue2 varchar(255), @szValue3 varchar(255);"
            sql &= vbCrLf & "Select @szValue1 = [Value1], @szValue2=[Value2], @szValue3=[Value3] FROM cat.dbo.DashControl With (nolock) WHERE (Key1='Dashboard') And (Key2='@strKey2Value');"
            sql &= vbCrLf & "IF (@szValue1 is null) And (@szValue2 is null) And (@szValue3 is null) select @szValue1 = '@DfltTypes', @szValue2 = '', @szValue3 = '';"
            sql &= vbCrLf & "select ISNULL(@szValue1,'') + ISNULL(@szValue2,'') + ISNULL(@szValue3,'');"

            sql = sql.Replace("@strKey2Value", strKey2Value)
            sql = sql.Replace("@DfltTypes", DfltTypes)
            LogData(vbTab + vbTab + "Before-->doGetDbCfgNoCC-->CustomerServiceBusinessInstance.GetScalarValue")
            LogData(FnGetStartTime())
            sz = CustomerServiceBusinessInstance.GetScalarValue(sql)
            LogData(vbTab + vbTab + "After -->doGetDbCfgNoCC-->CustomerServiceBusinessInstance.GetScalarValue ")
            LogData(FnGetElpasedStartTime())
            LogData(ElapsedCummulativeTime())
            LogData("")
            g_astrCallTypesDPL = sz.ToLower.Split(",")

        Catch ex As Exception
            RC = 5
            ErrorMsg = "doGetDbCfgNoCC Exception:" & ex.Message
            If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & ErrorMsg)

            WriteEventLog(ErrorMsg)
        End Try

    End Sub

    Private Function isInStrList(ByVal sz As String, ByVal aList() As String) As Boolean
        Dim i As Integer
        For i = 0 To aList.GetUpperBound(0)
            If aList(i) = sz Then Return True
        Next
        Return False
    End Function

    Private Sub doSql(ByRef RC As Integer, ByRef ErrorMsg As String)
        Dim sql As String = String.Empty
        Try
            'Build SELECT base
            sql = "select "
            sql &=
                "dispatch_number, customer_number, query_customer_number, customer_state, employee_number, call_type" &
                vbCrLf & ",entitlement, stop_code, stop_code_indicator, mfg_serial_number, mfg_model_number, system_number" &
                vbCrLf & ",call_received_date, projected_arrival_date, call_last_modified_date, call_status, timezone" &
                vbCrLf & ",actiongrp, stop_code_upd_dt, territory, srs_sched, ready_date, cust_sub_div" &
                vbCrLf & ",branch_rdb, region_rdb, area_rdb, district_rdb, VendorCode, csr_territory, customer_country, employee_class " &
                vbCrLf & ",sched_window_start_dt,sched_window_end_dt, call_closed_date, email_addr, (select count(*) from dbo.parts parts with (nolock) where calls.Dispatch_Number = parts.Dispatch_Number) as PartCount" &
                vbCrLf & ",case when exists (select 'x' from oneview.dbo.labor lbr WITH (nolock) " &
                                                       "where lbr.Dispatch_Number = calls.Dispatch_Number And lbr.stop_code In ('21','23','05','17','26','ONSITE') " &
                                                       "And CAST(getdate()+(calls.timezone/24) As date) = CAST(lbr.end_datetime As date)) " &
                                          "then 'y' else null end AS HasCC_Today"
            ' SAAS 939 Work_Orders_without_parts
            sql &= vbCrLf & ", case when ((select count(*) from dbo.parts AS P with (nolock)  where calls.Dispatch_Number = P.Dispatch_Number)=0)" &
                            " THEN 1 " &
                            " ELSE " &
                               "case when ((select count(*) from dbo.parts AS P with (nolock)  where calls.Dispatch_Number = P.Dispatch_Number)=1)" &
                                           "And EXISTS ( SELECT 'x' FROM oneview.dbo.Parts AS part2 WITH (nolock)" &
                                           "WHERE(part2.[Dispatch_Number] = calls.[Dispatch_Number])" &
                                           " And (part2.[Part_Number]='PARTHOLD')) " &
                             " then 1 " &
                             " Else 0 " &
                   " End " &
            "End As Work_Orders_without_parts"


            sql &= vbCrLf & ",(Select count(*) from dbo.labor lbr With (nolock) where calls.Dispatch_Number = lbr.Dispatch_Number And cast(calls.[ready_date] As Date) <= cast(lbr.start_datetime As Date) And lbr.stop_code In ('02','04','05')) as NumDeferSinceRdy "
            sql &= vbCrLf & ", appdb.dbo.GetBusDtCust(calls.[query_customer_number], calls.[customer_country], calls.[customer_state], CONVERT(date, GETDATE()+(calls.[timezone]/24)),1) AS EtaNbd "
            sql &= vbCrLf & ", case when EXISTS ( SELECT 'x' FROM dbo.LaborActivity a (nolock) " &
                                                       "WHERE(a.Dispatch_Number = calls.Dispatch_Number) " &
                                                       "And (a.product_reference In ('2ManAssist','2Man')) " &
                                                       "And (ISNULL(a.CSR,'')='' Or LEFT(a.CSR,1)='T')) then 'y' " &
                                          "else null end AS TwoManAssign "
            sql &= vbCrLf & ",case when " &
                                 vbCrLf & "(EXISTS ( SELECT 'x' FROM oneview.dbo.labor as lbr with (nolock) WHERE calls.dispatch_number = lbr.dispatch_number And (lbr.stop_code='ets01'))) " &
                                 vbCrLf & "  And (NOT EXISTS ( SELECT 'x' FROM oneview.dbo.labor as lbr with (nolock) WHERE calls.dispatch_number = lbr.dispatch_number And (lbr.stop_code='ets99')) Or (dbo.GetLastSC(calls.[dispatch_number], 'ets01') > dbo.GetLastSC(calls.[dispatch_number], 'ets99'))) " &
                                 vbCrLf & "then 'y' else null end AS Escalations "

            sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(getdate()+(calls.timezone/24) as date),-1) as Gbdc01"
            sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(call_received_date as date), case call_type when '05bd' then 5 when 'depo' then 5 when 'dpbr' then 5 when '03bd' then 3 when '02bd' then 2 else 1 end) as Gbdc02"
            sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(getdate()+(calls.timezone/24) as date),1) as Gbdc03"
            sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(getdate()+(calls.timezone/24) as date),1) as Gbdc04"
            sql &= vbCrLf & ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number And (cast(lbr.start_datetime as date) = CAST(getdate()+(calls.timezone/24) as date)) And lbr.stop_code='21') then 'y' else null end as Lbr01"
            sql &= vbCrLf & ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number And (cast(lbr.start_datetime as date) = CAST(getdate()+(calls.timezone/24) as date)) And lbr.stop_code In ('60','63','65','75','76','55','56','57','58')) then 'y' else null end as Lbr02"
            sql &= vbCrLf & ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number and lbr.stop_code='61') then 'y' else null end as Lbr03"
            sql &= ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number and lbr.stop_code In ('60','61','62','63','64','65','66','67','71','73','74','75','76','77','78','79','90','95')) then 'y' else null end as Lbr04"
            sql &= ",dbo.GetStopCodeList(calls.dispatch_number) as ScList "
            ' call opened after 17:00 and 1BD after call open
            sql &= vbCrLf & ",case when (CONVERT(time, calls.[call_received_date]) < CONVERT(time, '17:00')) " &
                    " And (CONVERT(date, GETDATE()+(calls.[timezone]/24)) >= appdb.dbo.GetBusDtCust(calls.[query_customer_number], calls.[customer_country], calls.[customer_state], CONVERT(date, calls.[call_received_date]), 1)) " &
                    " then 'y' else null end As PendingAppt1 "

            ' 2BD after call open
            sql &= vbCrLf & ",case when (CONVERT(time, calls.[call_received_date]) >= CONVERT(time, '17:00')) " &
                    " And (CONVERT(date, GETDATE()+(calls.[timezone]/24)) >= appdb.dbo.GetBusDtCust(calls.[query_customer_number], calls.[customer_country], calls.[customer_state], CONVERT(date, calls.[call_received_date]), 2)) " &
                    " then 'y' else null end As PendingAppt2 "

            ' EmeaHasCC   
            sql &= vbCrLf & ",case when exists (select 'x' from oneview.dbo.labor lbr WITH (nolock) " &
                    "where lbr.Dispatch_Number = calls.Dispatch_Number And lbr.stop_code In ('21','23','05','17')) " &
                    "then 'y' else null end AS HasCC "

            ' IsScheduledForToday  - SAAS 788

            sql &= vbCrLf & ", case when calls.[srs_sched]='y' " &
                      " And calls.[sched_window_start_dt] Is Not Null " &
                      " And CONVERT(date, calls.[sched_window_start_dt])=CONVERT(date, GETDATE()+(calls.[timezone]/24)) " &
                      " then 'y' else 'n' end As IsScheduledForToday "


            sql &= vbCrLf & "from dbo.vw_csr_org_calls as calls with (NOLOCK) "

            'Build WHERE
            Select Case mOrgFilterType
                Case OrgFilterType.OrgFilterTypeBranch
                    If (mOrgFilterList.Split(","c).Count(Function(c) c = "crop") > 0) _
                    Then
                        sql &= vbCrLf & "where ([branch_rdb] in " & List2Set(mOrgFilterList) & " Or [branch_rdb] Is Null Or [branch_rdb] = 'corp')"
                    Else
                        sql &= vbCrLf & "where ([branch_rdb] in " & List2Set(mOrgFilterList) & ")"
                    End If
                Case OrgFilterType.OrgFilterTypeTech
                    sql &= vbCrLf & "where (employee_number in " & List2Set(mOrgFilterList) & ")"
                Case OrgFilterType.OrgFilterTypeTerritory
                    sql &= vbCrLf & "where (csr_territory in " & List2Set(mOrgFilterList) & ")"
                Case Else
            End Select

            'Filetr Customers by Code, Sub-Division, Wo Type
            If Not String.IsNullOrEmpty(CustomerCamparatorName) Then
                sql &= vbCrLf & " and (" & GetCustomerComparatorString(CustomerCamparatorName, CustomerCamparatorValue) & ")"
            End If

            If Not String.IsNullOrEmpty(SubDivisionCamparatorName) And SubDivisionCamparatorName.ToLower <> "all" Then
                sql &= vbCrLf & " and (cust_sub_div " & GetComparatorString(SubDivisionCamparatorName, SubDivisionCamparatorValue) & ")"
            End If

            If Not String.IsNullOrEmpty(WorkOrderType) Then
                sql &= vbCrLf & " and (call_type in " & List2Set(WorkOrderType) & ")"
            End If




            'Execute SELECT
            LogData("Before-->doSql-->CustomerServiceBusinessInstance.GetCallResult ")
            LogData(FnGetStartTime())
            Try
                CallResults = CustomerServiceBusinessInstance.GetCallResult(sql)
                ' CallResults = GetNewResults(sql)

            Catch ex As IndexOutOfRangeException
                Debug.WriteLine(ex.Message)
            End Try

            LogData("After-->doSql-->CustomerServiceBusinessInstance.GetCallResult ")
            LogData(FnGetElpasedStartTime())
            LogData(ElapsedCummulativeTime())

            LogData("")
        Catch ex As Exception
            RC = 5
            ErrorMsg = "doSql Exception: " & Len(sql).ToString & vbCrLf & ex.Message & vbCrLf & vbCrLf & sql
            LogData(" !!!!!!!! ERROR !!!!!!!!!: " & ErrorMsg)

            WriteEventLog(ErrorMsg)
        End Try
    End Sub
    Private Function GetBaseSql(nRowID As Integer) As String
        Dim sql As String = String.Empty
        Try
            'Build SELECT base
            sql = "select "
            sql &=
                "dispatch_number, customer_number, query_customer_number, customer_state, employee_number, call_type" &
                vbCrLf & ",entitlement, stop_code, stop_code_indicator, mfg_serial_number, mfg_model_number, system_number" &
                vbCrLf & ",call_received_date, projected_arrival_date, call_last_modified_date, call_status, timezone" &
                vbCrLf & ",actiongrp, stop_code_upd_dt, territory, srs_sched, ready_date, cust_sub_div" &
                vbCrLf & ",branch_rdb, region_rdb, area_rdb, district_rdb, VendorCode, csr_territory, customer_country, employee_class " &
                vbCrLf & ",sched_window_start_dt,sched_window_end_dt, call_closed_date, email_addr, (select count(*) from dbo.parts parts with (nolock) where calls.Dispatch_Number = parts.Dispatch_Number) as PartCount" &
                vbCrLf & ",case when exists (select 'x' from oneview.dbo.labor lbr WITH (nolock) " &
                                                       "where lbr.Dispatch_Number = calls.Dispatch_Number And lbr.stop_code In ('21','23','05','17','26','ONSITE') " &
                                                       "And CAST(getdate()+(calls.timezone/24) As date) = CAST(lbr.end_datetime As date)) " &
                                          "then 'y' else null end AS HasCC_Today"
            sql &= vbCrLf & ",(select count(*) from dbo.labor lbr with (nolock) where calls.Dispatch_Number = lbr.Dispatch_Number and cast(calls.[ready_date] AS date) <= cast(lbr.start_datetime as date) and lbr.stop_code In ('02','04','05')) as NumDeferSinceRdy "


            If (nRowID = RowTypes.EmailReady2Send) Or (nRowID = RowTypes.DplyNBD) Then
                sql &= vbCrLf & ", appdb.dbo.GetBusDtCust(calls.[query_customer_number], calls.[customer_country], calls.[customer_state], CONVERT(date, GETDATE()+(calls.[timezone]/24)),1) AS EtaNbd "
            End If




            If (nRowID = RowTypes.TwoManAssignNoTB) Then
                sql &= vbCrLf & ", case when EXISTS ( SELECT 'x' FROM dbo.LaborActivity a (nolock) " &
                                                       "WHERE(a.Dispatch_Number = calls.Dispatch_Number) " &
                                                       "And (a.product_reference In ('2ManAssist','2Man')) " &
                                                       "And (ISNULL(a.CSR,'')='' Or LEFT(a.CSR,1)='T')) then 'y' " &
                                          "else null end AS TwoManAssign "
            End If

            sql &= vbCrLf & ",case when " &
                                 vbCrLf & "(EXISTS ( SELECT 'x' FROM oneview.dbo.labor as lbr with (nolock) WHERE calls.dispatch_number = lbr.dispatch_number And (lbr.stop_code='ets01'))) " &
                                 vbCrLf & "  And (NOT EXISTS ( SELECT 'x' FROM oneview.dbo.labor as lbr with (nolock) WHERE calls.dispatch_number = lbr.dispatch_number And (lbr.stop_code='ets99')) Or (dbo.GetLastSC(calls.[dispatch_number], 'ets01') > dbo.GetLastSC(calls.[dispatch_number], 'ets99'))) " &
                                 vbCrLf & "then 'y' else null end AS Escalations "

            If (nRowID = RowTypes.CallsInTB) Then
                sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(getdate()+(calls.timezone/24) as date),-1) as Gbdc01"
            End If

            If (nRowID = RowTypes.BacklogNoExcp) Then
                sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(call_received_date as date), case call_type when '05bd' then 5 when 'depo' then 5 when 'dpbr' then 5 when '03bd' then 3 when '02bd' then 2 else 1 end) as Gbdc02"
            End If

            If (nRowID = RowTypes.Backlog) Then
                sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(getdate()+(calls.timezone/24) as date),1) as Gbdc03"
            End If

            If (nRowID = RowTypes.PendingSchedule) Then
                sql &= vbCrLf & ",appdb.dbo.GetBusDtCust([query_customer_number], [customer_country], [customer_state], cast(getdate()+(calls.timezone/24) as date),1) as Gbdc04"
            End If



            If (nRowID = RowTypes.PendingCustomerETA) Then
                sql &= vbCrLf & ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number And (cast(lbr.start_datetime as date) = CAST(getdate()+(calls.timezone/24) as date)) And lbr.stop_code='21') then 'y' else null end as Lbr01"
            End If



            If (nRowID = RowTypes.MultipleVisits) Then
                sql &= vbCrLf & ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number And (cast(lbr.start_datetime as date) = CAST(getdate()+(calls.timezone/24) as date)) And lbr.stop_code In ('60','63','65','75','76','55','56','57','58')) then 'y' else null end as Lbr02"
            End If

            If (nRowID = RowTypes.NewCAD) Or nRowID = (RowTypes.NewBNN) Then
                sql &= vbCrLf & ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number and lbr.stop_code='61') then 'y' else null end as Lbr03"
            End If

            If (nRowID = RowTypes.BacklogNoExcp) Then
                sql &= ",case when exists (select 'x' from dbo.labor as lbr with (nolock) where calls.dispatch_number = lbr.dispatch_number and lbr.stop_code In ('60','61','62','63','64','65','66','67','71','73','74','75','76','77','78','79','90','95')) then 'y' else null end as Lbr04"
            End If


            sql &= ",dbo.GetStopCodeList(calls.dispatch_number) as ScList "

            If (nRowID = RowTypes.PendingAppointment) Then
                ' call opened after 17:00 and 1BD after call open
                sql &= vbCrLf & ",case when (CONVERT(time, calls.[call_received_date]) < CONVERT(time, '17:00')) " &
                    " And (CONVERT(date, GETDATE()+(calls.[timezone]/24)) >= appdb.dbo.GetBusDtCust(calls.[query_customer_number], calls.[customer_country], calls.[customer_state], CONVERT(date, calls.[call_received_date]), 1)) " &
                    " then 'y' else null end As PendingAppt1 "

                ' 2BD after call open
                sql &= vbCrLf & ",case when (CONVERT(time, calls.[call_received_date]) >= CONVERT(time, '17:00')) " &
                    " And (CONVERT(date, GETDATE()+(calls.[timezone]/24)) >= appdb.dbo.GetBusDtCust(calls.[query_customer_number], calls.[customer_country], calls.[customer_state], CONVERT(date, calls.[call_received_date]), 2)) " &
                    " then 'y' else null end As PendingAppt2 "
            End If



            ' EmeaHasCC   
            sql &= vbCrLf & ",case when exists (select 'x' from oneview.dbo.labor lbr WITH (nolock) " &
                    "where lbr.Dispatch_Number = calls.Dispatch_Number And lbr.stop_code In ('21','23','05','17')) " &
                    "then 'y' else null end AS HasCC "

            ' IsScheduledForToday  - SAAS 788

            sql &= vbCrLf & ", case when calls.[srs_sched]='y' " &
                      " And calls.[sched_window_start_dt] Is Not Null " &
                      " And CONVERT(date, calls.[sched_window_start_dt])=CONVERT(date, GETDATE()+(calls.[timezone]/24)) " &
                      " then 'y' else 'n' end As IsScheduledForToday "


            sql &= vbCrLf & "from dbo.vw_csr_org_calls as calls with (NOLOCK) "

            'Build WHERE
            Select Case mOrgFilterType
                Case OrgFilterType.OrgFilterTypeBranch
                    If (mOrgFilterList.Split(","c).Count(Function(c) c = "crop") > 0) _
                    Then
                        sql &= vbCrLf & "where ([branch_rdb] in " & List2Set(mOrgFilterList) & " Or [branch_rdb] Is Null Or [branch_rdb] = 'corp')"
                    Else
                        sql &= vbCrLf & "where ([branch_rdb] in " & List2Set(mOrgFilterList) & ")"
                    End If
                Case OrgFilterType.OrgFilterTypeTech
                    sql &= vbCrLf & "where (employee_number in " & List2Set(mOrgFilterList) & ")"
                Case OrgFilterType.OrgFilterTypeTerritory
                    sql &= vbCrLf & "where (csr_territory in " & List2Set(mOrgFilterList) & ")"
                Case Else
            End Select

            'Filetr Customers by Code, Sub-Division, Wo Type
            If Not String.IsNullOrEmpty(CustomerCamparatorName) Then
                sql &= vbCrLf & " and (" & GetCustomerComparatorString(CustomerCamparatorName, CustomerCamparatorValue) & ")"
            End If

            If Not String.IsNullOrEmpty(SubDivisionCamparatorName) And SubDivisionCamparatorName.ToLower <> "all" Then
                sql &= vbCrLf & " and (cust_sub_div " & GetComparatorString(SubDivisionCamparatorName, SubDivisionCamparatorValue) & ")"
            End If

            If Not String.IsNullOrEmpty(WorkOrderType) Then
                sql &= vbCrLf & " and (call_type in " & List2Set(WorkOrderType) & ")"
            End If

            GetBaseSql = sql


        Catch ex As Exception
            ErrorMsg = "doSql Exception: " & Len(sql).ToString & vbCrLf & ex.Message & vbCrLf & vbCrLf & sql
            LogData(" !!!!!!!! ERROR !!!!!!!!!: " & ErrorMsg)

            WriteEventLog(ErrorMsg)
        End Try
    End Function
    Private Function AddColInfoToBaseSQL(nRowID As Integer) As String


        AddColInfoToBaseSQL = vbCrLf & "UNION " & vbCrLf & GetBaseSql(nRowID)


    End Function


    Public Function GetNewResults(ByVal SQL As String) As IList(Of CallResult)
        Dim strConnStr As String = ""

        strConnStr = ConfigurationManager.ConnectionStrings("OneView_DB_Connection").ConnectionString
        Dim SQLConn As SqlConnection = GetConnection(strConnStr)
        Dim dr As SqlDataReader ' or OleDataReader
        Dim myReaderList As New List(Of CallResult)

        Try
            '          If SQLConn.State = ConnectionState.Open Then SQLConn.Close()
            Dim SQLCmd As New SqlCommand
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = SQL
            dr = SQLCmd.ExecuteReader()
            Dim schemaTable As DataTable = dr.GetSchemaTable()
            Dim Row As DataRow = schemaTable.Rows(0)




            While (dr.Read())
                Dim obj As New CallResult
                PopulateProperty(obj.actiongrp, dr, "actiongrp")
                PopulateProperty(obj.area_rdb, dr, "area_rdb")
                PopulateProperty(obj.branch_rdb, dr, "branch_rdb")
                PopulateProperty(obj.call_closed_date, dr, "call_closed_date")
                PopulateProperty(obj.call_last_modified_date, dr, "call_last_modified_date")
                PopulateProperty(obj.call_received_date, dr, "call_received_date")
                PopulateProperty(obj.call_status, dr, "call_status")
                PopulateProperty(obj.call_type, dr, "call_type")
                PopulateProperty(obj.csr_territory, dr, "csr_territory")
                PopulateProperty(obj.customer_country, dr, "customer_country")
                PopulateProperty(obj.customer_number, dr, "customer_number")
                PopulateProperty(obj.customer_state, dr, "customer_state")
                PopulateProperty(obj.cust_sub_div, dr, "cust_sub_div")
                PopulateProperty(obj.dispatch_number, dr, "dispatch_number")
                PopulateProperty(obj.district_rdb, dr, "district_rdb")
                PopulateProperty(obj.email_addr, dr, "email_addr")
                PopulateProperty(obj.employee_class, dr, "employee_class")
                PopulateProperty(obj.employee_number, dr, "employee_number")
                PopulateProperty(obj.entitlement, dr, "entitlement")
                PopulateProperty(obj.Escalations, dr, "Escalations")
                PopulateProperty(obj.EtaNbd, dr, "EtaNbd")
                'PopulateProperty(obj.Gbdc01, dr, "Gbdc01")
                'PopulateProperty(obj.Gbdc02, dr, "Gbdc02")
                'PopulateProperty(obj.Gbdc03, dr, "Gbdc03")
                'PopulateProperty(obj.Gbdc04, dr, "Gbdc04")
                PopulateProperty(obj.HasCC, dr, "HasCC")
                PopulateProperty(obj.HasCC_Today, dr, "HasCC_Today")
                PopulateProperty(obj.IsScheduledForToday, dr, "IsScheduledForToday")
                PopulateProperty(obj.Lbr01, dr, "Lbr01")
                PopulateProperty(obj.Lbr02, dr, "Lbr02")
                PopulateProperty(obj.Lbr03, dr, "Lbr03")
                PopulateProperty(obj.Lbr04, dr, "Lbr04")
                PopulateProperty(obj.mfg_model_number, dr, "mfg_model_number")
                PopulateProperty(obj.mfg_serial_number, dr, "mfg_serial_number")
                PopulateProperty(obj.NumDeferSinceRdy, dr, "NumDeferSinceRdy")
                PopulateProperty(obj.PartCount, dr, "PartCount")
                PopulateProperty(obj.PendingAppt1, dr, "PendingAppt1")
                PopulateProperty(obj.PendingAppt2, dr, "PendingAppt2")
                PopulateProperty(obj.projected_arrival_date, dr, "projected_arrival_date")
                PopulateProperty(obj.query_customer_number, dr, "query_customer_number")
                PopulateProperty(obj.ready_date, dr, "ready_date")
                PopulateProperty(obj.region_rdb, dr, "region_rdb")
                PopulateProperty(obj.sched_window_end_dt, dr, "sched_window_end_dt")
                PopulateProperty(obj.sched_window_start_dt, dr, "sched_window_start_dt")
                PopulateProperty(obj.ScList, dr, "ScList")
                PopulateProperty(obj.srs_sched, dr, "srs_sched")
                PopulateProperty(obj.stop_code, dr, "stop_code")
                PopulateProperty(obj.stop_code_indicator, dr, "stop_code_indicator")
                PopulateProperty(obj.stop_code_upd_dt, dr, "stop_code_upd_dt")
                PopulateProperty(obj.system_number, dr, "system_number")
                PopulateProperty(obj.territory, dr, "territory")
                PopulateProperty(obj.timezone, dr, "timezone")
                PopulateProperty(obj.TwoManAssign, dr, "TwoManAssign")
                PopulateProperty(obj.vendorcode, dr, "vendorcode")
                myReaderList.Add(obj)
            End While
            SQLCmd.Connection.Close()

        Catch ex As Exception

        End Try
        Return myReaderList
    End Function
    Private Sub PopulateProperty(ByRef objProperty As Object, dr As SqlDataReader, var As String)
        If Not IsDBNull(dr.Item(var)) Then

            Dim fldType As String = dr.GetFieldType(dr.GetOrdinal(var)).ToString()
            If fldType.ToUpper().Contains("DATETIME") Then
                objProperty = Convert.ToDateTime(dr.Item(var))
            Else
                objProperty = dr.Item(var)
            End If
        Else

            Dim fldType As String = dr.GetFieldType(dr.GetOrdinal(var)).ToString()
            If fldType.ToUpper().Contains("DATETIME") Then
                '                objProperty = 
            Else
                objProperty = ""
            End If

        End If

    End Sub
    Private Function GetConnection(strConnect As String) As SqlConnection
        Dim oconn As SqlConnection = New SqlConnection(strConnect)
        Return oconn
    End Function

    Public Function Count_POD(
                             ByVal filterData As FilterData,
                            Optional ByVal p_BusDaysAge As Integer = 0
                            ) As IList(Of WayBillResult)

        Dim strWhere As String
        Dim VendorUser As Boolean = False

        VendorUser = IIf((filterData.VendorCode <> String.Empty Or filterData.UserPermission.DashVendor), True, False)

        Select Case filterData.OrgFilterType.ToLower
            Case "b"
                strWhere = "([Branch] In " & List2Set(filterData.OrgBranchList) & ") "
            Case "t"
                strWhere = "([Territory] In " & List2Set(filterData.OrgFilterList) & ") And ([Branch] In " & List2Set(filterData.OrgBranchList) & ") "
            Case Else
                strWhere = "1=2 "
        End Select

        'Filetr Customers by Code, Sub-Division, Wo Type
        If Not String.IsNullOrEmpty(filterData.Col.CustomerCamparatorName) Then
            strWhere = strWhere & vbCrLf & " and ([CustCode] " & GetComparatorString(filterData.Col.CustomerCamparatorName, filterData.Col.CustomerCamparatorValue) & ")"
        End If

        If Not String.IsNullOrEmpty(SubDivisionCamparatorName) And SubDivisionCamparatorName.ToLower <> "all" Then
            strWhere = strWhere & vbCrLf & " and (Subdivision " & GetComparatorString(SubDivisionCamparatorName, SubDivisionCamparatorValue) & ")"
        End If

        ' If the user vendor code is not blank, make sure the vendor code matches the assigned employee
        If (VendorUser) Then
            If (mVendorCode <> String.Empty AndAlso (Not filterData.UserPermission.DashVendorFilterOff)) Then
                strWhere = strWhere & vbCrLf & "  And (VENDORCODE = '" & filterData.VendorCode & "') "
            End If
            If (mCustAssoc <> String.Empty AndAlso (Not filterData.UserPermission.DashCustomerFilterOff)) Then
                strWhere = strWhere & vbCrLf & "  And ((CHARINDEX(',' + [CustCode] + '|' + [Subdivision] + ',','" & mSrchCustAssoc & "')>0) " &
                            vbCrLf & " Or (CHARINDEX(','+ [CustCode] + ',','" & mSrchCustAssoc & "')>0)) "
            End If
            If ((mVendorCode = String.Empty) Or (mCustAssoc = String.Empty)) Then
                strWhere = " and 1=2 "
            End If
        End If

        If (p_BusDaysAge > 0) Then
            strWhere = strWhere &
                vbCrLf & "  And (CONVERT(date, GETDATE()) " &
                                ">= (appdb.dbo.GetBusDtCust([CustCode], 'US', 'MA', CONVERT(date, [comdate]), " & CStr(p_BusDaysAge) & "))) "
        End If

        Dim strSQL As String

        Dim objCountPOD As New List(Of WayBillResult)


        Try
            strSQL =
            "SELECT rid As RowId " &
            vbCrLf & "FROM oneview.dbo.vw_OpenWaybills " &
            vbCrLf & "WHERE " & strWhere & ";"
            LogData(vbTab + "Before GetSummaryData -->Count_POD-->CustomerServiceBusinessInstance.GetWayBillResult " + p_BusDaysAge.ToString())
            LogData(vbTab + "GetWayBillResult = [" + strSQL + "]")
            LogData(FnGetStartTime())
            objCountPOD = CustomerServiceBusinessInstance.GetWayBillResult(strSQL)
            LogData(vbTab + "After GetSummaryData -->Count_POD-->CustomerServiceBusinessInstance.GetWayBillResult ")
            LogData(FnGetElpasedStartTime())
            LogData(ElapsedCummulativeTime())
            LogData("")

        Catch ex As Exception
            If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR !!!!!!!!!: " & ErrorMsg)
        End Try

        Return objCountPOD  'this may cause a problem 
    End Function

End Class
