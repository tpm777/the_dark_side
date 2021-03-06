﻿Imports WWTS.DataEngine.Contracts
Imports System.Data.Entity
Imports WWTS.DataEngine.DataEnums

''' <summary>
''' Extends SerialCrudServiceBase, but makes a strongly typed DB Context available to sub-types, along with upsert and delete
''' </summary>
''' <typeparam name="TKey">The type of the key.</typeparam>
''' <typeparam name="TModel">The type of the model.</typeparam>
''' <typeparam name="TDbContext">The type of the database context.</typeparam>
Public MustInherit Class EFSynchronousRepository(Of TKey, TModel As {Class, IKeyedModel(Of TKey)}, TDbContext As {DbContext, New})
    Inherits SynchronousRepositoryBase(Of TKey, TModel)

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

    Protected Function GetContext(transaction As ITransaction) As TDbContext
        If transaction Is Nothing Then
            Throw New ArgumentException(String.Format("EF Repository GetContext invoked with {0} transaction", transaction.[GetType]().Name))
        End If
        Dim efTransaction = TryCast(transaction, EFTransaction)
        If efTransaction Is Nothing Then
            Throw New ArgumentException(String.Format("EF Repository GetContext invoked with {0} transaction", transaction.[GetType]().Name))
        End If

        Dim nameOrConnectionString As String = Nothing
        Dim repositoryTransactionManager = TryCast(Me.RepositoryTransactionManager, EFRepositoryTransactionManager)
        If repositoryTransactionManager IsNot Nothing Then
            nameOrConnectionString = repositoryTransactionManager.GetNameOrConfigurationString(Of TDbContext)()
        End If
        Return DirectCast(efTransaction.GetContext(Of TDbContext)(nameOrConnectionString), TDbContext)
    End Function



    Private Function GetDbSet(transaction As ITransaction) As DbSet(Of TModel)
        Return Me.GetDbSet(Me.GetContext(transaction))
    End Function

    Protected Overrides Function Expand(query As IQueryable(Of TModel), path As String) As IQueryable(Of TModel)
        Return query.Include(path)
    End Function

    Protected Overrides Function GetAllImpl(transaction As ITransaction) As IQueryable(Of TModel)
        Return Me.GetDbSet(transaction).AsQueryable()
    End Function

    Protected Overrides Function GetAllImpl(transaction As ITransaction, include As String()) As IQueryable(Of TModel)
        Dim data = Me.GetDbSet(transaction).AsQueryable()
        If include IsNot Nothing Then
            For Each entityName As String In include
                data = data.Include(entityName)
            Next
        End If
        Return data
    End Function

    Protected Overrides Function FindImpl(key As TKey, transaction As ITransaction) As TModel
        Return Me.FindImpl(key, Me.GetContext(transaction))
    End Function

    Protected Overrides Function FindImplWithExpand(key As TKey, includeQueryPath As String, externalTransaction As ITransaction) As TModel
        Return Me.FindImplWithExpand(key, Me.GetDbSet(externalTransaction), includeQueryPath)
    End Function

    Protected Overloads Function FindImpl(key As TKey, context As TDbContext) As TModel
        Return Me.FindImpl(key, Me.GetDbSet(context))
    End Function

    Protected MustOverride Overloads Function FindImpl(key As TKey, dbSet As DbSet(Of TModel)) As TModel
    Protected MustOverride Overloads Function FindImplWithExpand(key As TKey, dbSet As DbSet(Of TModel), includeQueryPath As String) As TModel

    Protected Overrides Function DetermineSaveAction(model As TModel, transaction As ITransaction) As RepositoryAction
        Dim action As RepositoryAction = RepositoryAction.Create

        Dim context = Me.GetContext(transaction)
        Dim old As TModel = Me.FindImpl(model.Key, context)
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
        Dim old As TModel = Me.FindImpl(model.Key, context)
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
            Return If(RepositoryContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, commandName, parameters) > 0, True, False)
        Else
            Return If(RepositoryContext.Database.ExecuteSqlCommand(commandName, parameters) > 0, True, False)
        End If
    End Function

    Protected Overrides Function ExecuteScalarCommandImpl(Of T)(commandName As String, ParamArray parameters As Object()) As T
        Return RepositoryContext.Database.SqlQuery(Of T)(String.Format("Exec {0}", commandName), parameters).FirstOrDefault()
    End Function
    Protected Overrides Function ExecuteScalarValueCommandImpl(Of T)(commandName As String) As T
        Return RepositoryContext.Database.SqlQuery(Of T)(commandName).FirstOrDefault()
    End Function

    Protected Overrides Function ExecuteSqlInlineQueryImpl(Of T)(query As String) As IEnumerable(Of T)
        Dim objList As IEnumerable(Of T)


        Try
            objList = RepositoryContext.Database.SqlQuery(Of T)(query).ToList()
        Catch ex As Exception

        End Try
        Return objList
    End Function
End Class
