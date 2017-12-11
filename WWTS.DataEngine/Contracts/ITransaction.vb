Namespace Contracts
    Public Interface ITransaction
        Inherits IDisposable

        Function Commit() As Object
        Function Cancel() As Object
    End Interface
End Namespace

