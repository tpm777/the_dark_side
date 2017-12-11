Imports WWTS.DataEngine.Contracts
Imports System.Data.Entity
Imports WWTS.DataEngine.DataEnums

Public MustInherit Class CompositeSynchronousRepository(Of TModel As {Class, ICompositeModel}, TDbContext As {DbContext, New})
    Inherits CompositeRepositoryBase(Of TModel)

    Private context As TDbContext
    Protected Sub New()
    End Sub

    Protected ReadOnly Property RepositoryContext() As TDbContext
        Get
            If context Is Nothing Then
                context = GetContext(New EFTransaction())
            End If
            Return context
        End Get
    End Property
    Protected MustOverride Function GetDbSet(context As TDbContext) As DbSet(Of TModel)

    Protected Function GetContext(externalTransaction As ITransaction) As TDbContext
        Dim efTransaction = TryCast(externalTransaction, EFTransaction)
        If efTransaction Is Nothing Then
            Throw New ArgumentException("External Transaction not provided, it has null value.")
        End If

        Dim nameOrConnectionString As String = Nothing
        Dim repositoryTransactionManager = TryCast(Me.RepositoryTransactionManager, EFRepositoryTransactionManager)
        If repositoryTransactionManager IsNot Nothing Then
            nameOrConnectionString = repositoryTransactionManager.GetNameOrConfigurationString(Of TDbContext)()
        End If
        Return DirectCast(efTransaction.GetContext(Of TDbContext)(nameOrConnectionString), TDbContext)
    End Function

    Private Function GetDbSet(externalTransaction As ITransaction) As DbSet(Of TModel)
        Return Me.GetDbSet(Me.GetContext(externalTransaction))
    End Function

    Protected Overrides Function Expand(query As IQueryable(Of TModel), path As String) As IQueryable(Of TModel)
        Return query.Include(path)
    End Function

    Protected Overrides Function GetAllImpl(externalTransaction As ITransaction) As IQueryable(Of TModel)
        Return Me.GetDbSet(externalTransaction).AsQueryable()
    End Function

    Protected Overrides Function GetAllImpl(externalTransaction As ITransaction, include As String()) As IQueryable(Of TModel)
        Dim data = Me.GetDbSet(externalTransaction).AsQueryable()
        If include IsNot Nothing Then
            For Each entityName As String In include
                data = data.Include(entityName)
            Next
        End If
        Return data
    End Function

    Protected Overrides Function FindImpl(keys As Dictionary(Of String, Object), externalTransaction As ITransaction) As TModel
        Return Me.FindImpl(keys, Me.GetContext(externalTransaction))
    End Function

    Protected Overloads Function FindImpl(keys As Dictionary(Of String, Object), context As TDbContext) As TModel
        Return Me.FindImpl(keys, Me.GetDbSet(context))
    End Function

    Protected MustOverride Overloads Function FindImpl(keys As Dictionary(Of String, Object), dbSet As DbSet(Of TModel)) As TModel

    Protected Overrides Function DetermineSaveAction(model As TModel, transaction As ITransaction) As RepositoryAction
        Dim action As RepositoryAction = RepositoryAction.Create

        Dim context = Me.GetContext(transaction)
        Dim old As TModel = Me.FindImpl(model.CompositeKeys, context)
        If old IsNot Nothing Then
            action = RepositoryAction.Update
        End If

        Return action
    End Function

    Protected Overrides Function SaveCreateImpl(model As TModel, transaction As ITransaction) As Boolean
        Return Me.EFSaveCreateImpl(model, transaction)
    End Function

    Protected Overridable Function EFSaveCreateImpl(model As TModel, transaction As ITransaction) As Boolean
        Me.GetDbSet(transaction).Add(model)

        Return True
    End Function

    Protected Overridable Function EFExecuteCommand(command As String, transaction As ITransaction, ParamArray parameters As Object()) As Boolean
        Return If(Me.GetContext(transaction).Database.ExecuteSqlCommand(command, parameters) > 0, True, False)
    End Function

    Protected Overrides Function SaveUpdateImpl(model As TModel, transaction As ITransaction) As Boolean
        Return Me.EFSaveUpdateImpl(model, transaction)
    End Function

    Protected Overrides Function ExecuteCommand(command As String, transaction As ITransaction, ParamArray parameters As Object()) As Boolean
        Return Me.EFExecuteCommand(command, transaction, parameters)
    End Function

    Protected Overridable Function EFSaveUpdateImpl(model As TModel, transaction As ITransaction) As Boolean
        Dim context = Me.GetContext(transaction)
        Dim old As TModel = Me.FindImpl(model.CompositeKeys, context)
        context.Entry(old).CurrentValues.SetValues(model)
        Return True
    End Function

    Protected Overrides Function DeleteImpl(model As TModel, transaction As ITransaction) As Boolean
        Dim dbSet = Me.GetDbSet(transaction)
        dbSet.Attach(model)
        dbSet.Remove(model)

        Return True
    End Function

    Protected Overrides Function ExecuteDbCommandImpl(Of TData)(commandName As String, ParamArray parameters As Object()) As IEnumerable(Of TData)
        Return RepositoryContext.Database.SqlQuery(Of TData)(String.Format("Exec {0}", commandName), parameters).ToList()
    End Function

    Protected Overrides Function ExecuteNonQueryCommandImpl(commandName As String, ensureTransaction As Boolean, ParamArray parameters As Object()) As Boolean
        If Not ensureTransaction Then
            Return If(RepositoryContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, String.Format("Exec {0}", commandName), parameters) > 0, True, False)
        Else
            Return If(RepositoryContext.Database.ExecuteSqlCommand(String.Format("Exec {0}", commandName), parameters) > 0, True, False)
        End If
    End Function

    Protected Overrides Function ExecuteScalarCommandImpl(Of T)(commandName As String, ParamArray parameters As Object()) As T
        Return RepositoryContext.Database.SqlQuery(Of T)(String.Format("Exec {0}", commandName), parameters).FirstOrDefault()
    End Function

    Protected Overrides Function ExecuteScalarValueCommandImpl(Of T)(commandName As String) As T
        Return RepositoryContext.Database.SqlQuery(Of T)(commandName).FirstOrDefault()
    End Function

    Protected Overrides Function ExecuteSqlInlineQueryImpl(Of T)(query As String) As IEnumerable(Of T)
        Return RepositoryContext.Database.SqlQuery(Of T)(query).ToList()
    End Function

End Class
