Imports System.Runtime.Serialization

''' <summary>
''' Class to define service response properties
''' </summary>
<DataContract> _
Public Class ResponseBase
    Private m_isSuccess As Boolean
    Private m_message As String
    Private m_RC As String

    ''' <summary>
    ''' True, If bussiness logic return successful response
    ''' </summary>
    <DataMember> _
    Public Property IsSuccess() As Boolean
        Get
            Return m_isSuccess
        End Get
        Set(value As Boolean)
            m_isSuccess = value
        End Set
    End Property

    ''' <summary>
    ''' Response message will be either for success or unsuccess
    ''' </summary>
    <DataMember> _
    Public Property Message() As String
        Get
            Return m_message
        End Get
        Set(value As String)
            m_message = value
        End Set
    End Property

    <DataMember> _
    Public Property RC() As String
        Get
            Return m_RC
        End Get
        Set(value As String)
            m_RC = value
        End Set
    End Property

End Class
