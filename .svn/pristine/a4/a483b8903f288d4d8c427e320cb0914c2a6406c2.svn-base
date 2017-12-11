Imports System.ComponentModel.Composition
Imports WWTS.DataEngine.Contracts

''' <summary>
''' This is mostly a factory class that simply instantiates the transactions, so it's sharable
''' </summary>
<Export(GetType(IRepositoryTransactionManager))> _
<PartCreationPolicy(CreationPolicy.[Shared])> _
Public Class EFRepositoryTransactionManager
    Inherits RepositoryTransactionManagerBase

    Private contextNamesOrConnectionStrings As New Dictionary(Of Type, String)()

    Public Sub AddNameOrConnectionString(Of TDbContext)(nameOrConnectionString As String)
        contextNamesOrConnectionStrings.Add(GetType(TDbContext), nameOrConnectionString)
    End Sub

    Public Function GetNameOrConfigurationString(Of TDbContext)() As String
        Dim nameOrConnectionString As String = Nothing
        Dim dbContextType = GetType(TDbContext)
        If contextNamesOrConnectionStrings.ContainsKey(dbContextType) Then
            nameOrConnectionString = contextNamesOrConnectionStrings(dbContextType)
        End If
        Return nameOrConnectionString
    End Function

    Public Overrides Function BeginChanges() As ITransaction
        ExecutionStrategyDbConfiguration.SuspendExecutionStrategy = True
        Return New EFTransaction()
    End Function
End Class

