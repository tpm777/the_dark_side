Imports WWTS.DataEngine.Contracts
Imports System.Transactions

Public MustInherit Class RepositoryTransactionManagerBase
    Implements IRepositoryTransactionManager

    Public MustOverride Function BeginChanges() As ITransaction Implements IRepositoryTransactionManager.BeginChanges

    Public Function Perform(action As Action(Of ITransaction), Optional localFinalAction As Action(Of ITransaction) = Nothing, Optional transaction As ITransaction = Nothing) As Boolean Implements IRepositoryTransactionManager.Perform

        Return Perform(Function(localTransaction)
                           action(localTransaction)
                           Return True

                       End Function, localFinalAction, transaction)
    End Function

    Public Function Perform(action As Func(Of ITransaction, Boolean), Optional localFinalAction As Action(Of ITransaction) = Nothing, Optional transaction As ITransaction = Nothing) As Boolean
        Debug.Assert(action IsNot Nothing, "Attempted to invoke RepositoryTransactionManager.Perform without an action")

        Dim success As Boolean = False
        Dim localTransaction As Boolean = False

        If transaction Is Nothing Then
            localTransaction = True
            transaction = Me.BeginChanges()
        End If

        Try
            success = action(transaction)
            If success AndAlso localTransaction AndAlso localFinalAction IsNot Nothing Then
                localFinalAction(transaction)

            End If
        Finally
            If localTransaction Then
                transaction.Dispose()
            End If
        End Try
        Return success
    End Function

    Public Function Perform(action As Action, isTransactionScope As Boolean) As Boolean Implements IRepositoryTransactionManager.Perform

        Debug.Assert(action IsNot Nothing, "Attempted to invoke RepositoryTransactionManager.Perform without an action")

        Dim success As Boolean = False
        If isTransactionScope Then
            Using scope As New TransactionScope()
                action.Invoke()
                scope.Complete()
                success = True
            End Using
        Else
            action.Invoke()
            success = True
        End If
        Return success
    End Function

    Public Function PerformWithNoLock(action As Action) As Boolean Implements IRepositoryTransactionManager.PerformWithNoLock
        Debug.Assert(action IsNot Nothing, "Attempted to invoke RepositoryTransactionManager.PerformWithNoLock without an action")

        Dim success As Boolean = False
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With { _
            .IsolationLevel = IsolationLevel.ReadUncommitted _
        })
            action.Invoke()
            scope.Complete()
            success = True
        End Using
        Return success
    End Function

End Class
