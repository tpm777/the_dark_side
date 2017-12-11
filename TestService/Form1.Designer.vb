<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnTestGetDataSummary = New System.Windows.Forms.Button()
        Me.drpCustomerCamparatorName = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCustomerCamparatorValue = New System.Windows.Forms.Label()
        Me.drpCustomerCamparatorValue = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.drpSubDivisionCamparatorName = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.drpSubDivisionCamparatorValue = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.drpWorkOrderType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txbOrgBranchList = New System.Windows.Forms.TextBox()
        Me.txbOrgFilterList = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.drpOrgFilterType = New System.Windows.Forms.ComboBox()
        Me.chkIsCallDetail = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.drpOrgType = New System.Windows.Forms.ComboBox()
        Me.chkDashVendor = New System.Windows.Forms.CheckBox()
        Me.chkDashVendorFilterOff = New System.Windows.Forms.CheckBox()
        Me.chkDashCustomerFilterOff = New System.Windows.Forms.CheckBox()
        Me.txbVendorCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txbUserID = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txbCustAssoc = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txbRows = New System.Windows.Forms.TextBox()
        Me.btnTestSQL = New System.Windows.Forms.Button()
        Me.opdlg = New System.Windows.Forms.OpenFileDialog()
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.tabSQLResults = New System.Windows.Forms.TabPage()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnSQLCmd = New System.Windows.Forms.Button()
        Me.rtxbSQLCMD = New System.Windows.Forms.RichTextBox()
        Me.drpInequalities = New System.Windows.Forms.ComboBox()
        Me.drpDataSource = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblScriptFileName = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.txbFilterValue = New System.Windows.Forms.TextBox()
        Me.lblDataColumns = New System.Windows.Forms.Label()
        Me.cmbDataCoLumns = New System.Windows.Forms.ComboBox()
        Me.lblSQLResultsTitle = New System.Windows.Forms.Label()
        Me.dgSQL = New System.Windows.Forms.DataGridView()
        Me.tabAPIResults = New System.Windows.Forms.TabPage()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkSaveAPIResponse = New System.Windows.Forms.CheckBox()
        Me.chkUseStaticFile = New System.Windows.Forms.CheckBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txbDetailOrgType = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txbDetailOrgFilterType = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txbDetailUserid = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txbDetailAPICall = New System.Windows.Forms.Button()
        Me.txbColumnCount = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkDetailDashCustomerFilterOff = New System.Windows.Forms.CheckBox()
        Me.chkDetailDashVendorFilterOff = New System.Windows.Forms.CheckBox()
        Me.chkDetailDashVendor = New System.Windows.Forms.CheckBox()
        Me.chkDetaiI_IsCallDetail = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txbDetailOrgBranchList = New System.Windows.Forms.TextBox()
        Me.txbDetailCustAssoc = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txbDetailOrgFilterList = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txbDetailVendorCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SQLToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenSQLScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnTestImport = New System.Windows.Forms.Button()
        Me.txbAyncCount = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblElaspsedTime = New System.Windows.Forms.Label()
        Me.lblReturnMessage = New System.Windows.Forms.Label()
        Me.lblRtnMsgText = New System.Windows.Forms.Label()
        Me.tabSQLResults.SuspendLayout()
        CType(Me.dgSQL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAPIResults.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.mnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnTestGetDataSummary
        '
        Me.btnTestGetDataSummary.Location = New System.Drawing.Point(121, 415)
        Me.btnTestGetDataSummary.Name = "btnTestGetDataSummary"
        Me.btnTestGetDataSummary.Size = New System.Drawing.Size(75, 23)
        Me.btnTestGetDataSummary.TabIndex = 0
        Me.btnTestGetDataSummary.Text = "Test API"
        Me.btnTestGetDataSummary.UseVisualStyleBackColor = True
        '
        'drpCustomerCamparatorName
        '
        Me.drpCustomerCamparatorName.FormattingEnabled = True
        Me.drpCustomerCamparatorName.Items.AddRange(New Object() {"-- Select --", "Equals", "NotEquals", "StartsWith", "DoesNotStartsWith", "Contains", "DoesNotContains", "InList", "NotInList", "All"})
        Me.drpCustomerCamparatorName.Location = New System.Drawing.Point(164, 33)
        Me.drpCustomerCamparatorName.Name = "drpCustomerCamparatorName"
        Me.drpCustomerCamparatorName.Size = New System.Drawing.Size(121, 21)
        Me.drpCustomerCamparatorName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Customer Camparator Name:"
        '
        'lblCustomerCamparatorValue
        '
        Me.lblCustomerCamparatorValue.AutoSize = True
        Me.lblCustomerCamparatorValue.Location = New System.Drawing.Point(12, 68)
        Me.lblCustomerCamparatorValue.Name = "lblCustomerCamparatorValue"
        Me.lblCustomerCamparatorValue.Size = New System.Drawing.Size(141, 13)
        Me.lblCustomerCamparatorValue.TabIndex = 5
        Me.lblCustomerCamparatorValue.Text = "Customer Camparator Value:"
        '
        'drpCustomerCamparatorValue
        '
        Me.drpCustomerCamparatorValue.FormattingEnabled = True
        Me.drpCustomerCamparatorValue.Location = New System.Drawing.Point(164, 60)
        Me.drpCustomerCamparatorValue.Name = "drpCustomerCamparatorValue"
        Me.drpCustomerCamparatorValue.Size = New System.Drawing.Size(121, 21)
        Me.drpCustomerCamparatorValue.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Sub Div Camparator Name:"
        '
        'drpSubDivisionCamparatorName
        '
        Me.drpSubDivisionCamparatorName.FormattingEnabled = True
        Me.drpSubDivisionCamparatorName.Items.AddRange(New Object() {"-- Select --", "Equals", "NotEquals", "StartsWith", "DoesNotStartsWith", "Contains", "DoesNotContains", "InList", "NotInList", "All"})
        Me.drpSubDivisionCamparatorName.Location = New System.Drawing.Point(164, 87)
        Me.drpSubDivisionCamparatorName.Name = "drpSubDivisionCamparatorName"
        Me.drpSubDivisionCamparatorName.Size = New System.Drawing.Size(121, 21)
        Me.drpSubDivisionCamparatorName.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Sub Div Camparator Value:"
        '
        'drpSubDivisionCamparatorValue
        '
        Me.drpSubDivisionCamparatorValue.FormattingEnabled = True
        Me.drpSubDivisionCamparatorValue.Location = New System.Drawing.Point(164, 114)
        Me.drpSubDivisionCamparatorValue.Name = "drpSubDivisionCamparatorValue"
        Me.drpSubDivisionCamparatorValue.Size = New System.Drawing.Size(121, 21)
        Me.drpSubDivisionCamparatorValue.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "WorkOrderType:"
        '
        'drpWorkOrderType
        '
        Me.drpWorkOrderType.FormattingEnabled = True
        Me.drpWorkOrderType.Location = New System.Drawing.Point(164, 141)
        Me.drpWorkOrderType.Name = "drpWorkOrderType"
        Me.drpWorkOrderType.Size = New System.Drawing.Size(121, 21)
        Me.drpWorkOrderType.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Org Branch List:"
        '
        'txbOrgBranchList
        '
        Me.txbOrgBranchList.Location = New System.Drawing.Point(164, 172)
        Me.txbOrgBranchList.Multiline = True
        Me.txbOrgBranchList.Name = "txbOrgBranchList"
        Me.txbOrgBranchList.Size = New System.Drawing.Size(493, 20)
        Me.txbOrgBranchList.TabIndex = 13
        '
        'txbOrgFilterList
        '
        Me.txbOrgFilterList.Location = New System.Drawing.Point(164, 200)
        Me.txbOrgFilterList.Multiline = True
        Me.txbOrgFilterList.Name = "txbOrgFilterList"
        Me.txbOrgFilterList.Size = New System.Drawing.Size(493, 20)
        Me.txbOrgFilterList.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 207)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Org Filter List:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 262)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Org Filter Type:"
        '
        'drpOrgFilterType
        '
        Me.drpOrgFilterType.FormattingEnabled = True
        Me.drpOrgFilterType.Location = New System.Drawing.Point(164, 254)
        Me.drpOrgFilterType.Name = "drpOrgFilterType"
        Me.drpOrgFilterType.Size = New System.Drawing.Size(121, 21)
        Me.drpOrgFilterType.TabIndex = 17
        '
        'chkIsCallDetail
        '
        Me.chkIsCallDetail.AutoSize = True
        Me.chkIsCallDetail.Location = New System.Drawing.Point(336, 56)
        Me.chkIsCallDetail.Name = "chkIsCallDetail"
        Me.chkIsCallDetail.Size = New System.Drawing.Size(73, 17)
        Me.chkIsCallDetail.TabIndex = 18
        Me.chkIsCallDetail.Text = "Call Detail"
        Me.chkIsCallDetail.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(332, 262)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Org Type:"
        '
        'drpOrgType
        '
        Me.drpOrgType.FormattingEnabled = True
        Me.drpOrgType.Location = New System.Drawing.Point(410, 254)
        Me.drpOrgType.Name = "drpOrgType"
        Me.drpOrgType.Size = New System.Drawing.Size(121, 21)
        Me.drpOrgType.TabIndex = 20
        '
        'chkDashVendor
        '
        Me.chkDashVendor.AutoSize = True
        Me.chkDashVendor.Location = New System.Drawing.Point(335, 85)
        Me.chkDashVendor.Name = "chkDashVendor"
        Me.chkDashVendor.Size = New System.Drawing.Size(88, 17)
        Me.chkDashVendor.TabIndex = 21
        Me.chkDashVendor.Text = "Dash Vendor"
        Me.chkDashVendor.UseVisualStyleBackColor = True
        '
        'chkDashVendorFilterOff
        '
        Me.chkDashVendorFilterOff.AutoSize = True
        Me.chkDashVendorFilterOff.Location = New System.Drawing.Point(336, 118)
        Me.chkDashVendorFilterOff.Name = "chkDashVendorFilterOff"
        Me.chkDashVendorFilterOff.Size = New System.Drawing.Size(130, 17)
        Me.chkDashVendorFilterOff.TabIndex = 22
        Me.chkDashVendorFilterOff.Text = "Dash Vendor Filter Off"
        Me.chkDashVendorFilterOff.UseVisualStyleBackColor = True
        '
        'chkDashCustomerFilterOff
        '
        Me.chkDashCustomerFilterOff.AutoSize = True
        Me.chkDashCustomerFilterOff.Location = New System.Drawing.Point(336, 145)
        Me.chkDashCustomerFilterOff.Name = "chkDashCustomerFilterOff"
        Me.chkDashCustomerFilterOff.Size = New System.Drawing.Size(140, 17)
        Me.chkDashCustomerFilterOff.TabIndex = 23
        Me.chkDashCustomerFilterOff.Text = "Dash Customer Filter Off"
        Me.chkDashCustomerFilterOff.UseVisualStyleBackColor = True
        '
        'txbVendorCode
        '
        Me.txbVendorCode.Location = New System.Drawing.Point(410, 11)
        Me.txbVendorCode.Name = "txbVendorCode"
        Me.txbVendorCode.Size = New System.Drawing.Size(100, 20)
        Me.txbVendorCode.TabIndex = 24
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(332, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Vendor Code:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "UserID"
        '
        'txbUserID
        '
        Me.txbUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txbUserID.Location = New System.Drawing.Point(164, 7)
        Me.txbUserID.Name = "txbUserID"
        Me.txbUserID.Size = New System.Drawing.Size(100, 20)
        Me.txbUserID.TabIndex = 26
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 233)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Cust Assoc:"
        '
        'txbCustAssoc
        '
        Me.txbCustAssoc.Location = New System.Drawing.Point(164, 226)
        Me.txbCustAssoc.Multiline = True
        Me.txbCustAssoc.Name = "txbCustAssoc"
        Me.txbCustAssoc.Size = New System.Drawing.Size(493, 20)
        Me.txbCustAssoc.TabIndex = 29
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 292)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Rows:"
        '
        'txbRows
        '
        Me.txbRows.Location = New System.Drawing.Point(164, 292)
        Me.txbRows.Multiline = True
        Me.txbRows.Name = "txbRows"
        Me.txbRows.Size = New System.Drawing.Size(506, 20)
        Me.txbRows.TabIndex = 31
        '
        'btnTestSQL
        '
        Me.btnTestSQL.Location = New System.Drawing.Point(532, 439)
        Me.btnTestSQL.Name = "btnTestSQL"
        Me.btnTestSQL.Size = New System.Drawing.Size(93, 23)
        Me.btnTestSQL.TabIndex = 32
        Me.btnTestSQL.Text = "Execute Sort"
        Me.btnTestSQL.UseVisualStyleBackColor = True
        '
        'opdlg
        '
        Me.opdlg.FileName = "OpenFileDialog1"
        '
        'tabSQLResults
        '
        Me.tabSQLResults.Controls.Add(Me.btnExport)
        Me.tabSQLResults.Controls.Add(Me.btnSQLCmd)
        Me.tabSQLResults.Controls.Add(Me.rtxbSQLCMD)
        Me.tabSQLResults.Controls.Add(Me.drpInequalities)
        Me.tabSQLResults.Controls.Add(Me.btnTestSQL)
        Me.tabSQLResults.Controls.Add(Me.drpDataSource)
        Me.tabSQLResults.Controls.Add(Me.Label16)
        Me.tabSQLResults.Controls.Add(Me.lblScriptFileName)
        Me.tabSQLResults.Controls.Add(Me.Label15)
        Me.tabSQLResults.Controls.Add(Me.lblCount)
        Me.tabSQLResults.Controls.Add(Me.txbFilterValue)
        Me.tabSQLResults.Controls.Add(Me.lblDataColumns)
        Me.tabSQLResults.Controls.Add(Me.cmbDataCoLumns)
        Me.tabSQLResults.Controls.Add(Me.lblSQLResultsTitle)
        Me.tabSQLResults.Controls.Add(Me.dgSQL)
        Me.tabSQLResults.Location = New System.Drawing.Point(4, 25)
        Me.tabSQLResults.Name = "tabSQLResults"
        Me.tabSQLResults.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSQLResults.Size = New System.Drawing.Size(704, 471)
        Me.tabSQLResults.TabIndex = 1
        Me.tabSQLResults.Text = "SQL Results"
        Me.tabSQLResults.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(19, 443)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(103, 22)
        Me.btnExport.TabIndex = 46
        Me.btnExport.Text = "Export Data"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnSQLCmd
        '
        Me.btnSQLCmd.Location = New System.Drawing.Point(128, 442)
        Me.btnSQLCmd.Name = "btnSQLCmd"
        Me.btnSQLCmd.Size = New System.Drawing.Size(75, 23)
        Me.btnSQLCmd.TabIndex = 42
        Me.btnSQLCmd.Text = "View SQL Cmd"
        Me.btnSQLCmd.UseVisualStyleBackColor = True
        Me.btnSQLCmd.Visible = False
        '
        'rtxbSQLCMD
        '
        Me.rtxbSQLCMD.Location = New System.Drawing.Point(19, 382)
        Me.rtxbSQLCMD.Name = "rtxbSQLCMD"
        Me.rtxbSQLCMD.Size = New System.Drawing.Size(679, 54)
        Me.rtxbSQLCMD.TabIndex = 47
        Me.rtxbSQLCMD.Text = ""
        '
        'drpInequalities
        '
        Me.drpInequalities.FormattingEnabled = True
        Me.drpInequalities.Items.AddRange(New Object() {"=", ">", "<", "<>"})
        Me.drpInequalities.Location = New System.Drawing.Point(374, 441)
        Me.drpInequalities.Name = "drpInequalities"
        Me.drpInequalities.Size = New System.Drawing.Size(43, 21)
        Me.drpInequalities.TabIndex = 45
        '
        'drpDataSource
        '
        Me.drpDataSource.Enabled = False
        Me.drpDataSource.FormattingEnabled = True
        Me.drpDataSource.Location = New System.Drawing.Point(94, 6)
        Me.drpDataSource.Name = "drpDataSource"
        Me.drpDataSource.Size = New System.Drawing.Size(251, 21)
        Me.drpDataSource.TabIndex = 44
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(82, 13)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Data Source:"
        '
        'lblScriptFileName
        '
        Me.lblScriptFileName.AutoSize = True
        Me.lblScriptFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScriptFileName.Location = New System.Drawing.Point(572, 3)
        Me.lblScriptFileName.Name = "lblScriptFileName"
        Me.lblScriptFileName.Size = New System.Drawing.Size(34, 13)
        Me.lblScriptFileName.TabIndex = 41
        Me.lblScriptFileName.Text = "base"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(503, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 13)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "SQL Script:"
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(572, 52)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(53, 13)
        Me.lblCount.TabIndex = 39
        Me.lblCount.Text = "Count = 1"
        '
        'txbFilterValue
        '
        Me.txbFilterValue.Location = New System.Drawing.Point(423, 442)
        Me.txbFilterValue.Name = "txbFilterValue"
        Me.txbFilterValue.Size = New System.Drawing.Size(100, 20)
        Me.txbFilterValue.TabIndex = 38
        '
        'lblDataColumns
        '
        Me.lblDataColumns.AutoSize = True
        Me.lblDataColumns.Location = New System.Drawing.Point(200, 445)
        Me.lblDataColumns.Name = "lblDataColumns"
        Me.lblDataColumns.Size = New System.Drawing.Size(41, 13)
        Me.lblDataColumns.TabIndex = 37
        Me.lblDataColumns.Text = "Sort By"
        '
        'cmbDataCoLumns
        '
        Me.cmbDataCoLumns.FormattingEnabled = True
        Me.cmbDataCoLumns.Location = New System.Drawing.Point(247, 442)
        Me.cmbDataCoLumns.Name = "cmbDataCoLumns"
        Me.cmbDataCoLumns.Size = New System.Drawing.Size(121, 21)
        Me.cmbDataCoLumns.TabIndex = 36
        '
        'lblSQLResultsTitle
        '
        Me.lblSQLResultsTitle.AutoSize = True
        Me.lblSQLResultsTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSQLResultsTitle.Location = New System.Drawing.Point(284, 52)
        Me.lblSQLResultsTitle.Name = "lblSQLResultsTitle"
        Me.lblSQLResultsTitle.Size = New System.Drawing.Size(77, 13)
        Me.lblSQLResultsTitle.TabIndex = 34
        Me.lblSQLResultsTitle.Text = "SQL Results"
        '
        'dgSQL
        '
        Me.dgSQL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSQL.Location = New System.Drawing.Point(19, 68)
        Me.dgSQL.Name = "dgSQL"
        Me.dgSQL.Size = New System.Drawing.Size(679, 308)
        Me.dgSQL.TabIndex = 0
        '
        'tabAPIResults
        '
        Me.tabAPIResults.AllowDrop = True
        Me.tabAPIResults.Controls.Add(Me.lblRtnMsgText)
        Me.tabAPIResults.Controls.Add(Me.lblReturnMessage)
        Me.tabAPIResults.Controls.Add(Me.dgView)
        Me.tabAPIResults.Controls.Add(Me.Label13)
        Me.tabAPIResults.Location = New System.Drawing.Point(4, 25)
        Me.tabAPIResults.Name = "tabAPIResults"
        Me.tabAPIResults.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAPIResults.Size = New System.Drawing.Size(704, 471)
        Me.tabAPIResults.TabIndex = 0
        Me.tabAPIResults.Text = "API Results"
        Me.tabAPIResults.UseVisualStyleBackColor = True
        '
        'dgView
        '
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Location = New System.Drawing.Point(118, 104)
        Me.dgView.Name = "dgView"
        Me.dgView.Size = New System.Drawing.Size(468, 150)
        Me.dgView.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(303, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(78, 13)
        Me.Label13.TabIndex = 33
        Me.Label13.Text = "Test Results"
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.tabAPIResults)
        Me.TabControl1.Controls.Add(Me.tabSQLResults)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(61, 46)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(712, 500)
        Me.TabControl1.TabIndex = 36
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkSaveAPIResponse)
        Me.TabPage2.Controls.Add(Me.chkUseStaticFile)
        Me.TabPage2.Controls.Add(Me.txbUserID)
        Me.TabPage2.Controls.Add(Me.btnTestGetDataSummary)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.txbRows)
        Me.TabPage2.Controls.Add(Me.drpWorkOrderType)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.drpCustomerCamparatorName)
        Me.TabPage2.Controls.Add(Me.txbVendorCode)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.txbCustAssoc)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.chkDashCustomerFilterOff)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.chkDashVendorFilterOff)
        Me.TabPage2.Controls.Add(Me.drpCustomerCamparatorValue)
        Me.TabPage2.Controls.Add(Me.chkDashVendor)
        Me.TabPage2.Controls.Add(Me.lblCustomerCamparatorValue)
        Me.TabPage2.Controls.Add(Me.chkIsCallDetail)
        Me.TabPage2.Controls.Add(Me.drpOrgType)
        Me.TabPage2.Controls.Add(Me.drpSubDivisionCamparatorName)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.drpSubDivisionCamparatorValue)
        Me.TabPage2.Controls.Add(Me.drpOrgFilterType)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.txbOrgFilterList)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.txbOrgBranchList)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(704, 471)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "API Call Parameters"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkSaveAPIResponse
        '
        Me.chkSaveAPIResponse.AutoSize = True
        Me.chkSaveAPIResponse.Location = New System.Drawing.Point(15, 321)
        Me.chkSaveAPIResponse.Name = "chkSaveAPIResponse"
        Me.chkSaveAPIResponse.Size = New System.Drawing.Size(122, 17)
        Me.chkSaveAPIResponse.TabIndex = 33
        Me.chkSaveAPIResponse.Text = "Save API Response"
        Me.chkSaveAPIResponse.UseVisualStyleBackColor = True
        '
        'chkUseStaticFile
        '
        Me.chkUseStaticFile.AutoSize = True
        Me.chkUseStaticFile.Location = New System.Drawing.Point(15, 344)
        Me.chkUseStaticFile.Name = "chkUseStaticFile"
        Me.chkUseStaticFile.Size = New System.Drawing.Size(94, 17)
        Me.chkUseStaticFile.TabIndex = 32
        Me.chkUseStaticFile.Text = "Use Static File"
        Me.chkUseStaticFile.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txbDetailOrgType)
        Me.TabPage1.Controls.Add(Me.Label23)
        Me.TabPage1.Controls.Add(Me.txbDetailOrgFilterType)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.txbDetailUserid)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.txbDetailAPICall)
        Me.TabPage1.Controls.Add(Me.txbColumnCount)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.chkDetailDashCustomerFilterOff)
        Me.TabPage1.Controls.Add(Me.chkDetailDashVendorFilterOff)
        Me.TabPage1.Controls.Add(Me.chkDetailDashVendor)
        Me.TabPage1.Controls.Add(Me.chkDetaiI_IsCallDetail)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.txbDetailOrgBranchList)
        Me.TabPage1.Controls.Add(Me.txbDetailCustAssoc)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.txbDetailOrgFilterList)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.txbDetailVendorCode)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(704, 471)
        Me.TabPage1.TabIndex = 3
        Me.TabPage1.Text = "DetailDetail"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txbDetailOrgType
        '
        Me.txbDetailOrgType.Location = New System.Drawing.Point(313, 230)
        Me.txbDetailOrgType.Name = "txbDetailOrgType"
        Me.txbDetailOrgType.Size = New System.Drawing.Size(100, 20)
        Me.txbDetailOrgType.TabIndex = 46
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(253, 233)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(54, 13)
        Me.Label23.TabIndex = 45
        Me.Label23.Text = "Org Type:"
        '
        'txbDetailOrgFilterType
        '
        Me.txbDetailOrgFilterType.Location = New System.Drawing.Point(104, 255)
        Me.txbDetailOrgFilterType.Name = "txbDetailOrgFilterType"
        Me.txbDetailOrgFilterType.Size = New System.Drawing.Size(100, 20)
        Me.txbDetailOrgFilterType.TabIndex = 44
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(11, 258)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(79, 13)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "Org Filter Type:"
        '
        'txbDetailUserid
        '
        Me.txbDetailUserid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txbDetailUserid.Location = New System.Drawing.Point(104, 11)
        Me.txbDetailUserid.Name = "txbDetailUserid"
        Me.txbDetailUserid.Size = New System.Drawing.Size(100, 20)
        Me.txbDetailUserid.TabIndex = 41
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(45, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(40, 13)
        Me.Label21.TabIndex = 42
        Me.Label21.Text = "UserID"
        '
        'txbDetailAPICall
        '
        Me.txbDetailAPICall.Location = New System.Drawing.Point(312, 416)
        Me.txbDetailAPICall.Name = "txbDetailAPICall"
        Me.txbDetailAPICall.Size = New System.Drawing.Size(75, 23)
        Me.txbDetailAPICall.TabIndex = 40
        Me.txbDetailAPICall.Text = "Test API"
        Me.txbDetailAPICall.UseVisualStyleBackColor = True
        '
        'txbColumnCount
        '
        Me.txbColumnCount.Location = New System.Drawing.Point(104, 223)
        Me.txbColumnCount.Name = "txbColumnCount"
        Me.txbColumnCount.Size = New System.Drawing.Size(100, 20)
        Me.txbColumnCount.TabIndex = 39
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(13, 226)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(76, 13)
        Me.Label20.TabIndex = 38
        Me.Label20.Text = "Column Count:"
        '
        'chkDetailDashCustomerFilterOff
        '
        Me.chkDetailDashCustomerFilterOff.AutoSize = True
        Me.chkDetailDashCustomerFilterOff.Location = New System.Drawing.Point(121, 160)
        Me.chkDetailDashCustomerFilterOff.Name = "chkDetailDashCustomerFilterOff"
        Me.chkDetailDashCustomerFilterOff.Size = New System.Drawing.Size(140, 17)
        Me.chkDetailDashCustomerFilterOff.TabIndex = 37
        Me.chkDetailDashCustomerFilterOff.Text = "Dash Customer Filter Off"
        Me.chkDetailDashCustomerFilterOff.UseVisualStyleBackColor = True
        '
        'chkDetailDashVendorFilterOff
        '
        Me.chkDetailDashVendorFilterOff.AutoSize = True
        Me.chkDetailDashVendorFilterOff.Location = New System.Drawing.Point(121, 133)
        Me.chkDetailDashVendorFilterOff.Name = "chkDetailDashVendorFilterOff"
        Me.chkDetailDashVendorFilterOff.Size = New System.Drawing.Size(130, 17)
        Me.chkDetailDashVendorFilterOff.TabIndex = 36
        Me.chkDetailDashVendorFilterOff.Text = "Dash Vendor Filter Off"
        Me.chkDetailDashVendorFilterOff.UseVisualStyleBackColor = True
        '
        'chkDetailDashVendor
        '
        Me.chkDetailDashVendor.AutoSize = True
        Me.chkDetailDashVendor.Location = New System.Drawing.Point(16, 162)
        Me.chkDetailDashVendor.Name = "chkDetailDashVendor"
        Me.chkDetailDashVendor.Size = New System.Drawing.Size(88, 17)
        Me.chkDetailDashVendor.TabIndex = 35
        Me.chkDetailDashVendor.Text = "Dash Vendor"
        Me.chkDetailDashVendor.UseVisualStyleBackColor = True
        '
        'chkDetaiI_IsCallDetail
        '
        Me.chkDetaiI_IsCallDetail.AutoSize = True
        Me.chkDetaiI_IsCallDetail.Location = New System.Drawing.Point(17, 133)
        Me.chkDetaiI_IsCallDetail.Name = "chkDetaiI_IsCallDetail"
        Me.chkDetaiI_IsCallDetail.Size = New System.Drawing.Size(73, 17)
        Me.chkDetaiI_IsCallDetail.TabIndex = 34
        Me.chkDetaiI_IsCallDetail.Text = "Call Detail"
        Me.chkDetaiI_IsCallDetail.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(7, 190)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 13)
        Me.Label19.TabIndex = 32
        Me.Label19.Text = "Org Branch List:"
        '
        'txbDetailOrgBranchList
        '
        Me.txbDetailOrgBranchList.Location = New System.Drawing.Point(104, 183)
        Me.txbDetailOrgBranchList.Multiline = True
        Me.txbDetailOrgBranchList.Name = "txbDetailOrgBranchList"
        Me.txbDetailOrgBranchList.Size = New System.Drawing.Size(493, 20)
        Me.txbDetailOrgBranchList.TabIndex = 33
        '
        'txbDetailCustAssoc
        '
        Me.txbDetailCustAssoc.Location = New System.Drawing.Point(104, 68)
        Me.txbDetailCustAssoc.Multiline = True
        Me.txbDetailCustAssoc.Name = "txbDetailCustAssoc"
        Me.txbDetailCustAssoc.Size = New System.Drawing.Size(493, 20)
        Me.txbDetailCustAssoc.TabIndex = 31
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(31, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 13)
        Me.Label18.TabIndex = 30
        Me.Label18.Text = "Cust Assoc:"
        '
        'txbDetailOrgFilterList
        '
        Me.txbDetailOrgFilterList.Location = New System.Drawing.Point(104, 37)
        Me.txbDetailOrgFilterList.Multiline = True
        Me.txbDetailOrgFilterList.Name = "txbDetailOrgFilterList"
        Me.txbDetailOrgFilterList.Size = New System.Drawing.Size(493, 20)
        Me.txbDetailOrgFilterList.TabIndex = 29
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(23, 44)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 13)
        Me.Label17.TabIndex = 28
        Me.Label17.Text = "Org Filter List:"
        '
        'txbDetailVendorCode
        '
        Me.txbDetailVendorCode.Location = New System.Drawing.Point(497, 11)
        Me.txbDetailVendorCode.Name = "txbDetailVendorCode"
        Me.txbDetailVendorCode.Size = New System.Drawing.Size(100, 20)
        Me.txbDetailVendorCode.TabIndex = 26
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(419, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Vendor Code:"
        '
        'mnuMain
        '
        Me.mnuMain.BackColor = System.Drawing.SystemColors.Menu
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SQLToolsToolStripMenuItem})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(814, 24)
        Me.mnuMain.TabIndex = 37
        Me.mnuMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'SQLToolsToolStripMenuItem
        '
        Me.SQLToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenSQLScriptToolStripMenuItem})
        Me.SQLToolsToolStripMenuItem.Name = "SQLToolsToolStripMenuItem"
        Me.SQLToolsToolStripMenuItem.Size = New System.Drawing.Size(78, 20)
        Me.SQLToolsToolStripMenuItem.Text = "SQL Scripts"
        '
        'OpenSQLScriptToolStripMenuItem
        '
        Me.OpenSQLScriptToolStripMenuItem.Name = "OpenSQLScriptToolStripMenuItem"
        Me.OpenSQLScriptToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.OpenSQLScriptToolStripMenuItem.Text = "Open SQL Script"
        '
        'btnTestImport
        '
        Me.btnTestImport.Location = New System.Drawing.Point(352, 571)
        Me.btnTestImport.Name = "btnTestImport"
        Me.btnTestImport.Size = New System.Drawing.Size(75, 23)
        Me.btnTestImport.TabIndex = 47
        Me.btnTestImport.Text = "Test Import"
        Me.btnTestImport.UseVisualStyleBackColor = True
        '
        'txbAyncCount
        '
        Me.txbAyncCount.Location = New System.Drawing.Point(441, 571)
        Me.txbAyncCount.Name = "txbAyncCount"
        Me.txbAyncCount.Size = New System.Drawing.Size(100, 20)
        Me.txbAyncCount.TabIndex = 48
        '
        'Timer1
        '
        Me.Timer1.Interval = 400
        '
        'lblElaspsedTime
        '
        Me.lblElaspsedTime.AutoSize = True
        Me.lblElaspsedTime.Location = New System.Drawing.Point(571, 576)
        Me.lblElaspsedTime.Name = "lblElaspsedTime"
        Me.lblElaspsedTime.Size = New System.Drawing.Size(74, 13)
        Me.lblElaspsedTime.TabIndex = 49
        Me.lblElaspsedTime.Text = "Elpased Time:"
        '
        'lblReturnMessage
        '
        Me.lblReturnMessage.AutoSize = True
        Me.lblReturnMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReturnMessage.Location = New System.Drawing.Point(232, 395)
        Me.lblReturnMessage.Name = "lblReturnMessage"
        Me.lblReturnMessage.Size = New System.Drawing.Size(67, 15)
        Me.lblReturnMessage.TabIndex = 34
        Me.lblReturnMessage.Text = "Return Msg:"
        Me.lblReturnMessage.Visible = False
        '
        'lblRtnMsgText
        '
        Me.lblRtnMsgText.AutoSize = True
        Me.lblRtnMsgText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRtnMsgText.Location = New System.Drawing.Point(304, 394)
        Me.lblRtnMsgText.Name = "lblRtnMsgText"
        Me.lblRtnMsgText.Size = New System.Drawing.Size(30, 15)
        Me.lblRtnMsgText.TabIndex = 35
        Me.lblRtnMsgText.Text = "       "
        Me.lblRtnMsgText.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 606)
        Me.Controls.Add(Me.lblElaspsedTime)
        Me.Controls.Add(Me.txbAyncCount)
        Me.Controls.Add(Me.btnTestImport)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.mnuMain)
        Me.MainMenuStrip = Me.mnuMain
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DashSum2API Utility"
        Me.tabSQLResults.ResumeLayout(False)
        Me.tabSQLResults.PerformLayout()
        CType(Me.dgSQL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAPIResults.ResumeLayout(False)
        Me.tabAPIResults.PerformLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnTestGetDataSummary As Button
    Friend WithEvents drpCustomerCamparatorName As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblCustomerCamparatorValue As Label
    Friend WithEvents drpCustomerCamparatorValue As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents drpSubDivisionCamparatorName As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents drpSubDivisionCamparatorValue As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents drpWorkOrderType As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txbOrgBranchList As TextBox
    Friend WithEvents txbOrgFilterList As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents drpOrgFilterType As ComboBox
    Friend WithEvents chkIsCallDetail As CheckBox
    Friend WithEvents Label8 As Label
    Friend WithEvents drpOrgType As ComboBox
    Friend WithEvents chkDashVendor As CheckBox
    Friend WithEvents chkDashVendorFilterOff As CheckBox
    Friend WithEvents chkDashCustomerFilterOff As CheckBox
    Friend WithEvents txbVendorCode As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txbUserID As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txbCustAssoc As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txbRows As TextBox
    Friend WithEvents btnTestSQL As Button
    Friend WithEvents opdlg As OpenFileDialog
    Friend WithEvents dlgSaveFile As SaveFileDialog
    Friend WithEvents tabSQLResults As TabPage
    Friend WithEvents lblDataColumns As Label
    Friend WithEvents cmbDataCoLumns As ComboBox
    Friend WithEvents lblSQLResultsTitle As Label
    Friend WithEvents dgSQL As DataGridView
    Friend WithEvents tabAPIResults As TabPage
    Friend WithEvents dgView As DataGridView
    Friend WithEvents Label13 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents txbFilterValue As TextBox
    Friend WithEvents lblCount As Label
    Friend WithEvents mnuMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SQLToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenSQLScriptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents lblScriptFileName As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents btnSQLCmd As Button
    Friend WithEvents drpDataSource As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents drpInequalities As ComboBox
    Friend WithEvents btnExport As Button
    Friend WithEvents chkUseStaticFile As CheckBox
    Friend WithEvents chkSaveAPIResponse As CheckBox
    Friend WithEvents rtxbSQLCMD As RichTextBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label19 As Label
    Friend WithEvents txbDetailOrgBranchList As TextBox
    Friend WithEvents txbDetailCustAssoc As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txbDetailOrgFilterList As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txbDetailVendorCode As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents chkDetailDashCustomerFilterOff As CheckBox
    Friend WithEvents chkDetailDashVendorFilterOff As CheckBox
    Friend WithEvents chkDetailDashVendor As CheckBox
    Friend WithEvents chkDetaiI_IsCallDetail As CheckBox
    Friend WithEvents txbDetailAPICall As Button
    Friend WithEvents txbColumnCount As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txbDetailUserid As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents txbDetailOrgFilterType As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents txbDetailOrgType As TextBox
    Friend WithEvents btnTestImport As Button
    Friend WithEvents txbAyncCount As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblElaspsedTime As Label
    Friend WithEvents lblRtnMsgText As Label
    Friend WithEvents lblReturnMessage As Label
End Class
