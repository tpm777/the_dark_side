Imports System.Runtime.Remoting.Messaging
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Public Class ExecutionStrategyDbConfiguration
    Inherits DbConfiguration

    Public Sub New()

        Me.SetExecutionStrategy("System.Data.SqlClient", Function() DirectCast(New DefaultExecutionStrategy(), IDbExecutionStrategy))
    End Sub

    ''' <summary>
    ''' Suspend the execution strategy for code that needs to use transactions.
    ''' </summary>
    ''' <value>The suspend execution strategy.</value>
    Public Shared Property SuspendExecutionStrategy() As Boolean
        Get
            Return If(DirectCast(CallContext.LogicalGetData("SuspendExecutionStrategy"), System.Nullable(Of Boolean)), False)
        End Get
        Set(value As Boolean)
            CallContext.LogicalSetData("SuspendExecutionStrategy", value)
        End Set
    End Property


End Class
