Namespace Contracts
    Public Interface IExecutionObject
        ''' <summary>
        ''' Execute the database stored procedures and inline queries
        ''' </summary>
        ''' <param name="commandName">command name</param>
        ''' <param name="parameters">parameters</param>
        ''' <returns></returns>
        Function ExecuteDbCommand(Of TData)(commandName As String, ParamArray parameters As Object()) As IEnumerable(Of TData)

        ''' <summary>
        ''' Execute the non query command such as insert, update & delete queries/objects in database
        ''' </summary>
        ''' <param name="commandName">command name</param>
        ''' <param name="ensureTransaction">require transaction</param>
        ''' <param name="parameters">parameters</param>
        ''' <returns>returns boolean status</returns>
        Function ExecuteNonQueryCommand(commandName As String, ensureTransaction As Boolean, ParamArray parameters As Object()) As Boolean

        ''' <summary>
        ''' Execute the non query command such as insert, update & delete queries/objects in database
        ''' </summary>
        ''' <param name="commandName">command name</param>
        ''' <param name="parameters">parameters</param>
        ''' <returns>returns boolean status</returns>
        Function ExecuteNonQueryCommand(commandName As String, ParamArray parameters As Object()) As Boolean

        ''' <summary>
        ''' Method to execute the scalar command values
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="commandName">command name</param>
        ''' <param name="parameters">parameters</param>
        ''' <returns>returns scalar value</returns>
        Function ExecuteScalarCommand(Of T)(commandName As String, ParamArray parameters As Object()) As T

        ''' <summary>
        ''' Method to execute the scalar command values
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="commandName">command name</param>
        ''' <returns>returns scalar value</returns>
        Function ExecuteScalarValueCommand(Of T)(commandName As String) As T

        ''' <summary>
        ''' Method to retuns collection using inline sql query
        ''' </summary>
        ''' <typeparam name="TData">Generic type</typeparam>
        ''' <param name="query">sql query</param>
        ''' <returns>returns entity collection</returns>
        Function ExecuteSqlInlineQuery(Of TData)(query As String) As IEnumerable(Of TData)
    End Interface
End Namespace

