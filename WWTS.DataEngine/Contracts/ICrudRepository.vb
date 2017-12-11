Imports System.Linq.Expressions
Imports System.Collections.Generic

Namespace Contracts
    Public Interface ICrudRepository(Of TKey, TModel)
        Inherits ITransactable
        Inherits IExecutionObject

        ''' <summary>
        ''' Finds the record off the key.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <param name="externalTransaction">The potential external transaction, otherwise the command will be executed in a local transaction</param>
        ''' <returns></returns>
        Function Find(key As TKey, Optional externalTransaction As ITransaction = Nothing) As TModel

        ''' <summary>
        ''' Finds the record off the key.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <param name="includeQueryPath">table name which needs to be included</param>
        ''' <param name="externalTransaction">The potential external transaction, otherwise the command will be executed in a local transaction</param>
        ''' <returns></returns>
        Function FindWithExpand(key As TKey, includeQueryPath As String, Optional externalTransaction As ITransaction = Nothing) As TModel

        ''' <summary>
        ''' Finds this instance based on a query expression.
        ''' </summary>
        ''' <returns></returns>
        Function Find(filterExpression As Expression(Of Func(Of TModel, Boolean)), Optional externalTransaction As ITransaction = Nothing, Optional expandPaths As IEnumerable(Of String) = Nothing) As IEnumerable(Of TModel)

        ''' <summary>
        ''' Gets all off the records and potentially immediately executes the query
        ''' </summary>
        ''' <param name="externalTransaction">The potential external transaction, otherwise the command will be executed in a local transaction</param>
        ''' <returns></returns>
        Function GetAll(Optional externalTransaction As ITransaction = Nothing) As IEnumerable(Of TModel)

        ''' <summary>
        ''' Gets the deferred entity to potentially add additional search clauses upon
        ''' </summary>
        ''' <param name="externalTransaction">The potential external transaction, otherwise the command will be executed in a local transaction</param>
        ''' <returns></returns>
        Function GetQueryable(transaction As ITransaction) As IQueryable(Of TModel)

        ''' <summary>
        ''' Saves the specified model.
        ''' </summary>
        ''' <param name="model">The model.</param>
        ''' <param name="externalTransaction">The potential external transaction, otherwise the command will be executed in a local transaction</param>
        ''' <returns></returns>
        Function Save(model As TModel, Optional externalTransaction As ITransaction = Nothing) As Boolean

        ''' <summary>
        ''' Deletes the specified model.
        ''' </summary>
        ''' <param name="model">The model.</param>
        ''' <param name="externalTransaction">The potential external transaction, otherwise the command will be executed in a local transaction</param>
        ''' <returns></returns>
        Function Delete(model As TModel, Optional externalTransaction As ITransaction = Nothing) As Boolean
    End Interface
End Namespace

