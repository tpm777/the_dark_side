Namespace Model
    Public Class EmailLogData
        Public Property EmailLogID As Integer
        Public Property FromEmail As String
        Public Property FromName As String
        Public Property ToEmail As String
        Public Property ToName As String
        Public Property CcEmail As String
        Public Property CcName As String
        Public Property BccEmail As String
        Public Property BccName As String
        Public Property Subject As String
        Public Property Body As String
        Public Property IsHtml As Boolean
        Public Property Priority As Integer
        Public Property Status As Integer
        Public Property IsAttachment As Boolean
        Public Property CreatedDate As Date
        Public Property ModifiedDate As Nullable(Of Date)
        Public Property Remarks As String
        Public Property FailedDeliveredAttemptes As Integer
        Public Property ExpectedDeliveryDate As Nullable(Of Date)
        Public Property Comments As String
    End Class
End Namespace

