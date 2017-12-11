Imports WWTS.DataEngine.Contracts
Imports System.Data.Entity
Imports System.Data.Entity.Validation

Public Class EFTransaction
    Implements ITransaction

    Private Property Contexts() As Dictionary(Of Type, DbContext)
        Get
            Return m_Contexts
        End Get
        Set(value As Dictionary(Of Type, DbContext))
            m_Contexts = value
        End Set
    End Property
    Private m_Contexts As Dictionary(Of Type, DbContext)

    Public Sub New()
        Me.Contexts = New Dictionary(Of Type, DbContext)()
    End Sub

    Public Function GetContext(Of TDbContext As {DbContext, New})(nameOrConnectionString As String) As DbContext
        Dim context As DbContext = Nothing
        Dim dbContextType As Type = GetType(TDbContext)
        If Not Contexts.ContainsKey(dbContextType) Then
            If String.IsNullOrEmpty(nameOrConnectionString) Then
                context = DirectCast(Activator.CreateInstance(GetType(TDbContext)), TDbContext)
            Else
                context = DirectCast(Activator.CreateInstance(GetType(TDbContext), nameOrConnectionString), TDbContext)
            End If
            Contexts.Add(dbContextType, context)
        Else
            context = Contexts(dbContextType)
        End If
        Return context
    End Function

    Public Function Commit() As Object Implements ITransaction.Commit

        Dim commitedItems As Integer = 0
        Dim success As Boolean = False
        Dim contextTransactions As New List(Of DbContextTransaction)()
        ExecutionStrategyDbConfiguration.SuspendExecutionStrategy = True
        For Each context As DbContext In Me.Contexts.Values
            contextTransactions.Add(context.Database.BeginTransaction())
            Try
                commitedItems += context.SaveChanges()
                success = True
            Catch ex As DbEntityValidationException
                success = False
                Dim errors As New List(Of String)()
                For Each [error] As DbEntityValidationResult In ex.EntityValidationErrors
                    Dim entityName = "(UNKNOWN)"

                    If [error].Entry IsNot Nothing AndAlso [error].Entry.Entity IsNot Nothing Then
                        entityName = [error].Entry.Entity.[GetType]().Name
                    End If
                    errors.Add(String.Format("Validation Error on {0}", entityName))
                    For Each propertyError As DbValidationError In [error].ValidationErrors
                        errors.Add(String.Format("  {0} - {1}", propertyError.PropertyName, propertyError.ErrorMessage))
                    Next
                Next

                Dim errorMessage As String = String.Join(Environment.NewLine, errors)

                Throw New Exception(errorMessage, ex)
            End Try
        Next
        If success Then
            contextTransactions.ForEach(Sub(contextTransaction) contextTransaction.Commit())

        Else
            contextTransactions.ForEach(Sub(contextTransaction) contextTransaction.Rollback())
        End If
        contextTransactions.ForEach(Sub(contextTransaction) contextTransaction.Dispose())
        ExecutionStrategyDbConfiguration.SuspendExecutionStrategy = False
        Return commitedItems
    End Function

    Public Function Cancel() As Object Implements ITransaction.Cancel

        Me.Dispose()

        ' this implementation doesn't return anything of significance.
        Return Nothing
    End Function

    Public Sub Dispose() Implements ITransaction.Dispose

        For Each context As DbContext In Me.Contexts.Values
            context.Dispose()
        Next
    End Sub

End Class
