Imports System.Runtime.Serialization

Namespace Model
    <DataContract> _
    Public Class ServiceFaultData
        ''' <summary>
        ''' Property to get set fault message of service method execution
        ''' </summary>
        <DataMember> _
        Public Property Message() As String
            Get
                Return m_Message
            End Get
            Set(value As String)
                m_Message = value
            End Set
        End Property
        Private m_Message As String

        ''' <summary>
        ''' Property to get set fault code of service method execution
        ''' </summary>
        <DataMember> _
        Public Property FaultCode() As String
            Get
                Return m_FaultCode
            End Get
            Set(value As String)
                m_FaultCode = value
            End Set
        End Property
        Private m_FaultCode As String
    End Class

End Namespace
