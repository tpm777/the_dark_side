Namespace Contracts

    Public Interface IRepositoryTransactionManager
        ''' <summary>
        ''' Start the transaction boundary for changes.
        ''' </summary>
        ''' <returns></returns>
        Function BeginChanges() As ITransaction

        ''' <summary>
        ''' Performs the specified action within a transaction.
        ''' </summary>
        ''' <param name="action">The action.</param>
        ''' <param name="localFinalAction">The action to execute if the transaction was local and prior to disposal.</param>
        ''' <param name="externalTransaction">A potential transaction passed in.</param>
        Function Perform(action As Action(Of ITransaction), Optional localFinalAction As Action(Of ITransaction) = Nothing, Optional externalTransaction As ITransaction = Nothing) As Boolean

        Function Perform(action As Action, isTransactionScope As Boolean) As Boolean

        Function PerformWithNoLock(action As Action) As Boolean

    End Interface

End Namespace
