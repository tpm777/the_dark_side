Imports WWTS.DataEngine.Contracts
Imports Microsoft.Practices.Unity
Imports WWTS.DataEngine.DataEnums

Public MustInherit Class CompositeRepositoryBase(Of TModel)
    Implements ICompositeCrudRepository(Of TModel)
    Protected serviceLock As New Object()

    <Dependency> _
    Public Property RepositoryTransactionManager() As IRepositoryTransactionManager Implements ICompositeCrudRepository(Of TModel).RepositoryTransactionManager

        Get
            Return m_RepositoryTransactionManager
        End Get
        Set(value As IRepositoryTransactionManager)
            m_RepositoryTransactionManager = value
        End Set
    End Property
    Private m_RepositoryTransactionManager As IRepositoryTransactionManager

    Public Function Delete(model As TModel, Optional externalTransaction As ITransaction = Nothing) As Boolean Implements ICompositeCrudRepository(Of TModel).Delete
        Dim success As Boolean = False
        SyncLock serviceLock
            Dim localTransaction As Boolean = False
            If externalTransaction Is Nothing Then
                localTransaction = True
                externalTransaction = Me.RepositoryTransactionManager.BeginChanges()
            End If

            success = DeleteImpl(model, externalTransaction)

            If localTransaction AndAlso success Then
                externalTransaction.Commit()
            End If
        End SyncLock
        Return success
    End Function

    Public Function Find(keys As Dictionary(Of String, Object), Optional externalTransaction As ITransaction = Nothing) As TModel Implements ICompositeCrudRepository(Of TModel).Find
        Dim result As TModel = Nothing
        SyncLock serviceLock
            Dim localTransaction As Boolean = False
            If externalTransaction Is Nothing Then
                localTransaction = True
                If Me.RepositoryTransactionManager Is Nothing Then
                    externalTransaction = New EFTransaction()
                Else
                    externalTransaction = Me.RepositoryTransactionManager.BeginChanges()
                End If
            End If

            result = FindImpl(keys, externalTransaction)

            If localTransaction Then
                externalTransaction.Dispose()
            End If
        End SyncLock
        Return result
    End Function

    Public Function Find(filterExpression As Expressions.Expression(Of Func(Of TModel, Boolean)), Optional externalTransaction As ITransaction = Nothing, Optional expandPaths As IEnumerable(Of String) = Nothing) As IEnumerable(Of TModel) Implements ICompositeCrudRepository(Of TModel).Find
        Dim result As IEnumerable(Of TModel) = Nothing

        SyncLock serviceLock
            Dim localTransaction As Boolean = False
            If externalTransaction Is Nothing Then
                localTransaction = True
                externalTransaction = Me.RepositoryTransactionManager.BeginChanges()
            End If

            result = GetQueryable(externalTransaction).Where(filterExpression)
            If expandPaths IsNot Nothing Then
                For Each expandPath As String In expandPaths
                    result = Expand(TryCast(result, IQueryable(Of TModel)), expandPath)
                Next
            End If

            If localTransaction Then
                If result IsNot Nothing Then
                    result = result.ToList()
                End If
                externalTransaction.Dispose()
            End If
        End SyncLock
        Return result
    End Function

    Public Function GetAll(Optional externalTransaction As ITransaction = Nothing) As IEnumerable(Of TModel) Implements ICompositeCrudRepository(Of TModel).GetAll
        Dim result As IEnumerable(Of TModel) = Nothing
        SyncLock serviceLock
            Dim localTransaction As Boolean = False
            If externalTransaction Is Nothing Then
                localTransaction = True
                If Me.RepositoryTransactionManager Is Nothing Then
                    externalTransaction = New EFTransaction()
                Else
                    externalTransaction = Me.RepositoryTransactionManager.BeginChanges()
                End If
            End If

            result = GetAllImpl(externalTransaction)

            If localTransaction Then
                ' if it's a local transaction, the object will be immediately converted to a list and extracted
                If result IsNot Nothing Then
                    result = result.ToList()
                End If
                externalTransaction.Dispose()
            End If
        End SyncLock
        Return result
    End Function

    Public Function GetQueryable(externalTransaction As ITransaction) As IQueryable(Of TModel) Implements ICompositeCrudRepository(Of TModel).GetQueryable
        Dim result As IQueryable(Of TModel) = Nothing
        Debug.Assert(externalTransaction IsNot Nothing, "GetQueryable invoked with no transaction")
        SyncLock serviceLock
            result = GetAllImpl(externalTransaction)
        End SyncLock
        Return result
    End Function

    Public Function Save(model As TModel, Optional externalTransaction As ITransaction = Nothing) As Boolean Implements ICompositeCrudRepository(Of TModel).Save
        Dim success As Boolean = False
        SyncLock serviceLock
            Dim localTransaction As Boolean = False
            'AzureExecutionStrategyDbConfiguration.
            If externalTransaction Is Nothing Then
                localTransaction = True
                externalTransaction = Me.RepositoryTransactionManager.BeginChanges()
            End If

            ValidateModel(model)
            Dim action = DetermineSaveAction(model, externalTransaction)

            Select Case action
                Case RepositoryAction.Create
                    success = SaveCreate(model, externalTransaction)
                    Exit Select

                Case RepositoryAction.Update
                    success = SaveUpdate(model, externalTransaction)
                    Exit Select
                Case Else

                    Throw New Exception(String.Format("Save yielded {0} action", action))
            End Select

            If localTransaction AndAlso success Then
                externalTransaction.Commit()
            End If
        End SyncLock
        Return success
    End Function

    Public Function ExecuteDbCommand(Of TData)(commandName As String, ParamArray parameters() As Object) As IEnumerable(Of TData) Implements IExecutionObject.ExecuteDbCommand
        Return ExecuteDbCommandImpl(Of TData)(commandName, parameters)
    End Function

    Public Function ExecuteNonQueryCommand(commandName As String, ensureTransaction As Boolean, ParamArray parameters() As Object) As Boolean Implements IExecutionObject.ExecuteNonQueryCommand
        Return ExecuteNonQueryCommandImpl(commandName, ensureTransaction, parameters)
    End Function

    Public Function ExecuteNonQueryCommand(commandName As String, ParamArray parameters() As Object) As Boolean Implements IExecutionObject.ExecuteNonQueryCommand
        Return ExecuteNonQueryCommandImpl(commandName, False, parameters)
    End Function

    Public Function ExecuteScalarCommand(Of T)(commandName As String, ParamArray parameters() As Object) As T Implements IExecutionObject.ExecuteScalarCommand
        Return ExecuteScalarCommandImpl(Of T)(commandName, parameters)
    End Function

    Public Function ExecuteScalarValueCommand(Of T)(commandName As String) As T Implements IExecutionObject.ExecuteScalarValueCommand
        Return ExecuteScalarValueCommandImpl(Of T)(commandName)
    End Function

    Public Function ExecuteSqlInlineQuery(Of TData)(query As String) As IEnumerable(Of TData) Implements IExecutionObject.ExecuteSqlInlineQuery
        Return ExecuteSqlInlineQueryImpl(Of TData)(query)
    End Function

    ''' <summary>
    ''' Validates the model. Throws an exception if 
    ''' </summary>
    ''' <param name="model">The model.</param>
    Protected Overridable Sub ValidateModel(model As TModel)
    End Sub

    Private Function SaveCreate(model As TModel, externalTransaction As ITransaction) As Boolean
        Return SaveCreateImpl(model, externalTransaction)
    End Function

    Private Function SaveUpdate(model As TModel, externalTransaction As ITransaction) As Boolean
        Return SaveUpdateImpl(model, externalTransaction)
    End Function

    Protected MustOverride Function Expand(query As IQueryable(Of TModel), path As String) As IQueryable(Of TModel)

    Protected MustOverride Function ExecuteCommand(command As String, externalTransaction As ITransaction, ParamArray parameters As Object()) As Boolean

    Protected MustOverride Function FindImpl(keys As Dictionary(Of String, Object), externalTransaction As ITransaction) As TModel

    Protected MustOverride Function GetAllImpl(externalTransaction As ITransaction) As IQueryable(Of TModel)
    Protected MustOverride Function GetAllImpl(externalTransaction As ITransaction, include As String()) As IQueryable(Of TModel)
    Protected MustOverride Function DetermineSaveAction(model As TModel, externalTransaction As ITransaction) As RepositoryAction
    Protected MustOverride Function SaveCreateImpl(model As TModel, externalTransaction As ITransaction) As Boolean
    Protected MustOverride Function SaveUpdateImpl(model As TModel, externalTransaction As ITransaction) As Boolean
    Protected MustOverride Function DeleteImpl(model As TModel, externalTransaction As ITransaction) As Boolean
    Protected MustOverride Function ExecuteDbCommandImpl(Of TData)(commandName As String, ParamArray parameters As Object()) As IEnumerable(Of TData)
    Protected MustOverride Function ExecuteNonQueryCommandImpl(commandName As String, ensureTransaction As Boolean, ParamArray parameters As Object()) As Boolean
    Protected MustOverride Function ExecuteScalarCommandImpl(Of T)(commandName As String, ParamArray parameters As Object()) As T
    Protected MustOverride Function ExecuteScalarValueCommandImpl(Of T)(commandName As String) As T
    Protected MustOverride Function ExecuteSqlInlineQueryImpl(Of T)(query As String) As IEnumerable(Of T)

End Class
