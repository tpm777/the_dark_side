Imports System.Runtime.CompilerServices
Imports WWTS.Aspects.Constants

Namespace Modules
    Public Module DateTimeExtensions
        <Extension> _
        Public Function ToStringFormat(value As DateTime) As String
            Return value.ToString(AppConstants.DateFormat)
        End Function

        <Extension> _
        Public Function ToStringFormat(value As System.Nullable(Of DateTime)) As String
            Return (If(value.HasValue, value.Value.ToString(AppConstants.DateFormat), String.Empty))
        End Function
    End Module
End Namespace

