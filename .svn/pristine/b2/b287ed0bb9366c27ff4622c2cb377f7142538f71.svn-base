Namespace Modules
    Public Module StringExtensions
        <System.Runtime.CompilerServices.Extension> _
        Public Function IsNullOrEmpty(value As String) As Boolean
            Return String.IsNullOrEmpty(value)
        End Function

        <System.Runtime.CompilerServices.Extension> _
        Public Function IsNullOrWhiteSpace(value As String) As Boolean
            Return String.IsNullOrWhiteSpace(value)
        End Function

        <System.Runtime.CompilerServices.Extension> _
        Public Function HasValue(value As String) As Boolean
            Return Not String.IsNullOrEmpty(value)
        End Function

        <System.Runtime.CompilerServices.Extension> _
        Public Function ToByteArray(value As String) As Byte()
            Dim bytes As Byte() = New Byte(value.Length * 2 - 1) {}
            System.Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length)
            Return bytes
        End Function

        <System.Runtime.CompilerServices.Extension> _
        Public Function ToDateTime(value As String) As System.Nullable(Of DateTime)
            Return (If(Not value.IsNullOrEmpty() AndAlso Not value.IsNullOrWhiteSpace(), Convert.ToDateTime(value), DirectCast(Nothing, System.Nullable(Of DateTime))))
        End Function
    End Module
End Namespace
