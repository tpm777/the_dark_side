﻿Imports WWTS.Aspects.Utils
Imports WWTS.Data.Model
Imports WWTS.Aspects.Enums

Partial Public Class CoreService
    Dim WOsWithDateIssues As String

    Public Function doSumMain(ByVal filterData As FilterData) As ColsData


        Dim PercentRows As Integer() = New Integer(2) {RowTypes.EtsCallComplete, RowTypes.DeferredPer04, RowTypes.DeferredPer05}
        Dim CustomerData As New ColsData()
        Dim Rows As New List(Of RowData)
        Dim VendorUser As Boolean = False
        Dim strRowIDs As String = ""

        VendorUser = IIf((filterData.VendorCode <> String.Empty Or filterData.UserPermission.DashVendor), True, False)


        CallResults.ForEach(Function(c)
                                c.call_status = If([String].IsNullOrEmpty(c.call_status), "", c.call_status.ToLower)
                                c.dispatch_number = If([String].IsNullOrEmpty(c.dispatch_number), "", c.dispatch_number.ToLower)
                                c.customer_number = If([String].IsNullOrEmpty(c.customer_number), "", c.customer_number.ToLower)
                                c.query_customer_number = If([String].IsNullOrEmpty(c.query_customer_number), "", c.query_customer_number.ToLower)
                                c.employee_number = If([String].IsNullOrEmpty(c.employee_number), "", c.employee_number.ToLower)
                                c.call_type = If([String].IsNullOrEmpty(c.call_type), "", c.call_type.ToLower)
                                c.stop_code = If([String].IsNullOrEmpty(c.stop_code), "", c.stop_code.ToLower)
                                c.stop_code_indicator = If([String].IsNullOrEmpty(c.stop_code_indicator), "", c.stop_code_indicator.ToLower)

                                ' SAAS-1017 '
                                Dim strDate As String = "01/01/1901"
                                c.call_received_date = If(c.call_received_date.HasValue, c.call_received_date, Convert.ToDateTime(strDate))
                                c.call_last_modified_date = If(c.call_last_modified_date.HasValue, c.call_last_modified_date, Convert.ToDateTime(strDate))
                                If c.call_status.StartsWith("c") = True Then
                                    If Not c.call_closed_date.HasValue Then
                                        WOsWithIssuesBuildMsg(c.dispatch_number, "Closed Date ")
                                        AddRowIds("Closed", filterData)
                                    End If
                                    c.call_closed_date = If(c.call_closed_date.HasValue, c.call_closed_date, Convert.ToDateTime(strDate))

                                End If

                                'If Not c.projected_arrival_date.HasValue Then
                                '    WOsWithIssuesBuildMsg(c.dispatch_number, "Projected Arrival Date ")
                                '    AddRowIds("Projected", filterData)
                                'End If
                                c.projected_arrival_date = If(c.projected_arrival_date.HasValue, c.projected_arrival_date, Convert.ToDateTime(strDate))


                                Try
                                    c.TwoManAssign = If([String].IsNullOrEmpty(c.TwoManAssign), "", c.TwoManAssign.ToLower)
                                Catch ex As Exception

                                End Try
                                c.HasCC = If([String].IsNullOrEmpty(c.HasCC), "", c.HasCC.ToLower)
                                c.HasCC_Today = If([String].IsNullOrEmpty(c.HasCC_Today), "", c.HasCC_Today.ToLower)
                                c.actiongrp = If([String].IsNullOrEmpty(c.actiongrp), "", c.actiongrp.ToLower)
                                c.srs_sched = If([String].IsNullOrEmpty(c.srs_sched), "", c.srs_sched.ToLower)
                                Try
                                    c.PendingAppt1 = If([String].IsNullOrEmpty(c.PendingAppt1), "", c.PendingAppt1.ToLower)

                                Catch ex As Exception

                                End Try
                                Try
                                    c.PendingAppt2 = If([String].IsNullOrEmpty(c.PendingAppt2), "", c.PendingAppt2.ToLower)
                                Catch ex As Exception

                                End Try

                                c.system_number = If([String].IsNullOrEmpty(c.system_number), "", c.system_number.ToLower)
                                Try
                                    c.Lbr01 = If([String].IsNullOrEmpty(c.Lbr01), "", c.Lbr01.ToLower)
                                Catch ex As Exception
                                End Try

                                Try
                                    c.Lbr02 = If([String].IsNullOrEmpty(c.Lbr02), "", c.Lbr02.ToLower)
                                Catch ex As Exception
                                End Try
                                Try
                                    c.Lbr03 = If([String].IsNullOrEmpty(c.Lbr03), "", c.Lbr03.ToLower)
                                Catch ex As Exception

                                End Try
                                Try
                                    c.Lbr04 = If([String].IsNullOrEmpty(c.Lbr04), "", c.Lbr04.ToLower)
                                Catch ex As Exception

                                End Try
                                c.entitlement = If([String].IsNullOrEmpty(c.entitlement), "", c.entitlement.ToLower)
                                c.Escalations = If([String].IsNullOrEmpty(c.Escalations), "", c.Escalations.ToLower)
                                '       c.srs_sched = If([String].IsNullOrEmpty(c.srs_sched), "", c.srs_sched.ToLower)
                            End Function)

        Dim CustomerCallResults As List(Of CallResult) = CallResults

        ' If the user vendor code is not blank, make sure the vendor code matches the assigned employee
        If (VendorUser) Then

            If (mVendorCode <> String.Empty AndAlso (Not filterData.UserPermission.DashVendorFilterOff)) Then
                CustomerCallResults = CustomerCallResults.FindAll(Function(c) (mVendorCode.ToLower = c.vendorcode.ToLower))

            End If
            If (mCustAssoc <> String.Empty AndAlso (Not filterData.UserPermission.DashCustomerFilterOff)) Then


                CustomerCallResults = CustomerCallResults.FindAll(Function(c) (mSrchCustAssoc.Contains("," & c.query_customer_number.ToLower & ",") _
                                            Or mSrchCustAssoc.Contains("," & c.query_customer_number.ToLower & "|" & c.cust_sub_div.ToLower & ",")))
                'CustomerCallResults = CustomerCallResults.FindAll(Function(c) (mSrchCustAssoc.Contains("," & c.query_customer_number.ToLower & "|" & c.cust_sub_div.ToLower & ",")))
            End If
            If ((mVendorCode = String.Empty) Or (mCustAssoc = String.Empty)) Then
                CustomerCallResults = New List(Of CallResult)()
            End If
        End If




        'If (mVendorCode <> String.Empty) And (mVendorCode <> vendorcode) Then
        '    If (mVendorOverride = False) Then
        '        Continue For
        '    ElseIf (mVendorOverride = True) And (employee_class <> "o") Then
        '        Continue For
        '    End If
        'End If

        'Check customer association
        'If (mCustAssoc <> String.Empty) _
        '    AndAlso (mSrchCustAssoc.Contains("," & query_customer_number & ",") = False) _
        '    AndAlso (mSrchCustAssoc.Contains("," & query_customer_number & "|" & cust_sub_div & ",") = False) _
        'Then
        '    Continue For
        'End If

        filterData.Rows _
            .FindAll(Function(r) Not PercentRows.Any(Function(p) p = r)) _
            .ForEach(Function(R)
                         Dim RowData As New RowData()
                         RowData.Id = R


                         Select Case R

                             'Calls due today or prior (	ETA <= today)

                             Case RowTypes.EtaB4Today             'Calls

                                 Dim etaB4Today = CustomerCallResults.FindAll(Function(c) ((c.call_status.StartsWith("c") = False) OrElse (c.call_status.StartsWith("c") = True And TruncDate(c.call_closed_date) = (TruncDate(TZShift(Now(), c.timezone))))) _
                                                                                  AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))
                                 RowData.CallCount = etaB4Today.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4Today.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayOpen        'Calls Still open (No BO)

                                 Dim etaB4TodayOpen = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") _
                                                                                 AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayOpen.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayOpen.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayCL          'Calls Closed/cancelled

                                 Dim etaB4TodayCL = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = True) _
                                                                                    AndAlso (TruncDate(c.call_closed_date) = (TruncDate(TZShift(Now(), c.timezone)))) _
                                                                                    AndAlso (TruncDate(c.projected_arrival_date) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayCL.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayCL.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayTB          '“T” Buckets (No BO)

                                 Dim etaB4TodayTB = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) _
                                                                                 AndAlso (c.employee_number.StartsWith("t") = True) AndAlso (c.call_status <> "bo") _
                                                                                 AndAlso (c.stop_code_indicator <> "p13") _
                                                                                 AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayTB.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayTB.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayAS          'As status Calls

                                 Dim etaB4TodayAS = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status = "as") _
                                                                                AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayAS.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayAS.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayBO          'BO status calls

                                 Dim etaB4TodayBO = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status = "bo") _
                                                                                 AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayBO.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayBO.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.JustGo                'Just-Go Calls

                                 Dim justGo = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.StopCodeSrchList.Contains(",17,")) _
                                                                                 AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = justGo.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = justGo.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayNoPU        'Parts available, not picked up

                                 Dim etaB4TodayNoPU = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") AndAlso (c.StopCodeSrchList.Contains(",pu,") = False) _
                                                                                 AndAlso (c.PartCount > 0) AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))
                                 RowData.CallCount = etaB4TodayNoPU.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayNoPU.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.NoCC                  'No contact by 10:00

                                 Dim noCC = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.query_customer_number.StartsWith("wwts") = False) AndAlso (c.query_customer_number.StartsWith("qxs") = False) _
                                                                                 AndAlso (c.call_status <> "bo") AndAlso (c.projected_arrival_date.HasValue = True) _
                                                                                 AndAlso (TruncDate(If(c.projected_arrival_date, (TruncDate(TZShift(Now(), c.timezone))))) = (TruncDate(TZShift(Now(), c.timezone)))) _
                                                                                 AndAlso (TimeValue(TZShift(Now(), c.timezone)) > TimeValue(mNoCcTime)) _
                                                                                 AndAlso (c.query_customer_number.StartsWith("ibm") = False) _
                                                                                 AndAlso (c.HasCC_Today <> "y") _
                                                                                 AndAlso (c.IsScheduledForToday = "n"))


                                 RowData.CallCount = noCC.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = noCC.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayEnroute     'En-Route  

                                 Dim etaB4TodayEnroute = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "26") _
                                                                                AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayEnroute.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayEnroute.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayOS          'Onsite

                                 Dim etaB4TodayOS = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "onsite") _
                                                                                 AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = etaB4TodayOS.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayOS.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaB4TodayOld         'Expired ETA

                                 Dim etaB4TodayOld = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) _
                                                                                 AndAlso (c.stop_code.StartsWith("onsite") = False) _
                                                                                 AndAlso (If(c.projected_arrival_date, TZShift(Now(), c.timezone)) < TZShift(Now(), c.timezone)))

                                 RowData.CallCount = etaB4TodayOld.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaB4TodayOld.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                                 'Escalation Information

                             Case RowTypes.Esc                   'Escalation (ESC 1,2,3)

                                 Dim esc = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (isInStrList(c.stop_code_indicator, {"ets99", "eccinfo"}) = False) _
                                                                                  AndAlso ((isInStrList(c.actiongrp, {"esc0", "esc1", "esc2", "esc3", "smtesc1", "smtesc2", "smtesc3"}) = True) _
                                                                                  OrElse (c.stop_code_indicator.StartsWith("ecc") = True) _
                                                                                  OrElse (c.stop_code_indicator.StartsWith("ets") = True)))

                                 RowData.CallCount = esc.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = esc.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.SC13                  'Stop Code 13

                                 Dim sC13 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "13") AndAlso (c.actiongrp <> "13priority"))

                                 RowData.CallCount = sC13.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = sC13.[Select](Function(s) s.dispatch_number).ToList()
                                 End If




                             Case RowTypes.SCP13                 'Work Orders without parts





                                 Dim sCP13 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) _
                                                AndAlso (c.Work_Orders_without_parts))

                                 RowData.CallCount = sCP13.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = sCP13.[Select](Function(s) s.dispatch_number).ToList()
                                 End If



                             Case RowTypes.SC02                  'Stop Code 02

                                 Dim sC02 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "02"))

                                 RowData.CallCount = sC02.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = sC02.[Select](Function(s) s.dispatch_number).ToList()
                                 End If


                             Case RowTypes.Deferred04              'Deferred (04)

                                 Dim deferred04 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (isInStrList(c.stop_code_indicator, {"04"}) = True))

                                 RowData.CallCount = deferred04.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = deferred04.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Deferred05              'Deferred (05)

                                 Dim deferred05 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (isInStrList(c.stop_code_indicator, {"05"}) = True))

                                 RowData.CallCount = deferred05.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = deferred05.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.ThreeDeferSinceRdy    '3+ defers since ready date

                                 Dim threeDeferSinceRdy = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.NumDeferSinceRdy >= 3) _
                                                                                 AndAlso (isInStrList(c.call_type, g_astrCallTypesDPL) = False))

                                 RowData.CallCount = threeDeferSinceRdy.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = threeDeferSinceRdy.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Gt4DaysRdy            'Part ready>4 days

                                 Dim gt4DaysRdy = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") _
                                                                                 AndAlso (c.PartCount > 0) _
                                                                                 AndAlso (If(TruncDate(c.ready_date) Is Nothing, True, (TruncDate(c.ready_date) < DateAdd(DateInterval.Day, -4, (TruncDate(TZShift(Now(), c.timezone))).Value)))) _
                                                                                 AndAlso (isInStrList(c.call_type, g_astrCallTypesDPL) = False))
                                 RowData.CallCount = gt4DaysRdy.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = gt4DaysRdy.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                                 ' Scheduling Information

                             Case RowTypes.Scheduled             'Scheduled Calls

                                 Dim scheduled = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.srs_sched = "y"))

                                 RowData.CallCount = scheduled.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = scheduled.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.SchedBO               'Scheduled Calls in BO

                                 Dim schedBO = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status = "bo") AndAlso (c.srs_sched = "y"))

                                 RowData.CallCount = schedBO.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = schedBO.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EmailReady2Send       'E-mail ready to send

                                 Dim emailReady2Send = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") AndAlso (c.srs_sched <> "y") _
                                                                                             AndAlso (c.StopCodeSrchList.Contains(",05,") = False) AndAlso (isInStrList(c.stop_code_indicator, {"04", "23", "c1", "c2"}) = False) _
                                                                                             AndAlso (isInStrList(c.call_type, g_astrCallTypesDPL) = False) _
                                                                                             AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= c.EtaNbd) _
                                                                                             AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) > (TruncDate(TZShift(Now(), c.timezone)))) _
                                                                                             AndAlso (c.email_addr.Contains("@") = True))

                                 RowData.CallCount = emailReady2Send.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = emailReady2Send.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EmailPendingC1        'Email contact pending

                                 Dim emailPendingC1 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "c1"))

                                 RowData.CallCount = emailPendingC1.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = emailPendingC1.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.TextPendingC2         'Text contact pending

                                 Dim textPendingC2 = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "c2"))

                                 RowData.CallCount = textPendingC2.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = textPendingC2.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                                 ' General Call Information

                             Case RowTypes.TOC                   'Total Open

                                 Dim tOC = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False))

                                 RowData.CallCount = tOC.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = tOC.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.DPLY                  'Deployment Calls

                                 Dim dPLY = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) _
                                                                                             AndAlso (isInStrList(c.call_type, g_astrCallTypesDPL) = True))

                                 RowData.CallCount = dPLY.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = dPLY.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.TwoManAssignNoTB      '2 Man Assist to be assigned

                                 Dim twoManAssignNoTB = CustomerCallResults.FindAll(Function(c) (c.employee_number.StartsWith("t") = False) AndAlso (c.TwoManAssign = "y") AndAlso (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc"))

                                 RowData.CallCount = twoManAssignNoTB.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = twoManAssignNoTB.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.DplyNBD               'Deployment Calls <= NBD

                                 Dim dplyNBD = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) _
                                                                                             AndAlso (isInStrList(c.call_type, g_astrCallTypesDPL) = True) _
                                                                                             AndAlso (TruncDate(If(c.projected_arrival_date, Date.Today)) <= c.EtaNbd))
                                 RowData.CallCount = dplyNBD.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = dplyNBD.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.NewOpen               'Calls received today

                                 Dim newOpen = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") And (TruncDate(c.call_received_date) >= (TruncDate(TZShift(Now(), c.timezone))).Value))

                                 RowData.CallCount = newOpen.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = newOpen.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.PendingCN             'Cust-Canc still open

                                 Dim pendingCN = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp = "cust-canc"))

                                 RowData.CallCount = pendingCN.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = pendingCN.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Closed                'Closed/canceled

                                 Dim closed = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = True) AndAlso (TruncDate(c.call_closed_date) = (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = closed.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = closed.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Pod                   'Outstanding Way Bills
                                 Dim pod = Count_POD(filterData, 0)
                                 Try

                                     RowData.CallCount = pod.Count
                                     If (filterData.IsCallDetail) Then
                                         RowData.CallIds = pod.[Select](Function(s) s.RowId.ToString).ToList()
                                     End If
                                 Catch ex As Exception
                                     If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR (Count_POD) !!!!!!!!!: " & ex.Message)
                                 End Try

                             Case RowTypes.Pod2                  'Outstanding Way Bills (4+ BO)
                                 Dim pod2 = Count_POD(filterData, 4)
                                 Try

                                     RowData.CallCount = pod2.Count
                                     If (filterData.IsCallDetail) Then
                                         RowData.CallIds = pod2.[Select](Function(s) s.RowId.ToString).ToList()
                                     End If
                                 Catch ex As Exception
                                     If (m_blnLoggingEnable) Then LogData(" !!!!!!!! ERROR (Count_POD) !!!!!!!!!: " & ex.Message)

                                 End Try

                                 'Running

                             Case RowTypes.RunningToday          'Calls Running today

                                 Dim runningToday = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") AndAlso (c.employee_number.StartsWith("t") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) <= (TruncDate(TZShift(Now(), c.timezone))))

                                 RowData.CallCount = runningToday.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = runningToday.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Escalations           'Escalations

                                 Dim escalations = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso (c.Escalations = "y"))

                                 RowData.CallCount = escalations.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = escalations.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.RunningSbdCalls       'Same Day Calls

                                 Dim runningSbdCalls = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso isInStrList(c.call_type, {"02hr", "03hr", "04hr", "05hr", "06hr", "08hr", "10hr", "emc1", "emc2", "emc3", "emc4", "emcy", "sdpr", "sev1", "sev2", "sev3", "spec", "temp", "3.5h", "c4hr", "4hdk", "4hmn", "sdpl"}) = True AndAlso TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) <= (TruncDate(TZShift(Now(), c.timezone))))

                                 RowData.CallCount = runningSbdCalls.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = runningSbdCalls.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.CallsInTB             'Calls in “T” Buckets

                                 Dim callsInTB = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") AndAlso (c.employee_number.StartsWith("t") = True) AndAlso (c.actiongrp <> "cust-canc") _
                                                                                             AndAlso (TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) <= (TruncDate(TZShift(Now(), c.timezone))) _
                                                                                             Or (c.call_received_date >= DateAdd(DateInterval.Minute, 1050, TruncDate(c.Gbdc01).Value) AndAlso TruncDate(c.call_received_date) < (TruncDate(TZShift(Now(), c.timezone))))))

                                 RowData.CallCount = callsInTB.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = callsInTB.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.CallsInAS             'Calls in “AS” Status

                                 Dim callsInAS = CustomerCallResults.FindAll(Function(c) (c.call_status = "as") AndAlso (c.employee_number.StartsWith("t") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) <= (TruncDate(TZShift(Now(), c.timezone))))

                                 RowData.CallCount = callsInAS.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = callsInAS.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.PendingCustomerETA    'Calls waiting for Customer ETA

                                 Dim pendingCustomerETA = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") AndAlso (c.actiongrp <> "cust-canc") AndAlso (If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value)) <= TZShift(Now(), c.timezone) AndAlso c.Lbr01 <> "y"))

                                 RowData.CallCount = pendingCustomerETA.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = pendingCustomerETA.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Onsite                'Onsite

                                 Dim onSite = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.stop_code_indicator = "onsite") AndAlso TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) = (TruncDate(TZShift(Now(), c.timezone))))

                                 RowData.CallCount = onSite.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = onSite.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.EtaOverdue            'ETA Overdue(1.5 hours)

                                 Dim etaOverdue = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso (DateAdd(DateInterval.Minute, 90, If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) <= TZShift(Now(), c.timezone) AndAlso (c.stop_code_indicator <> "onsite")))

                                 RowData.CallCount = etaOverdue.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = etaOverdue.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.MultipleVisits        'Multiple Visits

                                 Dim multipleVisits = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso c.Lbr02 = "y")

                                 RowData.CallCount = multipleVisits.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = multipleVisits.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.RunningPendingCN      'To be Cancelled

                                 Dim runningPendingCN = CustomerCallResults.FindAll(Function(c) ((c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp = "cust-canc")))

                                 RowData.CallCount = runningPendingCN.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = runningPendingCN.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.RunningCancelled      'Cancelled

                                 Dim runningCancelled = CustomerCallResults.FindAll(Function(c) ((c.call_status.StartsWith("c") = True) AndAlso (c.stop_code.StartsWith("can") = True)))

                                 RowData.CallCount = runningCancelled.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = runningCancelled.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.RunningClosed         'Closed

                                 Dim runningClosed = CustomerCallResults.FindAll(Function(c) ((c.call_status.StartsWith("c") = True) AndAlso (c.stop_code.StartsWith("can") = False)))

                                 RowData.CallCount = runningClosed.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = runningClosed.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                                 'New
                             Case RowTypes.NewOpenCalls          'New Calls received today

                                 Dim newOpenCalls = CustomerCallResults.FindAll(Function(c) (TruncDate(c.call_received_date) = (TruncDate(TZShift(Now(), c.timezone)))) AndAlso (c.actiongrp <> "cust-canc"))

                                 RowData.CallCount = newOpenCalls.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = newOpenCalls.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.CallsInBO             'BO Status

                                 Dim callsInBO = CustomerCallResults.FindAll(Function(c) (c.call_status = "bo") AndAlso (c.actiongrp <> "cust-canc") AndAlso (TruncDate(c.call_received_date) = (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = callsInBO.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = callsInBO.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.NewCAD                'New CAD Calls

                                 Dim newCAD = CustomerCallResults.FindAll(Function(c) (c.entitlement.StartsWith("cad") = True) AndAlso (c.actiongrp <> "cust-canc") AndAlso c.Lbr03 <> "y" AndAlso (TruncDate(c.call_received_date) = (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = newCAD.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = newCAD.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.NewBNN                'New BNN Calls

                                 Dim newBNN = CustomerCallResults.FindAll(Function(c) (c.system_number = "bnn") AndAlso (c.actiongrp <> "cust-canc") AndAlso (c.Lbr03 <> "y") AndAlso TruncDate(c.call_received_date) = (TruncDate(TZShift(Now(), c.timezone))))

                                 RowData.CallCount = newBNN.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = newBNN.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.NewPNA                'New PNA Calls

                                 Dim newPNA = CustomerCallResults.FindAll(Function(c) (c.actiongrp = "pna-new"))

                                 RowData.CallCount = newPNA.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = newPNA.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                                 'Future 

                             Case RowTypes.FutureSbdCalls        'Same Day Calls

                                 Dim futureSbdCalls = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso isInStrList(c.call_type, {"02hr", "03hr", "04hr", "05hr", "06hr", "08hr", "10hr", "emc1", "emc2", "emc3", "emc4", "emcy", "sdpr", "sev1", "sev2", "sev3", "spec", "temp", "3.5h", "c4hr", "4hdk", "4hmn", "sdpl"}) = True AndAlso (TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) > (TruncDate(TZShift(Now(), c.timezone)))))

                                 RowData.CallCount = futureSbdCalls.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = futureSbdCalls.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.BacklogNoExcp         'Backlog – No Exception code

                                 Dim backlogNoExcp = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso ((TruncDate(TZShift(Now(), c.timezone))) > c.Gbdc02) AndAlso (c.Lbr04 <> "y"))

                                 RowData.CallCount = backlogNoExcp.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = backlogNoExcp.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Backlog               'Backlog

                                 Dim backlog = CustomerCallResults.FindAll(Function(c) (c.projected_arrival_date.HasValue = True) AndAlso (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso (c.projected_arrival_date > c.Gbdc03))

                                 RowData.CallCount = backlog.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = backlog.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.OldPNA                'Old PNA code

                                 Dim oldPNA = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp = "pna"))

                                 RowData.CallCount = oldPNA.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = oldPNA.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Gt3Days               '>3 Days

                                 Dim gt3Days = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso (TruncDate(c.call_received_date) < TruncDate(DateAdd(DateInterval.Day, -3, Now()))))

                                 RowData.CallCount = gt3Days.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = gt3Days.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.Gt7Days               '>7 Days

                                 Dim gt7Days = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.actiongrp <> "cust-canc") AndAlso (TruncDate(c.call_received_date) < TruncDate(DateAdd(DateInterval.Day, -7, Now()))))

                                 RowData.CallCount = gt7Days.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = gt7Days.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.PendingAppointment    'New Calls pending Appointments

                                 Dim pendingAppointment = CustomerCallResults.FindAll(Function(c) (c.call_status.StartsWith("c") = False) AndAlso (c.PendingAppt1 = "y" Or c.PendingAppt2 = "y") AndAlso (c.HasCC <> "y"))

                                 RowData.CallCount = pendingAppointment.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = pendingAppointment.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case RowTypes.PendingSchedule       'Calls to be Scheduled

                                 Dim pendingSchedule = CustomerCallResults.FindAll(Function(c) c.Gbdc04.HasValue AndAlso (c.call_status.StartsWith("c") = False) AndAlso (c.call_status <> "bo") AndAlso (c.employee_number.StartsWith("t") = True) _
                                                                                 AndAlso (c.actiongrp <> "cust-canc") AndAlso (TruncDate(If(c.projected_arrival_date, DateAdd(DateInterval.Day, 1, c.call_received_date.Value))) = c.Gbdc04))

                                 RowData.CallCount = pendingSchedule.Count
                                 If (filterData.IsCallDetail) Then
                                     RowData.CallIds = pendingSchedule.[Select](Function(s) s.dispatch_number).ToList()
                                 End If

                             Case Else
                                 RowData.CallCount = 0
                         End Select
                         Rows.Add(RowData)
                     End Function)

        'Percent Calculate
        filterData.Rows _
            .FindAll(Function(r) PercentRows.Any(Function(p) p = r)) _
            .ForEach(Function(R)
                         Dim RowData As New RowData()
                         RowData.Id = R
                         Select Case R
                             Case RowTypes.EtsCallComplete       '% Complete
                                 Dim TodayCL As Integer = If(Rows.Find(Function(c) (c.Id = RowTypes.EtaB4TodayCL)) Is Nothing, 0, Rows.Find(Function(c) (c.Id = RowTypes.EtaB4TodayCL)).CallCount)
                                 Dim Total As Integer = If(Rows.Find(Function(c) (c.Id = RowTypes.EtaB4Today)) Is Nothing, 0, Rows.Find(Function(c) (c.Id = RowTypes.EtaB4Today)).CallCount)
                                 RowData.CallCount = If(Total > 0, Math.Round(((TodayCL * 100) / Total), 2, MidpointRounding.AwayFromZero), 0)


                             Case RowTypes.DeferredPer04           '% deferred (04)
                                 Dim Deferred04 As Integer = If(Rows.Find(Function(c) (c.Id = RowTypes.Deferred04)) Is Nothing, 0, Rows.Find(Function(c) (c.Id = RowTypes.Deferred04)).CallCount)
                                 Dim TotalTOC As Integer = If(Rows.Find(Function(c) (c.Id = RowTypes.TOC)) Is Nothing, 0, Rows.Find(Function(c) (c.Id = RowTypes.TOC)).CallCount)
                                 RowData.CallCount = If(TotalTOC > 0, Math.Round(((Deferred04 * 100) / TotalTOC), 2, MidpointRounding.AwayFromZero), 0)

                             Case RowTypes.DeferredPer05           '% deferred (05)
                                 Dim Deferred05 As Integer = If(Rows.Find(Function(c) (c.Id = RowTypes.Deferred05)) Is Nothing, 0, Rows.Find(Function(c) (c.Id = RowTypes.Deferred05)).CallCount)
                                 Dim TotalTOC As Integer = If(Rows.Find(Function(c) (c.Id = RowTypes.TOC)) Is Nothing, 0, Rows.Find(Function(c) (c.Id = RowTypes.TOC)).CallCount)
                                 RowData.CallCount = If(TotalTOC > 0, Math.Round(((Deferred05 * 100) / TotalTOC), 2, MidpointRounding.AwayFromZero), 0)

                             Case Else
                                 RowData.CallCount = 0
                         End Select
                         Rows.Add(RowData)
                     End Function)
        CustomerData.Rows = Rows
        CustomerData.CustomerPercentage = 0


        Return CustomerData
    End Function

    Private Sub WOsWithIssuesBuildMsg(strWO As String, strDateDesc As String)




        If String.IsNullOrEmpty(WOsWithDateIssues) Then
            WOsWithDateIssues = strWO + " " + strDateDesc + " was not set"
        Else
            WOsWithDateIssues = WOsWithDateIssues + ";" + strWO + " " + strDateDesc + " was not set"
        End If
    End Sub

    Private Sub AddRowIds(strRowName As String, FilterData As FilterData)
        Dim strClosedRowIDs As String = "1,3,32"
        Dim strProjectedRowIDs As String = "1,3,5,6,7,8,9,10,11,12,13,23,29,35,38,54,59"
        If String.IsNullOrEmpty(WOsWithDateIssues) Then Exit Sub
             
        Dim strFinalRowIDs As String = ""
        Dim strCallRowIds As String()

        If strRowName.ToUpper().Contains("CLOSED") Then
            strCallRowIds = strClosedRowIDs.Split(",")
        ElseIf strRowName.ToUpper().Contains("PROJECT") Then
            strCallRowIds = strProjectedRowIDs.Split(",")
        End If

        If strCallRowIds.Count = 0 Then Exit Sub

        For Each rowID In FilterData.Rows


            For Each strBuffer In strCallRowIds

                If rowID = Convert.ToInt32(strBuffer) Then
                    If String.IsNullOrEmpty(strFinalRowIDs) Then
                        strFinalRowIDs = strBuffer
                    Else
                        strFinalRowIDs = strFinalRowIDs + "," + strBuffer
                    End If
                End If

            Next


        Next





        WOsWithDateIssues = WOsWithDateIssues + " [" + strFinalRowIDs + "]"


    End Sub


    Private Function GetWOsWithIssues() As String
        Return WOsWithDateIssues
    End Function






End Class

