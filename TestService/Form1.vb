Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WWTS.Data
Imports WWTS.Data.Model
Imports WWTS.Aspects.Enums
Imports WWTS.Aspects.Utils
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient


Public Class Form1
    Dim m_oTest As New TestAsync
    Dim fd As New testlocal.FilterData

    '    Dim fdDetail As New testlocal.FilterDetailData
    ' Dim filterdetail As FilterDetailData = New FilterDetailData
    Dim filterdetail = New testlocal.FilterDetailData
    'Dim filterdetail = New testDashUmm2Service.FilterDetailData

    'Dim filterdetail = New FilterDetailData
    Dim fdapi As New FilterData

    Dim m_strInputFile As String
    Dim m_strOutputFile As String

    Dim m_strSQLInputFile As String
    Dim m_strSQLOutputFile As String
    Dim m_blnDetailAPICall As Boolean = False

    Dim CallResults As List(Of CallResult)
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
    Private m_blnCustomSQL As Boolean = False
    Private m_strRowSQLCMD As String = String.Empty
    Private m_nRowAPIID As Int16 = -1
    Private m_strSQLDATECHECK As String =
                    " AND  (case when projected_arrival_date IS NOT NULL" _
                 & " THEN projected_Arrival_date " _
                 & " ELSE GetDate() " _
                 & " END) <=  TODATETIMEOFFSET({fn NOW()},timezone)"
    Private m_strSQLRECEIVEDATECHECK As String =
                    " AND datediff(day,call_received_date,TODATETIMEOFFSET({fn NOW()},timezone)) "

    Dim KeyWords As List(Of String) = New List(Of String)(New String() {"SELECT", "AND", "THEN", "ELSE", "OR", "WHERE", "CAST", "IN", "FROM", "CASE", "AS", "END"})
    Dim KeyWordsColors As List(Of Color) = New List(Of Color)(New Color() {Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Red, Color.Green, Color.Green, Color.Green, Color.Green})
    Private m_dtEplasedtime As DateTime


    Private Enum RowTypes
        None = 0
        ' Calls due today or prior (	ETA <= today)
        EtaB4Today = 1                  'Calls
        EtaB4TodayOpen = 2              'Calls Still open (No BO)
        EtaB4TodayCL = 3                'Calls Closed/cancelled
        EtsCallComplete = 4             '% Complete
        EtaB4TodayTB = 5                '“T” Buckets (No BO)
        EtaB4TodayAS = 6                'As status Calls
        EtaB4TodayBO = 7                'BO status calls
        JustGo = 8                      'Just-Go Calls
        EtaB4TodayNoPU = 9              'Parts available, not picked up
        NoCC = 10                       'No contact by 10:00
        EtaB4TodayEnroute = 11          'En-Route   
        EtaB4TodayOS = 12               'Onsite
        EtaB4TodayOld = 13              'Expired ETA
        ' Escalation Information
        Esc = 14                        'Escalation (ESC 1,2,3)
        SC13 = 15                       'Stop Code 13
        SCP13 = 16                      'Possible stop code 13
        ' Deferred Information
        Deferred04 = 17                   'Deferred (04)
        DeferredPer04 = 18                '% deferred (04)
        ThreeDeferSinceRdy = 19         '3+ defers since ready date
        Gt4DaysRdy = 20                 'Part ready>4 days
        ' Scheduling Information
        Scheduled = 21                  'Scheduled Calls
        SchedBO = 22                    'Scheduled Calls in BO
        EmailReady2Send = 23            'E-mail ready to send
        EmailPendingC1 = 24             'Email contact pending
        TextPendingC2 = 25              'Text contact pending
        ' General Call Information
        TOC = 26                        'Total Open
        DPLY = 27                       'Deployment Calls
        TwoManAssignNoTB = 28           '2 Man Assist to be assigned
        DplyNBD = 29                    'Deployment Calls <= NBD
        NewOpen = 30                    'Calls received today
        PendingCN = 31                  'Cust-Canc still open
        Closed = 32                     'Closed/canceled
        Pod = 33                        'Outstanding Way Bills
        Pod2 = 34                       'Outstanding Way Bills (4+ BO)

        'Running
        RunningToday = 35               'Calls Running today
        Escalations = 36                'Escalations
        RunningSbdCalls = 37            'Same Day Calls
        CallsInTB = 38                  'Calls in “T” Buckets
        CallsInAS = 39                  'Calls in “AS” Status
        PendingCustomerETA = 40         'Calls waiting for Customer ETA
        Onsite = 41                     'Onsite
        EtaOverdue = 42                 'ETA Overdue(1.5 hours)
        MultipleVisits = 43             'Multiple Visits
        RunningPendingCN = 44           'To be Cancelled
        RunningCancelled = 45           'Cancelled
        RunningClosed = 46              'Closed

        'New
        NewOpenCalls = 47               'New Calls received today
        CallsInBO = 48                  'BO Status
        NewCAD = 49                     'New CAD Calls
        NewBNN = 50                     'New BNN Calls
        NewPNA = 51                     'New PNA Calls

        'Future 
        FutureSbdCalls = 52             'Same Day Calls
        BacklogNoExcp = 53              'Backlog – No Exception code
        Backlog = 54                    'Backlog
        OldPNA = 55                     'Old PNA code
        Gt3Days = 56                    '>3 Days
        Gt7Days = 57                    '>7 Days
        PendingAppointment = 58         'New Calls pending Appointments
        PendingSchedule = 59            'Calls to be Scheduled

        ' Deferred Information
        Deferred05 = 60                   'Deferred (05)
        DeferredPer05 = 61                '% deferred (05)

        SC02 = 62                      'stop code 02

    End Enum

    Private Sub ConnectSQL(strSQLCMD As String)
        Dim strConnStr As String = ""

        strConnStr = ConfigurationManager.ConnectionStrings("OneView_DB_Connection").ConnectionString
        Dim oSQLHelper As SQLHelper = New SQLHelper(strConnStr)

        Dim oDT As DataTable = oSQLHelper.ExecuteDataTable(strSQLCMD)

    End Sub
    Private Function GetSQLData(strSQLCMD As String) As DataTable
        Dim strConnStr As String = ""
        Dim strConnectInfo() As String

        strConnStr = ConfigurationManager.ConnectionStrings("OneView_DB_Connection").ConnectionString

        strConnectInfo = strConnStr.Split(";")
        drpDataSource.Text = strConnectInfo(0)
        Dim oSQLHelper As SQLHelper = New SQLHelper(strConnStr)

        GetSQLData = oSQLHelper.ExecuteDataTable(strSQLCMD)

    End Function




    Private Sub btnTestGetDataSummary_Click(sender As Object, e As EventArgs) Handles btnTestGetDataSummary.Click

        'Dim ws As New testlocal
        'Dim fd As New testlocal.FilterData






        ReadJsonFile()

        TestAPICall()


        Exit Sub





    End Sub

    Private Function GetRowDescription(nId As Int32) As String
        Dim strReturn As String = ""
        m_strRowSQLCMD = ""
        Select Case (nId)
            Case RowTypes.EtaB4Today
                strReturn = "Calls"   ' EtaB4Today = 1   
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= "AND  (case when projected_arrival_date IS NOT NULL"
                m_strRowSQLCMD &= " then projected_Arrival_date "
                m_strRowSQLCMD &= " else GetDate() "
                m_strRowSQLCMD &= " end) <=  TODATETIMEOFFSET({fn NOW()},timezone)"
                Exit Select
            Case RowTypes.EtaB4TodayOpen
                strReturn = "Calls Still open (No BO)" 'EtaB4TodayOpen = 2
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (call_status <> 'bo' )"
                m_strRowSQLCMD &= "AND  (case when projected_arrival_date IS NOT NULL"
                m_strRowSQLCMD &= " then projected_Arrival_date "
                m_strRowSQLCMD &= " else GetDate() "
                m_strRowSQLCMD &= " end) <=  TODATETIMEOFFSET({fn NOW()},timezone)"
                Exit Select
            Case RowTypes.EtaB4TodayCL
                strReturn = "Calls Closed/cancelled" 'EEtaB4TodayCL = 3 
                m_strRowSQLCMD = " AND LEFT( call_status, 1) = 'c' "
                m_strRowSQLCMD &= "AND  (case when projected_arrival_date IS NOT NULL"
                m_strRowSQLCMD &= " then projected_Arrival_date "
                m_strRowSQLCMD &= " else GetDate() "
                m_strRowSQLCMD &= " end) <=  TODATETIMEOFFSET({fn NOW()},timezone)"

                Exit Select
            Case RowTypes.EtsCallComplete 'EtsCallComplete = 4
                strReturn = "% Complete"
                Exit Select


            Case RowTypes.EtaB4TodayTB   'EtaB4TodayTB = 5
                strReturn = "T Buckets (No BO)"
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (call_status <> 'bo' )"
                m_strRowSQLCMD &= " and LEFT(employee_number,1) = 't' "
                m_strRowSQLCMD &= " and stop_code_indicator <> 'p13'"
                m_strRowSQLCMD &= "AND  (case when projected_arrival_date IS NOT NULL"
                m_strRowSQLCMD &= " then projected_Arrival_date "
                m_strRowSQLCMD &= " else GetDate() "
                m_strRowSQLCMD &= " end) <=  TODATETIMEOFFSET({fn NOW()},timezone)"

                Exit Select
            Case RowTypes.EtaB4TodayAS   'EtaB4TodayAS = 6
                strReturn = "As status Calls"
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (call_status = 'as' )"
                m_strRowSQLCMD &= "AND  (case when projected_arrival_date IS NOT NULL"
                m_strRowSQLCMD &= " then projected_Arrival_date "
                m_strRowSQLCMD &= " else GetDate() "
                m_strRowSQLCMD &= " end) <=  TODATETIMEOFFSET({fn NOW()},timezone)"
                Exit Select
            Case RowTypes.EtaB4TodayBO   'EtaB4TodayBO = 7
                strReturn = "BO status calls"
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (call_status = 'bo' ) "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.JustGo   'JustGo = 8 
                strReturn = "Just-Go Calls"
                m_strRowSQLCMD = ") AS X SELECT * from #temp  WHERE LEFT( call_status, 1) <> 'c' AND (SCLIST  LIKE '%17%') "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.EtaB4TodayNoPU
                strReturn = "Parts available, not picked up" 'EtaB4TodayNoPU = 9 

                m_strRowSQLCMD = ") AS X SELECT * from #temp WHERE LEFT( call_status, 1) <> 'c'  AND ( SCLIST NOT LIKE '%pu%')   "
                m_strRowSQLCMD &= " AND call_status <> 'bo' AND PartCount > 0 "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.NoCC
                strReturn = "No contact by 10:00" 'NoCC = 10  
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= " WHERE LEFT( call_status, 1) <> 'c' AND (query_customer_number NOT LIKE'%wwts%' ) "
                m_strRowSQLCMD &= " AND (LEFT(query_customer_number,3) <> 'qxs' ) "
                m_strRowSQLCMD &= " AND (call_status <> 'bo' ) AND  (projected_Arrival_date IS NOT NULL) AND ( HasCC_Today <> 'y')"
                m_strRowSQLCMD &= " AND (query_customer_number NOT LIKE '%ibm%' ) "
                m_strRowSQLCMD &= m_strSQLDATECHECK


                Exit Select
            Case RowTypes.EtaB4TodayEnroute
                strReturn = "En-Route " 'EtaB4TodayEnroute = 11  
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (stop_code_indicator = '26' ) "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.EtaB4TodayOS
                strReturn = "Onsite" 'EtaB4TodayOS = 12          
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (stop_code_indicator = 'onsite' ) "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.EtaB4TodayOld
                strReturn = "Expired ETA" 'EtaB4TodayOld = 13        
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (LEFT(stop_code_indicator,6 )<> 'onsite' ) "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.Esc
                strReturn = "Escalation (ESC 1,2,3)" 'EEsc = 14      
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND ( NOT stop_code_indicator IN ('ets99', 'eccinfo' )) "
                m_strRowSQLCMD &= " AND (( actiongrp IN ('esc0', 'esc1', 'esc2', 'esc3', 'smtesc1','smtesc2', 'smtesc3' ) "
                m_strRowSQLCMD &= " OR (stop_code_indicator  LIKE '%ecc%' ) "
                m_strRowSQLCMD &= " OR (stop_code_indicator  LIKE '%ets%' ))) "

                Exit Select
            Case RowTypes.SC13
                strReturn = "Stop Code 13" ' SC13 = 15    
                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " AND ( stop_code_indicator = 'p13') "
                Exit Select
            Case RowTypes.SCP13
                strReturn = "Work Orders without parts" ' SCP13 = 16       
                ' strReturn = "Stop Code 13" ' SC13 = 15    
                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " And (calls.[call_type] Not In ('dply','2dpl','3dpl','4dpl','5dpl','ndpl','sdpl','ndpb','fdpl','cdpl','pshe'))"
                m_strRowSQLCMD &= " And (actiongrp <> '13priority') "
                Exit Select
            Case RowTypes.Deferred04
                strReturn = "Deferred (04)" ' Deferred04 = 17       
                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " AND ( stop_code_indicator = '04') "
                Exit Select
            Case RowTypes.DeferredPer04
                strReturn = "% deferred (04)" ' DeferredPer04 = 18                 
                Exit Select
            Case RowTypes.ThreeDeferSinceRdy
                strReturn = "3+ defers since ready Date" 'ThreeDeferSinceRdy = 19   
                If m_blnCustomSQL Then
                    m_strRowSQLCMD = ") AS X SELECT * from #temp "
                    m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c' "
                    m_strRowSQLCMD &= " AND ( NumDeferSinceRdy >= 3) "

                    m_strRowSQLCMD &= " AND call_type NOT in ('" + String.Join("','", g_astrCallTypesDPL) + "')"
                End If
                Exit Select
            Case RowTypes.Gt4DaysRdy
                strReturn = "Part ready>4 days" 'Gt4DaysRdy = 20   
                If m_blnCustomSQL Then
                    m_strRowSQLCMD = ") AS X SELECT * from #temp "
                    m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c' "
                    m_strRowSQLCMD &= " AND ( call_status <> 'bo') "
                    m_strRowSQLCMD &= " AND ( PartCount > 0) "
                    m_strRowSQLCMD &= " AND (ready_date IS NULL OR ready_date < DateAdd(Day, -4, TODATETIMEOFFSET({fn NOW()},timezone))) "
                    m_strRowSQLCMD &= " And call_type Not In ('" + String.Join("','", g_astrCallTypesDPL) + "')"
                End If
                Exit Select
            Case RowTypes.Scheduled
                strReturn = "Scheduled Calls" 'Scheduled = 21        
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (srs_sched = 'y' ) "
                m_strRowSQLCMD &= m_strSQLDATECHECK
                Exit Select
            Case RowTypes.SchedBO
                strReturn = "Scheduled Calls In BO" 'SchedBO = 22                
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (call_status = 'bo')  AND (srs_sched = 'y' ) "

                Exit Select
            Case RowTypes.EmailReady2Send
                strReturn = "E-mail ready To send" 'EmailReady2Send = 23      
                If m_blnCustomSQL Then
                    m_strRowSQLCMD = ") AS X SELECT * from #temp "
                    m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c'  AND ( call_status <> 'bo')  AND (srs_sched <> 'y') "
                    m_strRowSQLCMD &= "  AND ( SCLIST NOT LIKE '%05%') "
                    m_strRowSQLCMD &= "  AND ( stop_code_indicator NOT IN ('04', '23', 'c1', 'c2')) "
                    m_strRowSQLCMD &= "  AND call_type Not In ('" + String.Join("','", g_astrCallTypesDPL) + "')"
                    m_strRowSQLCMD &= "  AND  ((case when projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE {fn NOW()} END) <= EtaNbd)"
                    m_strRowSQLCMD &= "  AND  ((case when projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE {fn NOW()} END) > TODATETIMEOFFSET({fn NOW()},timezone) )"
                End If
                Exit Select
            Case RowTypes.EmailPendingC1
                strReturn = "Email contact pending" 'EmailPendingC1 = 24               
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (stop_code_indicator = 'c1') "

                Exit Select
            Case RowTypes.TextPendingC2
                strReturn = "Text contact pending" 'TextPendingC2 = 25               
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c' AND (stop_code_indicator = 'c2') "

                Exit Select
            Case RowTypes.TOC
                strReturn = "Total Open" 'TOC = 26       
                m_strRowSQLCMD = " and LEFT( call_status, 1) <> 'c'  "

                Exit Select
            Case RowTypes.DPLY
                strReturn = "Deployment Calls" 'DPLY = 27  
                If m_blnCustomSQL Then

                    m_strRowSQLCMD &= " AND LEFT( call_status, 1) <> 'c'"

                    m_strRowSQLCMD &= " AND call_type in ('" + String.Join("','", g_astrCallTypesDPL) + "')"
                End If
                Exit Select
            Case RowTypes.TwoManAssignNoTB
                strReturn = "2 Man Assist To be assigned" 'TwoManAssignNoTB = 28  
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE LEFT( employee_number, 1) <> 't' "
                m_strRowSQLCMD &= " AND (TwoManAssign = 'y') "
                m_strRowSQLCMD &= " AND (LEFT( call_status, 1) <> 'c') "
                m_strRowSQLCMD &= " AND (actiongrp <> 'cust-canc') "

                Exit Select
            Case RowTypes.DplyNBD
                strReturn = "2Deployment Calls <= NBD" 'TDplyNBD = 29    
                If m_blnCustomSQL Then
                    m_strRowSQLCMD = ") AS X SELECT * from #temp "
                    m_strRowSQLCMD &= " WHERE LEFT( call_status, 1) <> 'c'"

                    m_strRowSQLCMD &= " AND call_type in ('" + String.Join("','", g_astrCallTypesDPL) + "')"
                    m_strRowSQLCMD &= " AND (CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE GetDate() END <= EtaNbd)"
                End If
                Exit Select
            Case RowTypes.NewOpen
                strReturn = "2Calls received today" 'NewOpen = 30          
                m_strRowSQLCMD = " AND LEFT( call_status, 1) <> 'c' AND (actiongrp <> 'cust-canc') "
                m_strRowSQLCMD &= m_strSQLRECEIVEDATECHECK + " = 0 "
                Exit Select
            Case RowTypes.PendingCN
                strReturn = "Cust-Canc still open" 'PendingCN = 31   
                m_strRowSQLCMD = " AND LEFT( call_status, 1) <> 'c' AND (actiongrp = 'cust-canc') "
                Exit Select
            Case RowTypes.Closed
                strReturn = "Closed/canceled" 'Closed = 32           
                m_strRowSQLCMD = " AND LEFT( call_status, 1) = 'c' AND (actiongrp <> 'cust-canc') "
                m_strRowSQLCMD &= m_strSQLRECEIVEDATECHECK + " = 0 "
                Exit Select
            Case RowTypes.Pod
                strReturn = "Outstanding Way Bills" 'Pod = 33  
                If m_blnCustomSQL Then
                    Dim pod = Count_POD(fd, 0)
                End If
                Exit Select
            Case RowTypes.Pod2
                strReturn = "Outstanding Way Bills (4+ BO)" 'Pod2 = 34 
                If m_blnCustomSQL Then
                    Dim pod = Count_POD(fd, 4)
                End If
                Exit Select
            Case RowTypes.RunningToday
                strReturn = "Calls Running today" 'RunningToday = 35      
                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' AND (call_status <> 'bo' )"
                m_strRowSQLCMD &= " AND LEFT( employee_number, 1) <> 't' "
                m_strRowSQLCMD &= " AND actiongrp <> 'cust-canc'"
                m_strRowSQLCMD &= " AND CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END <=  TODATETIMEOFFSET({fn NOW()},timezone) "
                Exit Select
            Case RowTypes.Escalations
                strReturn = "Escalations" 'Escalations = 36      
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= " WHERE LEFT( call_status, 1) <> 'c' AND (actiongrp <> 'cust-canc') AND (Escalations = 'y')  "
                Exit Select
            Case RowTypes.RunningSbdCalls
                strReturn = "Same Day Calls" 'RunningSbdCalls = 37     
                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' AND (actiongrp <> 'cust-canc')"
                m_strRowSQLCMD &= " AND call_type IN ('02hr', '03hr', '04hr', '05hr', '06hr', '08hr', '10hr', 'emc1', 'emc2', 'emc3', 'emc4', 'emcy', 'sdpr', 'sev1', 'sev2', 'sev3', 'spec', 'temp', '3.5h', 'c4hr', '4hdk', '4hmn', 'sdpl')"
                m_strRowSQLCMD &= " AND CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END <=  TODATETIMEOFFSET({fn NOW()},timezone) "

                Exit Select
            Case RowTypes.CallsInTB
                strReturn = "Calls In T Buckets" 'CallsInTB = 38    
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c' AND (call_status <> 'bo' )"
                m_strRowSQLCMD &= " AND LEFT( employee_number, 1) = 't' "
                m_strRowSQLCMD &= " AND actiongrp <> 'cust-canc'"
                m_strRowSQLCMD &= " AND ((CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END <=  TODATETIMEOFFSET({fn NOW()},timezone)) "
                m_strRowSQLCMD &= " OR (call_received_date >= DateAdd(Minute, 1050, Gbdc01))"
                m_strRowSQLCMD &= " AND (call_received_date < TODATETIMEOFFSET({fn NOW()},timezone)))"
                Exit Select
            Case RowTypes.CallsInAS
                strReturn = "Calls In As Status" 'CallsInAS = 39    
                m_strRowSQLCMD &= " AND  call_status = 'as'"
                m_strRowSQLCMD &= " AND LEFT( employee_number, 1) <> 't' "
                m_strRowSQLCMD &= " AND actiongrp <> 'cust-canc'"
                m_strRowSQLCMD &= " AND CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END <=  TODATETIMEOFFSET({fn NOW()},timezone) "

                Exit Select
            Case RowTypes.PendingCustomerETA
                strReturn = "Calls waiting For Customer ETA" 'PendingCustomerETA = 40  
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c' AND (call_status <> 'bo' )"
                m_strRowSQLCMD &= "  AND actiongrp <> 'cust-canc'"
                m_strRowSQLCMD &= "  AND ((CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END <=  TODATETIMEOFFSET({fn NOW()},timezone))"
                m_strRowSQLCMD &= "  AND (CASE WHEN (Lbr01 IS NOT NULL AND Lbr01 = 'y') THEN 'y' ELSE 'n' END) <> 'y')"
                Exit Select
            Case RowTypes.Onsite
                strReturn = "Onsite" 'Onsite = 41       
                m_strRowSQLCMD &= "  AND LEFT( call_status, 1) <> 'c'"
                m_strRowSQLCMD &= "  AND stop_code_indicator = 'onsite'"
                m_strRowSQLCMD &= "  AND ((CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END =  TODATETIMEOFFSET({fn NOW()},timezone)))"
                Exit Select
            Case RowTypes.EtaOverdue
                strReturn = "ETA Overdue(1.5 hours)" 'EtaOverdue = 42                 
                m_strRowSQLCMD = "  AND (LEFT( call_status, 1) <> 'c'"
                m_strRowSQLCMD &= "  AND actiongrp <> 'cust-canc'"
                m_strRowSQLCMD &= "  AND (DATEADD(MINUTE,90,((CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END))) <=  TODATETIMEOFFSET({fn NOW()},timezone)) AND stop_code_indicator <> 'onsite')"
                Exit Select
            Case RowTypes.MultipleVisits
                strReturn = "EMultiple Visits" 'EtaOverdue = 43     
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= " WHERE LEFT( call_status, 1) <> 'c' AND (actiongrp <> 'cust-canc') AND (Lbr02 = 'y')  "
                Exit Select
            Case RowTypes.RunningPendingCN
                strReturn = "To be Cancelled" 'RunningPendingCN = 44         
                m_strRowSQLCMD = " AND LEFT( call_status, 1) <> 'c' AND (actiongrp = 'cust-canc') "
                Exit Select
            Case RowTypes.RunningCancelled
                strReturn = "Cancelled" 'RunningCancelled = 45        
                m_strRowSQLCMD = " AND LEFT( call_status, 1) = 'c' AND LEFT( stop_code, 3) = 'can' "
                Exit Select
            Case RowTypes.RunningClosed
                strReturn = "Closed" 'RunningClosed = 46 
                m_strRowSQLCMD = " AND LEFT( call_status, 1) = 'c' AND LEFT( stop_code, 3) <> 'can' "
                Exit Select
            Case RowTypes.NewOpenCalls
                strReturn = "New Calls received today" 'NewOpenCalls = 47    
                m_strRowSQLCMD = m_strSQLRECEIVEDATECHECK + " = 0 "
                m_strRowSQLCMD &= "  AND (actiongrp <> 'cust-canc') "

                Exit Select
            Case RowTypes.CallsInBO
                strReturn = "BO Status" 'CallsInBO = 48 
                m_strRowSQLCMD = m_strSQLRECEIVEDATECHECK + " = 0 "
                m_strRowSQLCMD &= "  AND (actiongrp <> 'cust-canc') "
                m_strRowSQLCMD &= "  AND (call_status = 'bo') "

                Exit Select
            Case RowTypes.NewCAD
                strReturn = "New CAD Calls" 'NewCAD = 49     
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE (actiongrp <> 'cust-canc') "
                m_strRowSQLCMD &= "  AND (Lbr03 = 'y)' "
                m_strRowSQLCMD &= "  AND LEFT( entitlement, 3) <> 'cad'"
                m_strRowSQLCMD &= m_strSQLRECEIVEDATECHECK + " = 0 )"

                Exit Select
            Case RowTypes.NewBNN
                strReturn = "New BNN Calls" 'NewBNN = 50   
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE (actiongrp <> 'cust-canc') "
                m_strRowSQLCMD &= "  AND (Lbr03 = 'y)' "
                m_strRowSQLCMD &= "  AND (system_number = 'bnn') "
                m_strRowSQLCMD &= m_strSQLRECEIVEDATECHECK + " = 0 )"
                Exit Select
            Case RowTypes.NewPNA
                strReturn = "New PNA Calls" 'NewPNA = 51      
                m_strRowSQLCMD = "  AND (system_number = 'bnn') "
                Exit Select
            Case RowTypes.FutureSbdCalls
                strReturn = "Same Day Calls" 'FutureSbdCalls = 52        

                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' AND (actiongrp <> 'cust-canc')"
                m_strRowSQLCMD &= " AND call_type IN ('02hr', '03hr', '04hr', '05hr', '06hr', '08hr', '10hr', 'emc1', 'emc2', 'emc3', 'emc4', 'emcy', 'sdpr', 'sev1', 'sev2', 'sev3', 'spec', 'temp', '3.5h', 'c4hr', '4hdk', '4hmn', 'sdpl')"
                m_strRowSQLCMD &= " AND CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END >  TODATETIMEOFFSET({fn NOW()},timezone) "

                Exit Select
            Case RowTypes.BacklogNoExcp
                strReturn = "Backlog – No Exception code" '  BacklogNoExcp = 53 
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= " WHERE LEFT( call_status, 1) <> 'c' AND ( actiongrp <>  'cust-canc')  "
                m_strRowSQLCMD &= " AND ((Gbdc02 is NOT NULL) AND TODATETIMEOFFSET({fn NOW()}, TimeZone) > Gbdc02 )"
                m_strRowSQLCMD &= " AND (Lbr04 <> 'y' OR Lbr04 IS NULL) "
                Exit Select
            Case RowTypes.Backlog
                strReturn = "Backlog" '  Backlog = 54   
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE (actiongrp <> 'cust-canc') "
                m_strRowSQLCMD &= " AND Left(call_status, 1) <> 'c' AND ( projected_arrival_date IS NOT NULL) AND (Gbdc03 IS NOT NULL) AND (projected_arrival_date > Gbdc03) "
                Exit Select
            Case RowTypes.OldPNA
                strReturn = "Old PNA code" '   OldPNA = 55     
                m_strRowSQLCMD = " And LEFT( call_status, 1) <> 'c' AND ( actiongrp = 'pna')  "
                Exit Select
            Case RowTypes.Gt3Days
                strReturn = ">3 Days" '   Gt3Days = 56     
                m_strRowSQLCMD = " AND LEFT( call_status, 1) <> 'c' AND ( actiongrp <>  'cust-canc')  "
                m_strRowSQLCMD &= "AND ((call_received_date is NOT NULL) AND (call_received_date <  DateAdd(Day, -3, {fn NOW()})))  "

                Exit Select
            Case RowTypes.Gt7Days
                strReturn = ">7 Days" '   Gt7Days = 57                  
                m_strRowSQLCMD = " AND LEFT( call_status, 1) <> 'c' AND ( actiongrp <>  'cust-canc')  "
                m_strRowSQLCMD &= "AND ((call_received_date is NOT NULL) AND (call_received_date <  DateAdd(Day, -7, {fn NOW()})))  "

                Exit Select
            Case RowTypes.PendingAppointment
                strReturn = "New Calls pending Appointments" '   PendingAppointment = 58
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " AND ( (PendingAppt1 = 'y') OR (PendingAppt2 = 'y')) "
                m_strRowSQLCMD &= " AND (case when HasCC is not null then 'y' else 'n' end) <> 'y'"

                Exit Select
            Case RowTypes.PendingSchedule
                strReturn = "Calls To be Scheduled" '   PendingSchedule = 59         
                m_strRowSQLCMD = ") AS X SELECT * from #temp "
                m_strRowSQLCMD &= "  WHERE LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " AND (Gbdc04 IS NOT NULL) "
                m_strRowSQLCMD &= " AND ( actiongrp <>  'cust-canc') "
                m_strRowSQLCMD &= " AND ( call_status  <>  'bo') "
                m_strRowSQLCMD &= " AND LEFT( employee_number, 1) = 't' "
                m_strRowSQLCMD &= " AND ((CASE WHEN projected_arrival_date IS NOT NULL THEN projected_arrival_date ELSE DateAdd(Day, 1, call_received_date) END  ) > Gbdc04)"


                Exit Select
            Case RowTypes.Deferred05
                strReturn = "Deferred (05)" '   Deferred05 = 60          
                m_strRowSQLCMD = "  And LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " AND ( stop_code_indicator = '05') "
                Exit Select
            Case RowTypes.DeferredPer05
                strReturn = "% deferred (05)" '   DeferredPer05 = 61                   
                Exit Select
            Case RowTypes.SC02
                strReturn = "Stop code 02" '  stop code 02    
                m_strRowSQLCMD = "  AND LEFT( call_status, 1) <> 'c' "
                m_strRowSQLCMD &= " AND ( stop_code_indicator = '02') "
                Exit Select



            Case Else
                strReturn = "Unknown"


        End Select
        GetRowDescription = strReturn
    End Function

    Private Sub ReadJsonFile()
        If m_strInputFile = "" Then

            MsgBox("FileName Not Available")
            Exit Sub
        End If
        m_blnDetailAPICall = False
        Dim reader1 As New StreamReader(m_strInputFile)
        Dim assets As String = reader1.ReadToEnd()
        reader1.Close()
        If assets.Length = 0 Then
            MsgBox("Empty File")
            Exit Sub
        End If
        Dim o As JObject = JObject.Parse(assets)

        Dim arrCol As JArray = o.SelectToken("ColDatas")

        If Not IsNothing(arrCol) Then
            Try

                If arrCol.Count = 0 Then
                    MsgBox("No Col Data Or Corrupt Json File")
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox("Corrupt Json File:" + ex.Message)
                Exit Sub

            End Try
            ReadJsonCallDetailFile()
            TabControl1.SelectedIndex = 3
            m_blnDetailAPICall = True
            Exit Sub
        End If




        txbUserID.Text = Convert.ToString(o("UserId"))
        txbOrgBranchList.Text = Convert.ToString(o("OrgBranchList"))
        txbOrgFilterList.Text = Convert.ToString(o("OrgFilterList"))
        txbCustAssoc.Text = Convert.ToString(o("CustAssoc"))
        txbVendorCode.Text = Convert.ToString(o("VendorCode"))

        'txbOrgBranchList.Text = Convert.ToString(o("OrgFilterList"))

        'txbOrgFilterList.Text = Convert.ToString(o("OrgFilterType"))





        Dim arr As JArray = o.SelectToken("Rows")

        If IsNothing(arr) Then
            MsgBox("No Data Or Corrupt Json File")
            Exit Sub
        End If

        Dim strBuffer As String = ""
        For Each tok As JToken In arr
            If strBuffer <> "" Then strBuffer = strBuffer + ","
            strBuffer = strBuffer + tok.ToString
        Next

        If String.IsNullOrEmpty(txbRows.Text) Then
            txbRows.Text = strBuffer
        End If
        drpWorkOrderType.Text = Convert.ToString(o("Col")("WorkOrderType"))
        drpCustomerCamparatorName.Text = Convert.ToString(o("Col")("CustomerCamparatorName"))
        drpCustomerCamparatorValue.Text = Convert.ToString(o("Col")("CustomerCamparatorValue"))
        drpSubDivisionCamparatorName.Text = Convert.ToString(o("Col")("SubDivisionCamparatorName"))
        drpSubDivisionCamparatorValue.Text = Convert.ToString(o("Col")("SubDivisionCamparatorValue"))
        drpOrgFilterType.Text = Convert.ToString(o("OrgFilterType"))
        drpOrgType.Text = Convert.ToString(o("OrgType"))

        chkDashVendor.Checked = Convert.ToBoolean(o("UserPermission")("DashVendor"))
        chkDashVendorFilterOff.Checked = Convert.ToBoolean(o("UserPermission")("DashVendorFilterOff"))
        chkDashCustomerFilterOff.Checked = Convert.ToBoolean(o("UserPermission")("DashCustomerFilterOff"))


        chkIsCallDetail.Checked = Convert.ToBoolean(o("IsCallDetail"))
    End Sub
    Private Sub ReadJsonCallDetailFile()



        If m_strInputFile = "" Then

            MsgBox("FileName Not Available")
            Exit Sub
        End If
        Dim reader1 As New StreamReader(m_strInputFile)
        Dim assets As String = reader1.ReadToEnd()
        reader1.Close()
        If assets.Length = 0 Then
            MsgBox("Empty File")
            Exit Sub
        End If
        Dim o As JObject = JObject.Parse(assets)

        Dim arrCol As JArray = o.SelectToken("ColDatas")

        If Not IsNothing(arrCol) Then
            Try

                If arrCol.Count = 0 Then
                    MsgBox("No Col Data Or Corrupt Json File")
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox("Corrupt Json File:" + ex.Message)
                Exit Sub

            End Try

        End If


        '        Dim filterdetail As New FilterDetailData

        txbDetailUserid.Text = Convert.ToString(o("UserId"))
        filterdetail.UserId = txbDetailUserid.Text

        txbDetailOrgBranchList.Text = Convert.ToString(o("OrgBranchList"))
        filterdetail.OrgBranchList = txbDetailOrgBranchList.Text

        txbDetailOrgFilterList.Text = Convert.ToString(o("OrgFilterList"))
        filterdetail.OrgFilterList = txbDetailOrgFilterList.Text

        txbDetailCustAssoc.Text = Convert.ToString(o("CustAssoc"))
        filterdetail.CustAssoc = txbDetailCustAssoc.Text

        txbDetailVendorCode.Text = Convert.ToString(o("VendorCode"))
        filterdetail.VendorCode = txbDetailVendorCode.Text


        filterdetail.UserPermission = New testlocal.UserPermission

        'filterdetail.UserPermission = New testDashUmm2Service.UserPermission

        chkDetailDashVendorFilterOff.Checked = Convert.ToBoolean(o("UserPermission")("DashVendorFilterOff"))
        filterdetail.UserPermission.DashVendorFilterOff = chkDetailDashVendorFilterOff.Checked



        chkDetailDashCustomerFilterOff.Checked = Convert.ToBoolean(o("UserPermission")("DashCustomerFilterOff"))
        filterdetail.UserPermission.DashCustomerFilterOff = chkDetailDashCustomerFilterOff.Checked

        chkDetailDashVendor.Checked = Convert.ToBoolean(o("UserPermission")("DashVendor"))
        filterdetail.UserPermission.DashVendor = chkDetailDashVendor.Checked


        chkDetaiI_IsCallDetail.Checked = Convert.ToBoolean(o("IsCallDetail"))
        filterdetail.IsCallDetail = chkDetaiI_IsCallDetail.Checked


        txbDetailOrgFilterType.Text = Convert.ToString(o("OrgFilterType"))
        filterdetail.OrgFilterType = txbDetailOrgFilterType.Text


        txbDetailOrgType.Text = Convert.ToString(o("OrgType"))
        filterdetail.OrgType = txbDetailOrgType.Text



        Dim arr As JArray = o.SelectToken("Rows")

        If IsNothing(arr) Then
            MsgBox("No Data Or Corrupt Json File")
            Exit Sub
        End If

        Dim strBuffer As String = ""
        For Each tok As JToken In arr
            If strBuffer <> "" Then strBuffer = strBuffer + ","
            strBuffer = strBuffer + tok.ToString
        Next
        txbRows.Text = strBuffer
        Dim strARows() As String = txbRows.Text.Split(",")
        Dim strTemp = strARows.ToList()

        ' filterdetail.Rows = Array.ConvertAll(strARows, New Converter(Of String, Integer)(AddressOf ConvertToInteger))



        ' Dim strTemp = strARows.ToList()
        filterdetail.Rows = New List(Of Integer)
        For Each strBuf As String In strARows
            filterdetail.Rows.Add(Convert.ToInt32(strBuf))
        Next




        'filterdetail.ColDatas = arrCol ' Array.ConvertAll(arrCol, New testlocal.Col)

        'filterdetail.ColDatas = New List(Of testDashUmm2Service.Col)

        'filterdetail.ColDatas() = New testlocal.Col
        'filterdetail.ColDatas() = New testDashUmm2Service.Col
        filterdetail.ColDatas = New List(Of testlocal.Col)

        For Each colTol As JToken In arrCol
            Dim colData = New testlocal.Col

            ' Dim colData = New testDashUmm2Service.Col

            colData.WorkOrderType = Convert.ToString(colTol("WorkOrderType"))
            colData.CustomerCamparatorName = Convert.ToString(colTol("CustomerCamparatorName"))
            colData.CustomerCamparatorValue = Convert.ToString(colTol("CustomerCamparatorValue"))

            colData.SubDivisionCamparatorName = Convert.ToString(colTol("SubDivisionCamparatorName"))
            colData.SubDivisionCamparatorValue = Convert.ToString(colTol("SubDivisionCamparatorValue"))

            filterdetail.ColDatas.Add(colData)


        Next

    End Sub

    Private Sub TestAPICall()
        Dim ws As New testlocal.DashSumm2ServiceClient
        '    Dim fd As New testlocal.FilterData
        'Dim colResp As New testlocal.ColsDataResponse

        Dim colResp As JsonResponse(Of ColsData)
        fd.Col = New testlocal.Col



        fd.Col.CustomerCamparatorName = drpCustomerCamparatorName.Text

        fd.Col.CustomerCamparatorValue = drpCustomerCamparatorValue.Text

        fd.Col.SubDivisionCamparatorName = drpSubDivisionCamparatorName.Text
        fd.Col.SubDivisionCamparatorValue = drpSubDivisionCamparatorValue.Text
        fd.Col.WorkOrderType = drpWorkOrderType.Text



        fd.IsCallDetail = chkIsCallDetail.CheckState
        fd.OrgBranchList = txbOrgBranchList.Text

        fd.OrgFilterList = txbOrgFilterList.Text
        fd.OrgFilterType = drpOrgFilterType.Text
        fd.OrgType = drpOrgType.Text

        Dim strARows() As String = txbRows.Text.Split(",")

        Dim strTemp = strARows.ToList()
        fd.Rows = New List(Of Integer)
        For Each strBuffer As String In strARows
            fd.Rows.Add(Convert.ToInt32(strBuffer))
        Next


        'fd.Rows = Array.ConvertAll(strARows, New Converter(Of String, Integer)(AddressOf ConvertToInteger))


        fd.UserId = "tmw63818"
        fd.UserId = txbUserID.Text
        fd.VendorCode = txbVendorCode.Text
        fd.CustAssoc = txbCustAssoc.Text
        fd.UserPermission = New testlocal.UserPermission

        fd.UserPermission.DashVendor = chkDashVendor.CheckState
        fd.UserPermission.DashVendorFilterOff = chkDashCustomerFilterOff.CheckState
        fd.UserPermission.DashCustomerFilterOff = chkDashVendorFilterOff.CheckState

        If (chkUseStaticFile.Checked) Then
            colResp = GetJsonColResponseFromFile()
        Else
            colResp = ws.GetSummaryData(fd)
            '  colResp = ws.GetSummaryDetailData(fd)
        End If





        Dim dtTemp As DataTable = New DataTable()
        Dim oRowDT As DataRow
        dtTemp.Columns.Add("ID Type", GetType(System.Int32))
        dtTemp.Columns.Add("Desc", GetType(System.String))
        dtTemp.Columns.Add("Count", GetType(System.Decimal))

        Dim oRow As RowData
        If (colResp.IsSuccess) Then
            If String.IsNullOrEmpty(colResp.Message) Then
                lblReturnMessage.Visible = False
                lblRtnMsgText.Visible = False
            Else
                lblReturnMessage.Visible = True
                lblRtnMsgText.Visible = True

            End If

            lblRtnMsgText.Text = colResp.Message
            If (chkSaveAPIResponse.Checked) Then
                WriteToJsonFile(colResp)
            End If

            For Each oRow In colResp.SingleResult.Rows
                oRowDT = dtTemp.NewRow()
                oRowDT("ID Type") = oRow.Id
                oRowDT("Desc") = GetRowDescription(oRow.Id)
                oRowDT("Count") = oRow.CallCount
                dtTemp.Rows.Add(oRowDT)
            Next

            Debug.Print("Success")
            dgView.DataSource = dtTemp ' colResp.SingleResult.Rows
        End If
        TabControl1.SelectedIndex = 1
    End Sub

    Private Function GetJsonColResponseFromFile() As JsonResponse(Of ColsData)

        Dim strStaticInputFile As String

        opdlg.DefaultExt = "json"
        opdlg.Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
        opdlg.InitialDirectory = "c:\temp"
        opdlg.FileName = ""

        opdlg.ShowDialog()

        strStaticInputFile = opdlg.FileName
        If strStaticInputFile = "" Then

            MsgBox("FileName Not Available")
            Exit Function
        End If


        Dim GetJsonColResponseFromFileBuffer As JsonResponse(Of ColsData) = New JsonResponse(Of ColsData)

        Dim reader1 As New StreamReader(strStaticInputFile)
        Dim assets As String = reader1.ReadToEnd()
        reader1.Close()
        Dim o As JObject = JObject.Parse(assets)

        GetJsonColResponseFromFileBuffer.IsSuccess = Convert.ToBoolean(o("IsSuccess"))
        GetJsonColResponseFromFileBuffer.Message = Convert.ToString(o("Message"))
        GetJsonColResponseFromFileBuffer.RC = Convert.ToUInt32(o("RC"))
        GetJsonColResponseFromFileBuffer.TotalRecordCount = Convert.ToUInt32(o("TotalRecordCount"))


        'Dim arr As JArray = o.SelectToken("Rows")

        'Dim strBuffer As String = ""
        'For Each tok As JToken In arr
        '    If strBuffer <> "" Then strBuffer = strBuffer + ","
        '    strBuffer = strBuffer + tok.ToString
        'Next
        Dim jToken As JToken

        jToken = o("SingleResult")

        GetJsonColResponseFromFileBuffer = JsonConvert.DeserializeObject(jToken)(assets)


        GetJsonColResponseFromFile = GetJsonColResponseFromFileBuffer
    End Function

    Public Function ConvertToInteger(ByVal input As String) As Integer
        Dim output As Integer = 0

        Integer.TryParse(input, output)

        Return output
    End Function

    Private Sub WriteFDToJson()
        If m_strInputFile <> "" Then
            File.WriteAllText(m_strInputFile, JsonConvert.SerializeObject(fd))
        End If
    End Sub

    Private Sub WriteFDToJson_SaveAs()
        Dim strFilename As String = vbEmpty
        dlgSaveFile.DefaultExt = "json"
        dlgSaveFile.Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
        dlgSaveFile.InitialDirectory = "c:\temp"
        dlgSaveFile.FileName = m_strInputFile
        dlgSaveFile.ShowDialog()
        strFilename = dlgSaveFile.FileName
        If strFilename <> "" Then
            File.WriteAllText(strFilename, JsonConvert.SerializeObject(fd))
        End If
    End Sub

    Private Sub WriteToJsonFile(ColumnData As JsonResponse(Of ColsData))
        Dim strFilename As String = vbEmpty
        dlgSaveFile.DefaultExt = "json"
        dlgSaveFile.Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
        dlgSaveFile.InitialDirectory = "c:\temp"
        dlgSaveFile.FileName = m_strInputFile
        dlgSaveFile.ShowDialog()
        strFilename = dlgSaveFile.FileName
        If strFilename <> "" Then
            File.WriteAllText(strFilename, JsonConvert.SerializeObject(ColumnData))
        End If
    End Sub


    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs)
        opdlg.DefaultExt = "json"
        opdlg.Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
        opdlg.InitialDirectory = "c:\temp"
        opdlg.FileName = ""

        opdlg.ShowDialog()
        m_strInputFile = opdlg.FileName
        If (m_strInputFile <> "") Then ReadJsonFile()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        WriteFDToJson()
    End Sub


    Public Function GetSummaryData(ByVal filterData As FilterData) As JsonResponse(Of ColsData)

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
                Return response
            End If

            If mOrgFilterList = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid OrgFilterList"
                Return response
            End If

            If filterData.Col Is Nothing Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Customers"
                Return response
            End If

            If filterData.Col.CustomerCamparatorName = String.Empty Or filterData.Col.CustomerCamparatorValue = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Customers"
                Return response
            End If

            If filterData.Col.SubDivisionCamparatorName = String.Empty Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Sub Division"
                Return response
            End If

            If filterData.Rows.Count = 0 Then
                response.IsSuccess = False
                response.RC = 16
                response.Message = "Invalid Rows"
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
            doGetDbCfgNoCC(RC, ErrorMsg)

            If RC <> 0 Then
                response.IsSuccess = False
                response.RC = RC
                response.Message = ErrorMsg
                Return response
            End If

            'Get summary counts
            doSql(RC, ErrorMsg)


            'If RC <> 0 Then
            '    response.IsSuccess = False
            '    response.RC = RC
            '    response.Message = ErrorMsg
            '    Return response
            'End If

            'Select Case mOrgType
            '    Case OrgType.OrgTypeMain
            '        ResponseData = doSumMain(filterData)
            '        response.IsSuccess = True
            '        response.RC = 0
            '        response.SingleResult = ResponseData
            '    Case Else
            'End Select

            'If RC <> 0 Then
            '    response.IsSuccess = False
            '    response.RC = RC
            '    response.Message = ErrorMsg
            '    Return response
            'End If


        Catch ex As Exception
            response.IsSuccess = False
            response.RC = 5
            response.Message = "GetSummaryData Exception:" & ex.Message
            WriteEventLog(ErrorMsg)
            Return response
        End Try

        Return response
    End Function


    Private Sub doSql(ByRef RC As Integer, ByRef ErrorMsg As String)
        Dim sql As String = String.Empty

        Try
            If m_strSQLInputFile = "" Then



                'Build SELECT base
                sql = " select "
                sql &=
                    "dispatch_number, customer_number, query_customer_number, customer_state, employee_number, call_type" &
                    vbCrLf & ",entitlement, stop_code, stop_code_indicator, mfg_serial_number, mfg_model_number, system_number" &
                    vbCrLf & ",call_received_date, projected_arrival_date, call_last_modified_date, call_status, timezone" &
                    vbCrLf & ",actiongrp, stop_code_upd_dt, territory, srs_sched, ready_date, cust_sub_div" &
                    vbCrLf & ",branch_rdb, region_rdb, area_rdb, district_rdb, VendorCode, csr_territory, customer_country, employee_class " &
                    vbCrLf & ",sched_window_end_dt, call_closed_date, email_addr, (select count(*) from dbo.parts parts with (nolock) where calls.Dispatch_Number = parts.Dispatch_Number) as PartCount" &
                    vbCrLf & ",case when exists (select 'x' from oneview.dbo.labor lbr WITH (nolock) " &
                                                           "where lbr.Dispatch_Number = calls.Dispatch_Number And lbr.stop_code In ('21','23','05','17','26','ONSITE') " &
                                                           "And CAST(getdate()+(calls.timezone/24) As date) = CAST(lbr.end_datetime As date)) " &
                                              "then 'y' else null end AS HasCC_Today"
                sql &= vbCrLf & ",(select count(*) from dbo.labor lbr with (nolock) where calls.Dispatch_Number = lbr.Dispatch_Number and cast(calls.[ready_date] AS date) <= cast(lbr.start_datetime as date) and lbr.stop_code In ('02','04','05')) as NumDeferSinceRdy "
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

                sql &= vbCrLf & "from dbo.vw_csr_org_calls as calls with (NOLOCK) "
            Else
                sql = ReadSQLFile(m_strSQLInputFile)
            End If

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

            rtxbSQLCMD.Text = ""
            lblSQLResultsTitle.Text = GetRowDescription(m_nRowAPIID)
            If (m_blnCustomSQL = True) Then

                Select Case (m_nRowAPIID)

                    Case RowTypes.JustGo, RowTypes.EtaB4TodayNoPU, RowTypes.NoCC, RowTypes.TwoManAssignNoTB, RowTypes.PendingAppointment, RowTypes.DplyNBD,
                         RowTypes.CallsInTB, RowTypes.PendingCustomerETA, RowTypes.MultipleVisits, RowTypes.NewCAD, RowTypes.NewBNN, RowTypes.BacklogNoExcp,
                         RowTypes.ThreeDeferSinceRdy, RowTypes.Escalations, RowTypes.Backlog, RowTypes.Gt4DaysRdy, RowTypes.EmailReady2Send, RowTypes.PendingSchedule

                        sql = "select * into #temp FROM ( " + vbCrLf + sql
                        sql &= vbCrLf & m_strRowSQLCMD
                        Exit Select
                    Case RowTypes.Pod

                        sql = Count_POD(fd, 0)

                        Exit Select
                    Case RowTypes.Pod2

                        sql = Count_POD(fd, 4)

                        Exit Select

                    Case Else
                        sql &= vbCrLf & m_strRowSQLCMD

                End Select


            End If

            rtxbSQLCMD.Text = sql
            rtxbSQLCMD_ColorText(rtxbSQLCMD, sql)
            rtxbSQLCMD_ColorText(rtxbSQLCMD, sql)

            'Execute SELECT
            CallResults = GetCallResult(sql)
            SaveSQLLogGenerated(sql)
        Catch ex As Exception
            RC = 5
            ErrorMsg = "doSql Exception: " & Len(sql).ToString & vbCrLf & ex.Message & vbCrLf & vbCrLf & sql
            WriteEventLog(ErrorMsg)
        End Try
    End Sub

    Private Sub doGetDbCfgNoCC(ByRef RC As Integer, ByRef ErrorMsg As String)

        Dim strKey1Value As String = "MainCnt."
        Dim strKey2Value As String = ""
        Dim DfltTypes As String

        Try

            Dim sql As String
            Dim sz As String

            ' Get the DPLY Call Types
            strKey2Value = "WOTypes.DPLY"
            DfltTypes = "dply,2Dpl,3Dpl,4Dpl,5Dpl,ndpl,sdpl,ndpb,fdpl,cdpl,pshe"
            sql = String.Empty
            sql &= "Declare @szValue1 varchar(255), @szValue2 varchar(255), @szValue3 varchar(255);"
            sql &= vbCrLf & "Select @szValue1 = [Value1], @szValue2=[Value2], @szValue3=[Value3] FROM cat.dbo.DashControl With (nolock) WHERE (Key1='Dashboard') And (Key2='@strKey2Value');"
            sql &= vbCrLf & "IF (@szValue1 is null) And (@szValue2 is null) And (@szValue3 is null) select @szValue1 = '@DfltTypes', @szValue2 = '', @szValue3 = '';"
            sql &= vbCrLf & "select ISNULL(@szValue1,'') + ISNULL(@szValue2,'') + ISNULL(@szValue3,'');"

            sql = sql.Replace("@strKey2Value", strKey2Value)
            sql = sql.Replace("@DfltTypes", DfltTypes)

            sz = GetScalarValue(sql)
            SaveSQLLogGenerated("doGetDbCfgNoCC", sql)
            g_astrCallTypesDPL = sz.ToLower.Split(",")

        Catch ex As Exception
            RC = 5
            ErrorMsg = "doGetDbCfgNoCC Exception:" & ex.Message
            WriteEventLog(ErrorMsg)
        End Try

    End Sub
    Private Function GetScalarValue(sql As String) As String
        Dim strConnStr As String = ""
        Dim strConnectInfo() As String

        If sql = "" Then Exit Function


        strConnStr = ConfigurationManager.ConnectionStrings("OneView_DB_Connection").ConnectionString
        If strConnStr = "" Then Exit Function


        strConnectInfo = strConnStr.Split(";")
        drpDataSource.Text = strConnectInfo(0)
        Dim oSQLHelper As SQLHelper = New SQLHelper(strConnStr)

        GetScalarValue = oSQLHelper.ExecuteScalar(sql)


    End Function
    Private Function GetCallResult(sql) As List(Of CallResult)

        Dim dt As DataTable = New DataTable
        Dim dtSort As DataTable = New DataTable
        dgSQL.DataSource = vbNull
        dt = GetSQLData(sql)
        If IsNothing(dt) Then Exit Function
        If cmbDataCoLumns.Text <> "" Then

            If drpInequalities.Text = "" Then


                If txbFilterValue.Text = "" Then
                    dtSort = dt.Select("", cmbDataCoLumns.Text).CopyToDataTable()
                    dgSQL.DataSource = dtSort
                Else
                    dtSort = dt.Select(cmbDataCoLumns.Text + " = '" + txbFilterValue.Text + "'").CopyToDataTable()
                End If

            Else

                If txbFilterValue.Text = "" Then
                    dtSort = dt.Select("", cmbDataCoLumns.Text).CopyToDataTable()
                    dgSQL.DataSource = dtSort
                Else
                    dtSort = dt.Select(cmbDataCoLumns.Text + " " + drpInequalities.Text + "  '" + txbFilterValue.Text + "'").CopyToDataTable()
                End If

            End If


            dgSQL.DataSource = dtSort
            lblCount.Visible = True
            lblCount.Text = "Count = " + dtSort.Rows.Count.ToString
        Else
            dgSQL.DataSource = dt
            lblCount.Visible = True
            lblCount.Text = "Count = " + dt.Rows.Count.ToString
        End If
        cmbDataCoLumns.Items.Clear()

        For Each dCol As DataColumn In dt.Columns
            cmbDataCoLumns.Items.Add(dCol.ColumnName)
        Next



        GetCallResult = CallResults
    End Function


    Public Class FilterData

        Public Property UserId() As String

        Public Property OrgType() As String

        Public Property OrgFilterType() As String

        Public Property OrgFilterList() As String

        Public Property OrgBranchList() As String

        Public Property CustAssoc() As String

        Public Property VendorCode() As String

        Public Property Col() As Col

        Public Property Rows() As List(Of Integer)

        Public Property IsCallDetail() As Boolean

        Public Property UserPermission() As UserPermission

        Public Function Clone() As FilterData
            Return DirectCast(Me.MemberwiseClone(), FilterData)
        End Function

    End Class
    Public Class FilterDetailData

        Public Property UserId() As String

        Public Property OrgType() As String

        Public Property OrgFilterType() As String

        Public Property OrgFilterList() As String

        Public Property OrgBranchList() As String

        Public Property CustAssoc() As String

        Public Property VendorCode() As String

        Public Property Cols() As List(Of Col)

        Public Property Rows() As List(Of Integer)

        Public Property IsCallDetail() As Boolean

        Public Property UserPermission() As UserPermission

        Public Function Clone() As FilterData
            Return DirectCast(Me.MemberwiseClone(), FilterData)
        End Function

    End Class


    Public Class Col

        Public Property CustomerCamparatorName() As String
        Public Property CustomerCamparatorValue() As String
        Public Property SubDivisionCamparatorName() As String
        Public Property SubDivisionCamparatorValue() As String
        Public Property WorkOrderType() As String
    End Class
    Public Class UserPermission


        Public Property DashVendor() As Boolean
        Public Property DashVendorFilterOff() As Boolean
        Public Property DashCustomerFilterOff() As Boolean

    End Class
    Private Function isInStrList(ByVal sz As String, ByVal aList() As String) As Boolean
        Dim i As Integer
        For i = 0 To aList.GetUpperBound(0)
            If aList(i) = sz Then Return True
        Next
        Return False
    End Function
    Public Function Count_POD(
                             ByVal filterData As testlocal.FilterData,
                            Optional ByVal p_BusDaysAge As Integer = 0
                            ) As String

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
        strSQL =
            "SELECT rid As RowId " &
            vbCrLf & "FROM oneview.dbo.vw_OpenWaybills " &
            vbCrLf & "WHERE " & strWhere & ";"

        Return strSQL

    End Function

    Private Sub btnTestSQL_Click(sender As Object, e As EventArgs) Handles btnTestSQL.Click
        'If cmbDataCoLumns.Text <> "" Then
        Dim dt As DataTable = New DataTable
        Dim dtSort As DataTable = New DataTable
        If IsNothing(dgSQL.DataSource) Then
            MsgBox("No DataSource Availlable")
            Exit Sub
        End If



        dt = dgSQL.DataSource.DefaultView.Table




        If IsNothing(dt) Then Exit Sub
        If cmbDataCoLumns.Text <> "" Then

            If drpInequalities.Text = "" Then


                If txbFilterValue.Text = "" Then
                    dtSort = dt.Select("", cmbDataCoLumns.Text).CopyToDataTable()
                    dgSQL.DataSource = dtSort
                Else
                    dtSort = dt.Select(cmbDataCoLumns.Text + " = '" + txbFilterValue.Text + "'").CopyToDataTable()
                End If

            Else

                If txbFilterValue.Text = "" Then
                    dtSort = dt.Select("", cmbDataCoLumns.Text).CopyToDataTable()
                    dgSQL.DataSource = dtSort
                Else
                    dtSort = dt.Select(cmbDataCoLumns.Text + " " + drpInequalities.Text + "  '" + txbFilterValue.Text + "'").CopyToDataTable()
                End If

            End If


            dgSQL.DataSource = dtSort
            lblCount.Visible = True
            lblCount.Text = "Count = " + dtSort.Rows.Count.ToString
        Else
            dgSQL.DataSource = dt
            lblCount.Visible = True
            lblCount.Text = "Count = " + dt.Rows.Count.ToString
        End If

    End Sub
    Private Sub PopulateGrid()
        Dim fd As FilterData
        fd = Build_fd()
        GetSummaryData(fd)
        TabControl1.SelectTab(2)
        btnSQLCmd.Enabled = True
        m_blnCustomSQL = False
        m_strRowSQLCMD = String.Empty

    End Sub
    Private Function Build_fd() As FilterData
        Dim fd As New FilterData



        fd.Col = New Col


        fd.Col.CustomerCamparatorName = drpCustomerCamparatorName.Text

        fd.Col.CustomerCamparatorValue = drpCustomerCamparatorValue.Text

        fd.Col.SubDivisionCamparatorName = drpSubDivisionCamparatorName.Text
        fd.Col.SubDivisionCamparatorValue = drpSubDivisionCamparatorValue.Text
        fd.Col.WorkOrderType = drpWorkOrderType.Text



        fd.IsCallDetail = chkIsCallDetail.CheckState
        fd.OrgBranchList = txbOrgBranchList.Text

        fd.OrgFilterList = txbOrgFilterList.Text
        fd.OrgFilterType = drpOrgFilterType.Text
        fd.OrgType = drpOrgType.Text

        Dim strARows() As String = txbRows.Text.Split(",")

        Dim strTemp = strARows.ToList()
        fd.Rows = New List(Of Integer)

        If (strARows.Count > 0) Then



            For Each strBuffer As String In strARows
                fd.Rows.Add(Convert.ToInt32(strBuffer))
            Next

        Else
            MsgBox("No Row Data")
            Return fd
        End If




        '   fd.Rows = Array.ConvertAll(strARows, New Converter(Of String, Integer)(AddressOf ConvertToInteger))


        ' fd.UserId = "tmw63818"
        fd.UserId = txbUserID.Text
        fd.VendorCode = txbVendorCode.Text
        fd.CustAssoc = txbCustAssoc.Text
        fd.UserPermission = New UserPermission

        fd.UserPermission.DashVendor = chkDashVendor.CheckState
        fd.UserPermission.DashVendorFilterOff = chkDashCustomerFilterOff.CheckState
        fd.UserPermission.DashCustomerFilterOff = chkDashVendorFilterOff.CheckState

        Build_fd = fd

    End Function
    Private Function GetWayBillResult(strSQL) As IList(Of WayBillResult)

    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblCount.Text = ""
        lblCount.Visible = False

        btnTestGetDataSummary.Enabled = False
        btnTestSQL.Enabled = False
        btnSQLCmd.Enabled = False



    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        drpInequalities.Text = ""
        cmbDataCoLumns.Text = ""
        txbFilterValue.Text = ""

        opdlg.DefaultExt = "json"
        opdlg.Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
        opdlg.InitialDirectory = "c:\temp"
        opdlg.FileName = ""

        opdlg.ShowDialog()
        m_strInputFile = opdlg.FileName
        If (m_strInputFile <> "") Then ReadJsonFile()

        btnTestGetDataSummary.Enabled = True
        btnTestSQL.Enabled = True
        If (Not m_blnDetailAPICall) Then
            TabControl1.SelectedIndex = 0
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        WriteFDToJson()
    End Sub

    Private Sub OpenSQLScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenSQLScriptToolStripMenuItem.Click
        opdlg.DefaultExt = "sql"
        opdlg.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*"
        opdlg.InitialDirectory = "c:\temp"
        opdlg.FileName = ""

        opdlg.ShowDialog()
        m_strSQLInputFile = opdlg.FileName
        lblScriptFileName.Text = opdlg.SafeFileName
        btnSQLCmd.Enabled = True
        If System.IO.File.Exists(m_strSQLInputFile) = True Then
            cmbDataCoLumns.ResetText()
            drpInequalities.ResetText()
            txbFilterValue.ResetText()
            btnTestSQL_Click(vbNull, EventArgs.Empty)
            lblScriptFileName.Text = opdlg.SafeFileName
            btnSQLCmd.Enabled = True

        End If
    End Sub
    Private Function ReadSQLFile(strSQLFIle As String) As String
        ReadSQLFile = ""
        If strSQLFIle = "" Then

            MsgBox("FileName Not Available")
            Exit Function
        End If
        Dim reader1 As New StreamReader(strSQLFIle)
        Dim sqlSQLCmd As String = reader1.ReadToEnd()
        reader1.Close()
        If sqlSQLCmd.Length = 0 Then
            MsgBox("Empty File")
            Exit Function
        End If
        ReadSQLFile = sqlSQLCmd

    End Function
    Private Function ViewSQLCmdFile(strFileName) As Boolean

        ViewSQLCmdFile = False
        If System.IO.File.Exists(strFileName) = True Then
            Process.Start(strFileName)
        Else
            MsgBox("Base SQL Command Executed - No Custom File Selected")
            Exit Function
        End If

        ViewSQLCmdFile = True
    End Function

    Private Sub btnSQLCmd_Click(sender As Object, e As EventArgs) Handles btnSQLCmd.Click
        ViewSQLCmdFile(m_strSQLInputFile)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportDataView()
    End Sub

    Private Sub ExportDataView()
        'This way increases performance without casting
        Dim filePath As String = "c:\temp\OverrideViewer.csv"
        Dim delimeter As String = ","
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 0 To dgSQL.Rows.Count - 1
            Dim array As String() = New String(dgSQL.Columns.Count - 1) {}
            If i.Equals(0) Then
                For j As Integer = 0 To dgSQL.Columns.Count - 1
                    array(j) = dgSQL.Columns(j).HeaderText
                Next
                sb.AppendLine(String.Join(delimeter, array))
            End If
            For j As Integer = 0 To dgSQL.Columns.Count - 1
                If Not dgSQL.Rows(i).IsNewRow Then
                    array(j) = dgSQL(j, i).Value.ToString
                End If
            Next
            If Not dgSQL.Rows(i).IsNewRow Then
                sb.AppendLine(String.Join(delimeter, array))
            End If
        Next


        File.WriteAllText(filePath, sb.ToString)
        'Opens the file immediately after writing
        Process.Start(filePath)
    End Sub
    Private Sub SaveSQLLogGenerated(strSQLCmd As String)
        Try
            Dim filePath As String = "c:\temp\APISQLStatement.sql"
            File.WriteAllText(filePath, strSQLCmd)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub SaveSQLLogGenerated(strFileName As String, strSQLCmd As String)
        Try
            Dim filePath As String = "c:\temp\" + strFileName + ".sql"
            File.WriteAllText(filePath, strSQLCmd)
        Catch ex As Exception

        End Try

    End Sub




    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        WriteFDToJson_SaveAs()
    End Sub

    Private Sub dgView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellDoubleClick

        Dim row As DataGridViewRow = dgView.Rows(e.RowIndex)
        '    MessageBox.Show("RowID =" + GetRowDescription(row.Cells(0).Value))

        m_nRowAPIID = row.Cells(0).Value
        m_blnCustomSQL = True
        PopulateGrid()
        cmbDataCoLumns.Text = ""

        drpInequalities.Text = ""
        txbFilterValue.Text = ""
    End Sub

    Private Sub dgView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellContentClick

    End Sub

    Private Sub coloringRTB(rtb As RichTextBox, index As Integer, length As Integer, color As Color)
        Dim selectionStartSave As Integer = rtb.SelectionStart 'to return this back to its original position
        rtb.SelectionStart = index
        rtb.SelectionLength = length
        '    rtb.SelectedText = rtb.SelectedText.ToUpper()
        Dim currentFont As System.Drawing.Font = rtb.SelectionFont
        Dim currentFontSave As System.Drawing.Font = rtb.SelectionFont
        Dim newFontStyle As System.Drawing.FontStyle
        newFontStyle = currentFont.Style + Drawing.FontStyle.Bold + Drawing.FontStyle.Regular
        rtb.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle)
        rtb.SelectionColor = color
        rtb.SelectionLength = 0
        rtb.SelectionStart = selectionStartSave
        rtb.SelectionColor = rtb.ForeColor 'return back to the original color
        rtb.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, currentFont.Style)
    End Sub

    Private Sub rtxbSQLCMD_ColorText(rTbox As RichTextBox, strSQLCMD As String)
        If strSQLCMD = vbNullString Then Exit Sub
        '   rTbox.Text = strSQLCMD
        Dim words As IEnumerable(Of String) = rTbox.Text.Split(New Char() {" ", "(", ")"})
        Dim index As Integer = 0
        Dim rtb As RichTextBox = rTbox 'to give normal color according to the base fore color
        For Each word As String In words
            'If the list contains the word, then color it specially. Else, color it normally
            'Edit: Trim() is added such that it may guarantee the empty space after word does not cause error
            coloringRTB(rTbox, index, word.Length, If(KeyWords.Contains(word.ToUpper().Trim()), KeyWordsColors(KeyWords.IndexOf(word.ToUpper().Trim())), rtb.ForeColor))
            index = index + word.Length + 1 '1 is for the whitespace, though Trimmed, original word.Length is still used to advance
        Next
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub chkIsCallDetail_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsCallDetail.CheckedChanged

    End Sub

    Private Sub chkDashVendor_CheckedChanged(sender As Object, e As EventArgs) Handles chkDashVendor.CheckedChanged

    End Sub

    Private Sub chkDashVendorFilterOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkDashVendorFilterOff.CheckedChanged

    End Sub

    Private Sub chkDashCustomerFilterOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkDashCustomerFilterOff.CheckedChanged

    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txbDetailAPICall_Click(sender As Object, e As EventArgs) Handles txbDetailAPICall.Click

        Dim dtElaspsedTime As System.DateTime


        Dim ws As New testlocal.DashSumm2ServiceClient

        ' Dim ws As New testDashUmm2Service.DashSumm2ServiceClient
        Dim local_filterdetail = New testlocal.FilterDetailData

        local_filterdetail = filterdetail


        Dim colResp As JsonResponse(Of ColsData)
        Me.Cursor = Cursors.WaitCursor
        dtElaspsedTime = System.DateTime.Now
        colResp = ws.GetSummaryDetailData(local_filterdetail)
        lblElaspsedTime.Text = "Elapsed Time = " + Now.Subtract(dtElaspsedTime).TotalMilliseconds.ToString()
        '    txbColumnCount = colResp.Results.
        Dim oRow As RowData
        If (colResp.IsSuccess) Then

            If (chkSaveAPIResponse.Checked) Then
                WriteToJsonFile(colResp)
            End If

            For Each oRow In colResp.SingleResult.Rows

                txbColumnCount.Text = oRow.CallCount
            Next

            Debug.Print("Success")
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub txbDetailAPICall_EnabledChanged(sender As Object, e As EventArgs) Handles txbDetailAPICall.EnabledChanged

    End Sub

    Private Sub btnTestImport_Click(sender As Object, e As EventArgs) Handles btnTestImport.Click
        Timer1.Enabled = True

        m_oTest.ImportCSVFile()
        m_dtEplasedtime = Now.ToLocalTime
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        txbAyncCount.Text = m_oTest.GetRecordRecounts.ToString()
    End Sub

    Private Sub txbAyncCount_TextChanged(sender As Object, e As EventArgs) Handles txbAyncCount.TextChanged
        lblElaspsedTime.Text = "EplasedTime: =" + Now.Subtract(m_dtEplasedtime).TotalMilliseconds.ToString()
    End Sub

    Private Sub dgSQL_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSQL.CellContentClick

    End Sub
End Class



