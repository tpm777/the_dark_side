Imports WWTS.Aspects.Enums
Imports System.Configuration
Imports WWTS.Aspects.Factory
Imports System.Xml
Imports System.Xml.Serialization

Namespace Utils

    Public Class XmlTextSerializer
        ''' <summary>
        ''' Method to get the Application Configuration Settings value using key name of settings
        ''' </summary>
        ''' <param name="key">string Key</param>
        ''' <returns>returns application settings value</returns>
        Public Shared Function GetAppSettings(key As String) As String
            Dim value As String = String.Empty
            Try
                value = ConfigurationManager.AppSettings(key)
            Catch ex As Exception
                value = String.Empty
                LogTraceFactory.WriteLogWithCategory(ex.Message, LogTraceCategoryNames.General)

                Throw ex
            End Try
            Return value
        End Function

        ''' <summary>
        ''' Method to get the Application Configuration Settings value using key name of settings
        ''' </summary>
        ''' <param name="key">config key name enum</param>
        ''' <returns>returns application settings value</returns>
        Public Shared Function GetAppSettings(key As ConfigKeys) As String
            Dim value As String = String.Empty
            Try
                value = ConfigurationManager.AppSettings(key.ToString())
            Catch ex As Exception
                value = String.Empty
                LogTraceFactory.WriteLogWithCategory(ex.Message, LogTraceCategoryNames.General)

                Throw ex
            End Try
            Return value
        End Function

        Public Shared Function XmlSerialize(obj As Object) As String
            If obj IsNot Nothing Then
                ' Assuming obj is an instance of an object
                Dim ser As New XmlSerializer(obj.[GetType]())
                Dim sb As New System.Text.StringBuilder()
                Dim writer As New System.IO.StringWriter(sb)
                ser.Serialize(writer, obj)
                Return sb.ToString()
            End If
            Return String.Empty
        End Function

        Public Shared Function XmlDeserialize(objType As Type, xmlDoc As String) As Object
            If xmlDoc IsNot Nothing AndAlso objType IsNot Nothing Then
                Dim doc As New XmlDocument()
                doc.LoadXml(xmlDoc)
                'Assuming doc is an XML document containing a serialized object and objType is a System.Type set to the type of the object.
                Dim reader As New XmlNodeReader(doc.DocumentElement)
                Dim ser As New XmlSerializer(objType)
                Return ser.Deserialize(reader)
            End If
            Return Nothing
        End Function

    End Class
End Namespace

