Imports System.Data.Entity

Public Class EFContextBase
    Public Sub New()
    End Sub

    Protected Function GetContext(Of TDbContext As DbContext)() As TDbContext
        Dim context As TDbContext = Nothing
        Dim dbContextType As Type = GetType(TDbContext)
        context = DirectCast(Activator.CreateInstance(GetType(TDbContext)), TDbContext)
        Return context
    End Function

End Class
