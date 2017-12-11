Imports System.ServiceModel.Channels

Namespace Utils
    Public Class JsonContentMapper
        Inherits WebContentTypeMapper
        ''' <summary>
        ''' Method to override message format for content type in Json services
        ''' </summary>
        ''' <param name="contentType">content type</param>
        ''' <returns>returns json format</returns>
        Public Overrides Function GetMessageFormatForContentType(contentType As String) As WebContentFormat
            Return WebContentFormat.Json
        End Function
    End Class
End Namespace

