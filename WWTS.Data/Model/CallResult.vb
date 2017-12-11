﻿Namespace Model
    Public Class CallResult
        'Base result set columns
        Public Property dispatch_number As String
        Public Property customer_number As String
        Public Property query_customer_number As String
        Public Property customer_state As String
        Public Property employee_number As String
        Public Property call_type As String
        Public Property entitlement As String
        Public Property stop_code As String
        Public Property stop_code_indicator As String
        Public Property mfg_serial_number As String
        Public Property mfg_model_number As String
        Public Property system_number As String
        Public Property call_received_date As DateTime?
        Public Property projected_arrival_date As DateTime?
        Public Property call_last_modified_date As DateTime?
        Public Property call_status As String
        Public Property timezone As Decimal
        Public Property actiongrp As String
        Public Property stop_code_upd_dt As DateTime?
        Public Property territory As String
        Public Property srs_sched As String
        Public Property ready_date As DateTime?
        Public Property cust_sub_div As String
        Public Property branch_rdb As String
        Public Property region_rdb As String
        Public Property area_rdb As String
        Public Property district_rdb As String
        Public Property vendorcode As String
        Public Property csr_territory As String
        Public Property customer_country As String
        Public Property employee_class As String
        Public Property sched_window_start_dt As DateTime?
        Public Property sched_window_end_dt As DateTime?
        Public Property call_closed_date As DateTime?
        Public Property email_addr As String
        Public Property PartCount As Integer
        Public Property HasCC_Today As String
        Public Property NumDeferSinceRdy As Integer
        Public Property EtaNbd As DateTime?
        Public Property TwoManAssign As String
        Public Property Escalations As String
        Public Property Gbdc01 As DateTime?
        Public Property Gbdc02 As DateTime?
        Public Property Gbdc03 As DateTime?
        Public Property Gbdc04 As DateTime?
        Public Property Lbr01 As String
        Public Property Lbr02 As String
        Public Property Lbr03 As String
        Public Property Lbr04 As String
        Public Property PendingAppt1 As String
        Public Property PendingAppt2 As String
        Public Property HasCC As String
        Public Property ScList As String
        Public Property IsScheduledForToday As String  'SAAS 788 03/28/2017
        Public Property Work_Orders_without_parts As Integer 'SAAS 939 08/01/2017


        Public ReadOnly Property StopCodeSrchList() As String
            Get
                Return "," & ScList.ToLower & ","
            End Get
        End Property
    End Class

    Public Class WayBillResult
        Public Property RowId As Integer
    End Class
End Namespace
