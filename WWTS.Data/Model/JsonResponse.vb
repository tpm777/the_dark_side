Imports System.Runtime.Serialization

<DataContract(Name:="{0}Response")> _
Public Class JsonResponse(Of T)
    Inherits ResponseBase
#Region "[Private Member]"


    Private result As List(Of T)
    Private m_singleResult As T

#End Region

#Region "Properties"

    ''' <summary>
    ''' Response result
    ''' </summary>
    <DataMember> _
    Public Property Results() As List(Of T)
        Get
            Return result
        End Get
        Set(value As List(Of T))
            result = value
        End Set
    End Property

    <DataMember> _
    Public Property SingleResult() As T
        Get
            Return m_singleResult
        End Get
        Set(value As T)
            m_singleResult = value
        End Set
    End Property

    <DataMember> _
    Public Property TotalRecordCount() As Integer
        Get
            Return m_TotalRecordCount
        End Get
        Set(value As Integer)
            m_TotalRecordCount = value
        End Set
    End Property
    Private m_TotalRecordCount As Integer


#End Region
End Class
