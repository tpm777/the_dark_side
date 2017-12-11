Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class CoreServiceTest
    Dim serviceClient As New WWTSService.WWTSServiceClient()
    <TestMethod()> Public Sub GetUserEmailLogsTest()
        Dim data = serviceClient.GetUserEmailLogs()
        Assert.IsNotNull(data)
    End Sub
End Class